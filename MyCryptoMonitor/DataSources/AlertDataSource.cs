using MyCryptoMonitor.Statics;
using System.ComponentModel;

namespace MyCryptoMonitor.DataSources
{
    public class AlertDataSource
    {
        public string Coin { get; set; }
        public decimal Current { get; set; }
        public AlertService.Operators Operator { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public bool Enabled { get; set; } = true;
    }

    public class AlertDataSourceList : BindingList<AlertDataSource> { }
}
