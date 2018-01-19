using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyCryptoMonitor
{
    public class CoinGuiLine
    {
        private const int StartY = 116;
        private const int Spacing = 18;

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
            int yindex = StartY + index * 25;

            CoinLabel.AutoSize = true;
            PriceLabel.AutoSize = true;
            TotalLabel.AutoSize = true;
            ProfitLabel.AutoSize = true;
            ChangeDollarLabel.AutoSize = true;
            ChangePercentLabel.AutoSize = true;
            Change24HrPercentLabel.AutoSize = true;
            
            CoinLabel.Size = new Size(28, 13);
            PriceLabel.Size = new Size(58, 13);
            BoughtTextBox.Size = new Size(80, 20);
            TotalLabel.Size = new Size(58, 13);
            PaidTextBox.Size = new Size(80, 20);
            ProfitLabel.Size = new Size(58, 13);
            ChangeDollarLabel.Size = new Size(58, 13);
            ChangePercentLabel.Size = new Size(54, 13);
            Change24HrPercentLabel.Size = new Size(52, 13);
            
            CoinLabel.Location = new Point(12, yindex);
            PriceLabel.Location = new Point(CoinLabel.Location.X + CoinLabel.Width + Spacing, yindex);
            BoughtTextBox.Location = new Point(PriceLabel.Location.X + PriceLabel.Width + Spacing, yindex -3);
            TotalLabel.Location = new Point(BoughtTextBox.Location.X + BoughtTextBox.Width + Spacing, yindex);
            PaidTextBox.Location = new Point(TotalLabel.Location.X + TotalLabel.Width + Spacing, yindex - 3);
            ProfitLabel.Location = new Point(PaidTextBox.Location.X + PaidTextBox.Width + Spacing, yindex);
            ChangeDollarLabel.Location = new Point(ProfitLabel.Location.X + ProfitLabel.Width + Spacing, yindex);
            ChangePercentLabel.Location = new Point(ChangeDollarLabel.Location.X + ChangeDollarLabel.Width + Spacing, yindex);
            Change24HrPercentLabel.Location = new Point(ChangePercentLabel.Location.X + ChangePercentLabel.Width + Spacing, yindex);

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
