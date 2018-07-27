using MyCryptoMonitor.DataSources;
using MyCryptoMonitor.Objects;
using MyCryptoMonitor.Statics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class ManageAlerts : Form
    {
        #region Private Variables
        private List<Coin> _coins;
        private List<AlertDataSource> _otherAlerts;
        private AlertService.Operators _operator => (AlertService.Operators)cmbOperators.SelectedValue;
        #endregion

        #region Constructor
        public ManageAlerts(List<Coin> coins)
        {
            InitializeComponent();

            _coins = coins;
        }
        #endregion

        #region Methods
        private void SaveAlerts()
        {
            if (grdAlerts.Rows.Count > 0)
                grdAlerts.CurrentCell = grdAlerts.Rows[0].Cells[0];

            UserConfigService.DeleteAlerts = cbDeleteAlerts.Checked;
            AlertService.SendAddress = txtSendAddress.Text;
            AlertService.SendPassword = txtSendPassword.Text;
            AlertService.ReceiveAddress = txtReceiveAddress.Text;
            AlertService.ReceiveType = cmbReceiveType.Text;
            AlertService.Alerts = bsAlerts.DataSource as List<AlertDataSource>;
            AlertService.Alerts.AddRange(_otherAlerts);
            AlertService.Save();
        }

        private bool CheckValidAlert(decimal currentPrice, decimal checkPrice)
        {
            if (_operator == AlertService.Operators.GreaterThan && currentPrice > checkPrice)
            {
                MessageBox.Show("Current price is already greater than check price");
                return false;
            }
            else if(_operator == AlertService.Operators.LessThan && currentPrice < checkPrice)
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
            AlertService.Load();

            cmbCoins.DataSource = _coins.OrderBy(c =>c.ShortName).Select(c => c.ShortName).ToList();

            txtPrice.Text = txtCurrent.Text = _coins.Where(c => c.ShortName.ExtEquals(cmbCoins.Text)).Select(c => c.Price).FirstOrDefault().ToString();

            cmbOperators.DataSource = Enum.GetValues(typeof(AlertService.Operators))
                .Cast<Enum>()
                .Select(value => new { (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description, value })
                .OrderBy(d => d.Description)
                .ToList();

            cmbReceiveType.DataSource = Enum.GetValues(typeof(AlertService.Types))
                .Cast<Enum>()
                .Select(value => new { (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description, value })
                .OrderBy(d => d.Description)
                .ToList();

            //Setup inputs
            cbDeleteAlerts.Checked = UserConfigService.DeleteAlerts;
            txtSendAddress.Text = AlertService.SendAddress;
            txtSendPassword.Text = AlertService.SendPassword;
            txtReceiveAddress.Text = AlertService.ReceiveAddress;
            cmbReceiveType.Text = AlertService.ReceiveType;

            //Set the current price for alerts
            foreach (AlertDataSource alert in AlertService.Alerts)
            {
                if (_coins.Any(c => c.ShortName.ExtEquals(alert.Coin)))
                    alert.Current = _coins.Where(c => c.ShortName.ExtEquals(alert.Coin)).Select(c => c.Price).First();
            }

            //Store other currency alerts
            _otherAlerts = AlertService.Alerts.Where(a => !a.Currency.ExtEquals(UserConfigService.Currency) || !_coins.Any(c => c.ShortName.ExtEquals(a.Coin))).OrderBy(a => a.Coin).ThenByDescending(a => a.Price).ToList();

            //Show current currency alerts
            bsAlerts.DataSource = AlertService.Alerts.Where(a => a.Currency.ExtEquals(UserConfigService.Currency) && _coins.Any(c => c.ShortName.ExtEquals(a.Coin))).OrderBy(a => a.Coin).ThenByDescending(a => a.Price).ToList();

            if (!UserConfigService.Encrypted)
            {
                grpEmail.Enabled = false;
                grpContact.Enabled = false;
            }

            Globals.SetTheme(this);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Decimal.TryParse(txtPrice.Text, out decimal value) || string.IsNullOrEmpty(cmbCoins.Text))
            {
                MessageBox.Show("Coin not selected or price is not a valid number.");
                return;
            }
            
            if (!CheckValidAlert(txtCurrent.Text.ConvertToDecimal(), txtPrice.Text.ConvertToDecimal()))
                return;

            bsAlerts.Add(new AlertDataSource { Coin = cmbCoins.Text, Current = txtCurrent.Text.ConvertToDecimal(), Operator = _operator, Price = txtPrice.Text.ConvertToDecimal(), Currency = UserConfigService.Currency });
            bsAlerts.DataSource = ((List<AlertDataSource>)bsAlerts.DataSource).OrderBy(a => a.Coin).ThenByDescending(a => a.Price).ToList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdAlerts.SelectedCells.Count > 0)
                bsAlerts.Remove((AlertDataSource)grdAlerts.SelectedCells[0].OwningRow.DataBoundItem);
        }

        private void cmbCoins_Validated(object sender, EventArgs e)
        {
            //Set the coin price
            var currentPrice = _coins.Where(c => c.ShortName.ExtEquals(cmbCoins.Text)).Select(c => c.Price).FirstOrDefault().ToString();
            txtPrice.Text = currentPrice;
            txtCurrent.Text = currentPrice;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            SaveAlerts();
        }

        private void Alerts_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveAlerts();
        }
        #endregion
    }
}
