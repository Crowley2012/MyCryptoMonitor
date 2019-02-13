using MyCryptoMonitor.Objects;
using MyCryptoMonitor.Statics;

namespace MyCryptoMonitor.ViewModels
{
    public class FrmMinefulViewModel : NotifyPropertyChangedBase
    {
        #region Public Properties

        public bool Enabled { get => _enabled; set => SetValue(ref _enabled, value); }
        public int PowerPercentage { get => _powerPercentage; set => SetValue(ref _powerPercentage, value); }
        public string PowerPercentageDisplay => $"{PowerPercentage} %";
        public bool Mining { get; set; }
        public int HashRate { get; set; }
        public bool Accepted { get; set; }
        public int CpuUsage { get; set; }
        public int CpuCores { get; set; }
        public int Port { get; set; }

        #endregion Public Properties

        #region Private Fields

        private int _powerPercentage;
        private bool _enabled;

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