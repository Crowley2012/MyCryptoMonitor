using MyCryptoMonitor.Api;
using MyCryptoMonitor.Configs;
using MyCryptoMonitor.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MyCryptoMonitor.Statics
{
    public class MappingService
    {
        #region Public Methods

        public static List<Coin> CoinMarketCap(string response)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            return JsonConvert.DeserializeObject<List<ApiCoinMarketCap>>(response, settings).Select(c => new Coin
            {
                ShortName = c.symbol == "MIOTA" ? "IOTA" : c.symbol,
                LongName = c.name,
                Change1HourPercent = c.percent_change_1h.ConvertToDecimal(),
                Change24HourPercent = c.percent_change_24h.ConvertToDecimal(),
                Change7DayPercent = c.percent_change_7d.ConvertToDecimal(),
                MarketCap = c.market_cap_usd.ConvertToDecimal(),
                Price = c.price_usd.ConvertToDecimal(),
                Supply = c.total_supply.ConvertToDecimal()
            }).ToList();
        }

        public static List<string> CryptoCompareCoinList(string response)
        {
            var coinData = JsonConvert.DeserializeObject<List<Dictionary<string, ApiCryptoCompareCoin>>>($"[{(JObject.Parse(response)["Data"]).ToString()}]");

            var coinList = coinData[0].Keys.ToList();
            coinList.AddRange(UserConfigService.CustomCoins);

            return coinList.OrderBy(k => k).ToList();
        }

        public static List<Coin> MapCombination(string responseCryptoCompare, string responseCoinMarketCap, List<CoinConfig> coinConfigs)
        {
            List<Coin> list = new List<Coin>();
            var cryptoCompareCoins = JObject.Parse(responseCryptoCompare).First.First.Children<JProperty>();
            var coinMarketCapCoins = CoinMarketCap(responseCoinMarketCap);

            foreach (var data in cryptoCompareCoins)
            {
                var cryptoCompareCoin = JsonConvert.DeserializeObject<ApiCryptoCompare>(data.First.First.First.ToString());

                list.Add(new Coin
                {
                    ShortName = cryptoCompareCoin.FROMSYMBOL,
                    LongName = coinMarketCapCoins.Where(c => c.ShortName.ExtEquals(cryptoCompareCoin.FROMSYMBOL)).Select(c => c.LongName).FirstOrDefault(),
                    Change1HourPercent = coinMarketCapCoins.Where(c => c.ShortName.ExtEquals(cryptoCompareCoin.FROMSYMBOL)).Select(c => c.Change1HourPercent).FirstOrDefault(),
                    Change24HourPercent = cryptoCompareCoin.CHANGEPCT24HOUR.ConvertToDecimal(),
                    Change7DayPercent = coinMarketCapCoins.Where(c => c.ShortName.ExtEquals(cryptoCompareCoin.FROMSYMBOL)).Select(c => c.Change7DayPercent).FirstOrDefault(),
                    MarketCap = cryptoCompareCoin.MKTCAP.ConvertToDecimal(),
                    Price = cryptoCompareCoin.PRICE?.ConvertToDecimal() ?? coinMarketCapCoins.First(c => c.ShortName == cryptoCompareCoin.FROMSYMBOL).Price,
                    Supply = cryptoCompareCoin.SUPPLY.ConvertToDecimal()
                });
            }

            foreach (var config in coinConfigs)
            {
                if (!list.Any(c => c.ShortName == config.Name))
                {
                    var cmc = coinMarketCapCoins.Where(c => c.ShortName == config.Name).FirstOrDefault();

                    if (cmc != null)
                        list.Add(cmc);
                }
            }

            return list;
        }

        #endregion Public Methods
    }
}