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
        public static string CurrentPortfolio { get; set; }
        #endregion

        #region Private Variables
        private const string FILEEXTENSION = ".portfolio";
        #endregion

        #region Methods
        public static List<PortfolioDataSource> GetPortfolios()
        {
            return (from filePath in Directory.GetFiles(Directory.GetCurrentDirectory(), $"*{FILEEXTENSION}")
                    select new PortfolioDataSource
                    {
                        Name = Path.GetFileName(filePath).Replace(FILEEXTENSION, string.Empty),
                        FileName = Path.GetFileName(filePath),
                        Startup = UserConfigService.StartupPortfolio.Equals(Path.GetFileName(filePath))
                    })
                    .OrderByDescending(p => p.Name)
                    .ToList();
        }

        public static bool Create(string portfolio)
        {
            if (!File.Exists(portfolio + FILEEXTENSION))
            {
                File.WriteAllText(portfolio + FILEEXTENSION, string.Empty);
                return true;
            }

            return false;
        }

        public static void Save(string portfolio, List<CoinConfig> coinConfigs)
        {
            if (UserConfigService.Encrypted)
                SaveEncrypted(portfolio, coinConfigs);
            else
                SaveUnencrypted(portfolio, coinConfigs);
        }

        public static List<CoinConfig> Load(string portfolio)
        {
            CurrentPortfolio = portfolio;
            return UserConfigService.Encrypted ? LoadEncrypted(portfolio) ?? new List<CoinConfig>() : LoadUnencrypted(portfolio) ?? new List<CoinConfig>();
        }

        public static void EncryptPortfolios()
        {
            foreach (var portfolio in GetPortfolios())
                File.WriteAllText(portfolio.Name + FILEEXTENSION, EncryptionService.AesEncryptString(JsonConvert.SerializeObject(LoadUnencrypted(portfolio.Name + FILEEXTENSION))));
        }

        public static void DecryptPortfolios()
        {
            foreach (var portfolio in GetPortfolios())
                File.WriteAllText(portfolio.Name + FILEEXTENSION, JsonConvert.SerializeObject(LoadEncrypted(portfolio.Name + FILEEXTENSION)));
        }

        public static void SaveEncrypted(string portfolio, List<CoinConfig> coinConfigs) => File.WriteAllText(portfolio, EncryptionService.AesEncryptString(JsonConvert.SerializeObject(coinConfigs)));

        public static void SaveUnencrypted(string portfolio, List<CoinConfig> coinConfigs) => File.WriteAllText(portfolio, JsonConvert.SerializeObject(coinConfigs));

        public static List<CoinConfig> LoadStartup() => File.Exists(UserConfigService.StartupPortfolio) ? Load(UserConfigService.StartupPortfolio) : new List<CoinConfig>();

        private static List<CoinConfig> LoadEncrypted(string portfolio) => JsonConvert.DeserializeObject<List<CoinConfig>>(EncryptionService.AesDecryptString(File.ReadAllText(portfolio)));

        private static List<CoinConfig> LoadUnencrypted(string portfolio) => JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText(portfolio));

        public static void Rename(string oldPortfolio, string newPortfolio) => File.Move(oldPortfolio + FILEEXTENSION, newPortfolio + FILEEXTENSION);

        public static void Delete(string portfolio) => File.Delete(portfolio + FILEEXTENSION);

        public static void SetStartup(string portfolio) => UserConfigService.StartupPortfolio = portfolio += FILEEXTENSION;
        #endregion
    }
}
