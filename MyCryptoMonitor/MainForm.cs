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
        private decimal _startBtc;
        private decimal _startEth;
        private decimal _startLtc;
        private decimal _startXlm;
        private decimal _startXrp;
        private bool _refresh;
        private bool _start;
        private int _refreshCount;
        private List<LoadCoin> coins;

        public MainForm()
        {
            InitializeComponent();

            _refresh = true;
            _start = true;

            coins = new List<LoadCoin> {
                new LoadCoin { coin = "BTC", bought =  0, paid = 0},
                new LoadCoin { coin = "ETH", bought =  0, paid = 0},
                new LoadCoin { coin = "LTC", bought =  0, paid = 0},
                new LoadCoin { coin = "XRP", bought = (decimal) 608.79, paid = 650},
                new LoadCoin { coin = "XLM", bought = (decimal) 1238.73999, paid = 350}
            };

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


            List<Coin> coinObj = new List<Coin>();
            int j = 0;
            foreach(LoadCoin coin in coins)
            {
                Coin test = downloadedCoins.Single(c => c.shortName == coin.coin);
                //test.CoinLine = new CoinLine(j);

                if (_start)
                {
                    //Controls.Add(test.CoinLine);
                    //Controls.Add(coin.CoinLine.priceLabel);
                    //Controls.Add(coin.CoinLine.boughtTextBox);
                    //Controls.Add(coin.CoinLine.totalLabel);
                    //Controls.Add(coin.CoinLine.paidTextBox);
                    //Controls.Add(coin.CoinLine.profitLabel);
                    //Controls.Add(coin.CoinLine.changeDollarLabel);
                    //Controls.Add(coin.CoinLine.changePercentLabel);
                }

                if (_start)
                {
                    for (int i = 0; i < coins.Count; i++)
                    {
                        test.original = test.price;

                        invoke = delegate
                        {
                            Controls.Add(test.CoinLine.coinLabel);
                            Controls.Add(test.CoinLine.priceLabel);
                            Controls.Add(test.CoinLine.boughtTextBox);
                            Controls.Add(test.CoinLine.totalLabel);
                            Controls.Add(test.CoinLine.paidTextBox);
                            Controls.Add(test.CoinLine.profitLabel);
                            Controls.Add(test.CoinLine.changeDollarLabel);
                            Controls.Add(test.CoinLine.changePercentLabel);
                        };
                        Invoke(invoke);
                    }
                }

                decimal total = coin.bought * test.price;

                invoke = delegate
                {
                    test.CoinLine.coinLabel.Text = test.shortName;
                    test.CoinLine.priceLabel.Text = $"${test.price:0.00}";
                    test.CoinLine.boughtTextBox.Text = coin.bought.ToString();
                    test.CoinLine.totalLabel.Text = $"${total:0.00}";
                    test.CoinLine.paidTextBox.Text = coin.paid.ToString();
                    test.CoinLine.profitLabel.Text = $"${total - Convert.ToDecimal(coin.paid):0.00}";
                    //test.CoinLine.changeDollarLabel.Text = $"${(test.price - test.original):0.000000}"; ;
                    //test.CoinLine.changePercentLabel.Text = $"{((test.price - test.original) / test.original) * 100:0.00}%";
                };
                Invoke(invoke);

                coinObj.Add(test);

                j++;
            }


            //Parse coins
            Coin btc = downloadedCoins.Single(c => c.shortName == "BTC");
            Coin eth = downloadedCoins.Single(c => c.shortName == "ETH");
            Coin ltc = downloadedCoins.Single(c => c.shortName == "LTC");
            Coin xrp = downloadedCoins.Single(c => c.shortName == "XRP");
            Coin xlm = downloadedCoins.Single(c => c.shortName == "XLM");

            //Set startup price of coins
            if (_start)
            {
                _startBtc = btc.price;
                _startEth = eth.price;
                _startLtc = ltc.price;
                _startXrp = xrp.price;
                _startXlm = xlm.price;
                _start = false;
            }

            //Calculations
            _refreshCount++;
            //decimal btcTotal = Convert.ToDecimal(btcBoughtLabel.Text) * btc.price;
            //decimal ethTotal = Convert.ToDecimal(ethBoughtLabel.Text) * eth.price;
            //decimal ltcTotal = Convert.ToDecimal(ltcBoughtLabel.Text) * ltc.price;
            //decimal xrpTotal = Convert.ToDecimal(xrpBoughtLabel.Text) * xrp.price;
            //decimal xlmTotal = Convert.ToDecimal(xlmBoughtLabel.Text) * xlm.price;
            //decimal profit = btcTotal + ethTotal + ltcTotal + xrpTotal + xlmTotal;
            //decimal profitChange = profit - (Convert.ToDecimal(btcPaid.Text) + Convert.ToDecimal(ethPaid.Text) + Convert.ToDecimal(ltcPaid.Text) + Convert.ToDecimal(xlmPaid.Text) + Convert.ToDecimal(xrpPaid.Text));

            invoke = delegate
            {
                //Totals
                //totalProfit.Text = $"${profit:#.00}";
                //totalProfitChange.Text = $"(${profitChange:#.00})";

                //Status
                statusLabel.Text = "Status: Sleeping";
                refreshLabel.Text = $"Refreshes: {_refreshCount}";
            };

            //Update UI
            Invoke(invoke);

            //Sleep and rerun
            if (_refresh)
            {
                Thread.Sleep(5000);
                //LoadCoinCapData();
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "Status: Loading";
            _refreshCount = 0;

            totalProfit.Text = "$0.00";
            totalProfitChange.Text = "($0.00)";

            _start = true;
        }
    }
}
