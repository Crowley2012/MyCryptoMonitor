namespace MyCryptoMonitor.ApiData
{
    public class ApiCryptoCompare
    {
        public string TYPE { get; set; }
        public string MARKET { get; set; }
        public string FROMSYMBOL { get; set; }
        public string TOSYMBOL { get; set; }
        public string FLAGS { get; set; }
        public float PRICE { get; set; }
        public int LASTUPDATE { get; set; }
        public float LASTVOLUME { get; set; }
        public float LASTVOLUMETO { get; set; }
        public string LASTTRADEID { get; set; }
        public float VOLUMEDAY { get; set; }
        public float VOLUMEDAYTO { get; set; }
        public float VOLUME24HOUR { get; set; }
        public float VOLUME24HOURTO { get; set; }
        public float OPENDAY { get; set; }
        public float HIGHDAY { get; set; }
        public float LOWDAY { get; set; }
        public float OPEN24HOUR { get; set; }
        public float HIGH24HOUR { get; set; }
        public float LOW24HOUR { get; set; }
        public string LASTMARKET { get; set; }
        public float CHANGE24HOUR { get; set; }
        public float CHANGEPCT24HOUR { get; set; }
        public float CHANGEDAY { get; set; }
        public float CHANGEPCTDAY { get; set; }
        public int SUPPLY { get; set; }
        public float MKTCAP { get; set; }
        public float TOTALVOLUME24H { get; set; }
        public float TOTALVOLUME24HTO { get; set; }
    }
}
