using MyCryptoMonitor.Functions;
using System;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class PortfolioManager : Form
    {
        public PortfolioManager()
        {
            InitializeComponent();
        }

        private void PortfolioManager_Load(object sender, EventArgs e)
        {
            bsPortfolios.DataSource = Management.Portfolios;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void grdPortfolios_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var test = (DataGridView)sender;
            var name = test.SelectedCells[0].Value;

            if (string.IsNullOrEmpty(name.ToString()))
            {
                return;
            }
        }
    }
}
