using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using MyCryptoMonitor.DataSources;
using MyCryptoMonitor.Statics;
using MyCryptoMonitor.Objects;

namespace MyCryptoMonitor.Forms
{
    public partial class ManageAlerts : Form
    {
        #region Private Variables
        private List<Coin> _coins;
        private List<AlertDataSource> _alerts;
        private List<AlertDataSource> _otherAlerts;
        private decimal _oldCheckPrice;
        #endregion

        #region Constructor
        public ManageAlerts(List<Coin> coins)
        {
            InitializeComponent();

            _coins = coins;
        }
        #endregion

        #region Methods
        private void LoadAlerts()
        {
            _otherAlerts = new List<AlertDataSource>();

            AlertService.Load();
            txtSendAddress.Text = AlertService.SendAddress;
            txtSendPassword.Text = AlertService.SendPassword;
            txtReceiveAddress.Text = AlertService.ReceiveAddress;
            cmbReceiveType.Text = AlertService.ReceiveType;

            //Get the current price of coin
            foreach(AlertDataSource alert in AlertService.Alerts)
            {
                if (alert.Coin.Equals("NANO"))
                    alert.Coin = "XRB";

                alert.Current = _coins.Where(c => c.ShortName.Equals(alert.Coin)).Select(c => c.Price).First();
            }

            _otherAlerts = AlertService.Alerts.Where(a => !a.Currency.Equals(UserConfigService.Currency)).OrderBy(a => a.Coin).ThenByDescending(a => a.Price).ToList();
            bsAlerts.DataSource = AlertService.Alerts.Where(a => a.Currency.Equals(UserConfigService.Currency)).OrderBy(a => a.Coin).ThenByDescending(a => a.Price).ToList();

            if (!UserConfigService.Encrypted)
            {
                txtSendAddress.Text = string.Empty;
                txtSendPassword.Text = string.Empty;
                tblEmailInput.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtSendAddress.Text) || string.IsNullOrEmpty(txtSendPassword.Text))
                grpContact.Enabled = false;
        }

        private void SaveAlerts()
        {
            AlertService.SendAddress = txtSendAddress.Text;
            AlertService.SendPassword = txtSendPassword.Text;
            AlertService.ReceiveAddress = txtReceiveAddress.Text;
            AlertService.ReceiveType = cmbReceiveType.Text;
            AlertService.Alerts = bsAlerts.DataSource as List<AlertDataSource>;
            AlertService.Alerts.AddRange(_otherAlerts);

            //Save config
            AlertService.Save();
        }

        private bool CheckValid(decimal currentPrice, decimal checkPrice, AlertService.Operators op)
        {
            if (op == AlertService.Operators.GreaterThan && currentPrice > checkPrice)
            {
                MessageBox.Show("Current price is already greater than check price");
                return false;
            }
            else if(op == AlertService.Operators.LessThan && currentPrice < checkPrice)
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
            cmbReceiveType.DataSource = Enum.GetValues(typeof(AlertService.Types))
                .Cast<Enum>()
                .Select(value => new { (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description, value })
                .OrderBy(d => d.Description)
                .ToList();

            //Set operators
            cmbOperator.DataSource = Enum.GetValues(typeof(AlertService.Operators))
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

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSendAddress.Text) || string.IsNullOrEmpty(txtSendPassword.Text))
            {
                txtSendAddress.Text = string.Empty;
                txtSendPassword.Text = string.Empty;
                grpContact.Enabled = false;
            }
            else
            {
                grpContact.Enabled = true;
            }

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
            Enum.TryParse(cmbOperator.SelectedValue.ToString(), out AlertService.Operators op);
            if (!CheckValid(Convert.ToDecimal(txtCurrent.Text), Convert.ToDecimal(txtPrice.Text), op))
                return;

            bsAlerts.Add(new AlertDataSource { Coin = cmbCoins.Text, Current = Convert.ToDecimal(txtCurrent.Text), Operator = cmbOperator.Text, Price = Convert.ToDecimal(txtPrice.Text), Currency = UserConfigService.Currency });
            bsAlerts.DataSource = ((List<AlertDataSource>)bsAlerts.DataSource).OrderBy(a => a.Coin).ThenByDescending(a => a.Price).ToList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdAlerts.SelectedCells.Count > 0)
                bsAlerts.Remove((AlertDataSource)grdAlerts.SelectedCells[0].OwningRow.DataBoundItem);
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
                Enum.TryParse(cmbOperator.SelectedValue.ToString(), out AlertService.Operators op);
                if (!CheckValid(Convert.ToDecimal(grdAlerts.SelectedCells[0].OwningRow.Cells[1].Value), Convert.ToDecimal(grdAlerts.SelectedCells[0].OwningRow.Cells[3].Value), op))
                    grdAlerts.SelectedCells[0].OwningRow.Cells[3].Value = _oldCheckPrice;

                return;
            }

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
            var currentPrice = _coins.Where(c => c.ShortName.Equals(cmbCoins.Text)).Select(c => c.Price).FirstOrDefault().ToString();
            txtPrice.Text = currentPrice;
            txtCurrent.Text = currentPrice;
        }
        #endregion
    }
}
