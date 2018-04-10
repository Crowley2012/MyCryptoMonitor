using MyCryptoMonitor.Configs;
using MyCryptoMonitor.DataSources;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyCryptoMonitor.Statics
{
    public class PortfolioService
    {
        #region Public Variables
        public static string SelectedPortfolio { get; set; }
        public static List<PortfolioDataSource> Portfolios { get; set; }
        #endregion

        #region Methods
        public static void LoadPortfolios()
        {
            Portfolios = new List<PortfolioDataSource>();
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.portfolio");

            foreach (string file in files)
            {
                var name = Path.GetFileName(file);
                Portfolios.Add(new PortfolioDataSource { Name = name.Replace(".portfolio", string.Empty), Startup = UserConfigService.StartupPortfolio.Equals(name) });
            }

            Portfolios = Portfolios.OrderByDescending(p => p.Name).ToList();
        }

        public static List<CoinConfig> LoadFirstPortfolio()
        {
            var portfolio = $"{UserConfigService.StartupPortfolio}";

            if (File.Exists(portfolio))
                return LoadPortfolio(portfolio);

            return new List<CoinConfig>();
        }

        public static List<CoinConfig> LoadPortfolio(string portfolio)
        {
            SelectedPortfolio = portfolio;

            if (UserConfigService.Encrypted)
                return LoadPortfolioEncrypted(portfolio) ?? new List<CoinConfig>();
            else
                return LoadPortfolioUnencrypted(portfolio) ?? new List<CoinConfig>();
        }

        public static List<CoinConfig> LoadPortfolioEncrypted(string portfolio)
        {
            return JsonConvert.DeserializeObject<List<CoinConfig>>(Encryption.AesDecryptString(File.ReadAllText(portfolio)));
        }

        public static List<CoinConfig> LoadPortfolioUnencrypted(string portfolio)
        {
            return JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText(portfolio));
        }

        public static void SetStartupPortfolio(string portfolio)
        {
            UserConfigService.StartupPortfolio = portfolio += ".portfolio";
            UserConfigService.Save();
        }

        public static void RenamePortfolio(string oldPortfolio, string newPortfolio)
        {
            File.Move(oldPortfolio + ".portfolio", newPortfolio + ".portfolio");
        }

        public static void DeletePortfolio(string portfolio)
        {
            File.Delete(portfolio + ".portfolio");
        }

        public static bool NewPortfolio(string portfolio)
        {
            portfolio += ".portfolio";

            if (!File.Exists(portfolio))
            {
                File.WriteAllText(portfolio, string.Empty);
                return true;
            }

            return false;
        }

        public static void SavePortfolio(string portfolio, List<CoinConfig> coinConfigs)
        {
            if (UserConfigService.Encrypted)
                SavePortfolioEncrypted(portfolio, coinConfigs);
            else
                SavePortfolioUnencrypted(portfolio, coinConfigs);
        }

        public static void SavePortfolioEncrypted(string portfolio, List<CoinConfig> coinConfigs)
        {
            File.WriteAllText(portfolio, Encryption.AesEncryptString(JsonConvert.SerializeObject(coinConfigs)));
        }

        public static void SavePortfolioUnencrypted(string portfolio, List<CoinConfig> coinConfigs)
        {
            File.WriteAllText(portfolio, JsonConvert.SerializeObject(coinConfigs));
        }

        public static void EncryptPortfolios()
        {
            LoadPortfolios();

            foreach (var portfolio in Portfolios)
                File.WriteAllText(portfolio.Name + ".portfolio", Encryption.AesEncryptString(JsonConvert.SerializeObject(LoadPortfolioUnencrypted(portfolio.Name + ".portfolio"))));
        }

        public static void DecryptPortfolios()
        {
            LoadPortfolios();

            foreach (var portfolio in Portfolios)
                File.WriteAllText(portfolio.Name + ".portfolio", JsonConvert.SerializeObject(LoadPortfolioEncrypted(portfolio.Name + ".portfolio")));
        }
        #endregion
    }
}
