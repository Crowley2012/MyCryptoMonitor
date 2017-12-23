using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System;

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
            _coinConfigs = new List<CoinConfig> {
                new CoinConfig { coin = "BTC", bought =  0, paid = 0},
                new CoinConfig { coin = "ETH", bought =  0, paid = 0},
                new CoinConfig { coin = "LTC", bought =  0, paid = 0},
                new CoinConfig { coin = "XRP", bought = (decimal) 608.79, paid = 650},
                new CoinConfig { coin = "XLM", bought = (decimal) 1238.73999, paid = 350}
            };

            //List of gui coin fields
            _coinGuiLines = new List<CoinGuiLine>();

            //Download data on another thread
            Thread thread = new Thread(() => LoadCoinCapData());
            thread.Start();
        }

        private void LoadCoinCapData()
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
                    newLine.boughtTextBox.Text = coin.bought.ToString();
                    newLine.paidTextBox.Text = coin.paid.ToString();

                    //Add the line elements to gui
                    invoke = delegate
                    {
                        Controls.Add(newLine.coinLabel);
                        Controls.Add(newLine.priceLabel);
                        Controls.Add(newLine.boughtTextBox);
                        Controls.Add(newLine.totalLabel);
                        Controls.Add(newLine.paidTextBox);
                        Controls.Add(newLine.profitLabel);
                        Controls.Add(newLine.changeDollarLabel);
                        Controls.Add(newLine.changePercentLabel);
                    };
                    Invoke(invoke);

                    //Add line to list
                    _coinGuiLines.Add(newLine);
                }

                //Get the gui line for coin
                CoinGuiLine line = (from c in _coinGuiLines where c.Coin.Equals(downloadedCoin.shortName) select c).First();

                //Calculate
                decimal bought = Convert.ToDecimal(line.boughtTextBox.Text);
                decimal paid = Convert.ToDecimal(line.paidTextBox.Text);
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
                    line.coinLabel.Text = downloadedCoin.shortName;
                    line.priceLabel.Text = $"${downloadedCoin.price:0.00}";
                    line.totalLabel.Text = $"${total:0.00}";
                    line.profitLabel.Text = $"${profit:0.00}";
                    line.changeDollarLabel.Text = $"${changeDollar:0.000000}";
                    line.changePercentLabel.Text = $"{changePercent:0.00}%";
                };
                Invoke(invoke);

                //Incremenet coin line index
                index++;
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
                    CoinGuiLine line = (from c in _coinGuiLines where c.Coin.Equals(coin.coin) select c).First();

                    //Reset labels
                    line.profitLabel.Text = $"$0.00";
                    line.changeDollarLabel.Text = $"$0.000000";
                    line.changePercentLabel.Text = $"0.00%";

                    //Parse the selected coin
                    CoinData downloadedCoin = coins.Single(c => c.shortName == coin.coin);

                    //Reset elements
                    coin.StartupPrice = downloadedCoin.price;
                    line.boughtTextBox.Text = coin.bought.ToString();
                    line.paidTextBox.Text = coin.paid.ToString();
                }
            };
            Invoke(invoke);
        }
    }
}
