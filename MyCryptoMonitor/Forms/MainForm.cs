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
        private List<CoinLine> _coinLines = new List<CoinLine>();
        private List<Coin> _coins = new List<Coin>();
        private List<string> _coinNames = new List<string>();
        private DateTime _resetTime = DateTime.Now;
        private DateTime _refreshTime = DateTime.Now;
        private bool _loadLines = true;
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

        private void GetCoinData()
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

        private void Timers()
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
            int lineIndex = 0;
            decimal totalPaid = 0;
            decimal totalOverall = 0;
            decimal totalNegativeProfits = 0;
            decimal totalPostivieProfits = 0;

            _coinNames = MappingService.CoinMarketCap(coinMarketCapResponse).OrderBy(c => c.ShortName).Select(c => c.ShortName).ToList();
            _coins = MappingService.MapCombination(cryptoCompareResponse, coinMarketCapResponse);

            MainService.CheckAlerts(_coins);

            foreach (CoinConfig coinConfig in _coinConfigs)
            {
                Coin coin = _coins.Find(c => c.ShortName == coinConfig.Name);

                if (_loadLines)
                    AddLine(coinConfig, coin, lineIndex++);

                CoinLine line = (from c in _coinLines where c.CoinName.Equals(coin.ShortName) && c.CoinIndex == coinConfig.Index select c).First();

                decimal bought = line.BoughtTextBox.Text.ConvertToDecimal();
                decimal paid = line.PaidTextBox.Text.ConvertToDecimal();
                decimal total = bought * coin.Price;
                decimal profit = total - paid;

                totalPaid += paid;
                totalOverall += paid + profit;

                if (profit >= 0)
                    totalPostivieProfits += profit;
                else
                    totalNegativeProfits += profit;

                var coinIndexLabel = _coinConfigs.Count(c => c.Name.Equals(coinConfig.Name)) > 1 ? $"[{coinConfig.Index + 1}]" : string.Empty;
                var coinLabel = coin.ShortName;
                var priceLabel = $"${coin.Price.ConvertToString(7)}";
                var boughtLabel = $"${bought.SafeDivision(paid).ConvertToString(7)}";
                var totalLabel = $"${total:0.00}";
                var profitLabel = $"${profit:0.00}";
                var ratioLabel = paid != 0 ? $"{profit / paid:0.00}" : "0.00";
                var changeDollarLabel = $"${(coin.Price - coinConfig.StartupPrice):0.000000}";
                var changePercentLabel = $"{coinConfig.StartupPrice.SafeDivision(coin.Price - coinConfig.StartupPrice) * 100:0.00}%";
                var change1HrLabel = $"{coin.Change1HourPercent:0.00}%";
                var change24HrLabel = $"{coin.Change24HourPercent:0.00}%";
                var change7DayLabel = $"{coin.Change7DayPercent:0.00}%";

                Invoke((MethodInvoker)delegate
                {
                    line.CoinIndexLabel.Text = coinIndexLabel;
                    line.CoinLabel.Text = coinLabel;
                    line.PriceLabel.Text = priceLabel;
                    line.BoughtPriceLabel.Text = boughtLabel;
                    line.TotalLabel.Text = totalLabel;
                    line.ProfitLabel.Text = profitLabel;
                    line.RatioLabel.Text = ratioLabel;
                    line.ChangeDollarLabel.Text = changeDollarLabel;
                    line.ChangePercentLabel.Text = changePercentLabel;
                    line.Change1HrPercentLabel.Text = change1HrLabel;
                    line.Change24HrPercentLabel.Text = change24HrLabel;
                    line.Change7DayPercentLabel.Text = change7DayLabel;
                });
            }

            _refreshTime = DateTime.Now;
            _loadLines = false;
            UpdateStatus("Sleeping");

            var totalProfitColor = totalOverall - totalPaid >= 0 ? Color.Green : Color.Red;
            var totalProfitLabel = $"${totalOverall - totalPaid:0.00}";
            var totalNegativeProfitLabel = $"${totalNegativeProfits:0.00}";
            var totalPositiveProfitLabel = $"${totalPostivieProfits:0.00}";
            var totalOverallLabel = $"${totalOverall:0.00}";

            Invoke((MethodInvoker)delegate
            {
                lblTotalProfit.ForeColor = totalProfitColor;
                lblTotalProfit.Text = totalProfitLabel;
                lblTotalNegativeProfit.Text = totalNegativeProfitLabel;
                lblTotalPositiveProfit.Text = totalPositiveProfitLabel;
                lblOverallTotal.Text = totalOverallLabel;
                alertsToolStripMenuItem.Enabled = true;
                coinsToolStripMenuItem.Enabled = true;
            });
        }

        private void AddLine(CoinConfig coinConfig, Coin coin, int lineIndex)
        {
            CoinLine newLine = new CoinLine(coin.ShortName, coinConfig.Index, lineIndex);
            coinConfig.StartupPrice = coin.Price;

            Invoke((MethodInvoker)delegate
            {
                newLine.BoughtTextBox.Text = coinConfig.Bought.ToString();
                newLine.PaidTextBox.Text = coinConfig.Paid.ToString();

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

                _coinLines.Add(newLine);
            });
        }

        private void RemoveLines()
        {
            Invoke((MethodInvoker)delegate
            {
                UpdateStatus("Loading");
                
                foreach (var line in _coinLines)
                {
                    Height -= 25;
                    line.Dispose();
                }

                _coinLines = new List<CoinLine>();
                _loadLines = true;
            });
        }

        private void SavePortfolio(string portfolio)
        {
            if (_coinLines.Count <= 0)
                return;

            var config = _coinLines.Select(coinLine => new CoinConfig
            {
                Name = coinLine.CoinLabel.Text,
                Bought = Convert.ToDecimal(coinLine.BoughtTextBox.Text),
                Paid = Convert.ToDecimal(coinLine.PaidTextBox.Text),
                Index = coinLine.CoinIndex
            }).ToList();

            PortfolioService.Save(portfolio, config);
        }

        private void LoadPortfolio(string portfolio)
        {
            _coinConfigs = PortfolioService.Load(portfolio);

            RemoveLines();
        }

        private void SetupPortfolioMenu()
        {
            foreach (var portfolio in PortfolioService.GetPortfolios())
            {
                savePortfolioMenu.DropDownItems.Add(new ToolStripMenuItem(portfolio.Name, null, SavePortfolio_Click) { Name = portfolio.Name, Checked = PortfolioService.CurrentPortfolio.Equals(portfolio.Name) });
                loadPortfolioMenu.DropDownItems.Add(new ToolStripMenuItem(portfolio.Name, null, LoadPortfolio_Click) { Name = portfolio.Name, Checked = PortfolioService.CurrentPortfolio.Equals(portfolio.Name) });
            }
        }

        private void SelectPortfolio(string portfolio)
        {
            PortfolioService.CurrentPortfolio = portfolio;

            foreach (ToolStripMenuItem item in savePortfolioMenu.DropDownItems.OfType<ToolStripMenuItem>())
                item.Checked = item.Text.Equals(portfolio);

            foreach (ToolStripMenuItem item in loadPortfolioMenu.DropDownItems.OfType<ToolStripMenuItem>())
                item.Checked = item.Text.Equals(portfolio);
        }
        #endregion

        #region Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                MainService.Startup();

                _coinConfigs = PortfolioService.LoadStartup();
                cbCurrency.Text = UserConfigService.Currency;
                SetupPortfolioMenu();

                ThreadStarter(new Thread(new ThreadStart(CheckUpdate)));
                ThreadStarter(new Thread(new ThreadStart(Timers)));
                ThreadStarter(new Thread(new ThreadStart(GetCoinData)));
            }
            catch (Exception)
            {
                if (MessageBox.Show($"There was an error starting up. Would you like to reset? \nThis will remove encryption and delete all portfolios and alerts.", "Error on startup", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    MainService.Reset();
                    Close();
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void Currency_SelectedIndexChanged(object sender, EventArgs e)
        {
            coinsToolStripMenuItem.Enabled = false;
            UserConfigService.Currency = cbCurrency.Text;
            RemoveLines();
        }

        #region File Menu
        private void Open_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory());
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            if (MainService.ConfirmReset())
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
            Close();
        }
        #endregion

        #region Coins Menu
        private void AddCoin_Click(object sender, EventArgs e)
        {
            using (ManageCoins form = new ManageCoins(_coinNames))
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                
                if (!_coinNames.Any(c => c.Equals(form.InputText)))
                {
                    MessageBox.Show("Coin does not exist.", "Error");
                    return;
                }
                
                _coinConfigs.Add(new CoinConfig
                {
                    Name = form.InputText,
                    Bought = 0,
                    Paid = 0,
                    StartupPrice = 0,
                    Index = _coinConfigs.Count(c => c.Name.Equals(form.InputText))
                });

                RemoveLines();
                SelectPortfolio(string.Empty);
            }
        }

        private void RemoveCoin_Click(object sender, EventArgs e)
        {
            using (ManageCoins form = new ManageCoins(_coinConfigs))
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                
                _coinConfigs.RemoveAll(a => a.Name.Equals(form.InputText) && a.Index == form.CoinIndex);

                //Reset coin indexes
                foreach (CoinConfig coinConfig in _coinConfigs)
                {
                    int index = 0;

                    foreach (CoinConfig sameCoinConfig in _coinConfigs.Where(c => c.Name == coinConfig.Name).ToList())
                    {
                        sameCoinConfig.Index = index;
                        index++;
                    }
                }
            }

            RemoveLines();
            SelectPortfolio(string.Empty);
        }

        private void RemoveAllCoins_Click(object sender, EventArgs e)
        {
            _coinConfigs = new List<CoinConfig>();
            RemoveLines();
            SelectPortfolio(string.Empty);
        }
        #endregion

        #region Alerts Menu
        private void Alerts_Click(object sender, EventArgs e)
        {
            using (ManageAlerts form = new ManageAlerts(_coins))
                form.ShowDialog();
        }
        #endregion

        #region Portfolio Menu
        private void ManagePortfolios_Click(object sender, EventArgs e)
        {
            foreach (var portfolio in PortfolioService.GetPortfolios())
            {
                savePortfolioMenu.DropDownItems.RemoveByKey(portfolio.Name);
                loadPortfolioMenu.DropDownItems.RemoveByKey(portfolio.Name);
            }

            using(ManagePortfolios form = new ManagePortfolios())
                form.ShowDialog();

            SetupPortfolioMenu();
        }

        private void SavePortfolio_Click(object sender, EventArgs e)
        {
            var portfolio = ((ToolStripMenuItem)sender).Text;

            SelectPortfolio(portfolio);
            SavePortfolio(portfolio);
        }

        private void LoadPortfolio_Click(object sender, EventArgs e)
        {
            var portfolio = ((ToolStripMenuItem)sender).Text;
            alertsToolStripMenuItem.Enabled = false;
            coinsToolStripMenuItem.Enabled = false;

            SelectPortfolio(portfolio);
            LoadPortfolio(portfolio);
        }
        #endregion

        #region Encrypt Menu
        private void Encrypt_Click(object sender, EventArgs e)
        {
            using (ManageEncryption form = new ManageEncryption())
                form.ShowDialog();
        }
        #endregion

        #region Donate Menu
        private void Donate_Click(object sender, EventArgs e)
        {
            using (PopupDonate form = new PopupDonate())
                form.ShowDialog();
        }
        #endregion

        #region About Menu
        private void About_Click(object sender, EventArgs e)
        {
            using (PopupAbout form = new PopupAbout())
                form.ShowDialog();
        }
        #endregion
        #endregion
    }
}
