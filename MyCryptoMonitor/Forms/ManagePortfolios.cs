using MyCryptoMonitor.Statics;
using System;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class ManagePortfolios : Form
    {
        #region Constructor
        public ManagePortfolios()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void PortfolioManager_Load(object sender, EventArgs e)
        {
            bsPortfolios.DataSource = PortfolioService.GetPortfolios();
        }

        private void grdPortfolios_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var grid = (DataGridView)sender;
            var oldValue = grid[e.ColumnIndex, e.RowIndex].Value;
            var newValue = e.FormattedValue.ToString();

            //No change
            if(oldValue != null && oldValue.ToString().Equals(newValue))
                return;

            //New row
            else if(oldValue == null && !string.IsNullOrEmpty(newValue))
            {
                if (!PortfolioService.Create(newValue))
                    grid.CancelEdit();
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

                //Rename portfolio
                if(e.ColumnIndex == 0)
                {
                    PortfolioService.Rename(oldValue.ToString(), newValue);

                    if (Convert.ToBoolean(grid[1, e.RowIndex].Value))
                        PortfolioService.SetStartup(newValue);
                }

                //Set startups to false
                if (e.ColumnIndex == 1)
                {
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        if (i == e.RowIndex)
                        {
                            PortfolioService.SetStartup(Convert.ToBoolean(newValue) ? grid[0, i].Value.ToString() : string.Empty);
                            continue;
                        }

                        grid[1, i].Value = false;
                    }
                }
            }

            //Cancelled new row
            else if (oldValue == null && string.IsNullOrEmpty(newValue))
                grid.CancelEdit();

            //Existing row empty name
            else if(oldValue != null && string.IsNullOrEmpty(newValue))
                grid[0, e.RowIndex].Value = oldValue;
        }

        private void grdPortfolios_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            PortfolioService.Delete(e.Row.Cells[0].Value.ToString());
        }

        private void PortfolioManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(grdPortfolios.Rows.Count > 0)
                grdPortfolios.CurrentCell = grdPortfolios.Rows[0].Cells[0];
        }
        #endregion
    }
}
