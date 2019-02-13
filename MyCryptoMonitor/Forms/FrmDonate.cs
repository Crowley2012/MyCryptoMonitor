using MyCryptoMonitor.Statics;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class FrmDonate : Form
    {
        #region Public Constructors

        public FrmDonate()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void PopupDonate_Load(object sender, System.EventArgs e)
        {
            Globals.SetTheme(this);
        }

        #endregion Private Methods
    }
}