namespace MyCryptoMonitor
{
    public class CoinData
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }

        public int CoinIndex { get; set; }

        public decimal Change1HourPercent { get; set; }
        public decimal Change24HourPercent { get; set; }
        public decimal MarketCap { get; set; }
        public decimal Price { get; set; }
        public decimal Supply { get; set; }
    }
}
