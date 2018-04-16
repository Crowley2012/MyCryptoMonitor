namespace MyCryptoMonitor.Configs
{
    public class CoinConfig
    {
        public string Coin { get; set; } = string.Empty;
        public int CoinIndex { get; set; }
        public decimal Bought { get; set; }
        public decimal Paid { get; set; }
        public decimal StartupPrice { get; set; }
        public bool SetStartupPrice { get; set; } = true;
    }
}
