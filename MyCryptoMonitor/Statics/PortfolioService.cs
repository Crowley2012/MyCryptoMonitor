using MyCryptoMonitor.Configs;
using MyCryptoMonitor.DataSources;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyCryptoMonitor.Statics
{
    public static class PortfolioService
    {
        #region Private Fields

        private const string FILEEXTENSION = ".portfolio";

        #endregion Private Fields

        #region Public Properties

        public static string CurrentPortfolio { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static bool Create(string portfolio)
        {
            if (!File.Exists(portfolio))
            {
                Save(portfolio, new List<CoinConfig>());
                return true;
            }

            return false;
        }

        public static void Delete(string portfolio)
        {
            File.Delete(portfolio += FILEEXTENSION);
        }

        public static void DeleteAll()
        {
            foreach (var portfolio in GetPortfolios())
                File.Delete(portfolio.FileName);
        }

        public static List<PortfolioDataSource> GetPortfolios()
        {
            return (from filePath in Directory.GetFiles(Directory.GetCurrentDirectory(), $"*{FILEEXTENSION}")
                    let name = Path.GetFileNameWithoutExtension(filePath)
                    let fileName = Path.GetFileName(filePath)
                    select new PortfolioDataSource
                    {
                        Name = name,
                        FileName = fileName,
                        Startup = UserConfigService.StartupPortfolio.ExtEquals(name)
                    })
                    .OrderBy(p => p.Name)
                    .ToList();
        }

        public static List<CoinConfig> Load(string portfolio)
        {
            CurrentPortfolio = portfolio;
            portfolio += FILEEXTENSION;

            var coinConfigs = File.Exists(portfolio) ? (UserConfigService.Encrypted ? LoadEncrypted(portfolio) : LoadUnencrypted(portfolio)) : new List<CoinConfig>();

            //Get currency from portfolio
            UserConfigService.Currency = coinConfigs.FirstOrDefault()?.Currency ?? UserConfigService.Currency;

            return coinConfigs;
        }

        public static List<CoinConfig> LoadStartup()
        {
            return Load(UserConfigService.StartupPortfolio);
        }

        public static void Rename(string oldPortfolio, string newPortfolio)
        {
            File.Move(oldPortfolio += FILEEXTENSION, newPortfolio += FILEEXTENSION);
        }

        public static void Save(string portfolio, List<CoinConfig> coinConfigs)
        {
            portfolio += FILEEXTENSION;

            if (UserConfigService.Encrypted)
                File.WriteAllText(portfolio, EncryptionService.AesEncryptString(JsonConvert.SerializeObject(coinConfigs)));
            else
                File.WriteAllText(portfolio, JsonConvert.SerializeObject(coinConfigs));
        }

        public static void SetStartup(string portfolio)
        {
            UserConfigService.StartupPortfolio = portfolio;
        }

        public static void ToggleEncryption()
        {
            foreach (var portfolio in GetPortfolios())
                if (UserConfigService.Encrypted)
                    File.WriteAllText(portfolio.FileName, JsonConvert.SerializeObject(LoadEncrypted(portfolio.FileName)));
                else
                    File.WriteAllText(portfolio.FileName, EncryptionService.AesEncryptString(JsonConvert.SerializeObject(LoadUnencrypted(portfolio.FileName))));
        }

        #endregion Public Methods

        #region Private Methods

        private static List<CoinConfig> LoadEncrypted(string portfolio)
        {
            return JsonConvert.DeserializeObject<List<CoinConfig>>(EncryptionService.AesDecryptString(File.ReadAllText(portfolio))) ?? new List<CoinConfig>();
        }

        private static List<CoinConfig> LoadUnencrypted(string portfolio)
        {
            return JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText(portfolio)) ?? new List<CoinConfig>();
        }

        #endregion Private Methods
    }
}