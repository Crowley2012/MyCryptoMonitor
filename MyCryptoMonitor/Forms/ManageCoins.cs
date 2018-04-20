using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using MyCryptoMonitor.Configs;

namespace MyCryptoMonitor.Forms
{
    public partial class ManageCoins : Form
    {
        #region Public Variables
        public string InputText { get => cbCoins.Text.ToUpper(); }
        public int CoinIndex { get => !string.IsNullOrEmpty(cbCoinIndex.Text) ? Convert.ToInt32(cbCoinIndex.Text) - 1 : 0; }
        #endregion

        #region Private Variables
        private List<CoinConfig> _coinConfigs { get; set; }
        #endregion

        #region Constructor
        public ManageCoins(List<string> coins)
        {
            InitializeComponent();

            Text = "Add Coin";
            btnSubmit.Text = "Add";
            cbCoinIndex.Enabled = false;

            cbCoins.DataSource = coins.ToList();
        }

        public ManageCoins(List<CoinConfig> coinsConfig)
        {
            InitializeComponent();

            Text = "Remove Coin";
            btnSubmit.Text = "Remove";
            cbCoinIndex.Enabled = true;
            cbCoins.DropDownStyle = ComboBoxStyle.DropDownList;

            _coinConfigs = coinsConfig;
            cbCoins.DataSource = coinsConfig.OrderBy(c => c.Name).Select(c => c.Name).Distinct().ToList();
            cbCoinIndex.DataSource = (from c in _coinConfigs where c.Name == cbCoins.Text select c.Index + 1).ToList();
        }
        #endregion

        #region Events
        private void Submit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cbCoins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_coinConfigs == null)
                return;

            cbCoinIndex.DataSource = (from c in _coinConfigs where c.Name == cbCoins.Text select c.Index + 1).ToList();
        }
        #endregion
    }
}
