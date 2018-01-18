using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyCryptoMonitor
{
    public class CoinGuiLine
    {
        private const int _startY = 116;

        public string CoinName;
        public Label CoinLabel;
        public Label PriceLabel;
        public Label TotalLabel;
        public Label ProfitLabel;
        public Label ChangeDollarLabel;
        public Label ChangePercentLabel;
        public Label Change24HrPercentLabel;
        public TextBox BoughtTextBox;
        public TextBox PaidTextBox;

        public CoinGuiLine(string coin, int index)
        {
            CoinLabel = new Label();
            PriceLabel = new Label();
            BoughtTextBox = new TextBox();
            TotalLabel = new Label();
            PaidTextBox = new TextBox();
            ProfitLabel = new Label();
            ChangeDollarLabel = new Label();
            ChangePercentLabel = new Label();
            Change24HrPercentLabel = new Label();

            CoinName = coin;
            int yindex = index * 25;

            CoinLabel.AutoSize = true;
            PriceLabel.AutoSize = true;
            TotalLabel.AutoSize = true;
            ProfitLabel.AutoSize = true;
            ChangeDollarLabel.AutoSize = true;
            ChangePercentLabel.AutoSize = true;
            Change24HrPercentLabel.AutoSize = true;

            CoinLabel.Size = new Size(28, 13);
            PriceLabel.Size = new Size(34, 13);
            BoughtTextBox.Size = new Size(90, 20);
            TotalLabel.Size = new Size(34, 13);
            PaidTextBox.Size = new Size(66, 20);
            ProfitLabel.Size = new Size(34, 13);
            ChangeDollarLabel.Size = new Size(58, 13);
            ChangePercentLabel.Size = new Size(36, 13);
            Change24HrPercentLabel.Size = new Size(36, 13);

            CoinLabel.Location = new Point(12, _startY + yindex);
            PriceLabel.Location = new Point(57, _startY + yindex);
            BoughtTextBox.Location = new Point(135, _startY - 3 + yindex);
            TotalLabel.Location = new Point(240, _startY + yindex);
            PaidTextBox.Location = new Point(307, _startY - 3 + yindex);
            ProfitLabel.Location = new Point(394, _startY + yindex);
            ChangeDollarLabel.Location = new Point(451, _startY + yindex);
            ChangePercentLabel.Location = new Point(534, _startY + yindex);
            Change24HrPercentLabel.Location = new Point(617, _startY + yindex);

            ChangePercentLabel.TextChanged += new EventHandler(ChangePercentLabel_TextChanged);
            Change24HrPercentLabel.TextChanged += new EventHandler(Change24HrPercentLabel_TextChanged);
        }

        private void Change24HrPercentLabel_TextChanged(object sender, EventArgs e)
        {
            decimal change24HrPercent = Convert.ToDecimal(((Label)sender).Text.Replace("%", string.Empty));

            //Set color
            if (change24HrPercent >= 0)
                Change24HrPercentLabel.ForeColor = Color.Green;
            else
                Change24HrPercentLabel.ForeColor = Color.Red;

            //Set weight
            if (change24HrPercent >= 10 || change24HrPercent <= -10)
                Change24HrPercentLabel.Font = new Font(Change24HrPercentLabel.Font, FontStyle.Bold);
            else
                Change24HrPercentLabel.Font = new Font(Change24HrPercentLabel.Font, FontStyle.Regular);
        }

        private void ChangePercentLabel_TextChanged(object sender, EventArgs e)
        {
            decimal changePercent = Convert.ToDecimal(((Label)sender).Text.Replace("%", string.Empty));

            //Set color
            if (changePercent >= 0)
            {
                ChangeDollarLabel.ForeColor = Color.Green;
                ChangePercentLabel.ForeColor = Color.Green;
            }
            else
            {
                ChangeDollarLabel.ForeColor = Color.Red;
                ChangePercentLabel.ForeColor = Color.Red;
            }

            //Set weight
            if (changePercent >= 10 || changePercent <= -10)
            {
                ChangeDollarLabel.Font = new Font(ChangeDollarLabel.Font, FontStyle.Bold);
                ChangePercentLabel.Font = new Font(ChangePercentLabel.Font, FontStyle.Bold);
            }
            else
            {
                ChangeDollarLabel.Font = new Font(ChangeDollarLabel.Font, FontStyle.Regular);
                ChangePercentLabel.Font = new Font(ChangePercentLabel.Font, FontStyle.Regular);
            }
        }
    }
}
