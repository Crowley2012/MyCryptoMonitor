using MyCryptoMonitor.Api;
using MyCryptoMonitor.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MyCryptoMonitor.Services
{
    public static class CoinMapping
    {
        public static List<Coin> CoinMarketCap(string response)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            return JsonConvert.DeserializeObject<List<ApiCoinMarketCap>>(response, settings).Select(c => new Coin
            {
                ShortName = c.symbol,
                LongName = c.name,
                Change1HourPercent = c.percent_change_1h,
                Change24HourPercent = c.percent_change_24h,
                Change7DayPercent = c.percent_change_7d,
                MarketCap = c.market_cap_usd,
                Price = c.price_usd,
                Supply = c.total_supply
            }).ToList();
        }

        public static List<Coin> MapCombination(string responseCryptoCompare, string responseCoinMarketCap)
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
                    LongName = coinMarketCapCoins.Where(c => c.ShortName.Equals(cryptoCompareCoin.FROMSYMBOL)).Select(c => c.LongName).FirstOrDefault(),
                    Change1HourPercent = coinMarketCapCoins.Where(c => c.ShortName.Equals(cryptoCompareCoin.FROMSYMBOL)).Select(c => c.Change1HourPercent).FirstOrDefault(),
                    Change24HourPercent = cryptoCompareCoin.CHANGEPCT24HOUR,
                    Change7DayPercent = coinMarketCapCoins.Where(c => c.ShortName.Equals(cryptoCompareCoin.FROMSYMBOL)).Select(c => c.Change7DayPercent).FirstOrDefault(),
                    MarketCap = cryptoCompareCoin.MKTCAP,
                    Price = cryptoCompareCoin.PRICE,
                    Supply = cryptoCompareCoin.SUPPLY
                });
            }

            return list;
        }
    }
}
