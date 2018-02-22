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
using MyCryptoMonitor.DataSources;
using MyCryptoMonitor.Functions;

namespace MyCryptoMonitor.Forms
{
    public partial class MainForm : Form
    {
        #region Constant Variables
        private const string API_COIN_MARKET_CAP = "https://api.coinmarketcap.com/v1/ticker/?limit=9999";
        private const string API_COIN_CAP = "https://coincap.io/front";
        #endregion

        #region Private Variables
        private List<CoinConfig> _coinConfigs;
        private List<CoinGuiLine> _coinGuiLines;
        private List<CoinData> _coins;
        private DateTime _resetTime;
        private DateTime _refreshTime;
        private bool _loadGuiLines;
        private string _api;
        #endregion

        #region Load
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _coinGuiLines = new List<CoinGuiLine>();
            _coinConfigs = new List<CoinConfig>();
            _coins = new List<CoinData>();
            _resetTime = DateTime.Now;
            _refreshTime = DateTime.Now;
            _loadGuiLines = true;

            //User config
            if (File.Exists("UserConfig"))
            {
                Management.UserConfig = JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText("UserConfig"));
            }
            else
            {
                Management.UserConfig = new UserConfig();
                File.WriteAllText("UserConfig", JsonConvert.SerializeObject(Management.UserConfig));
            }

            //Unlock if encryption is enabled
            Management.Unlock();

            //Attempt to load portfolio on startup
            _coinConfigs = Management.LoadFirstPortfolio();

            //Update status
            UpdateStatusDelegate updateStatus = new UpdateStatusDelegate(UpdateStatus);
            BeginInvoke(updateStatus, "Loading");

            //Start main thread
            Thread mainThread = new Thread(() => DownloadData());
            mainThread.IsBackground = true;
            mainThread.Start();

            //Start time thread
            Thread timeThread = new Thread(() => Timer());
            timeThread.IsBackground = true;
            timeThread.Start();
            
            //Start check update thread
            Thread checkUpdateThread = new Thread(() => CheckUpdate());
            checkUpdateThread.IsBackground = true;
            checkUpdateThread.Start();
        }
        #endregion

        #region Threads
        private void DownloadData()
        {
            //Update status
            UpdateStatusDelegate updateStatus = new UpdateStatusDelegate(UpdateStatus);
            BeginInvoke(updateStatus, "Refreshing");

            try
            {
                using (var webClient = new WebClient())
                {
                    //Set api
                    _api = mnuCoinMarketCap.Checked ? API_COIN_MARKET_CAP : API_COIN_CAP;

                    //Download coin data from API
                    string response = webClient.DownloadString(_api);

                    //Update coins
                    UpdateCoinDelegates updateCoins = new UpdateCoinDelegates(UpdateCoins);
                    BeginInvoke(updateCoins, response);

                    //Update status
                    updateStatus = new UpdateStatusDelegate(UpdateStatus);
                    BeginInvoke(updateStatus, "Sleeping");
                }
            }
            catch (WebException)
            {
                //Update status
                updateStatus = new UpdateStatusDelegate(UpdateStatus);
                BeginInvoke(updateStatus, "No internet connection");
            }

            //Sleep and refresh
            Thread.Sleep(5000);
            DownloadData();
        }

        private void Timer()
        {
            //Update times
            UpdateTimesDelegate updateTimes = new UpdateTimesDelegate(UpdateTimes);
            BeginInvoke(updateTimes);

            //Sleep and refresh
            Thread.Sleep(500);
            Timer();
        }

        private void CheckUpdate()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    //Github api requires a user agent
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2;)");

                    //Download lastest release data from GitHub
                    string response = webClient.DownloadString("https://api.github.com/repos/Crowley2012/MyCryptoMonitor/releases/latest");
                    ApiGithub release = JsonConvert.DeserializeObject<ApiGithub>(response);

                    //Parse versions
                    Version currentVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                    Version latestVersion = new Version(release.tag_name);

                    //Check if latest is newer than current
                    if (currentVersion.CompareTo(latestVersion) < 0)
                    {
                        //Ask if user wants to open github page
                        if (MessageBox.Show($"Download new version?\n\nCurrent Version: {currentVersion}\nLatest Version {latestVersion}", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                            System.Diagnostics.Process.Start("https://github.com/Crowley2012/MyCryptoMonitor/releases/latest");
                    }
                }
            }
            catch (WebException)
            {
                //Update status
                UpdateStatusDelegate updateStatus = new UpdateStatusDelegate(UpdateStatus);
                BeginInvoke(updateStatus, "No internet connection");
                CheckUpdate();
            }
        }
        #endregion

        #region Delegates
        private delegate void UpdateStatusDelegate(string status);
        public void UpdateStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateStatusDelegate(UpdateStatus), status);
                return;
            }

            //Update status
            lblStatus.Text = $"Status: {status}";
        }

        private delegate void UpdateTimesDelegate();
        public void UpdateTimes()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateTimesDelegate(UpdateTimes));
                return;
            }

            //Update reset time
            TimeSpan spanRefresh = DateTime.Now.Subtract(_refreshTime);
            lblRefreshTime.Text = $"Time since refresh: {spanRefresh.Minutes}:{spanRefresh.Seconds:00}";

            //Update reset time
            TimeSpan spanReset = DateTime.Now.Subtract(_resetTime);
            lblResetTime.Text = $"Time since reset: {spanReset.Hours}:{spanReset.Minutes:00}:{spanReset.Seconds:00}";
        }

        private delegate void UpdateCoinDelegates(string response);
        public void UpdateCoins(string response)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateCoinDelegates(UpdateCoins), response);
                return;
            }

            //Overall values
            decimal totalPaid = 0;
            decimal overallTotal = 0;
            decimal totalNegativeProfits = 0;
            decimal totalPostivieProfits = 0;

            //Index of coin gui line
            int index = 0;

            //Deserialize settings
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            //Deserialize response and map to generic coin
            List<CoinData> coins = new List<CoinData>();
            switch (_api)
            {
                case API_COIN_MARKET_CAP:
                    var coinsCoinMarketCap = JsonConvert.DeserializeObject<List<ApiCoinMarketCap>>(response, settings);
                    coins = coinsCoinMarketCap.Select(c => Mappings.MapCoinMarketCap(c)).ToList();
                    break;
                case API_COIN_CAP:
                    var coinsCoinCap = JsonConvert.DeserializeObject<List<ApiCoinCap>>(response, settings);
                    coins = coinsCoinCap.Select(c => Mappings.MapCoinCap(c)).ToList();
                    break;
            }

            //Create list of coin names
            _coins = coins.OrderBy(c => c.ShortName).ToList();

            //Loop through all coins from config
            foreach (CoinConfig coin in _coinConfigs)
            {
                CoinData downloadedCoin;

                //Parse coins, if coin doesnt exist set to 0
                downloadedCoin = coins.Any(c => c.ShortName == coin.coin) 
                    ? coins.Single(c => c.ShortName == coin.coin) 
                    : new CoinData { ShortName = coin.coin, CoinIndex = coin.coinIndex, Change1HourPercent = 0, Change24HourPercent = 0, Price = 0 };

                //Check if gui lines need to be loaded
                if (_loadGuiLines)
                    AddCoin(coin, downloadedCoin, index);

                //Incremenet coin line index
                index++;
                
                //Check if coinguiline exists for coinConfig
                if (!_coinGuiLines.Any(cg => cg.CoinName.Equals(downloadedCoin.ShortName)))
                {
                    RemoveDelegate remove = new RemoveDelegate(Remove);
                    BeginInvoke(remove);
                    return;
                }

                //Get the gui line for coin
                CoinGuiLine line = (from c in _coinGuiLines where c.CoinName.Equals(downloadedCoin.ShortName) && c.CoinIndex == coin.coinIndex select c).First();

                if (string.IsNullOrEmpty(line.BoughtTextBox.Text))
                    line.BoughtTextBox.Text = "0";

                if (string.IsNullOrEmpty(line.PaidTextBox.Text))
                    line.PaidTextBox.Text = "0";

                //Calculate
                decimal bought = Convert.ToDecimal(line.BoughtTextBox.Text);
                decimal paid = Convert.ToDecimal(line.PaidTextBox.Text);
                decimal boughtPrice = bought == 0 ? 0 : paid / bought;
                decimal total = bought * downloadedCoin.Price;
                decimal profit = total - paid;
                decimal changeDollar = downloadedCoin.Price - coin.StartupPrice;
                decimal changePercent = coin.StartupPrice == 0 ? 0 : ((downloadedCoin.Price - coin.StartupPrice) / coin.StartupPrice) * 100;

                //Update total profits
                if (profit >= 0)
                    totalPostivieProfits += profit;
                else
                    totalNegativeProfits += profit;

                //Add to totals
                totalPaid += paid;
                overallTotal += paid + profit;

                //Update gui
                line.CoinLabel.Show();
                line.CoinIndexLabel.Text = _coinConfigs.Count(c => c.coin.Equals(coin.coin)) > 1 ? $"[{coin.coinIndex + 1}]" : string.Empty;
                line.CoinLabel.Text = downloadedCoin.ShortName;
                line.PriceLabel.Text = $"${downloadedCoin.Price}";
                line.BoughtPriceLabel.Text = $"${boughtPrice:0.000000}";
                line.TotalLabel.Text = $"${total:0.00}";
                line.ProfitLabel.Text = $"${profit:0.00}";
                line.ChangeDollarLabel.Text = $"${changeDollar:0.000000}";
                line.ChangePercentLabel.Text = $"{changePercent:0.00}%";
                line.Change1HrPercentLabel.Text = $"{downloadedCoin.Change1HourPercent:0.00}%";
                line.Change24HrPercentLabel.Text = $"{downloadedCoin.Change24HourPercent:0.00}%";
            }

            //Update gui
            lblOverallTotal.Text = $"${overallTotal:0.00}";
            lblTotalProfit.ForeColor = overallTotal - totalPaid >= 0 ? Color.Green : Color.Red;
            lblTotalProfit.Text = $"${overallTotal - totalPaid:0.00}";
            lblTotalNegativeProfit.Text = $"${totalNegativeProfits:0.00}";
            lblTotalPositiveProfit.Text = $"${totalPostivieProfits:0.00}";
            lblStatus.Text = "Status: Sleeping";
            _refreshTime = DateTime.Now;

            //Sleep and rerun
            _loadGuiLines = false;
        }

        private delegate void RemoveDelegate();
        public void Remove()
        {
            if (InvokeRequired)
            {
                Invoke(new RemoveDelegate(Remove));
                return;
            }

            //Set status
            lblStatus.Text = "Status: Loading";

            //Reset totals
            lblOverallTotal.Text = "$0.00";
            lblTotalProfit.Text = "($0.00)";

            //Remove the line elements from gui
            foreach (var coin in _coinGuiLines)
            {
                Height -= 25;
                Controls.Remove(coin.CoinLabel);
                Controls.Remove(coin.CoinIndexLabel);
                Controls.Remove(coin.PriceLabel);
                Controls.Remove(coin.BoughtTextBox);
                Controls.Remove(coin.BoughtPriceLabel);
                Controls.Remove(coin.TotalLabel);
                Controls.Remove(coin.PaidTextBox);
                Controls.Remove(coin.ProfitLabel);
                Controls.Remove(coin.ChangeDollarLabel);
                Controls.Remove(coin.ChangePercentLabel);
                Controls.Remove(coin.Change1HrPercentLabel);
                Controls.Remove(coin.Change24HrPercentLabel);
            }

            _coinGuiLines = new List<CoinGuiLine>();
            _loadGuiLines = true;
        }
        #endregion

        #region Methods
        private void AddCoin(CoinConfig coin, CoinData downloadedCoin, int index)
        {
            //Store the intial coin price at startup
            if(coin.SetStartupPrice)
                coin.StartupPrice = downloadedCoin.Price;

            //Create the gui line
            CoinGuiLine newLine = new CoinGuiLine(downloadedCoin.ShortName, coin.coinIndex, index);

            //Set the bought and paid amounts
            newLine.BoughtTextBox.Text = coin.bought.ToString();
            newLine.PaidTextBox.Text = coin.paid.ToString();

            //Add the line elements to gui
            MethodInvoker invoke = delegate
            {
                Height += 25;
                Controls.Add(newLine.CoinIndexLabel);
                Controls.Add(newLine.CoinLabel);
                Controls.Add(newLine.PriceLabel);
                Controls.Add(newLine.BoughtTextBox);
                Controls.Add(newLine.BoughtPriceLabel);
                Controls.Add(newLine.TotalLabel);
                Controls.Add(newLine.PaidTextBox);
                Controls.Add(newLine.ProfitLabel);
                Controls.Add(newLine.ChangeDollarLabel);
                Controls.Add(newLine.ChangePercentLabel);
                Controls.Add(newLine.Change1HrPercentLabel);
                Controls.Add(newLine.Change24HrPercentLabel);
            };
            Invoke(invoke);

            //Add line to list
            _coinGuiLines.Add(newLine);
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
                    coin = coinLine.CoinLabel.Text,
                    coinIndex = coinConfigs.Count(c => c.coin.Equals(coinLine.CoinName)),
                    bought = Convert.ToDecimal(coinLine.BoughtTextBox.Text),
                    paid = Convert.ToDecimal(coinLine.PaidTextBox.Text)
                });
            }

            //Save portfolio
            Management.SavePortfolio(portfolio, coinConfigs);
        }

        private void LoadPortfolio(string portfolio)
        {
            if (!File.Exists(portfolio))
                return;

            //Load portfolio
            _coinConfigs = Management.LoadPortfolio(portfolio);

            RemoveDelegate remove = new RemoveDelegate(Remove);
            BeginInvoke(remove);
        }

        private void ResetCoinIndex()
        {
            //Reset the coin index
            foreach (CoinConfig config in _coinConfigs)
            {
                int index = 0;

                foreach(CoinConfig coin in _coinConfigs.Where(c => c.coin == config.coin).ToList())
                {
                    coin.coinIndex = index;
                    index++;
                }
            }
        }
        #endregion

        #region Events
        private void Reset_Click(object sender, EventArgs e)
        {
            _resetTime = DateTime.Now;
            LoadPortfolio(Management.SelectedPortfolio);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddCoin_Click(object sender, EventArgs e)
        {
            //Check if coin list has been downloaded
            while(_coins.Count <= 0)
            {
                if (MessageBox.Show("Please wait while coin list is being downloaded.", "Loading", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                    return;
            }

            //Get coin to add
            InputForm form = new InputForm("Add", _coins);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            //Check if coin exists
            if (!_coins.Any(c => c.ShortName.Equals(form.InputText.ToUpper())))
            {
                MessageBox.Show("Coin does not exist.");
                return;
            }

            //Update coin config bought and paid values
            foreach (var coinGuiLine in _coinGuiLines)
            {
                var coinConfig = _coinConfigs.Single(c => c.coin == coinGuiLine.CoinName && c.coinIndex == coinGuiLine.CoinIndex);
                coinConfig.bought = Convert.ToDecimal(coinGuiLine.BoughtTextBox.Text);
                coinConfig.paid = Convert.ToDecimal(coinGuiLine.PaidTextBox.Text);
                coinConfig.SetStartupPrice = false;
            }

            //Add coin config
            _coinConfigs.Add(new CoinConfig { coin = form.InputText.ToUpper(), coinIndex = _coinConfigs.Count(c => c.coin.Equals(form.InputText.ToUpper())), bought = 0, paid = 0, StartupPrice = 0, SetStartupPrice = true });

            RemoveDelegate remove = new RemoveDelegate(Remove);
            BeginInvoke(remove);
        }

        private void RemoveCoin_Click(object sender, EventArgs e)
        {
            //Get coin to remove
            InputForm form = new InputForm("Remove", _coinConfigs);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            //Check if coin exists
            if (!_coinConfigs.Any(a => a.coin.Equals(form.InputText.ToUpper()) && a.coinIndex == form.CoinIndex))
            {
                MessageBox.Show("Coin does not exist.");
                return;
            }

            //Update coin config bought and paid values
            foreach (var coinGuiLine in _coinGuiLines)
            {
                var coinConfig = _coinConfigs.Single(c => c.coin == coinGuiLine.CoinName && c.coinIndex == coinGuiLine.CoinIndex);
                coinConfig.bought = Convert.ToDecimal(coinGuiLine.BoughtTextBox.Text);
                coinConfig.paid = Convert.ToDecimal(coinGuiLine.PaidTextBox.Text);
                coinConfig.SetStartupPrice = false;
            }

            //Remove coin config
            _coinConfigs.RemoveAll(a => a.coin.Equals(form.InputText.ToUpper()) && a.coinIndex == form.CoinIndex);

            //Reset coin indexes
            ResetCoinIndex();

            RemoveDelegate remove = new RemoveDelegate(Remove);
            BeginInvoke(remove);
        }

        private void RemoveAllCoins_Click(object sender, EventArgs e)
        {
            //Remove coin configs
            _coinConfigs = new List<CoinConfig>();
            RemoveDelegate remove = new RemoveDelegate(Remove);
            BeginInvoke(remove);
        }

        private void SavePortfolio_Click(object sender, EventArgs e)
        {
            SavePortfolio(((ToolStripMenuItem)sender).Tag.ToString());
        }

        private void LoadPortfolio_Click(object sender, EventArgs e)
        {
            LoadPortfolio(((ToolStripMenuItem)sender).Tag.ToString());
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Donate form = new Donate();
            form.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Created by Sean Crowley\n\nVersion: {Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
        }

        private void mnuCoinMarketCap_Click(object sender, EventArgs e)
        {
            //Check menu item
            mnuCoinMarketCap.Checked = true;
            mnuCoinCap.Checked = false;
        }

        private void mnuCoinCap_Click(object sender, EventArgs e)
        {
            //Check menu item
            mnuCoinMarketCap.Checked = false;
            mnuCoinCap.Checked = true;
        }

        private void alertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Alerts form = new Alerts(_coins);
            form.Show();
        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encrypt form = new Encrypt();
            form.Show();
        }
        #endregion
    }
}
