using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using MyCryptoMonitor.Objects;
using MyCryptoMonitor.Configs;

namespace MyCryptoMonitor.Forms
{
    public partial class ManageCoins : Form
    {
        public string InputText { get; set; }
        public int CoinIndex { get; set; }

        private List<CoinConfig> _coinConfigs { get; set; }

        public ManageCoins(string submitLabel, List<Coin> coins)
        {
            InitializeComponent();

            //Setup labels
            Text = $"{submitLabel} Coin";
            btnSubmit.Text = submitLabel;

            //Disable coin index selection
            cbCoinIndex.Enabled = false;

            //Set data sources
            cbCoins.DataSource = coins.Select(c => c.ShortName).ToList();
        }

        public ManageCoins(string submitLabel, List<CoinConfig> coinsConfig)
        {
            InitializeComponent();

            //Setup labels
            Text = $"{submitLabel} Coin";
            btnSubmit.Text = submitLabel;

            //Set coin configs
            _coinConfigs = coinsConfig;

            //Set data sources
            cbCoins.DataSource = coinsConfig.OrderBy(c => c.Coin).Select(c => c.Coin).Distinct().ToList();
            cbCoinIndex.DataSource = (from c in _coinConfigs where c.Coin == cbCoins.Text select c.CoinIndex + 1).ToList();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            InputText = cbCoins.Text;

            if(!string.IsNullOrEmpty(cbCoinIndex.Text))
                CoinIndex = Convert.ToInt32(cbCoinIndex.Text) - 1;

            DialogResult = DialogResult.OK;
        }

        private void cbCoins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_coinConfigs == null)
                return;

            cbCoinIndex.DataSource = (from c in _coinConfigs where c.Coin == cbCoins.Text select c.CoinIndex + 1).ToList();
        }
    }
}
