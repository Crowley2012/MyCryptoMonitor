using MyCryptoMonitor.Statics;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class PopupDonate : Form
    {
        #region Public Constructors

        public PopupDonate()
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