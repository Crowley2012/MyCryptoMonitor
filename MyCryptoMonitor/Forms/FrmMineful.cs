using MyCryptoMonitor.Statics;
using MyCryptoMonitor.ViewModels;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class FrmMineful : Form
    {
        #region Private Fields

        private FrmMinefulViewModel _viewModel;

        #endregion Private Fields

        #region Public Constructors

        public FrmMineful()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void MinefulForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _viewModel.Save();
        }

        private void MinefulForm_Load(object sender, System.EventArgs e)
        {
            _viewModel = new FrmMinefulViewModel();
            bsViewModel.DataSource = _viewModel;
            Globals.SetTheme(this);
        }

        #endregion Private Methods
    }
}