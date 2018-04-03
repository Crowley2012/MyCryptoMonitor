using System.ComponentModel;

namespace MyCryptoMonitor.DataSources
{
    public class AlertDataSource
    {
        public string Coin { get; set; }
        public decimal Current { get; set; }
        public string Operator { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }

    public class AlertDataSourceList : BindingList<AlertDataSource> { }
}
