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
            }
        }

        private void EncryptPortfolios()
        {
            if (File.Exists("portfolio1"))
                File.WriteAllText("portfolio1", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(Management.LoadPortfolioUnencrypted("portfolio1"))));

            if (File.Exists("portfolio2"))
                File.WriteAllText("portfolio2", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(Management.LoadPortfolioUnencrypted("portfolio2"))));

            if (File.Exists("portfolio3"))
                File.WriteAllText("portfolio3", AESEncrypt.AesEncryptString(JsonConvert.SerializeObject(Management.LoadPortfolioUnencrypted("portfolio3"))));
        }

        private void DecryptPortfolios()
        {
            if (File.Exists("portfolio1"))
                File.WriteAllText("portfolio1", JsonConvert.SerializeObject(Management.LoadPortfolioEncrypted("portfolio1")));

            if (File.Exists("portfolio2"))
                File.WriteAllText("portfolio2", JsonConvert.SerializeObject(Management.LoadPortfolioEncrypted("portfolio2")));

            if (File.Exists("portfolio3"))
                File.WriteAllText("portfolio3", JsonConvert.SerializeObject(Management.LoadPortfolioEncrypted("portfolio3")));
        }
    }
}
