namespace MyCryptoMonitor
{
    public class ApiCoinMarketCap
    {
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string rank { get; set; }
        public string price_btc { get; set; }
        public string _24h_volume_usd { get; set; }
        public string available_supply { get; set; }
        public string max_supply { get; set; }
        public string last_updated { get; set; }

        public decimal price_usd { get; set; }
        public decimal market_cap_usd { get; set; }
        public decimal total_supply { get; set; }
        public decimal percent_change_1h { get; set; }
        public decimal percent_change_24h { get; set; }
        public decimal percent_change_7d { get; set; }
    }
}