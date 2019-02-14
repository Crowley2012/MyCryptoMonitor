namespace MyCryptoMonitor.Objects
{
    public static class Addresses
    {
        #region Private Fields

        public const string API_COIN_MARKET_CAP = "https://api.coinmarketcap.com/v1/ticker/?limit=9999&convert={0}";
        public const string API_CRYPTO_COMPARE = "https://min-api.cryptocompare.com/data/pricemultifull?tsyms={0}&fsyms=";
        public const string API_CRYPTO_COMPARE_COINS = "https://min-api.cryptocompare.com/data/all/coinlist";

        #endregion Private Fields
    }
}