using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System;
using System.IO;
using System.Reflection;
using System.Drawing;
using MyCryptoMonitor.Api;
using MyCryptoMonitor.Statics;
using MyCryptoMonitor.Gui;
using MyCryptoMonitor.Configs;
using MyCryptoMonitor.Objects;

namespace MyCryptoMonitor.Forms
{
    public partial class MainForm : Form
    {
        #region Constant Variables
        private const string API_COIN_MARKET_CAP = "https://api.coinmarketcap.com/v1/ticker/?limit=9999&convert={0}";
        private const string API_CRYPTO_COMPARE = "https://min-api.cryptocompare.com/data/pricemultifull?tsyms={0}&fsyms=";
        #endregion

        #region Private Variables
        private List<CoinConfig> _coinConfigs = new List<CoinConfig>();
        private List<CoinLine> _coinGuiLines = new List<CoinLine>();
        private List<Coin> _mappedCoins = new List<Coin>();
        private List<Coin> _coinNameList = new List<Coin>();
        private DateTime _resetTime = DateTime.Now;
        private DateTime _refreshTime = DateTime.Now;
        private bool _loadGuiLines = true;
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Threads
        private void ThreadStarter(Thread thread)
        {
            thread.IsBackground = true;
            thread.Start();
        }

        private void DownloadData()
        {
            while (true)
            {
                UpdateStatus("Refreshing");

                try
                {
                    string currency = (string)cbCurrency.Invoke(new Func<string>(() => cbCurrency.Text));
                    string cryptoCompareAddress = string.Format(API_CRYPTO_COMPARE, currency);

                    foreach (var coinConfig in _coinConfigs)
                        cryptoCompareAddress += $"{coinConfig.Name},";

                    using (var webClient = new WebClient())
                        UpdateCoins(webClient.DownloadString(cryptoCompareAddress), webClient.DownloadString(string.Format(API_COIN_MARKET_CAP, currency)));
                }
                catch (WebException)
                {
                    UpdateStatus("No internet connection");
                }

                UpdateStatus("Sleeping");
                Thread.Sleep(5000);
            }
        }

        private void TimerThread()
        {
            while (true)
            {
                TimeSpan spanReset = DateTime.Now.Subtract(_resetTime);
                TimeSpan spanRefresh = DateTime.Now.Subtract(_refreshTime);
                string resetTime = $"Time since reset: {spanReset.Hours}:{spanReset.Minutes:00}:{spanReset.Seconds:00}";
                string refreshTime = $"Time since refresh: {spanRefresh.Minutes}:{spanRefresh.Seconds:00}";
                
                UpdateTimers(resetTime, refreshTime);

                Thread.Sleep(500);
            }
        }

        private void CheckUpdate()
        {
            bool checkingUpdate = true;
            int attempts = 0;

            while (checkingUpdate && attempts < 3)
            {
                try
                {
                    using (var webClient = new WebClient())
                    {
                        webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2;)");

                        ApiGithub release = JsonConvert.DeserializeObject<ApiGithub>(webClient.DownloadString("https://api.github.com/repos/Crowley2012/MyCryptoMonitor/releases/latest"));
                        Version currentVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                        Version latestVersion = new Version(release.tag_name);

                        if (currentVersion.CompareTo(latestVersion) < 0)
                        {
                            if (MessageBox.Show($"Download new version?\n\nCurrent Version: {currentVersion}\nLatest Version {latestVersion}", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                System.Diagnostics.Process.Start("https://github.com/Crowley2012/MyCryptoMonitor/releases/latest");
                        }

                        checkingUpdate = false;
                    }
                }
                catch (WebException)
                {
                    attempts++;
                    UpdateStatus("Failed checking for update");
                }
            }
        }
        #endregion

        #region Methods
        private void UpdateStatus(string status)
        {
            Invoke((MethodInvoker)delegate
            {
                lblStatus.Text = $"Status: {status}";
            });
        }

        private void UpdateTimers(string resetTime, string refreshTime)
        {
            Invoke((MethodInvoker)delegate
            {
                lblResetTime.Text = resetTime;
                lblRefreshTime.Text = refreshTime;
            });
        }

        private void UpdateCoins(string cryptoCompareResponse, string coinMarketCapResponse)
        {
            int index = 0;
            decimal totalPaid = 0;
            decimal overallTotal = 0;
            decimal totalNegativeProfits = 0;
            decimal totalPostivieProfits = 0;

            _coinNameList = MappingService.CoinMarketCap(coinMarketCapResponse).OrderBy(c => c.ShortName).ToList();
            _mappedCoins = MappingService.MapCombination(cryptoCompareResponse, coinMarketCapResponse);

            MainService.CheckAlerts(_mappedCoins);

            foreach (CoinConfig coin in _coinConfigs)
            {
                Coin downloadedCoin = _mappedCoins.Find(c => c.ShortName == coin.Name);

                if (_loadGuiLines)
                    AddCoin(coin, downloadedCoin, index);

                index++;

                if (coin.SetStartupPrice)
                {
                    coin.StartupPrice = downloadedCoin.Price;
                    coin.SetStartupPrice = false;
                }

                CoinLine line = (from c in _coinGuiLines where c.CoinName.Equals(downloadedCoin.ShortName) && c.CoinIndex == coin.Index select c).First();

                if (string.IsNullOrEmpty(line.BoughtTextBox.Text))
                    line.BoughtTextBox.Text = "0";

                if (string.IsNullOrEmpty(line.PaidTextBox.Text))
                    line.PaidTextBox.Text = "0";

                decimal bought = Convert.ToDecimal(line.BoughtTextBox.Text);
                decimal paid = Convert.ToDecimal(line.PaidTextBox.Text);
                decimal boughtPrice = bought == 0 ? 0 : paid / bought;
                decimal total = bought * downloadedCoin.Price;
                decimal profit = total - paid;
                decimal changeDollar = downloadedCoin.Price - coin.StartupPrice;
                decimal changePercent = coin.StartupPrice == 0 ? 0 : ((downloadedCoin.Price - coin.StartupPrice) / coin.StartupPrice) * 100;

                if (profit >= 0)
                    totalPostivieProfits += profit;
                else
                    totalNegativeProfits += profit;

                totalPaid += paid;
                overallTotal += paid + profit;

                Invoke((MethodInvoker)delegate
                {
                    line.CoinLabel.Show();
                    line.CoinIndexLabel.Text = _coinConfigs.Count(c => c.Name.Equals(coin.Name)) > 1 ? $"[{coin.Index + 1}]" : string.Empty;
                    line.CoinLabel.Text = downloadedCoin.ShortName;
                    line.PriceLabel.Text = downloadedCoin.Price.ToString().Substring(downloadedCoin.Price.ToString().IndexOf(".") + 1).Length > 7 ? $"${downloadedCoin.Price:0.0000000}" : $"${downloadedCoin.Price}";
                    line.BoughtPriceLabel.Text = $"${boughtPrice:0.000000}";
                    line.TotalLabel.Text = $"${total:0.00}";
                    line.ProfitLabel.Text = $"${profit:0.00}";
                    line.RatioLabel.Text = paid != 0 ? $"{profit / paid:0.00}" : "0.00";
                    line.ChangeDollarLabel.Text = $"${changeDollar:0.000000}";
                    line.ChangePercentLabel.Text = $"{changePercent:0.00}%";
                    line.Change1HrPercentLabel.Text = $"{downloadedCoin.Change1HourPercent:0.00}%";
                    line.Change24HrPercentLabel.Text = $"{downloadedCoin.Change24HourPercent:0.00}%";
                    line.Change7DayPercentLabel.Text = $"{downloadedCoin.Change7DayPercent:0.00}%";
                });
            }

            Invoke((MethodInvoker)delegate
            {
                lblOverallTotal.Text = $"${overallTotal:0.00}";
                lblTotalProfit.ForeColor = overallTotal - totalPaid >= 0 ? Color.Green : Color.Red;
                lblTotalProfit.Text = $"${overallTotal - totalPaid:0.00}";
                lblTotalNegativeProfit.Text = $"${totalNegativeProfits:0.00}";
                lblTotalPositiveProfit.Text = $"${totalPostivieProfits:0.00}";
                lblStatus.Text = "Status: Sleeping";
                _refreshTime = DateTime.Now;
                _loadGuiLines = false;
                alertsToolStripMenuItem.Enabled = true;
            });
        }

        private void RemoveGuiLines()
        {
            Invoke((MethodInvoker)delegate
            {
                lblStatus.Text = "Status: Loading";
                lblOverallTotal.Text = "$0.00";
                lblTotalProfit.Text = "$0.00";
                
                foreach (var coin in _coinGuiLines)
                {
                    Height -= 25;
                    coin.Dispose();
                }

                _coinGuiLines = new List<CoinLine>();
                _loadGuiLines = true;
            });
        }

        private void AddCoin(CoinConfig coin, Coin downloadedCoin, int index)
        {
            //Store the intial coin price at startup
            if(coin.SetStartupPrice)
                coin.StartupPrice = downloadedCoin.Price;

            Invoke((MethodInvoker)delegate
            {
                //Create the gui line
                CoinLine newLine = new CoinLine(downloadedCoin.ShortName, coin.Index, index);

                //Set the bought and paid amounts
                newLine.BoughtTextBox.Text = coin.Bought.ToString();
                newLine.PaidTextBox.Text = coin.Paid.ToString();

                Height += 25;
                Controls.Add(newLine.CoinIndexLabel);
                Controls.Add(newLine.CoinLabel);
                Controls.Add(newLine.PriceLabel);
                Controls.Add(newLine.BoughtTextBox);
                Controls.Add(newLine.BoughtPriceLabel);
                Controls.Add(newLine.TotalLabel);
                Controls.Add(newLine.PaidTextBox);
                Controls.Add(newLine.ProfitLabel);
                Controls.Add(newLine.RatioLabel);
                Controls.Add(newLine.ChangeDollarLabel);
                Controls.Add(newLine.ChangePercentLabel);
                Controls.Add(newLine.Change1HrPercentLabel);
                Controls.Add(newLine.Change24HrPercentLabel);
                Controls.Add(newLine.Change7DayPercentLabel);

                //Add line to list
                _coinGuiLines.Add(newLine);
            });
        }

        private void SavePortfolio(string portfolio)
        {
            if (_coinGuiLines.Count <= 0)
                return;

            List<CoinConfig> coinConfigs = new List<CoinConfig>();

            //Create new list of gui lines
            foreach (var coinLine in _coinGuiLines)
            {
                coinConfigs.Add(new CoinConfig
                {
                    Name = coinLine.CoinLabel.Text,
                    Index = coinConfigs.Count(c => c.Name.Equals(coinLine.CoinName)),
                    Bought = Convert.ToDecimal(coinLine.BoughtTextBox.Text),
                    Paid = Convert.ToDecimal(coinLine.PaidTextBox.Text)
                });
            }

            //Save portfolio
            PortfolioService.Save(portfolio, coinConfigs);
        }

        private void LoadPortfolio(string portfolio)
        {
            _coinConfigs = PortfolioService.Load(portfolio);

            RemoveGuiLines();
        }

        private void ResetCoinIndex()
        {
            //Reset the coin index
            foreach (CoinConfig config in _coinConfigs)
            {
                int index = 0;

                foreach(CoinConfig coin in _coinConfigs.Where(c => c.Name == config.Name).ToList())
                {
                    coin.Index = index;
                    index++;
                }
            }
        }

        private void SetupPortfolioMenu()
        {
            foreach (var portfolio in PortfolioService.GetPortfolios())
            {
                savePortfolioMenu.DropDownItems.Add(new ToolStripMenuItem(portfolio.Name, null, SavePortfolio_Click) { Name = portfolio.Name, Checked = portfolio.Startup });
                loadPortfolioMenu.DropDownItems.Add(new ToolStripMenuItem(portfolio.Name, null, LoadPortfolio_Click) { Name = portfolio.Name, Checked = portfolio.Startup });
            }
        }
        #endregion

        #region Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            MainService.Startup();

            _coinConfigs = PortfolioService.LoadStartup();
            cbCurrency.Text = UserConfigService.Currency;
            SetupPortfolioMenu();

            ThreadStarter(new Thread(new ThreadStart(DownloadData)));
            ThreadStarter(new Thread(new ThreadStart(TimerThread)));
            ThreadStarter(new Thread(new ThreadStart(CheckUpdate)));
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"This will delete all saved files (portfolios, alerts, etc) and remove encryption. Do you want to continue?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                foreach (var portfolio in PortfolioService.GetPortfolios())
                {
                    savePortfolioMenu.DropDownItems.RemoveByKey(portfolio.Name);
                    loadPortfolioMenu.DropDownItems.RemoveByKey(portfolio.Name);
                }

                MainService.Reset();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddCoin_Click(object sender, EventArgs e)
        {
            //Check if coin list has been downloaded
            while(_coinNameList.Count <= 0)
            {
                if (MessageBox.Show("Please wait while coin list is being downloaded.", "Loading", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                    return;
            }

            //Get coin to add
            ManageCoins form = new ManageCoins(true, _coinNameList);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            //Check if coin exists
            if (!_coinNameList.Any(c => c.ShortName.Equals(form.InputText)))
            {
                MessageBox.Show("Coin does not exist.", "Error");
                return;
            }

            //Update coin config bought and paid values
            foreach (var coinGuiLine in _coinGuiLines)
            {
                var coinConfig = _coinConfigs.Single(c => c.Name == coinGuiLine.CoinName && c.Index == coinGuiLine.CoinIndex);
                coinConfig.Bought = Convert.ToDecimal(coinGuiLine.BoughtTextBox.Text);
                coinConfig.Paid = Convert.ToDecimal(coinGuiLine.PaidTextBox.Text);
                coinConfig.SetStartupPrice = false;
            }

            //Add coin config
            _coinConfigs.Add(new CoinConfig { Name = form.InputText, Index = _coinConfigs.Count(c => c.Name.Equals(form.InputText)), Bought = 0, Paid = 0, StartupPrice = 0, SetStartupPrice = true });
            RemoveGuiLines();

            UncheckPortfolios(string.Empty);
        }

        private void RemoveCoin_Click(object sender, EventArgs e)
        {
            //Get coin to remove
            ManageCoins form = new ManageCoins(false, _coinConfigs);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            //Check if coin exists
            if (!_coinConfigs.Any(a => a.Name.Equals(form.InputText) && a.Index == form.CoinIndex))
            {
                MessageBox.Show("Coin does not exist.", "Error");
                return;
            }

            //Update coin config bought and paid values
            foreach (var coinGuiLine in _coinGuiLines)
            {
                var coinConfig = _coinConfigs.Single(c => c.Name == coinGuiLine.CoinName && c.Index == coinGuiLine.CoinIndex);
                coinConfig.Bought = Convert.ToDecimal(coinGuiLine.BoughtTextBox.Text);
                coinConfig.Paid = Convert.ToDecimal(coinGuiLine.PaidTextBox.Text);
                coinConfig.SetStartupPrice = false;
            }

            //Remove coin config
            _coinConfigs.RemoveAll(a => a.Name.Equals(form.InputText) && a.Index == form.CoinIndex);

            //Reset coin indexes
            ResetCoinIndex();
            RemoveGuiLines();

            UncheckPortfolios(string.Empty);
        }

        private void RemoveAllCoins_Click(object sender, EventArgs e)
        {
            _coinConfigs = new List<CoinConfig>();
            RemoveGuiLines();
            UncheckPortfolios(string.Empty);
        }

        private void UncheckPortfolios(string portfolio)
        {
            PortfolioService.CurrentPortfolio = portfolio;

            foreach (ToolStripMenuItem item in savePortfolioMenu.DropDownItems.OfType<ToolStripMenuItem>())
            {
                item.Checked = false;

                if (item.Text.Equals(portfolio))
                    item.Checked = true;
            }

            foreach (ToolStripMenuItem item in loadPortfolioMenu.DropDownItems.OfType<ToolStripMenuItem>())
            {
                item.Checked = false;

                if (item.Text.Equals(portfolio))
                    item.Checked = true;
            }
        }

        private void SavePortfolio_Click(object sender, EventArgs e)
        {
            var portfolio = ((ToolStripMenuItem)sender).Text;

            UncheckPortfolios(portfolio);
            SavePortfolio(portfolio);
        }

        private void LoadPortfolio_Click(object sender, EventArgs e)
        {
            var portfolio = ((ToolStripMenuItem)sender).Text;

            UncheckPortfolios(portfolio);
            LoadPortfolio(portfolio);
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopupDonate form = new PopupDonate();
            form.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopupAbout form = new PopupAbout();
            form.Show();
        }

        private void alertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageAlerts form = new ManageAlerts(_mappedCoins);
            form.Show();
        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageEncryption form = new ManageEncryption();
            form.Show();
        }

        private void cbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            alertsToolStripMenuItem.Enabled = false;

            foreach (var config in _coinConfigs)
                config.SetStartupPrice = true;

            UserConfigService.Currency = cbCurrency.Text;
        }

        private void menuManagePortfolios_Click(object sender, EventArgs e)
        {
            UncheckPortfolios(PortfolioService.CurrentPortfolio);

            foreach (var portfolio in PortfolioService.GetPortfolios())
            {
                savePortfolioMenu.DropDownItems.RemoveByKey(portfolio.Name);
                loadPortfolioMenu.DropDownItems.RemoveByKey(portfolio.Name);
            }

            new ManagePortfolios().ShowDialog();

            SetupPortfolioMenu();
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
        #endregion
    }
}
