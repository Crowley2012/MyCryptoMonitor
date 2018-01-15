using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System;
using System.IO;

namespace MyCryptoMonitor
{
    public partial class MainForm : Form
    {
        #region Private Variables
        private List<CoinConfig> _coinConfigs;
        private List<CoinGuiLine> _coinGuiLines;
        private bool _loadGuiLines;
        private int _refreshCount;
        private string _selectedPortfolio;
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
            _loadGuiLines = true;

            //Attempt to load portfolio on startup
            if (File.Exists("Portfolio1"))
            {
                _coinConfigs = JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText("Portfolio1") ?? string.Empty);
                _selectedPortfolio = "Portfolio1";
            }
            else if (File.Exists("Portfolio2"))
            {
                _coinConfigs = JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText("Portfolio2") ?? string.Empty);
                _selectedPortfolio = "Portfolio2";
            }
            else if (File.Exists("Portfolio3"))
            {
                _coinConfigs = JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText("Portfolio3") ?? string.Empty);
                _selectedPortfolio = "Portfolio3";
            }

            //Update status
            UpdateStatusDelegate updateStatus = new UpdateStatusDelegate(UpdateStatus);
            BeginInvoke(updateStatus, "Loading");

            //Update refreshes
            UpdateRefreshesDelegate updateRefreshes = new UpdateRefreshesDelegate(UpdateRefreshes);
            BeginInvoke(updateRefreshes);

            //Start thread
            Thread thread = new Thread(() => DownloadDataThread());
            thread.Start();
        }
        #endregion

        #region Threads
        private void DownloadDataThread()
        {
            //Update status
            UpdateStatusDelegate updateStatus = new UpdateStatusDelegate(UpdateStatus);
            BeginInvoke(updateStatus, "Refreshing");

            using (var webClient = new WebClient())
            {
                //Download coin data from CoinCap
                string response = webClient.DownloadString("http://coincap.io/front");

                //Update coins
                UpdateCoinDelegates updateCoins = new UpdateCoinDelegates(UpdateCoins);
                BeginInvoke(updateCoins, response);
            }

            //Update status
            updateStatus = new UpdateStatusDelegate(UpdateStatus);
            BeginInvoke(updateStatus, "Sleeping");

            //Update refreshes
            UpdateRefreshesDelegate updateRefreshes = new UpdateRefreshesDelegate(UpdateRefreshes);
            BeginInvoke(updateRefreshes);

            //Sleep and refresh
            Thread.Sleep(5000);
            DownloadDataThread();
        }
        #endregion

        #region Delegates
        //Update refresh label
        private delegate void UpdateRefreshesDelegate();
        public void UpdateRefreshes()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateRefreshesDelegate(UpdateRefreshes));
                return;
            }

            refreshLabel.Text = $"Refreshes: {_refreshCount}";
        }

        //Update status label
        private delegate void UpdateStatusDelegate(string status);
        public void UpdateStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateStatusDelegate(UpdateStatus), status);
                return;
            }

            statusLabel.Text = $"Status: {status}";
        }

        //Update coin
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
            decimal totalProfits = 0;

            //Index of coin gui line
            int index = 0;

            //Loop through all coins from config
            foreach (CoinConfig coin in _coinConfigs)
            {
                //Parse coins
                var coins = JsonConvert.DeserializeObject<List<CoinData>>(response);
                CoinData downloadedCoin = coins.Single(c => c.shortName == coin.coin);

                //Check if gui lines need to be loaded
                if (_loadGuiLines)
                    AddCoin(coin, downloadedCoin, index);

                //Incremenet coin line index
                index++;

                //Get the gui line for coin
                CoinGuiLine line = (from c in _coinGuiLines where c.CoinName.Equals(downloadedCoin.shortName) select c).First();

                //Calculate
                decimal bought = Convert.ToDecimal(line.BoughtTextBox.Text);
                decimal paid = Convert.ToDecimal(line.PaidTextBox.Text);
                decimal total = bought * downloadedCoin.price;
                decimal profit = total - paid;
                decimal changeDollar = downloadedCoin.price - coin.StartupPrice;
                decimal changePercent = ((downloadedCoin.price - coin.StartupPrice) / coin.StartupPrice) * 100;

                //Add to totals
                totalPaid += paid;
                totalProfits += paid + profit;

                //Update gui
                line.CoinLabel.Show();
                line.CoinLabel.Text = downloadedCoin.shortName;
                line.PriceLabel.Text = $"${downloadedCoin.price}";
                line.TotalLabel.Text = $"${total:0.00}";
                line.ProfitLabel.Text = $"${profit:0.00}";
                line.ChangeDollarLabel.Text = $"${changeDollar:0.000000}";
                line.ChangePercentLabel.Text = $"{changePercent:0.00}%";
                line.Change24HrPercentLabel.Text = $"{downloadedCoin.cap24hrChange}%";
            }

            //Update gui
            totalProfit.Text = $"${totalProfits:0.00}";
            totalProfitChange.Text = $"(${totalProfits - totalPaid:0.00})";
            statusLabel.Text = "Status: Sleeping";
            refreshLabel.Text = $"Refreshes: {_refreshCount}";

            //Sleep and rerun
            _loadGuiLines = false;
            _refreshCount++;
        }

        //Remove lines
        private delegate void RemoveDelegate();
        public void Remove()
        {
            if (InvokeRequired)
            {
                Invoke(new RemoveDelegate(Remove));
                return;
            }

            //Set status
            statusLabel.Text = "Status: Loading";
            _refreshCount = 0;

            //Reset totals
            totalProfit.Text = "$0.00";
            totalProfitChange.Text = "($0.00)";

            //Remove the line elements from gui
            foreach (var coin in _coinGuiLines)
            {
                Height -= 25;
                Controls.Remove(coin.CoinLabel);
                Controls.Remove(coin.PriceLabel);
                Controls.Remove(coin.BoughtTextBox);
                Controls.Remove(coin.TotalLabel);
                Controls.Remove(coin.PaidTextBox);
                Controls.Remove(coin.ProfitLabel);
                Controls.Remove(coin.ChangeDollarLabel);
                Controls.Remove(coin.ChangePercentLabel);
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
            coin.StartupPrice = downloadedCoin.price;

            //Create the gui line
            CoinGuiLine newLine = new CoinGuiLine(downloadedCoin.shortName, index);

            //Set the bought and paid amounts
            newLine.BoughtTextBox.Text = coin.bought.ToString();
            newLine.PaidTextBox.Text = coin.paid.ToString();

            //Add the line elements to gui
            MethodInvoker invoke = delegate
            {
                Height += 25;
                Controls.Add(newLine.CoinLabel);
                Controls.Add(newLine.PriceLabel);
                Controls.Add(newLine.BoughtTextBox);
                Controls.Add(newLine.TotalLabel);
                Controls.Add(newLine.PaidTextBox);
                Controls.Add(newLine.ProfitLabel);
                Controls.Add(newLine.ChangeDollarLabel);
                Controls.Add(newLine.ChangePercentLabel);
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

            List<CoinConfig> newCoinConfigs = new List<CoinConfig>();

            //Create new list of gui lines
            foreach (var coinLine in _coinGuiLines)
            {
                newCoinConfigs.Add(new CoinConfig
                {
                    coin = coinLine.CoinLabel.Text,
                    bought = Convert.ToDecimal(coinLine.BoughtTextBox.Text),
                    paid = Convert.ToDecimal(coinLine.PaidTextBox.Text)
                });
            }

            //Serialize lines to file
            File.WriteAllText(portfolio, JsonConvert.SerializeObject(newCoinConfigs));
        }

        private void LoadPortfolio(string portfolio)
        {
            if (!File.Exists(portfolio))
                return;

            //Load portfolio from file
            _coinConfigs = JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText(portfolio) ?? string.Empty);
            _selectedPortfolio = portfolio;

            RemoveDelegate remove = new RemoveDelegate(Remove);
            BeginInvoke(remove);
        }
        #endregion

        #region Events
        private void Reset_Click(object sender, EventArgs e)
        {
            LoadPortfolio(_selectedPortfolio);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddCoin_Click(object sender, EventArgs e)
        {
            //Get coin to add
            InputForm form = new InputForm();
            form.SetSubmitLabel("Add");

            if (form.ShowDialog() != DialogResult.OK)
                return;

            //Check if coin exists
            if (!_coinConfigs.Any(a => a.coin.Equals(form.InputText.ToUpper())))
            {
                //Add config
                _coinConfigs.Add(new CoinConfig { coin = form.InputText.ToUpper(), bought = 0, paid = 0, StartupPrice = 0 });

                RemoveDelegate remove = new RemoveDelegate(Remove);
                BeginInvoke(remove);
            }
            else
            {
                MessageBox.Show("Coin already exist!");
            }
        }

        private void RemoveCoin_Click(object sender, EventArgs e)
        {
            //Get coin to remove
            InputForm form = new InputForm();
            form.SetSubmitLabel("Remove");

            if (form.ShowDialog() != DialogResult.OK)
                return;

            //Check if coin exists
            if (_coinConfigs.Any(a => a.coin.Equals(form.InputText.ToUpper())))
            {
                //Remove config
                _coinConfigs.RemoveAll(a => a.coin.Equals(form.InputText.ToUpper()));

                RemoveDelegate remove = new RemoveDelegate(Remove);
                BeginInvoke(remove);
            }
            else
            {
                MessageBox.Show("Coin doesn't exist!");
            }
        }
        
        private void SavePortfolio_Click(object sender, EventArgs e)
        {
            SavePortfolio(((ToolStripMenuItem)sender).Tag.ToString());
        }

        private void LoadPortfolio_Click(object sender, EventArgs e)
        {
            LoadPortfolio(((ToolStripMenuItem)sender).Tag.ToString());
        }
        #endregion
    }
}
