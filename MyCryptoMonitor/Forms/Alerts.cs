using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using MyCryptoMonitor.DataSources;
using System.IO;
using MyCryptoMonitor.Functions;

namespace MyCryptoMonitor.Forms
{
    public partial class Alerts : Form
    {
        #region Private Variables
        private List<CoinData> _coins;
        private List<AlertDataSource> _alerts;
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
            if (File.Exists("Alerts"))
            {
                AlertConfig alertConfig = Management.LoadAlerts();
                txtSendAddress.Text = alertConfig.SendAddress;
                txtSendPassword.Text = alertConfig.SendPassword;
                txtReceiveAddress.Text = alertConfig.ReceiveAddress;
                cmbReceiveType.Text = alertConfig.ReceiveType;

                //Get the current price of coin
                foreach(AlertDataSource alert in alertConfig.Alerts)
                    alert.Current = _coins.Where(c => c.ShortName.Equals(alert.Coin)).Select(c => c.Price).First();

                bsAlerts.DataSource = alertConfig.Alerts.OrderBy(a => a.Coin).ThenByDescending(a => a.Price).ToList();
            }

            if (!Management.UserConfig.Encryption)
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
            AlertConfig alertConfig = new AlertConfig
            {
                SendAddress = txtSendAddress.Text,
                SendPassword = txtSendPassword.Text,
                ReceiveAddress = txtReceiveAddress.Text,
                ReceiveType = cmbReceiveType.Text,
                Alerts = bsAlerts.DataSource as List<AlertDataSource>
            };

            //Save config
            Management.SaveAlerts(alertConfig);
        }

        private bool CheckValid(decimal currentPrice, decimal checkPrice, Management.Operators op)
        {
            if (op == Management.Operators.GreaterThan && currentPrice > checkPrice)
            {
                MessageBox.Show("Current price is already greater than check price");
                return false;
            }
            else if(op == Management.Operators.LessThan && currentPrice < checkPrice)
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
            cmbReceiveType.DataSource = Enum.GetValues(typeof(Management.Types))
                .Cast<Enum>()
                .Select(value => new { (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description, value })
                .OrderBy(d => d.Description)
                .ToList();

            //Set operators
            cmbOperator.DataSource = Enum.GetValues(typeof(Management.Operators))
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
            Enum.TryParse(cmbOperator.SelectedValue.ToString(), out Management.Operators op);
            if (!CheckValid(Convert.ToDecimal(txtCurrent.Text), Convert.ToDecimal(txtPrice.Text), op))
                return;

            bsAlerts.Add(new AlertDataSource { Coin = cmbCoins.Text, Current = Convert.ToDecimal(txtCurrent.Text), Operator = cmbOperator.Text, Price = Convert.ToDecimal(txtPrice.Text) });
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
                Enum.TryParse(cmbOperator.SelectedValue.ToString(), out Management.Operators op);
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
