using MyCryptoMonitor.Statics;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class Unlock : Form
    {
        #region Public Constructors

        public Unlock()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Properties

        public string PasswordInput { get { return txtPassword.Text; } }

        #endregion Public Properties

        #region Private Methods

        private void btnForgot_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void btnUnlock_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Unlock_Load(object sender, System.EventArgs e)
        {
            Globals.SetTheme(this);
        }

        #endregion Private Methods
    }
}