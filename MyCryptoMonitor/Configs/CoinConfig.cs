namespace MyCryptoMonitor.Configs
{
    public class CoinConfig
    {
        public string coin { get; set; } = string.Empty;
        public int coinIndex { get; set; }
        public decimal bought { get; set; }
        public decimal paid { get; set; }
        public decimal StartupPrice { get; set; }
        public bool SetStartupPrice { get; set; } = true;
    }
}
