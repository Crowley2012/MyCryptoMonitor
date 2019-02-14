using System.ComponentModel;

namespace MyCryptoMonitor.Objects
{
    public static class Constants
    {
        #region Public Fields

        public const string API_COIN_MARKET_CAP = "https://api.coinmarketcap.com/v1/ticker/?limit=9999&convert={0}";
        public const string API_CRYPTO_COMPARE = "https://min-api.cryptocompare.com/data/pricemultifull?tsyms={0}&fsyms=";
        public const string API_CRYPTO_COMPARE_COINS = "https://min-api.cryptocompare.com/data/all/coinlist";

        #endregion Public Fields

        #region Public Enums

        public enum Operators {[Description("Greater Than")] GreaterThan, [Description("Less Than")] LessThan, [Description("Both")] Both }

        public enum Types {[Description("Email")] Email, [Description("Verizon")] Verizon, [Description("AT&T")] ATT, [Description("Sprint")] Sprint, [Description("Boost Mobile")] Boost, [Description("T-Mobile")] TMobile, [Description("US Cellular")] USCellular, [Description("Virgin Mobile")] VirginMobile }

        #endregion Public Enums
    }
}