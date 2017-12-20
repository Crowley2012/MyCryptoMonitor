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
        private decimal _startBtc;
        private decimal _startEth;
        private decimal _startLtc;
        private decimal _startXlm;
        private decimal _startXrp;
        private bool _refresh;
        private bool _start;
        private int _refreshCount;

        public MainForm()
        {
            InitializeComponent();

            _refresh = true;
            _start = true;

            btcBoughtLabel.Text = "0.0028856";
            ethBoughtLabel.Text = "0.0676444";
            ltcBoughtLabel.Text = "0.15087371";
            xrpBoughtLabel.Text = "293.406";
            xlmBoughtLabel.Text = "349.64";

            btcPaid.Text = "50";
            ethPaid.Text = "50";
            ltcPaid.Text = "50";
            xrpPaid.Text = "250";
            xlmPaid.Text = "100";

            Thread loadData = new Thread(() => LoadCoinCapData());
            loadData.Start();
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
            var coins = JsonConvert.DeserializeObject<List<Coin>>(response);

            //Parse coins
            Coin btc = coins.Single(c => c.shortName == "BTC");
            Coin eth = coins.Single(c => c.shortName == "ETH");
            Coin ltc = coins.Single(c => c.shortName == "LTC");
            Coin xrp = coins.Single(c => c.shortName == "XRP");
            Coin xlm = coins.Single(c => c.shortName == "XLM");

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
            decimal btcTotal = Convert.ToDecimal(btcBoughtLabel.Text) * btc.price;
            decimal ethTotal = Convert.ToDecimal(ethBoughtLabel.Text) * eth.price;
            decimal ltcTotal = Convert.ToDecimal(ltcBoughtLabel.Text) * ltc.price;
            decimal xrpTotal = Convert.ToDecimal(xrpBoughtLabel.Text) * xrp.price;
            decimal xlmTotal = Convert.ToDecimal(xlmBoughtLabel.Text) * xlm.price;
            decimal profit = btcTotal + ethTotal + ltcTotal + xrpTotal + xlmTotal;
            decimal profitChange = profit - (Convert.ToDecimal(btcPaid.Text) + Convert.ToDecimal(ethPaid.Text) + Convert.ToDecimal(ltcPaid.Text) + Convert.ToDecimal(xlmPaid.Text) + Convert.ToDecimal(xrpPaid.Text));

            invoke = delegate
            {
                //Price
                btcPrice.Text = $"${btc.price:0.00}";
                ethPrice.Text = $"${eth.price:0.00}";
                ltcPrice.Text = $"${ltc.price:0.00}";
                xrpPrice.Text = $"${xrp.price:0.00}";
                xlmPrice.Text = $"${xlm.price:0.00}";
                
                //Total
                btcTotalLabel.Text = $"${btcTotal:0.00}";
                ethTotalLabel.Text = $"${ethTotal:0.00}";
                ltcTotalLabel.Text = $"${ltcTotal:0.00}";
                xrpTotalLabel.Text = $"${xrpTotal:0.00}";
                xlmTotalLabel.Text = $"${xlmTotal:0.00}";

                //Profit
                btcProfit.Text = $"${btcTotal - Convert.ToDecimal(btcPaid.Text):0.00}";
                ethProfit.Text = $"${ethTotal - Convert.ToDecimal(ethPaid.Text):0.00}";
                ltcProfit.Text = $"${ltcTotal - Convert.ToDecimal(ltcPaid.Text):0.00}";
                xrpProfit.Text = $"${xrpTotal - Convert.ToDecimal(xrpPaid.Text):0.00}";
                xlmProfit.Text = $"${xlmTotal - Convert.ToDecimal(xlmPaid.Text):0.00}";

                //Change
                btcChange.Text = $"${(btc.price - _startBtc):0.000000}";
                ethChange.Text = $"${(eth.price - _startEth):0.000000}";
                ltcChange.Text = $"${(ltc.price - _startLtc):0.000000}";
                xrpChange.Text = $"${(xrp.price - _startXrp):0.000000}";
                xlmChange.Text = $"${(xlm.price - _startXlm):0.000000}";

                //Totals
                totalProfit.Text = $"${profit:#.00}";
                totalProfitChange.Text = $"(${profitChange:#.00})";

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
                LoadCoinCapData();
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            btcBoughtLabel.Text = "0.0028856";
            ethBoughtLabel.Text = "0.0676444";
            ltcBoughtLabel.Text = "0.15087371";
            xrpBoughtLabel.Text = "293.406";
            xlmBoughtLabel.Text = "349.64";

            btcPaid.Text = "50";
            ethPaid.Text = "50";
            ltcPaid.Text = "50";
            xrpPaid.Text = "250";
            xlmPaid.Text = "100";

            btcProfit.Text = "$0.00";
            ethProfit.Text = "$0.00";
            ltcProfit.Text = "$0.00";
            xrpProfit.Text = "$0.00";
            xlmProfit.Text = "$0.00";
            
            btcChange.Text = "$0.000000";
            ethChange.Text = "$0.000000";
            ltcChange.Text = "$0.000000";
            xrpChange.Text = "$0.000000";
            xlmChange.Text = "$0.000000";

            totalProfit.Text = "$0.00";
            totalProfitChange.Text = "($0.00)";

            _start = true;
        }
    }
}
