using MyCryptoMonitor.DataSources;
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

            if(Globals.UserConfig.Encryption)
                cbEnableEncryption.Checked = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (cbEnableEncryption.Checked && AESEncrypt.AesDecryptString(File.ReadAllText("Encryption"), txtPassword.Text).Equals("Success"))
            {
                cbEnableEncryption.Enabled = true;
            }
            else if (cbEnableEncryption.Checked && !AESEncrypt.AesDecryptString(File.ReadAllText("Encryption"), txtPassword.Text).Equals("Success"))
            {
                cbEnableEncryption.Enabled = false;
            }
            else if(!string.IsNullOrEmpty(txtPassword.Text))
            {
                cbEnableEncryption.Enabled = true;
            }
            else
            {
                cbEnableEncryption.Enabled = false;
            }
        }

        private void cbEnableEncryption_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnableEncryption.Checked)
            {
                if (Globals.UserConfig.Encryption)
                    return;

                Globals.UserConfig.Encryption = true;

                File.WriteAllText("Encryption", AESEncrypt.AESEncryptString("Success", txtPassword.Text));
                File.WriteAllText("UserConfig", JsonConvert.SerializeObject(Globals.UserConfig));

                AESEncrypt.Password = txtPassword.Text;

                EncryptPortfolios();
            }
            else
            {
                Globals.UserConfig.Encryption = false;
                File.Delete("Encryption");
                File.WriteAllText("UserConfig", JsonConvert.SerializeObject(Globals.UserConfig));

                AESEncrypt.Password = string.Empty;

                DecryptPortfolios();
            }
        }

        private void EncryptPortfolios()
        {
            if (File.Exists("portfolio1"))
                File.WriteAllText("portfolio1", AESEncrypt.AESEncryptString(JsonConvert.SerializeObject(Manage.LoadPortfolioUnencrypted("portfolio1")), txtPassword.Text));

            if (File.Exists("portfolio2"))
                File.WriteAllText("portfolio2", AESEncrypt.AESEncryptString(JsonConvert.SerializeObject(Manage.LoadPortfolioUnencrypted("portfolio2")), txtPassword.Text));

            if (File.Exists("portfolio3"))
                File.WriteAllText("portfolio3", AESEncrypt.AESEncryptString(JsonConvert.SerializeObject(Manage.LoadPortfolioUnencrypted("portfolio3")), txtPassword.Text));
        }

        private void DecryptPortfolios()
        {
            if (File.Exists("portfolio1"))
                File.WriteAllText("portfolio1", JsonConvert.SerializeObject(Manage.LoadPortfolioEncrypted("portfolio1")));

            if (File.Exists("portfolio2"))
                File.WriteAllText("portfolio2", JsonConvert.SerializeObject(Manage.LoadPortfolioEncrypted("portfolio2")));

            if (File.Exists("portfolio3"))
                File.WriteAllText("portfolio3", JsonConvert.SerializeObject(Manage.LoadPortfolioEncrypted("portfolio3")));
        }
    }
}
