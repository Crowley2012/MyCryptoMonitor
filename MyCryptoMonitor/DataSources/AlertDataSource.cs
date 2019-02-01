using MyCryptoMonitor.Statics;
using System.ComponentModel;

namespace MyCryptoMonitor.DataSources
{
    public class AlertDataSource
    {
        #region Public Properties

        public string Coin { get; set; }
        public string Currency { get; set; }
        public decimal Current { get; set; }
        public bool Enabled { get; set; } = true;
        public AlertService.Operators? LastOperator { get; set; }
        public AlertService.Operators Operator { get; set; }
        public decimal Price { get; set; }

        #endregion Public Properties
    }

    public class AlertDataSourceList : BindingList<AlertDataSource> { }
}