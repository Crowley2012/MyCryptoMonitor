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
        private List<CoinConfig> _coinConfigs;
        private List<CoinGuiLine> _coinGuiLines;
        private bool _loadGuiLines;
        private int _refreshCount;

        #region Load
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _coinGuiLines = new List<CoinGuiLine>();
            _loadGuiLines = true;

            //Load portfolio
            _coinConfigs = JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText("portfolio1") ?? string.Empty);

            //Update status
            UpdateStatusDelegate updateStatus = new UpdateStatusDelegate(UpdateStatus);
            BeginInvoke(updateStatus, "Loading");

            //Update refreshes
            UpdateRefreshesDelegate updateRefreshes = new UpdateRefreshesDelegate(UpdateStatus);
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
            UpdateRefreshesDelegate updateRefreshes = new UpdateRefreshesDelegate(UpdateStatus);
            BeginInvoke(updateRefreshes);

            //Sleep and refresh
            Thread.Sleep(5000);
            DownloadDataThread();
        }
        #endregion

        #region Delegates
        //Update refresh label
        private delegate void UpdateRefreshesDelegate();
        public void UpdateStatus()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateRefreshesDelegate(UpdateStatus));
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
                var coins = JsonConvert.DeserializeObject<List<CoinData>>(response);
                CoinData downloadedCoin = coins.Single(c => c.shortName == coin.coin);

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
                line.PriceLabel.Text = $"${downloadedCoin.price:0.00}";
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

        //Reset
        private delegate void ReloadDelegate();
        public void Reload()
        {
            if (InvokeRequired)
            {
                Invoke(new ReloadDelegate(Reload));
                return;
            }
            
            //Set status
            statusLabel.Text = "Status: Reloading";
            _refreshCount = 0;

            //Reset totals
            totalProfit.Text = "$0.00";
            totalProfitChange.Text = "($0.00)";

            foreach (CoinConfig coin in _coinConfigs)
            {
                //Get the selected coin line
                CoinGuiLine line = (from c in _coinGuiLines where c.CoinName.Equals(coin.coin) select c).First();

                //Reset labels
                line.PriceLabel.Text = "$0.00";
                line.TotalLabel.Text = "$0.00";
                line.ProfitLabel.Text = "$0.00";
                line.ChangeDollarLabel.Text = "$0.000000";
                line.ChangePercentLabel.Text = "0.00%";
                line.Change24HrPercentLabel.Text = "0.00%";

                //Reset inputs
                line.BoughtTextBox.Text = coin.bought.ToString();
                line.PaidTextBox.Text = coin.paid.ToString();
            }
        }

        //Remove lines
        private delegate void RemoveDelegate();
        public void Remove()
        {
            if (InvokeRequired)
            {
                Invoke(new ReloadDelegate(Reload));
                return;
            }

            foreach (var coin in _coinGuiLines)
            {
                //Add the line elements to gui

                //Height -= 25;
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
        #endregion

        #region Events
        private void Reset_Click(object sender, EventArgs e)
        {
            //Update refreshes
            ReloadDelegate reload = new ReloadDelegate(Reload);
            BeginInvoke(reload);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReloadForm()
        {
            foreach (var coin in _coinGuiLines)
            {
                //Add the line elements to gui
                MethodInvoker invoke = delegate
                {
                    //Height -= 25;
                    Controls.Remove(coin.CoinLabel);
                    Controls.Remove(coin.PriceLabel);
                    Controls.Remove(coin.BoughtTextBox);
                    Controls.Remove(coin.TotalLabel);
                    Controls.Remove(coin.PaidTextBox);
                    Controls.Remove(coin.ProfitLabel);
                    Controls.Remove(coin.ChangeDollarLabel);
                    Controls.Remove(coin.ChangePercentLabel);
                    Controls.Remove(coin.Change24HrPercentLabel);
                };
                Invoke(invoke);
            }

            _loadGuiLines = true;
        }

        private void AddCoin_Click(object sender, EventArgs e)
        {
            InputForm form = new InputForm();
            form.SetSubmitLabel("Add");
            var result = form.ShowDialog();

            if (result != DialogResult.OK)
                return;

            if (!_coinConfigs.Any(a => a.coin.Equals(form.InputText.ToUpper())))
            {
                _coinConfigs.Add(new CoinConfig { coin = form.InputText.ToUpper(), bought = 0, paid = 0, StartupPrice = 0 });
                ReloadForm();
            }
            else
            {
                MessageBox.Show("Coin already exists!");
            }
        }

        private void RemoveCoin_Click(object sender, EventArgs e)
        {
            //Update refreshes
            RemoveDelegate remove = new RemoveDelegate(Remove);
            BeginInvoke(remove);

            /*
            InputForm form = new InputForm();
            form.SetSubmitLabel("Remove");
            var result = form.ShowDialog();

            if (result != DialogResult.OK)
                return;

            if (_coinConfigs.Any(a => a.coin.Equals(form.InputText.ToUpper())))
            {
                _coinConfigs.RemoveAll(x => x.coin.Equals(form.InputText.ToUpper()));
                ReloadForm();
            }
            */
        }
        
        //Load portfolios
        private void LoadPortfolio1_Click(object sender, EventArgs e)
        {
            var test = JsonConvert.SerializeObject(new List<CoinConfig> {
                new CoinConfig { coin = "BTC", bought =  0, paid = 0},
                new CoinConfig { coin = "ETH", bought =  0, paid = 0},
                new CoinConfig { coin = "LTC", bought =  0, paid = 0},
                new CoinConfig { coin = "XRP", bought = (decimal) 630.592988, paid = 675},
                new CoinConfig { coin = "XLM", bought = (decimal) 1575.40299, paid = 675},
                new CoinConfig { coin = "ADA", bought =  0, paid = 0},
                new CoinConfig { coin = "TRX", bought =  (decimal) 5657.337, paid = 575}
            });

            File.WriteAllText("portfolio1", test);
        }

        private void LoadPortfolio2_Click(object sender, EventArgs e)
        {

        }

        private void LoadPortfolio3_Click(object sender, EventArgs e)
        {

        }

        //Save portfolios
        private void SavePortfolio1_Click(object sender, EventArgs e)
        {

        }

        private void SavePortfolio2_Click(object sender, EventArgs e)
        {

        }

        private void SavePortfolio3_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
