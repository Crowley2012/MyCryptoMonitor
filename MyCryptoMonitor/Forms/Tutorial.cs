using MyCryptoMonitor.Properties;
using MyCryptoMonitor.Statics;
using System;
using System.Windows.Forms;

namespace MyCryptoMonitor.Forms
{
    public partial class Tutorial : Form
    {
        #region Private Variables
        private int step;
        #endregion

        #region Constructor
        public Tutorial()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void ChangeStep()
        {
            switch (step)
            {
                case 0:
                    lblName.Text = "Main Screen";
                    lblInfo.Text = "Thank you for using My Crypto Monitor, I hope you enjoy it!\r\n\r\n" +
                        "\u2022 This is the main screen of the monitor. Here you can see all the coins in your portfolio with calculated profits and changes in coin price.\r\n" +
                        "\u2022 The bought column is the number of coins you own and the paid column is how much you paid for those coins. \r\n" +
                        "\u2022 These fields will need to be in the following format (1,000.00).\r\n" +
                        "\u2022 The timers to the top left show how long the monitor has been running and how long the refresh is taking.\r\n" +
                        "\u2022 You can change the refresh interval and currency the monitor uses to the top right.\r\n" +
                        "\u2022 If you want to know how a column is calculated, hover over it for a tooltip with more information.";
                    pictureBox.Image = Resources.tut_main;
                    btnPrevious.Enabled = false;
                    break;
                case 1:
                    lblName.Text = "Add Coin";
                    lblInfo.Text = "\u2022 Accessed from the main screen via Coins > Add Coin Menu\r\n\r\n" +
                        "\u2022 To add a coin select a coin from the drop down and click add.\r\n" +
                        "\u2022 Some coins may not be supported, in this case a popup will appear with more information.";
                    pictureBox.Image = Resources.tut_add;
                    btnPrevious.Enabled = true;
                    break;
                case 2:
                    lblName.Text = "Remove Coin";
                    lblInfo.Text = "\u2022 Accessed from the main screen via Coins > Remove Coin Menu\r\n\r\n" +
                        "\u2022 To remove a coin select a coin from the drop down and click remove.\r\n" +
                        "\u2022 If multiple of the same coin exists, you can choose the one you want to remove from the second drop down.";
                    pictureBox.Image = Resources.tut_remove;
                    break;
                case 3:
                    lblName.Text = "Portfolio Management";
                    lblInfo.Text = "\u2022 Accessed from the main screen via Save/Load Portfolio > Manage Portfolios Menu\r\n\r\n" +
                        "\u2022 To add a portfolio type a name into the cell next to the * at the bottom.\r\n" +
                        "\u2022 To rename a portfolio type the new name in the portfolio you want to update.\r\n" +
                        "\u2022 To delete a portfolio click the row header to the left of the portfolio and press the delete key on your keyboard.\r\n" +
                        "\u2022 To load a portfolio on startup, select the checkbox.";
                    pictureBox.Image = Resources.tut_portfolios;
                    break;
                case 4:
                    lblName.Text = "Encryption";
                    lblInfo.Text = "\u2022 Accessed from the main screen via Encrypt Menu\r\n\r\n" +
                        "\u2022 Enabling encryption will encrypt your files containing portfolio, alert and email data. It will also require your password on startup.\r\n" +
                        "\u2022 Type a password and click encrypt to encrypt.\r\n" +
                        "\u2022 When encryption is enabled you can disable encryption by typing your password and clicking decrypt.";
                    pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox.Image = Resources.tut_encrypt;
                    break;
                case 5:
                    lblName.Text = "Alert Management";
                    lblInfo.Text = "\u2022 Accessed from the main screen via Alerts Menu\r\n\r\n" +
                        "\u2022 Alerts allow you to receive a popup when a specified condition is met. In this example an alert will be triggered when BTC is worth more than 10,000.\r\n" +
                        "\u2022 Alerts can only be set for coins in your current portfolio.\r\n" +
                        "\u2022 You can also setup email alerts at the bottom of the screen if you have encryption enabled. This will allow you to send a text message or an email to an address.\r\n" +
                        "\u2022 In order for this to work you must use a gmail account with 'Allow less secure apps' enabled.\r\n" +
                        "\u2022 WARNING: Do not use your primary email account, create a new gmail for this monitor. Your email and password will be saved to a file. Although the file is encrypted there is still the possibility of it being cracked.";
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Image = Resources.tut_alerts;
                    btnNext.Text = "Next";
                    break;
                case 6:
                    lblName.Text = "Themes";
                    lblInfo.Text = "\u2022 Accessed from the main screen via Themes Menu\r\n\r\n" +
                        "\u2022 Here you can customize the colors of the monitor to your hearts desire.\r\n" +
                        "\u2022 You can specify custom hex color codes or choose one of the default themes to the right.";
                    pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox.Image = Resources.tut_theme;
                    btnNext.Text = "Finish";
                    break;
                case 7:
                    Close();
                    break;
            }
        }
        #endregion

        #region Events
        private void Tutorial_Load(object sender, EventArgs e)
        {
            ChangeStep();
            Globals.SetTheme(this);
        }

        private void Tutorial_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserConfigService.TutorialCompleted = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            step++;
            ChangeStep();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            step--;
            ChangeStep();
        }
        #endregion
    }
}
