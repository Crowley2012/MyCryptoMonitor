using MyCryptoMonitor.Functions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class Encrypt : Form
    {
        public Encrypt()
        {
            InitializeComponent();
        }

        private void Encrypt_Load(object sender, EventArgs e)
        {
            cbEnableEncryption.Checked = Management.UserConfig.Encryption;
            btnEncrypt.Text = Management.UserConfig.Encryption ? "Decrypt" : "Encrypt";
            lblInstructions.Text = Management.UserConfig.Encryption ? "Type in your password to disable encryption." : "Type in a password to enable encryption.";
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
                return;

            if (Management.UserConfig.Encryption && Management.CheckPassword(txtPassword.Text))
            {
                Management.UserConfig.Encryption = false;
                cbEnableEncryption.Checked = false;
                btnEncrypt.Text = "Encrypt";
                lblInstructions.Text = "Type in a password to enable encryption.";

                File.Delete("Encryption");
                File.WriteAllText("UserConfig", JsonConvert.SerializeObject(Management.UserConfig));

                DecryptPortfolios();
                DecryptAlerts();
            }
            else if (!Management.UserConfig.Encryption)
            {
                Management.Password = txtPassword.Text;
                Management.UserConfig.Encryption = true;
                cbEnableEncryption.Checked = true;
                btnEncrypt.Text = "Decrypt";
                lblInstructions.Text = "Type in your password to disable encryption.";

                File.WriteAllText("Encryption", AESEncrypt.AesEncryptString("Success"));
                File.WriteAllText("UserConfig", JsonConvert.SerializeObject(Management.UserConfig));

                EncryptPortfolios();
                EncryptAlerts();
            }
        }

        private void DecryptPortfolios()
        {
            if (File.Exists("Portfolio1"))
                File.WriteAllText("Portfolio1", JsonConvert.SerializeObject(Management.LoadPortfolioEncrypted("Portfolio1")));

            if (File.Exists("Portfolio2"))
                File.WriteAllText("Portfolio2", JsonConvert.SerializeObject(Management.LoadPortfolioEncrypted("Portfolio2")));

            if (File.Exists("Portfolio3"))
                File.WriteAllText("Portfolio3", JsonConvert.SerializeObject(Management.LoadPortfolioEncrypted("Portfolio3")));
        }

        private void DecryptAlerts()
        {
            if (File.Exists("Alerts"))
                File.WriteAllText("Alerts", JsonConvert.SerializeObject(Management.LoadAlertsEncrypted()));
        }

        private void EncryptPortfolios()
        {
            if (File.Exists("Portfolio1"))
                File.WriteAllText("Portfolio1", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(Management.LoadPortfolioUnencrypted("Portfolio1"))));

            if (File.Exists("Portfolio2"))
                File.WriteAllText("Portfolio2", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(Management.LoadPortfolioUnencrypted("Portfolio2"))));

            if (File.Exists("Portfolio3"))
                File.WriteAllText("Portfolio3", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(Management.LoadPortfolioUnencrypted("Portfolio3"))));
        }

        private void EncryptAlerts()
        {
            if (File.Exists("Alerts"))
                File.WriteAllText("Alerts", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(Management.LoadAlertsUnencrypted())));
        }
    }
}
