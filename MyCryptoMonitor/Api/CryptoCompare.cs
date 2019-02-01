namespace MyCryptoMonitor.Api
{
    public class ApiCryptoCompare
    {
        #region Public Properties

        public string CHANGE24HOUR { get; set; }
        public string CHANGEDAY { get; set; }
        public string CHANGEPCT24HOUR { get; set; }
        public string CHANGEPCTDAY { get; set; }
        public string FLAGS { get; set; }
        public string FROMSYMBOL { get; set; }
        public string HIGH24HOUR { get; set; }
        public string HIGHDAY { get; set; }
        public string LASTMARKET { get; set; }
        public string LASTTRADEID { get; set; }
        public string LASTUPDATE { get; set; }
        public string LASTVOLUME { get; set; }
        public string LASTVOLUMETO { get; set; }
        public string LOW24HOUR { get; set; }
        public string LOWDAY { get; set; }
        public string MARKET { get; set; }
        public string MKTCAP { get; set; }
        public string OPEN24HOUR { get; set; }
        public string OPENDAY { get; set; }
        public string PRICE { get; set; }
        public string SUPPLY { get; set; }
        public string TOSYMBOL { get; set; }
        public string TOTALVOLUME24H { get; set; }
        public string TOTALVOLUME24HTO { get; set; }
        public string TYPE { get; set; }
        public string VOLUME24HOUR { get; set; }
        public string VOLUME24HOURTO { get; set; }
        public string VOLUMEDAY { get; set; }
        public string VOLUMEDAYTO { get; set; }

        #endregion Public Properties
    }

    public class ApiCryptoCompareCoin
    {
        #region Public Properties

        public string Algorithm { get; set; }
        public string CoinName { get; set; }
        public string FullName { get; set; }
        public string FullyPremined { get; set; }
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsTrading { get; set; }
        public string Name { get; set; }
        public string PreMinedValue { get; set; }
        public string ProofType { get; set; }
        public string SortOrder { get; set; }
        public bool Sponsored { get; set; }
        public string Symbol { get; set; }
        public string TotalCoinsFreeFloat { get; set; }
        public string TotalCoinSupply { get; set; }
        public string Url { get; set; }

        #endregion Public Properties
    }
}