using MyCryptoMonitor.Properties;
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
                    lblInfo.Text = "Thank you for using My Crypto Monitor, I hope you enjoy it!\r\n\r\n-This is the main screen of the monitor. " +
                        "Here you can see all coins in your portfolio with your calculated profits and changes in coin price. The bought column " +
                        "is the number of coins you own and the paid column is how much you paid for those coins. These fields will need to be in " +
                        "this format (1,000.00).\r\n-The timers in the top left show how long the monitor has been running and how long the refresh is " +
                        "taking. A refresh occurs every 5 seconds.\r\n-Your total portfolio worth is displayed at the top along with some other profit " +
                        "measurements.\r\n-You can change the conversion currency to the top right, this will change values to the selected currency." +
                        "\r\n-If you want to know more about a column or how it is calculated hover over it for a tooltip with more information.";
                    pictureBox.Image = Resources.tut_main;
                    btnPrevious.Enabled = false;
                    break;
                case 1:
                    lblName.Text = "Add Coin";
                    pictureBox.Image = Resources.tut_add;
                    btnPrevious.Enabled = true;
                    break;
                case 2:
                    lblName.Text = "Remove Coin";
                    pictureBox.Image = Resources.tut_remove;
                    break;
                case 3:
                    lblName.Text = "Tutorial Management";
                    pictureBox.Image = Resources.tut_portfolios;
                    break;
                case 4:
                    lblName.Text = "Encryption";
                    pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox.Image = Resources.tut_encrypt;
                    break;
                case 5:
                    lblName.Text = "Alert Management";
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Image = Resources.tut_alerts;
                    btnNext.Enabled = true;
                    break;
                case 6:
                    lblName.Text = "Themes";
                    pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox.Image = Resources.tut_theme;
                    btnNext.Enabled = false;
                    break;
            }
        }
        #endregion

        #region Events
        private void Tutorial_Load(object sender, EventArgs e)
        {
            ChangeStep();
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
