namespace MyCryptoMonitor.Configs
{
    public class CoinConfig
    {
        public string Name { get; set; } = string.Empty;
        public int Index { get; set; }
        public decimal Bought { get; set; }
        public decimal Paid { get; set; }
        public decimal StartupPrice { get; set; }
        public string Currency { get; set; }
    }
}
