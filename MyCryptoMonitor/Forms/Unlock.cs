using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class Unlock : Form
    {
        public string PasswordInput { get { return txtPassword.Text; } }

        public Unlock()
        {
            InitializeComponent();
        }

        private void btnUnlock_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnForgot_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
    }
}
