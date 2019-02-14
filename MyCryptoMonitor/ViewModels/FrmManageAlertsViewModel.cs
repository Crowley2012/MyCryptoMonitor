using MyCryptoMonitor.DataSources;
using MyCryptoMonitor.Objects;
using MyCryptoMonitor.Services;
using MyCryptoMonitor.Statics;
using System;
using System.Collections.Generic;

namespace MyCryptoMonitor.ViewModels
{
    public class FrmManageAlertsViewModel
    {
        #region Public Properties

        public List<AlertDataSource> Alerts { get; set; }
        public decimal CheckPrice { get; set; }
        public string Coin { get; set; }
        public List<string> Coins { get; set; }
        public decimal CurrentPrice { get; set; }
        public bool DeleteTriggeredAlerts { get; set; }
        public Constants.Operators Operator { get; set; }
        public List<Tuple<string, Constants.Operators>> Operators { get; set; }
        public string ReceiveAddress { get; set; }
        public Constants.Types ReceiveType { get; set; }
        public string SendAddress { get; set; }
        public string SendPassword { get; set; }
        public List<Tuple<string, Constants.Types>> Types { get; set; }

        #endregion Public Properties

        #region Private Fields

        private readonly ApplicationLayer _applicationLayer = new ApplicationLayer();
        private List<AlertDataSource> _otherAlerts;

        #endregion Private Fields

        #region Public Constructors

        public FrmManageAlertsViewModel()
        {
            Coins = _applicationLayer.GetCoinNames();
        }

        public void Save()
        {
            UserConfigService.DeleteAlerts = DeleteTriggeredAlerts;
            AlertService.SendAddress = SendAddress;
            AlertService.SendPassword = SendPassword;
            AlertService.ReceiveAddress = ReceiveAddress;
            AlertService.ReceiveType = ReceiveType;
            AlertService.Alerts = Alerts;
            AlertService.Alerts.AddRange(_otherAlerts);
            AlertService.Save();
        }

        #endregion Public Constructors
    }
}