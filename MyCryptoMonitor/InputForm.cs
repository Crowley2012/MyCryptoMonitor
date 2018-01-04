using System;
using System.Windows.Forms;

namespace MyCryptoMonitor
{
    public partial class InputForm : Form
    {
        public string InputText { get; set; }

        public InputForm()
        {
            InitializeComponent();
        }

        public void SetSubmitLabel(string label)
        {
            btnSubmit.Text = label;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            InputText = txtInput.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
