namespace MyCryptoMonitor
{
    public static class Mappings
    {
        public static CoinData MapCoinMarketCap(ApiCoinMarketCap coin)
        {
            return new CoinData
            {
                ShortName = coin.symbol,
                LongName = coin.name,
                Change1HourPercent = coin.percent_change_1h,
                Change24HourPercent = coin.percent_change_24h,
                MarketCap = coin.market_cap_usd,
                Price = coin.price_usd,
                Supply = coin.total_supply
            };
        }

        public static CoinData MapCoinCap(ApiCoinCap coin)
        {
            return new CoinData
            {
                ShortName = coin.shortName,
                LongName = coin.longName,
                Change24HourPercent = coin.cap24hrChange,
                MarketCap = coin.mktcap,
                Price = coin.price,
                Supply = coin.supply
            };
        }
    }
}
