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
        private bool _refresh;
        private int count;

        public MainForm()
        {
            InitializeComponent();

            _refresh = true;

            btcBought.Text = "0.0028856";
            ethBought.Text = "0.0676444";
            ltcBought.Text = "0.00";
            xrpBought.Text = "293.406";
            xlmBought.Text = "349.64";
            
            Thread loadData = new Thread(() => LoadCoinCapData());
            loadData.Start();
        }

        private void LoadCoinCapData()
        {
            MethodInvoker invoke = delegate
            {
                statusLabel.Text = "Status: Refreshing";
            };

            Invoke(invoke);

            var newclient = new WebClient();
            var testres = newclient.DownloadString("http://coincap.io/front");
            var test = JsonConvert.DeserializeObject<List<Coin>>(testres);

            count++;

            invoke = delegate
            {
                btcPrice.Text = $"${test.Single(c => c._short == "BTC").price}";
                ethPrice.Text = $"${test.Single(c => c._short == "ETH").price}";
                ltcPrice.Text = $"${test.Single(c => c._short == "LTC").price}";
                xlmPrice.Text = $"${test.Single(c => c._short == "XLM").price}";
                xrpPrice.Text = $"${test.Single(c => c._short == "XRP").price}";
                
                btcTotal.Text = $"${test.Single(c => c._short == "BTC").price * Convert.ToDecimal(btcBought.Text)}";
                ethTotal.Text = $"${test.Single(c => c._short == "ETH").price * Convert.ToDecimal(ethBought.Text)}";
                ltcTotal.Text = $"${test.Single(c => c._short == "LTC").price * Convert.ToDecimal(ltcBought.Text)}";
                xlmTotal.Text = $"${test.Single(c => c._short == "XLM").price * Convert.ToDecimal(xlmBought.Text)}";
                xrpTotal.Text = $"${test.Single(c => c._short == "XRP").price * Convert.ToDecimal(xrpBought.Text)}";

                totalLabel.Text = $"${Convert.ToDecimal(btcTotal.Text.Replace("$", string.Empty)) + Convert.ToDecimal(ethTotal.Text.Replace("$", string.Empty)) + Convert.ToDecimal(ltcTotal.Text.Replace("$", string.Empty)) + Convert.ToDecimal(xlmTotal.Text.Replace("$", string.Empty)) + Convert.ToDecimal(xrpTotal.Text.Replace("$", string.Empty)):#.00}";

                statusLabel.Text = "Status: Sleeping";
                refreshLabel.Text = $"Refreshes: {count}";
            };

            Invoke(invoke);

            if (_refresh)
            {
                Thread.Sleep(5000);
                LoadCoinCapData();
            }
        }
    }
}
