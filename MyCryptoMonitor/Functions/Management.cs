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

        #region User Config Management
        public static void LoadUserConfig()
        {
            if (File.Exists("UserConfig"))
                UserConfig = JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText("UserConfig"));
            else
                SaveUserConfig();
        }

        public static void SaveUserConfig()
        {
            if(UserConfig != null)
                File.WriteAllText("UserConfig", JsonConvert.SerializeObject(UserConfig));
            else
                File.WriteAllText("UserConfig", JsonConvert.SerializeObject(new UserConfig()));
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
                MessageBox.Show("Incorrect password.");
                return false;
            }
        }

        public static void Unlock()
        {
            if (!UserConfig.Encryption)
                return;

            using (Password form = new Password())
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (!CheckPassword(form.PasswordInput))
                        Unlock();
                }
                else if(result == DialogResult.Abort)
                {
                    if (MessageBox.Show($"This will delete all saved files (portfolios, alerts, etc) and remove encryption. Do you want to continue?", "Forgot Password", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        ResetEncryption();
                    else
                        Unlock();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        public static void EncryptFiles(string password)
        {
            Password = password;
            UserConfig.Encryption = true;

            CreateEncryptionFile();
            SaveUserConfig();
            EncryptPortfolios();
            EncryptAlerts();
        }

        public static void DecryptFiles()
        {
            UserConfig.Encryption = false;

            RemoveEncryptionFile();
            SaveUserConfig();
            DecryptPortfolios();
            DecryptAlerts();
        }

        public static void CreateEncryptionFile()
        {
            File.WriteAllText("Encryption", AESEncrypt.AesEncryptString("Success"));
        }

        public static void RemoveEncryptionFile()
        {
            File.Delete("Encryption");
        }

        public static void ResetEncryption()
        {
            if (File.Exists("Portfolio1"))
                File.Delete("Portfolio1");

            if (File.Exists("Portfolio2"))
                File.Delete("Portfolio2");

            if (File.Exists("Portfolio3"))
                File.Delete("Portfolio3");

            if (File.Exists("Alerts"))
                File.Delete("Alerts");

            if (File.Exists("Encryption"))
                File.Delete("Encryption");

            UserConfig.Encryption = false;
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

        public static void EncryptPortfolios()
        {
            if (File.Exists("Portfolio1"))
                File.WriteAllText("Portfolio1", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(LoadPortfolioUnencrypted("Portfolio1"))));

            if (File.Exists("Portfolio2"))
                File.WriteAllText("Portfolio2", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(LoadPortfolioUnencrypted("Portfolio2"))));

            if (File.Exists("Portfolio3"))
                File.WriteAllText("Portfolio3", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(LoadPortfolioUnencrypted("Portfolio3"))));
        }

        public static void DecryptPortfolios()
        {
            if (File.Exists("Portfolio1"))
                File.WriteAllText("Portfolio1", JsonConvert.SerializeObject(LoadPortfolioEncrypted("Portfolio1")));

            if (File.Exists("Portfolio2"))
                File.WriteAllText("Portfolio2", JsonConvert.SerializeObject(LoadPortfolioEncrypted("Portfolio2")));

            if (File.Exists("Portfolio3"))
                File.WriteAllText("Portfolio3", JsonConvert.SerializeObject(LoadPortfolioEncrypted("Portfolio3")));
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
            AlertConfig alertConfig = JsonConvert.DeserializeObject<AlertConfig>(File.ReadAllText("Alerts"));
            alertConfig.EmailAddress = string.Empty;
            alertConfig.Password = string.Empty;

            return alertConfig;
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
            alertConfig.EmailAddress = string.Empty;
            alertConfig.Password = string.Empty;
            File.WriteAllText("Alerts", JsonConvert.SerializeObject(alertConfig));
        }

        public static void EncryptAlerts()
        {
            if (File.Exists("Alerts"))
                File.WriteAllText("Alerts", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(LoadAlertsUnencrypted())));
        }

        public static void DecryptAlerts()
        {
            if (File.Exists("Alerts"))
                File.WriteAllText("Alerts", JsonConvert.SerializeObject(LoadAlertsEncrypted()));
        }
        #endregion
    }
}
