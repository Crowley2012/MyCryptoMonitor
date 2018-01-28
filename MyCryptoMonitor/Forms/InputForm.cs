using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace MyCryptoMonitor
{
    public partial class InputForm : Form
    {
        public string InputText { get; set; }
        public int CoinIndex { get; set; }

        public List<CoinConfig> coinConfigs { get; set; }

        public InputForm(string submitLabel, List<string> coins)
        {
            InitializeComponent();
            Text = $"{submitLabel} Coin";
            btnSubmit.Text = submitLabel;
            cbCoins.DataSource = coins;
        }

        public InputForm(string submitLabel, List<string> coins, List<CoinConfig> coinsConfig)
        {
            InitializeComponent();
            Text = $"{submitLabel} Coin";
            btnSubmit.Text = submitLabel;
            cbCoins.DataSource = coins;
            coinConfigs = coinsConfig;
            cbCoinIndex.DataSource = (from c in coinConfigs where c.coin == cbCoins.Text select c.coinIndex).ToList();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            InputText = cbCoins.Text;
            if(!string.IsNullOrEmpty(cbCoinIndex.Text))
                CoinIndex = Convert.ToInt32(cbCoinIndex.Text);
            DialogResult = DialogResult.OK;
        }

        private void cbCoins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coinConfigs == null)
                return;

            cbCoinIndex.DataSource = (from c in coinConfigs where c.coin == cbCoins.Text select c.coinIndex).ToList();
        }
    }
}
