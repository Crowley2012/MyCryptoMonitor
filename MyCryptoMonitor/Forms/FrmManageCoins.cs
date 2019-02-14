using MyCryptoMonitor.Configs;
using MyCryptoMonitor.Statics;
using MyCryptoMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class FrmManageCoins : Form
    {
        #region Public Properties

        public string SelectedCoin => _viewModel.SelectedCoin;
        public int SelectedCoinIndex => _viewModel.SelectedCoinIndex - 1;

        #endregion Public Properties

        #region Private Fields

        private readonly FrmManageCoinsViewModel _viewModel;

        #endregion Private Fields

        #region Public Constructors

        public FrmManageCoins(bool addCoin, List<CoinConfig> currentlyAddedCoins)
        {
            InitializeComponent();

            _viewModel = new FrmManageCoinsViewModel(addCoin, currentlyAddedCoins);
            bsViewModel.DataSource = _viewModel;
        }

        #endregion Public Constructors

        #region Private Methods

        private void ManageCoins_Load(object sender, EventArgs e)
        {
            Globals.SetTheme(this);
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        #endregion Private Methods
    }
}