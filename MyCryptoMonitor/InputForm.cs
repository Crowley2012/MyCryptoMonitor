using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyCryptoMonitor
{
    public partial class InputForm : Form
    {
        public string InputText { get; set; }

        public InputForm(string submitLabel)
        {
            InitializeComponent();
            btnSubmit.Text = submitLabel;
        }

        public InputForm(string submitLabel, List<string> coins)
        {
            InitializeComponent();
            btnSubmit.Text = submitLabel;
            cbCoins.DataSource = coins;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            InputText = cbCoins.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
