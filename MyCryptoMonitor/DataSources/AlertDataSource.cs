using System.ComponentModel;

namespace MyCryptoMonitor.DataSources
{
    public class AlertDataSource
    {
        public string Coin { get; set; }
        public string Current { get; set; }
        public string Operator { get; set; }
        public string Price { get; set; }
    }

    public class AlertDataSourceList : BindingList<AlertDataSource> { }
}
