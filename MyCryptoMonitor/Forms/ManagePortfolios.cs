using MyCryptoMonitor.Statics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace MyCryptoMonitor.Forms
{
    public partial class ManagePortfolios : Form
    {
        private List<string> _deletedPortfolios;

        public ManagePortfolios()
        {
            InitializeComponent();
        }

        private void PortfolioManager_Load(object sender, EventArgs e)
        {
            _deletedPortfolios = new List<string>();
            bsPortfolios.DataSource = PortfolioService.Portfolios.OrderBy(p => p.Name).ToList();
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
                if (!PortfolioService.NewPortfolio(newValue))
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
                    PortfolioService.RenamePortfolio(oldValue.ToString(), newValue);

                    if (Convert.ToBoolean(grid[1, e.RowIndex].Value))
                        PortfolioService.SetStartupPortfolio(newValue);
                }

                //Set startups to false
                if (e.ColumnIndex == 1)
                {
                    for (int i = 0; i < grid.Rows.Count; i++)
                    {
                        if (i == e.RowIndex)
                        {
                            PortfolioService.SetStartupPortfolio(Convert.ToBoolean(newValue) ? grid[0, i].Value.ToString() : string.Empty);
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

        private void grdPortfolios_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            _deletedPortfolios.Add(e.Row.Cells[0].Value.ToString());
        }

        private void PortfolioManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(grdPortfolios.Rows.Count > 0)
                grdPortfolios.CurrentCell = grdPortfolios.Rows[0].Cells[0];

            foreach (var portfolio in _deletedPortfolios)
                PortfolioService.DeletePortfolio(portfolio);
        }
    }
}
