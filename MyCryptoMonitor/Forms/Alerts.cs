using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using MyCryptoMonitor.DataSources;
using System.IO;
using Newtonsoft.Json;

namespace MyCryptoMonitor.Forms
{
    public partial class Alerts : Form
    {
        public enum Types {[Description("Email")] Email, [Description("Version")] Version, [Description("AT&T")] ATT, [Description("Sprint")] Sprint, [Description("Boost")] Boost, [Description("Mobile")] Mobile }
        public enum Operators {[Description("Greater Than")] GreaterThan, [Description("Less Than")] LessThan }

        #region Private Variables
        private List<CoinData> _coins;
        private List<AlertDataSource> _alerts;
        private const string AlertsConfigFile = "Alerts";
        private decimal _oldCheckPrice;
        #endregion

        #region Constructor
        public Alerts(List<CoinData> coins)
        {
            InitializeComponent();

            _coins = coins;
        }
        #endregion

        #region Methods
        private void LoadAlerts()
        {
            if (!File.Exists(AlertsConfigFile))
                return;

            try
            {
                //Deserialize the config
                AlertConfig alertConfig = JsonConvert.DeserializeObject<AlertConfig>(File.ReadAllText(AlertsConfigFile) ?? string.Empty);
                txtContactAddress.Text = alertConfig.ContactAddress;
                cmbContactType.Text = alertConfig.ContactType;

                //Get the current price of coin
                foreach(AlertDataSource alert in alertConfig.Alerts)
                    alert.Current = _coins.Where(c => c.ShortName.Equals(alert.Coin)).Select(c => c.Price).First();

                bsAlerts.DataSource = alertConfig.Alerts;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load alerts, config reset.");
                File.Delete(AlertsConfigFile);
            }
        }

        private void SaveAlerts()
        {
            AlertConfig alertConfig = new AlertConfig
            {
                ContactAddress = txtContactAddress.Text,
                ContactType = cmbContactType.Text,
                Alerts = bsAlerts.DataSource as List<AlertDataSource>
            };

            //Save config
            File.WriteAllText(AlertsConfigFile, JsonConvert.SerializeObject(alertConfig));
        }

        private bool CheckValid(decimal currentPrice, decimal checkPrice, Operators op)
        {
            if (op == Operators.GreaterThan && currentPrice > checkPrice)
            {
                MessageBox.Show("Current price is already greater than check price");
                return false;
            }
            else if(op == Operators.LessThan && currentPrice < checkPrice)
            {
                MessageBox.Show("Current price is already less than check price");
                return false;
            }

            return true;
        }
        #endregion

        #region Events
        private void Alerts_Load(object sender, EventArgs e)
        {
            //Set contact types
            cmbContactType.DataSource = Enum.GetValues(typeof(Types))
                .Cast<Enum>()
                .Select(value => new { (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description, value })
                .ToList();

            //Set operators
            cmbOperator.DataSource = Enum.GetValues(typeof(Operators))
                .Cast<Enum>()
                .Select(value => new { (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description, value })
                .ToList();

            //Set coins
            cmbCoins.DataSource = _coins.Select(c => c.ShortName).ToList();

            //Set alerts
            _alerts = new List<AlertDataSource>();
            bsAlerts.DataSource = _alerts;

            //Set coin price
            txtCurrent.Text = _coins.Where(c => c.ShortName.Equals(cmbCoins.Text.ToUpper())).Select(c => c.Price).FirstOrDefault().ToString();

            LoadAlerts();
        }

        private void Alerts_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveAlerts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Decimal.TryParse(txtPrice.Text, out decimal value) || string.IsNullOrEmpty(cmbCoins.Text))
            {
                MessageBox.Show("Coin not selected or price is not a valid number.");
                return;
            }

            //Check if check value is valid
            Enum.TryParse(cmbOperator.SelectedValue.ToString(), out Operators op);
            if (!CheckValid(Convert.ToDecimal(txtCurrent.Text), Convert.ToDecimal(txtPrice.Text), op))
                return;

            bsAlerts.Add(new AlertDataSource { Coin = cmbCoins.Text, Current = Convert.ToDecimal(txtCurrent.Text), Operator = cmbOperator.Text, Price = Convert.ToDecimal(txtPrice.Text) });
        }

        private void grdAlerts_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _oldCheckPrice = Convert.ToDecimal(grdAlerts[e.ColumnIndex, e.RowIndex].Value);
        }

        private void grdAlerts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (grdAlerts.SelectedCells.Count <= 0 || grdAlerts.SelectedCells[0].Value != null || Decimal.TryParse(txtPrice.Text, out decimal value))
            {
                //Check if check value is valid
                Enum.TryParse(cmbOperator.SelectedValue.ToString(), out Operators op);
                if (!CheckValid(Convert.ToDecimal(grdAlerts.SelectedCells[0].OwningRow.Cells[1].Value), Convert.ToDecimal(grdAlerts.SelectedCells[0].OwningRow.Cells[3].Value), op))
                    grdAlerts.SelectedCells[0].OwningRow.Cells[3].Value = _oldCheckPrice;

                return;
            }

            bsAlerts.Remove((AlertDataSource)grdAlerts.SelectedCells[0].OwningRow.DataBoundItem);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdAlerts.SelectedCells.Count > 0)
                bsAlerts.Remove((AlertDataSource)grdAlerts.SelectedCells[0].OwningRow.DataBoundItem);
        }

        private void cmbCoins_Validated(object sender, EventArgs e)
        {
            //Set coin input to capital
            cmbCoins.Text = cmbCoins.Text.ToUpper();

            //Check if exists
            if (!_coins.Any(c => c.ShortName.Equals(cmbCoins.Text)))
            {
                cmbCoins.Text = string.Empty;
                return;
            }

            //Set the coin price
            txtCurrent.Text = _coins.Where(c => c.ShortName.Equals(cmbCoins.Text)).Select(c => c.Price).FirstOrDefault().ToString();
        }
        #endregion
    }
}
