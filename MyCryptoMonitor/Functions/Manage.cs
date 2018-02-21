using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MyCryptoMonitor.Functions
{
    public class Manage
    {
        public static string SelectedPortfolio;

        public static List<CoinConfig> LoadPortfolio()
        {
            if (File.Exists("Portfolio1"))
                return LoadPortfolio("Portfolio1");

            else if (File.Exists("Portfolio2"))
                return LoadPortfolio("Portfolio2");

            else if (File.Exists("Portfolio3"))
                return LoadPortfolio("Portfolio3");

            return new List<CoinConfig>();
        }

        public static List<CoinConfig> LoadPortfolio(string portfolio)
        {
            if (!File.Exists(portfolio))
                return null;

            SelectedPortfolio = portfolio;

            if (Globals.UserConfig.Encryption)
                return JsonConvert.DeserializeObject<List<CoinConfig>>(AESEncrypt.AesDecryptString(File.ReadAllText(portfolio)));
            else
                return JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText(portfolio));
        }

        public static List<CoinConfig> LoadPortfolioUnencrypted(string portfolio)
        {
            return JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText(portfolio));
        }

        public static List<CoinConfig> LoadPortfolioEncrypted(string portfolio)
        {
            return JsonConvert.DeserializeObject<List<CoinConfig>>(AESEncrypt.AesDecryptString(File.ReadAllText(portfolio)));
        }
    }
}
