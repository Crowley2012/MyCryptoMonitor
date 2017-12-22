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
        private Thread _mainThread;
        private bool _reaload;
        private bool _loadLines;
        private int _refreshCount;
        private List<LoadCoin> coins;
        private List<CoinLine> coinLines;

        public MainForm()
        {
            InitializeComponent();

            _reaload = false;
            _loadLines = true;

            coins = new List<LoadCoin> {
                new LoadCoin { coin = "BTC", bought =  0, paid = 0},
                new LoadCoin { coin = "ETH", bought =  0, paid = 0},
                new LoadCoin { coin = "LTC", bought =  0, paid = 0},
                new LoadCoin { coin = "XRP", bought = (decimal) 608.79, paid = 650},
                new LoadCoin { coin = "XLM", bought = (decimal) 1238.73999, paid = 350}
            };

            coinLines = new List<CoinLine>();

            _mainThread = new Thread(() => LoadCoinCapData());
            _mainThread.Start();
        }

        private void LoadCoinCapData()
        {
            //Display status
            MethodInvoker invoke = delegate
            {
                statusLabel.Text = "Status: Refreshing";
            };
            Invoke(invoke);

            //Pull coin data from CoinCap
            var webClient = new WebClient();
            var response = webClient.DownloadString("http://coincap.io/front");
            var downloadedCoins = JsonConvert.DeserializeObject<List<Coin>>(response);

            decimal totalPaid = 0;
            decimal totalProfits = 0;

            int lineIndex = 0;

            foreach(LoadCoin coin in coins)
            {
                Coin downloadedCoin = downloadedCoins.Single(c => c.shortName == coin.coin);

                if (_loadLines)
                {
                    coin.StartupPrice = downloadedCoin.price;

                    CoinLine newLine = new CoinLine(downloadedCoin.shortName, lineIndex);

                    newLine.boughtTextBox.Text = coin.bought.ToString();
                    newLine.paidTextBox.Text = coin.paid.ToString();

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

                    coinLines.Add(newLine);
                }

                CoinLine line = (from x in coinLines where x.Coin == downloadedCoin.shortName select x).First();

                if (_reaload)
                {
                    invoke = delegate
                    {
                        coin.StartupPrice = downloadedCoin.price;
                        line.boughtTextBox.Text = coin.bought.ToString();
                        line.paidTextBox.Text = coin.paid.ToString();
                    };
                    Invoke(invoke);
                }

                decimal bought = Convert.ToDecimal(line.boughtTextBox.Text);
                decimal paid = Convert.ToDecimal(line.paidTextBox.Text);
                decimal total = bought * downloadedCoin.price;
                decimal profit = total - paid;
                decimal changeDollar = downloadedCoin.price - coin.StartupPrice;
                decimal changePercent = ((downloadedCoin.price - coin.StartupPrice) / coin.StartupPrice) * 100;

                totalPaid += paid;
                totalProfits += paid + profit;

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

                lineIndex++;
            }

            invoke = delegate
            {
                totalProfit.Text = $"${totalProfits:0.00}";
                totalProfitChange.Text = $"(${totalProfits - totalPaid:0.00})";

                statusLabel.Text = "Status: Sleeping";
                refreshLabel.Text = $"Refreshes: {_refreshCount}";
            };
            Invoke(invoke);

            //Sleep and rerun
            _loadLines = false;
            _reaload = false;
            _refreshCount++;
            Thread.Sleep(5000);
            LoadCoinCapData();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "Status: Loading";
            _refreshCount = 0;

            totalProfit.Text = "$0.00";
            totalProfitChange.Text = "($0.00)";

            MethodInvoker invoke = delegate
            {
                foreach (LoadCoin coin in coins)
                {
                    CoinLine line = (from x in coinLines where x.Coin == coin.coin select x).First();

                    line.profitLabel.Text = $"$0.00";
                    line.changeDollarLabel.Text = $"$0.000000";
                    line.changePercentLabel.Text = $"0.00%";
                }
            };
            Invoke(invoke);

            _reaload = true;
        }
    }
}
