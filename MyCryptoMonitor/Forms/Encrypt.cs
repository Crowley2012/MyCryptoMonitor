using MyCryptoMonitor.Functions;
using System;
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
                Management.DecryptFiles();

                cbEnableEncryption.Checked = false;
                btnEncrypt.Text = "Encrypt";
                lblInstructions.Text = "Type in a password to enable encryption.";
            }
            else if (!Management.UserConfig.Encryption)
            {
                Management.EncryptFiles(txtPassword.Text);

                cbEnableEncryption.Checked = true;
                btnEncrypt.Text = "Decrypt";
                lblInstructions.Text = "Type in your password to disable encryption.";
            }
        }
    }
}
