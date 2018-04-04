using MyCryptoMonitor.Functions;
using System;
using System.IO;
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

        private void grdPortfolios_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grdPortfolios_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            //Name is empty
            if (grid[0, e.RowIndex].Value == null)
            {
                grid.Rows.RemoveAt(e.RowIndex);
                return;
            }
        }

        private void grdPortfolios_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var grid = (DataGridView)sender;
            var oldValue = grid[e.ColumnIndex, e.RowIndex].Value;
            var newValue = e.FormattedValue.ToString();

            //No change
            if(oldValue != null && oldValue.ToString().Equals(newValue))
            {
                return;
            }

            //New row
            else if(oldValue == null && !string.IsNullOrEmpty(newValue))
            {

            }

            //Updating row
            else if (oldValue != null && !string.IsNullOrEmpty(newValue))
            {
                //Name is empty
                if (grid[0, e.RowIndex].Value == null)
                {
                    //e.Cancel = true;
                    //grid.Rows.RemoveAt(e.RowIndex);
                    return;
                }
            }

            //Cancelled new row
            else if (oldValue == null && string.IsNullOrEmpty(newValue))
            {
                return;
            }

            /*
            if (oldValue != null && oldValue.Equals(newValue))
                return;

            if (newValue == null)
            {
                grid.Rows.RemoveAt(e.RowIndex);
                return;
            }

            if (e.ColumnIndex == 1)
            {
                for (int i = 0; i < grid.Rows.Count; i++)
                {
                    if (i == e.RowIndex)
                    {
                        Management.UserConfig.StartupPortfolio = Convert.ToBoolean(grid.Rows[i].Cells[1].Value) ? grid.Rows[i].Cells[0].Value.ToString() : string.Empty;
                        Management.SaveUserConfig();
                        continue;
                    }

                    grid.Rows[i].Cells[1].Value = false;
                }
            }

            var name = grid.Rows[e.RowIndex].Cells[0].Value.ToString();
            var file = $"{name}.portfolio";

            if (!File.Exists(file))
                File.Create(file).Dispose();
                */
        }

        private void grdPortfolios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            //Name is empty
            if (grid[0, e.RowIndex].Value == null)
            {
                //grid.Rows.RemoveAt(e.RowIndex);
                return;
            }
        }
    }
}
