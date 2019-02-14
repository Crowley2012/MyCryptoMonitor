using MyCryptoMonitor.Configs;
using MyCryptoMonitor.Objects;
using MyCryptoMonitor.Services;
using System.Collections.Generic;
using System.Linq;

namespace MyCryptoMonitor.ViewModels
{
    public class FrmManageCoinsViewModel : NotifyPropertyChangedBase
    {
        #region Public Properties

        public string ButtonText { get; set; }
        public bool CoinIndexEnabled { get; set; }
        public List<int> CoinIndexes { get; set; }
        public List<string> Coins { get; set; }
        public string SelectedCoin { get { return _selectedCoin; } set { _selectedCoin = value; GetCoinIndexes(); } }
        public int SelectedCoinIndex { get; set; } = 1;
        public string Title { get; set; }

        #endregion Public Properties

        #region Private Fields

        private readonly ApplicationLayer _applicationLayer = new ApplicationLayer();
        private readonly List<CoinConfig> _currentlyAddedCoins;
        private string _selectedCoin;

        #endregion Private Fields

        #region Public Constructors

        public FrmManageCoinsViewModel(bool addCoinSetup, List<CoinConfig> currentlyAddedCoins)
        {
            _currentlyAddedCoins = currentlyAddedCoins;
            Coins = _applicationLayer.GetCoinNames();

            if (addCoinSetup)
            {
                Title = "Add Coin";
                ButtonText = "Add";
                CoinIndexEnabled = false;
            }
            else
            {
                Title = "Remove Coin";
                ButtonText = "Remove";
                CoinIndexEnabled = true;

                Coins = currentlyAddedCoins.Select(x => x.Name).OrderBy(x => x).Distinct().ToList();
                GetCoinIndexes();
            }

            FirePropertyChanged();
        }

        #endregion Public Constructors

        #region Private Methods

        private void GetCoinIndexes()
        {
            CoinIndexes = _currentlyAddedCoins.Where(x => x.Name == SelectedCoin).Select(x => x.Index + 1).ToList();
            FirePropertyChanged(() => CoinIndexes);
        }

        #endregion Private Methods
    }
}