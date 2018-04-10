using MyCryptoMonitor.Forms;
using System.IO;
using System.Windows.Forms;

namespace MyCryptoMonitor.Statics
{
    public class EncryptionService
    {
        #region Public Variables
        public static string Password { get; set; } = string.Empty;
        #endregion

        #region Methods
        public static bool CheckPassword(string password)
        {
            if (Encryption.AesDecryptString(File.ReadAllText("Encryption"), password).Equals("Success"))
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
            if (!UserConfigService.Encrypted)
                return;

            using (InputPassword form = new InputPassword())
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (!CheckPassword(form.PasswordInput))
                        Unlock();
                }
                else if (result == DialogResult.Abort)
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
            UserConfigService.Encrypted = true;

            CreateEncryptionFile();
            UserConfigService.Save();
            PortfolioService.EncryptPortfolios();
            AlertService.EncryptAlerts();
        }

        public static void DecryptFiles()
        {
            UserConfigService.Encrypted = false;

            RemoveEncryptionFile();
            UserConfigService.Save();
            PortfolioService.DecryptPortfolios();
            AlertService.DecryptAlerts();
        }

        public static void CreateEncryptionFile()
        {
            File.WriteAllText("Encryption", Encryption.AesEncryptString("Success"));
        }

        public static void RemoveEncryptionFile()
        {
            File.Delete("Encryption");
        }

        public static void ResetEncryption()
        {
            PortfolioService.LoadPortfolios();

            foreach (var portfolio in PortfolioService.Portfolios)
                PortfolioService.DeletePortfolio(portfolio.Name);

            if (File.Exists("Alerts"))
                File.Delete("Alerts");

            if (File.Exists("Encryption"))
                File.Delete("Encryption");

            UserConfigService.Delete();

            UserConfigService.Load();
        }
        #endregion
    }
}
