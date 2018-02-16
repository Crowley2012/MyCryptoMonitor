using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using MyCryptoMonitor.DataSources;

namespace MyCryptoMonitor.Forms
{
    public partial class Alerts : Form
    {
        public enum Types {[Description("Email")] Email, [Description("Version")] Version, [Description("AT&T")] ATT, [Description("Sprint")] Sprint, [Description("Boost")] Boost, [Description("Mobile")] Mobile }
        public enum Operators {[Description("Greater Than")] GreaterThan, [Description("Less Than")] LessThan }

        private List<string> _coins;
        private List<AlertDataSource> _alerts;

        #region Constructor
        public Alerts(List<string> coins)
        {
            InitializeComponent();

            _coins = coins;
        }
        #endregion

        #region Events
        private void Alerts_Load(object sender, EventArgs e)
        {
            cmbContactType.DataSource = Enum.GetValues(typeof(Types))
                .Cast<Enum>()
                .Select(value => new { (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description, value })
                .ToList();

            cmbOperator.DataSource = Enum.GetValues(typeof(Operators))
                .Cast<Enum>()
                .Select(value => new { (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description, value })
                .ToList();

            cmbCoins.DataSource = _coins;

            _alerts = new List<AlertDataSource>();
            bsAlerts.DataSource = _alerts;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            var contactAddress = txtContactAddress.Text;
            var contextType = cmbContactType.ValueMember;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Decimal.TryParse(txtPrice.Text, out decimal value))
            {
                MessageBox.Show("Price is not a valid number.");
                return;
            }

            _alerts.Add(new AlertDataSource { Coin = cmbCoins.Text, Operator = cmbOperator.Text, Price = txtPrice.Text });
            bsAlerts.ResetBindings(false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdAlerts.SelectedRows.Count > 0)
                _alerts.Remove((AlertDataSource)grdAlerts.SelectedRows[0].DataBoundItem);
            else if (grdAlerts.SelectedCells.Count > 0)
                _alerts.Remove((AlertDataSource)grdAlerts.SelectedCells[0].OwningRow.DataBoundItem);

            bsAlerts.ResetBindings(false);
        }

        private void grdAlerts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion
    }
}
