using MyCryptoMonitor.Statics;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class ManageEncryption : Form
    {
        #region Public Constructors

        public ManageEncryption()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
                return;

            Cursor.Current = Cursors.WaitCursor;
            btnEncrypt.Enabled = false;
            txtPassword.Enabled = false;

            if (UserConfigService.Encrypted && !EncryptionService.ValidatePassword(txtPassword.Text))
                MessageBox.Show("Incorrect password.");
            else if (UserConfigService.Encrypted)
                EncryptionService.DecryptFiles();
            else
                EncryptionService.EncryptFiles(txtPassword.Text);

            Setup();
        }

        private void Encrypt_Load(object sender, EventArgs e)
        {
            Globals.SetTheme(this);
            Setup();
        }

        private void Setup()
        {
            cbEnableEncryption.Checked = UserConfigService.Encrypted;
            btnEncrypt.Text = UserConfigService.Encrypted ? "Decrypt" : "Encrypt";
            cbEnableEncryption.ForeColor = UserConfigService.Encrypted ? Color.Green : Color.Crimson;
            btnEncrypt.Enabled = true;
            txtPassword.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        #endregion Private Methods
    }
}