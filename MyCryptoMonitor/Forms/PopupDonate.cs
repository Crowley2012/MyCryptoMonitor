using MyCryptoMonitor.Statics;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class PopupDonate : Form
    {
        #region Constructor
        public PopupDonate()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void PopupDonate_Load(object sender, System.EventArgs e)
        {
            Globals.SetTheme(this);
        }
        #endregion
    }
}
