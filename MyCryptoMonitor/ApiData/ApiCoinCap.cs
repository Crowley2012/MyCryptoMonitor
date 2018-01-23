using Newtonsoft.Json;

namespace MyCryptoMonitor
{
    public class ApiCoinCap
    {
        [JsonProperty("long")]
        public string longName { get; set; }
        [JsonProperty("short")]
        public string shortName { get; set; }

        public decimal cap24hrChange { get; set; }
        public decimal mktcap { get; set; }
        public decimal perc { get; set; }
        public decimal price { get; set; }
        public decimal supply { get; set; }
        public decimal usdVolume { get; set; }
        public decimal volume { get; set; }
        public decimal vwapData { get; set; }
        public decimal vwapDataBTC { get; set; }

        public bool shapeshift { get; set; }
    }
}
