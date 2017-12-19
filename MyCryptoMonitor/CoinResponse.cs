namespace MyCryptoMonitor
{
    public class CoinResponse
    {
        public float altCap { get; set; }
        public decimal bitnodesCount { get; set; }
        public long btcCap { get; set; }
        public decimal btcPrice { get; set; }
        public float dom { get; set; }
        public float totalCap { get; set; }
        public float volumeAlt { get; set; }
        public float volumeBtc { get; set; }
        public float volumeTotal { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public float price_btc { get; set; }
        public float price_eth { get; set; }
        public float price_eur { get; set; }
        public float price_usd { get; set; }
        public float price_ltc { get; set; }
        public float price_zec { get; set; }
        public float market_cap { get; set; }
        public float cap24hrChange { get; set; }
        public string display_name { get; set; }
        public string status { get; set; }
        public long supply { get; set; }
        public decimal volume { get; set; }
        public float price { get; set; }
        public float? vwap_h24 { get; set; }
    }
}
