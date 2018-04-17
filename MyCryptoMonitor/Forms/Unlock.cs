using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class Unlock : Form
    {
        #region Public Variables
        public string PasswordInput { get { return txtPassword.Text; } }
        #endregion

        #region Constructor
        public Unlock()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void btnUnlock_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnForgot_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
        #endregion
    }
}
