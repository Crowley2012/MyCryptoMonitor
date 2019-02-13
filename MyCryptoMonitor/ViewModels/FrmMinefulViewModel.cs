using MyCryptoMonitor.Objects;
using MyCryptoMonitor.Statics;

namespace MyCryptoMonitor.ViewModels
{
    public class FrmMinefulViewModel : NotifyPropertyChangedBase
    {
        #region Public Properties

        public bool Enabled { get; set; }
        public int PowerPercentage { get => _powerPercentage; set => SetValue(ref _powerPercentage, value); }
        public string PowerPercentageDisplay => $"{PowerPercentage} %";

        #endregion Public Properties

        #region Private Fields

        private int _powerPercentage;

        #endregion Private Fields

        #region Public Constructors

        public FrmMinefulViewModel()
        {
            Enabled = UserConfigService.MinerEnabled;
            PowerPercentage = UserConfigService.MinerPercentage;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Save()
        {
            UserConfigService.MinerEnabled = Enabled;
            UserConfigService.MinerPercentage = PowerPercentage;
        }

        #endregion Public Methods
    }
}