using System.ComponentModel;

namespace MyCryptoMonitor.DataSources
{
    public class PortfolioSource
    {
        public string Name { get; set; }
        public bool Startup { get; set; }
    }

    public class PortfolioSourceList : BindingList<PortfolioSource> { }
}
