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

        public MainForm()
        {
            InitializeComponent();

            //Setup gui
            _loadGuiLines = true;

            //TODO: Move to serialized file
            /*
            _coinConfigs = new List<CoinConfig> {
                new CoinConfig { coin = "BTC", bought =  0, paid = 0},
                new CoinConfig { coin = "ETH", bought =  0, paid = 0},
                new CoinConfig { coin = "LTC", bought =  0, paid = 0},
                new CoinConfig { coin = "XRP", bought = (decimal) 630.592988, paid = 675},
                new CoinConfig { coin = "XLM", bought = (decimal) 1238.73999, paid = 350},
                new CoinConfig { coin = "ADA", bought =  0, paid = 0},
                new CoinConfig { coin = "TRX", bought =  0, paid = 0}
            };
            */

            string readText = string.Empty;

            if(File.Exists("portfolio1"))
                readText = File.ReadAllText("portfolio1");

            _coinConfigs = JsonConvert.DeserializeObject<List<CoinConfig>>(readText);

            //List of gui coin fields
            _coinGuiLines = new List<CoinGuiLine>();

            //Download data on another thread
            Thread thread = new Thread(() => LoadCoinCapData());
            thread.Start();
        }

        private void LoadCoinCapData()
        {
            try
            {
                //Display status
                MethodInvoker invoke = delegate
                {
                    statusLabel.Text = "Status: Refreshing";
                };
                Invoke(invoke);

                //Download coin data from CoinCap
                var webClient = new WebClient();
                var response = webClient.DownloadString("http://coincap.io/front");
                var coins = JsonConvert.DeserializeObject<List<CoinData>>(response);

                //Overall values
                decimal totalPaid = 0;
                decimal totalProfits = 0;

                //Index of coin gui line
                int index = 0;

                //Loop through all coins from config
                foreach(CoinConfig coin in _coinConfigs)
                {
                    //Parse the selected coin
                    CoinData downloadedCoin = coins.Single(c => c.shortName == coin.coin);

                    //Configure the gui
                    if (_loadGuiLines)
                    {
                        //Store the intial coin price at startup
                        coin.StartupPrice = downloadedCoin.price;

                        //Create the gui line
                        CoinGuiLine newLine = new CoinGuiLine(downloadedCoin.shortName, index);

                        //Set the bought and paid amounts
                        newLine.BoughtTextBox.Text = coin.bought.ToString();
                        newLine.PaidTextBox.Text = coin.paid.ToString();

                        //Add the line elements to gui
                        invoke = delegate
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

                        //Incremenet coin line index
                        index++;
                    }

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
                    invoke = delegate
                    {
                        line.CoinLabel.Text = downloadedCoin.shortName;
                        line.PriceLabel.Text = $"${downloadedCoin.price:0.00}";
                        line.TotalLabel.Text = $"${total:0.00}";
                        line.ProfitLabel.Text = $"${profit:0.00}";
                        line.ChangeDollarLabel.Text = $"${changeDollar:0.000000}";
                        line.ChangePercentLabel.Text = $"{changePercent:0.00}%";
                        line.Change24HrPercentLabel.Text = $"{downloadedCoin.cap24hrChange}%";
                    };
                    Invoke(invoke);
                }

                //Update gui
                invoke = delegate
                {
                    totalProfit.Text = $"${totalProfits:0.00}";
                    totalProfitChange.Text = $"(${totalProfits - totalPaid:0.00})";
                    statusLabel.Text = "Status: Sleeping";
                    refreshLabel.Text = $"Refreshes: {_refreshCount}";
                };
                Invoke(invoke);

                //Sleep and rerun
                _loadGuiLines = false;
                _refreshCount++;
                Thread.Sleep(5000);
                LoadCoinCapData();
            }
            catch(Exception e)
            {
                //Display status
                MethodInvoker invoke = delegate
                {
                    statusLabel.Text = $"Status: Failed - {e.Message}";
                };
                Invoke(invoke);

                //Sleep and rerun
                Thread.Sleep(5000);
                LoadCoinCapData();
            }
        }

        #region Events
        private void Reset_Click(object sender, EventArgs e)
        {
            //Set status
            statusLabel.Text = "Status: Reset";
            _refreshCount = 0;

            //Reset totals
            totalProfit.Text = "$0.00";
            totalProfitChange.Text = "($0.00)";

            MethodInvoker invoke = delegate
            {
                //Download coin data from CoinCap
                var webClient = new WebClient();
                var response = webClient.DownloadString("http://coincap.io/front");
                var coins = JsonConvert.DeserializeObject<List<CoinData>>(response);

                foreach (CoinConfig coin in _coinConfigs)
                {
                    //Get the selected coin line
                    CoinGuiLine line = (from c in _coinGuiLines where c.CoinName.Equals(coin.coin) select c).First();

                    //Reset labels
                    line.ProfitLabel.Text = $"$0.00";
                    line.ChangeDollarLabel.Text = $"$0.000000";
                    line.ChangePercentLabel.Text = $"0.00%";

                    //Parse the selected coin
                    CoinData downloadedCoin = coins.Single(c => c.shortName == coin.coin);

                    //Reset elements
                    coin.StartupPrice = downloadedCoin.price;
                    line.BoughtTextBox.Text = coin.bought.ToString();
                    line.PaidTextBox.Text = coin.paid.ToString();
                }
            };
            Invoke(invoke);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddCoin(string coinstring)
        {
            //Download coin data from CoinCap
            var webClient = new WebClient();
            var response = webClient.DownloadString("http://coincap.io/front");
            var coins = JsonConvert.DeserializeObject<List<CoinData>>(response);

            var coin = new CoinConfig { coin = coinstring, bought = 0, paid = 0, StartupPrice = 0 };

            if (!coins.Any(c => c.shortName == coin.coin))
                return;

            CoinData downloadedCoin = coins.Single(c => c.shortName == coin.coin);

            //Store the intial coin price at startup
            coin.StartupPrice = downloadedCoin.price;

            //Create the gui line
            CoinGuiLine newLine = new CoinGuiLine(downloadedCoin.shortName, _coinConfigs.Count);

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
            _coinConfigs.Add(coin);
        }

        private void AddCoin_Click(object sender, EventArgs e)
        {
            InputForm form = new InputForm();
            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (!_coinConfigs.Any(a => a.coin.Equals(form.InputText.ToUpper())))
                    AddCoin(form.InputText.ToUpper());
                else
                    MessageBox.Show("Coin already exists!");
            }
        }

        private void RemoveCoin_Click(object sender, EventArgs e)
        {
            InputForm form = new InputForm();
            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (!_coinConfigs.Any(a => a.coin.Equals(form.InputText.ToUpper())))
                {
                    //_coinGuiLines.Remove(_coinGuiLines.Select(a => a.CoinName.Equals(form.InputText.ToUpper())));
                    //_coinConfigs.Remove(coin);
                }
            }
        }

        //Load portfolios
        private void LoadPortfolio1_Click(object sender, EventArgs e)
        {
            var test = JsonConvert.SerializeObject(new List<CoinConfig> {
                new CoinConfig { coin = "BTC", bought =  0, paid = 0},
                new CoinConfig { coin = "ETH", bought =  0, paid = 0},
                new CoinConfig { coin = "LTC", bought =  0, paid = 0},
                new CoinConfig { coin = "XRP", bought = (decimal) 630.592988, paid = 675},
                new CoinConfig { coin = "XLM", bought = (decimal) 1238.73999, paid = 350},
                new CoinConfig { coin = "ADA", bought =  0, paid = 0},
                new CoinConfig { coin = "TRX", bought =  (decimal) 3731.265, paid = 250}
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
