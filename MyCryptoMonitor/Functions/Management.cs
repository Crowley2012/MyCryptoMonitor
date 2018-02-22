using MyCryptoMonitor.DataSources;
using MyCryptoMonitor.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MyCryptoMonitor.Functions
{
    public class Management
    {
        #region Public Variables
        public static UserConfig UserConfig { get; set; }
        public static string SelectedPortfolio { get; set; }
        public static string Password { get; set; } = string.Empty;
        #endregion

        #region User Config
        public static void LoadUserConfig()
        {
            if (File.Exists("UserConfig"))
            {
                UserConfig = JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText("UserConfig"));
            }
            else
            {
                UserConfig = new UserConfig();
                File.WriteAllText("UserConfig", JsonConvert.SerializeObject(UserConfig));
            }
        }
        #endregion

        #region Encryption Management
        public static bool CheckPassword(string password)
        {
            if (AESEncrypt.AesDecryptString(File.ReadAllText("Encryption"), password).Equals("Success"))
            {
                Password = password;
                return true;
            }
            else
            {
                MessageBox.Show("Wrong password.");
                return false;
            }
        }

        public static void Unlock()
        {
            if (!UserConfig.Encryption)
                return;

            using (Password form = new Password())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (!CheckPassword(form.PasswordInput))
                        Unlock();
                }
                else
                {
                    Application.Exit();
                }
            }
        }
        #endregion

        #region Portfolio Management
        public static List<CoinConfig> LoadFirstPortfolio()
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
            SelectedPortfolio = portfolio;

            if (UserConfig.Encryption)
                return LoadPortfolioEncrypted(portfolio);
            else
                return LoadPortfolioUnencrypted(portfolio);
        }

        public static List<CoinConfig> LoadPortfolioEncrypted(string portfolio)
        {
            return JsonConvert.DeserializeObject<List<CoinConfig>>(AESEncrypt.AesDecryptString(File.ReadAllText(portfolio)));
        }

        public static List<CoinConfig> LoadPortfolioUnencrypted(string portfolio)
        {
            return JsonConvert.DeserializeObject<List<CoinConfig>>(File.ReadAllText(portfolio));
        }

        public static void SavePortfolio(string portfolio, List<CoinConfig> coinConfigs)
        {
            if (UserConfig.Encryption)
                SavePortfolioEncrypted(portfolio, coinConfigs);
            else
                SavePortfolioUnencrypted(portfolio, coinConfigs);
        }

        public static void SavePortfolioEncrypted(string portfolio, List<CoinConfig> coinConfigs)
        {
            File.WriteAllText(portfolio, AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(coinConfigs)));
        }

        public static void SavePortfolioUnencrypted(string portfolio, List<CoinConfig> coinConfigs)
        {
            File.WriteAllText(portfolio, JsonConvert.SerializeObject(coinConfigs));
        }
        #endregion

        #region Alert Management
        public static AlertConfig LoadAlerts()
        {
            if (UserConfig.Encryption)
                return LoadAlertsEncrypted();
            else
                return LoadAlertsUnencrypted();
        }

        public static AlertConfig LoadAlertsEncrypted()
        {
            return JsonConvert.DeserializeObject<AlertConfig>(AESEncrypt.AesDecryptString(File.ReadAllText("Alerts")));
        }

        public static AlertConfig LoadAlertsUnencrypted()
        {
            return JsonConvert.DeserializeObject<AlertConfig>(File.ReadAllText("Alerts"));
        }

        public static void SaveAlerts(AlertConfig alertConfig)
        {
            if (UserConfig.Encryption)
                SaveAlertsEncrypted(alertConfig);
            else
                SaveAlertsUnencrypted(alertConfig);
        }

        public static void SaveAlertsEncrypted(AlertConfig alertConfig)
        {
            File.WriteAllText("Alerts", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(alertConfig)));
        }

        public static void SaveAlertsUnencrypted(AlertConfig alertConfig)
        {
            File.WriteAllText("Alerts", JsonConvert.SerializeObject(alertConfig));
        }
        #endregion
    }
}
