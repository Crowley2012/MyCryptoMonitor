using MyCryptoMonitor.Objects;
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
        public Constants.Operators? LastOperator { get; set; }
        public Constants.Operators Operator { get; set; }
        public decimal Price { get; set; }

        #endregion Public Properties
    }

    public class AlertDataSourceList : BindingList<AlertDataSource> { }
}