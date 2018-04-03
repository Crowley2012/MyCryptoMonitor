using MyCryptoMonitor.DataSources;
using MyCryptoMonitor.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCryptoMonitor.Functions
{
    public class Management
    {
        public enum Types {[Description("Email")] Email, [Description("Verizon")] Verizon, [Description("AT&T")] ATT, [Description("Sprint")] Sprint, [Description("Boost Mobile")] Boost, [Description("T-Mobile")] TMobile, [Description("US Cellular")] USCellular, [Description("Virgin Mobile")] VirginMobile }
        public enum Operators {[Description("Greater Than")] GreaterThan, [Description("Less Than")] LessThan }

        #region Public Variables
        public static UserConfig UserConfig { get; set; }
        public static AlertConfig AlertConfig { get; set; }
        public static string SelectedPortfolio { get; set; }
        public static string Password { get; set; } = string.Empty;
        public static List<PortfolioSource> Portfolios { get; set; }
        #endregion

        #region User Config Management
        public static void LoadUserConfig()
        {
            if (File.Exists("UserConfig"))
                UserConfig = JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText("UserConfig"));
            else
            {
                UserConfig = new UserConfig();
                SaveUserConfig();
            }
        }

        public static void SaveUserConfig()
        {
            File.WriteAllText("UserConfig", JsonConvert.SerializeObject(UserConfig));
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

            if (File.Exists("UserConfig"))
                File.Delete("UserConfig");

            LoadUserConfig();
        }
        #endregion

        #region Portfolio Management
        public static void LoadPortfolios()
        {
            Portfolios = new List<PortfolioSource>();
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.portfolio");

            UserConfig.StartupPortfolio = "Portfolio1";

            foreach (string file in files)
            {
                var name = Path.GetFileName(file).Replace(".portfolio", string.Empty);
                Portfolios.Add(new PortfolioSource { Name = name, Startup = UserConfig.StartupPortfolio.Equals(name) });
            }
        }

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
            alertConfig.SendAddress = string.Empty;
            alertConfig.SendPassword = string.Empty;

            return alertConfig;
        }

        public static void SaveAlerts(AlertConfig alertConfig)
        {
            //Update the cached alerts
            AlertConfig = alertConfig;

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
            alertConfig.SendAddress = string.Empty;
            alertConfig.SendPassword = string.Empty;
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

        public static void RemoveAlerts(List<AlertDataSource> alerts)
        {
            AlertConfig alertConfig = LoadAlerts();
            
            foreach(AlertDataSource alert in alerts)
                alertConfig.Alerts.RemoveAll(c => c.Coin.Equals(alert.Coin) && c.Operator.Equals(alert.Operator) && c.Price.Equals(alert.Price));

            SaveAlerts(alertConfig);
        }

        public static string GetContactAddress(string address, string type)
        {
            switch ((Types)Enum.Parse(typeof(Types), type))
            {
                case Types.Email:
                    return address;
                case Types.ATT:
                    return $"{address}@txt.att.net";
                case Types.Boost:
                    return $"{address}@myboostmobile.com";
                case Types.Sprint:
                    return $"{address}@messaging.sprintpcs.com";
                case Types.Verizon:
                    return $"{address}@vtext.com";
                case Types.TMobile:
                    return $"{address}@tmomail.net";
                case Types.USCellular:
                    return $"{address}@email.uscc.net";
                case Types.VirginMobile:
                    return $"{address}@vmobl.com";
                default:
                    return string.Empty;
            }
        }

        public static void SendAlert(AlertDataSource alert)
        {
            string alertMessage = $"{alert.Coin} is {alert.Operator} than {alert.Price}";

            Task.Factory.StartNew(() => { MessageBox.Show(alertMessage, "Alert"); });

            Thread sendEmail = new Thread(new ParameterizedThreadStart(sendEmailThread));
            sendEmail.IsBackground = true;
            sendEmail.Start(alertMessage);
        }

        public static void sendEmailThread(object alertMessage)
        {
            try
            {
                if (!string.IsNullOrEmpty(AlertConfig.SendAddress) && !string.IsNullOrEmpty(AlertConfig.SendPassword))
                {
                    var sendAddress = new MailAddress(AlertConfig.SendAddress);
                    var receiveAddress = new MailAddress(GetContactAddress(AlertConfig.ReceiveAddress, AlertConfig.ReceiveType));
                    string fromPassword = AlertConfig.SendPassword;
                    string subject = "My Crypto Monitor Alert";
                    string body = Convert.ToString(alertMessage);

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 25,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sendAddress.Address, fromPassword)
                    };

                    using (var message = new MailMessage(sendAddress, receiveAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ensure that 'allow less secure apps' is enabled for gmail, your email and password are correct, and that port 25 is not blocked on your network.\n\nError Message: {e.Message}", "Error sending external alert");
            }
        }
        #endregion
    }
}
