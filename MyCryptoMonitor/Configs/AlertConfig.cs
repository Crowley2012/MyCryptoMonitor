using MyCryptoMonitor.DataSources;
using System.Collections.Generic;

namespace MyCryptoMonitor.Configs
{
    public class AlertConfig
    {
        #region Public Properties

        public List<AlertDataSource> Alerts { get; set; } = new List<AlertDataSource>();
        public string ReceiveAddress { get; set; } = string.Empty;
        public string ReceiveType { get; set; } = string.Empty;
        public string SendAddress { get; set; } = string.Empty;
        public string SendPassword { get; set; } = string.Empty;

        #endregion Public Properties
    }
}