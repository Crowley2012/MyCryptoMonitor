namespace MyCryptoMonitor.Configs
{
    public class CoinConfig
    {
        #region Public Properties

        public decimal Bought { get; set; }
        public string Currency { get; set; }
        public int Index { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Paid { get; set; }
        public decimal StartupPrice { get; set; }

        #endregion Public Properties
    }
}