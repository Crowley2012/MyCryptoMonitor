using System.ComponentModel;

namespace MyCryptoMonitor.DataSources
{
    public class PortfolioDataSource
    {
        #region Public Properties

        public string FileName { get; set; }
        public string Name { get; set; }
        public bool Startup { get; set; }

        #endregion Public Properties
    }

    public class PortfolioDataSourceList : BindingList<PortfolioDataSource> { }
}