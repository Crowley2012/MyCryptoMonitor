using System.Collections.Generic;

namespace MyCryptoMonitor.DataSources
{
    public class AlertConfig
    {
        public string SendAddress { get; set; }
        public string SendPassword { get; set; }
        public string ReceiveAddress { get; set; }
        public string ReceiveType { get; set; }
        public List<AlertDataSource> Alerts { get; set; }
    }
}
