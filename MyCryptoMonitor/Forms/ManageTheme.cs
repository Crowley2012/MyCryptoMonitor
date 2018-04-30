using MyCryptoMonitor.Objects;
using MyCryptoMonitor.Statics;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class ManageTheme : Form
    {
        #region Constructor
        public ManageTheme()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void ManageTheme_Load(object sender, EventArgs e)
        {
            Globals.SetTheme(this);

            txtBackground.Text = UserConfigService.Theme.BackgroundColor.ToUpper().Replace("#", string.Empty);
            txtInput.Text = UserConfigService.Theme.InputColor.ToUpper().Replace("#", string.Empty);
            txtButton.Text = UserConfigService.Theme.ButtonColor.ToUpper().Replace("#", string.Empty);
            txtDisabled.Text = UserConfigService.Theme.DisabledColor.ToUpper().Replace("#", string.Empty);
            txtFont.Text = UserConfigService.Theme.FontColor.ToUpper().Replace("#", string.Empty);
            txtPositive.Text = UserConfigService.Theme.PositiveColor.ToUpper().Replace("#", string.Empty);
            txtNegative.Text = UserConfigService.Theme.NegativeColor.ToUpper().Replace("#", string.Empty);
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtBackground.Text = "F0F0F0";
            txtInput.Text = "FFFFFF";
            txtButton.Text = "E1E1E1";
            txtDisabled.Text = "F1F1F1";
            txtFont.Text = "000000";
            txtPositive.Text = "008000";
            txtNegative.Text = "FF0000";
        }

        private void btnDark_Click(object sender, EventArgs e)
        {
            txtBackground.Text = "252526";
            txtInput.Text = "333333";
            txtButton.Text = "333333";
            txtDisabled.Text = "333333";
            txtFont.Text = "94979C";
            txtPositive.Text = "008000";
            txtNegative.Text = "FF0000";
        }

        private void btnRoyal_Click(object sender, EventArgs e)
        {
            txtBackground.Text = "000019";
            txtInput.Text = "000032";
            txtButton.Text = "000023";
            txtDisabled.Text = "333333";
            txtFont.Text = "94979C";
            txtPositive.Text = "FF0";
            txtNegative.Text = "FF0000";
        }

        private void btnHighlight_Click(object sender, EventArgs e)
        {
            txtBackground.Text = "212121";
            txtInput.Text = "333333";
            txtButton.Text = "333333";
            txtDisabled.Text = "333333";
            txtFont.Text = "94979C";
            txtPositive.Text = "FF80AB";
            txtNegative.Text = "FF1744";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ColorTranslator.FromHtml($"#{txtBackground.Text}".ToUpper());
                ColorTranslator.FromHtml($"#{txtInput.Text}".ToUpper());
                ColorTranslator.FromHtml($"#{txtButton.Text}".ToUpper());
                ColorTranslator.FromHtml($"#{txtDisabled.Text}".ToUpper());
                ColorTranslator.FromHtml($"#{txtFont.Text}".ToUpper());
                ColorTranslator.FromHtml($"#{txtPositive.Text}".ToUpper());
                ColorTranslator.FromHtml($"#{txtNegative.Text}".ToUpper());
            }
            catch (Exception)
            {
                MessageBox.Show("Not a valid hex color.");
                return;
            }

            UserConfigService.Theme = new Theme
            {
                BackgroundColor = $"#{txtBackground.Text}".ToUpper(),
                InputColor = $"#{txtInput.Text}".ToUpper(),
                ButtonColor = $"#{txtButton.Text}".ToUpper(),
                DisabledColor = $"#{txtDisabled.Text}".ToUpper(),
                FontColor = $"#{txtFont.Text}".ToUpper(),
                PositiveColor = $"#{txtPositive.Text}".ToUpper(),
                NegativeColor = $"#{txtNegative.Text}".ToUpper()
            };

            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
