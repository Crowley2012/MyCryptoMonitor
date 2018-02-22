using System.Collections.Generic;

namespace MyCryptoMonitor.DataSources
{
    public class AlertConfig
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ContactAddress { get; set; }
        public string ContactType { get; set; }
        public List<AlertDataSource> Alerts { get; set; }
    }
}
