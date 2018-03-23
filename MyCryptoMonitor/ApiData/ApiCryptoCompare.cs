namespace MyCryptoMonitor.ApiData
{
    public class ApiCryptoCompare
    {
        public string TYPE { get; set; }
        public string MARKET { get; set; }
        public string FROMSYMBOL { get; set; }
        public string TOSYMBOL { get; set; }
        public string FLAGS { get; set; }
        public decimal PRICE { get; set; }
        public int LASTUPDATE { get; set; }
        public decimal LASTVOLUME { get; set; }
        public decimal LASTVOLUMETO { get; set; }
        public string LASTTRADEID { get; set; }
        public decimal VOLUMEDAY { get; set; }
        public decimal VOLUMEDAYTO { get; set; }
        public decimal VOLUME24HOUR { get; set; }
        public decimal VOLUME24HOURTO { get; set; }
        public decimal OPENDAY { get; set; }
        public decimal HIGHDAY { get; set; }
        public decimal LOWDAY { get; set; }
        public decimal OPEN24HOUR { get; set; }
        public decimal HIGH24HOUR { get; set; }
        public decimal LOW24HOUR { get; set; }
        public string LASTMARKET { get; set; }
        public decimal CHANGE24HOUR { get; set; }
        public decimal CHANGEPCT24HOUR { get; set; }
        public decimal CHANGEDAY { get; set; }
        public decimal CHANGEPCTDAY { get; set; }
        public long SUPPLY { get; set; }
        public decimal MKTCAP { get; set; }
        public decimal TOTALVOLUME24H { get; set; }
        public decimal TOTALVOLUME24HTO { get; set; }
    }
}
