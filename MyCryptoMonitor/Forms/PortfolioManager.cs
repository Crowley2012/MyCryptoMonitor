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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdPortfolios_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var grid = (DataGridView)sender;
            var oldValue = grid[e.ColumnIndex, e.RowIndex].Value;
            var newValue = e.FormattedValue.ToString();

            //No change
            if(oldValue != null && oldValue.ToString().Equals(newValue))
            {

            }

            //New row
            else if(oldValue == null && !string.IsNullOrEmpty(newValue))
            {

            }

            //Updating row
            else if (oldValue != null && !string.IsNullOrEmpty(newValue) && !oldValue.ToString().Equals(newValue))
            {
                //Name is empty
                if (grid[0, e.RowIndex].Value == null || string.IsNullOrEmpty(grid[0, e.RowIndex].Value.ToString()))
                {
                    grid.CancelEdit();
                    return;
                }

                //Set startups to false
                if (e.ColumnIndex == 1)
                {
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        if (i == e.RowIndex)
                        {
                            Management.UserConfig.StartupPortfolio = Convert.ToBoolean(grid[1, i].Value) ? grid[0, i].Value.ToString() : string.Empty;
                            Management.SaveUserConfig();
                            continue;
                        }

                        grid[1, i].Value = false;
                    }
                }
            }

            //Cancelled new row
            else if (oldValue == null && string.IsNullOrEmpty(newValue))
            {
                grid.CancelEdit();
            }

            //Existing row empty name
            else if(oldValue != null && string.IsNullOrEmpty(newValue))
            {
                grid[0, e.RowIndex].Value = oldValue;
            }
        }
    }
}
