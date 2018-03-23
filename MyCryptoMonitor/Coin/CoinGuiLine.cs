using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyCryptoMonitor
{
    public sealed class CoinGuiLine : IDisposable
    {
        private const int StartY = 122;
        private const int Spacing = 18;

        public string CoinName;
        public int CoinIndex;
        public Label CoinLabel;
        public Label CoinIndexLabel;
        public Label PriceLabel;
        public Label BoughtPriceLabel;
        public Label TotalLabel;
        public Label ProfitLabel;
        public Label ChangeDollarLabel;
        public Label ChangePercentLabel;
        public Label Change1HrPercentLabel;
        public Label Change24HrPercentLabel;
        public Label Change7DayPercentLabel;
        public TextBox BoughtTextBox;
        public TextBox PaidTextBox;

        public CoinGuiLine(string coin, int coinIndex, int index)
        {
            CoinName = coin;
            CoinIndex = coinIndex;
            int yindex = StartY + index * 25;

            CoinIndexLabel = new Label();
            CoinLabel = new Label();
            PriceLabel = new Label();
            BoughtTextBox = new TextBox();
            BoughtPriceLabel = new Label();
            TotalLabel = new Label();
            PaidTextBox = new TextBox();
            ProfitLabel = new Label();
            ChangeDollarLabel = new Label();
            ChangePercentLabel = new Label();
            Change1HrPercentLabel = new Label();
            Change24HrPercentLabel = new Label();
            Change7DayPercentLabel = new Label();

            CoinIndexLabel.AutoSize = true;
            CoinLabel.AutoSize = true;
            PriceLabel.AutoSize = true;
            BoughtPriceLabel.AutoSize = true;
            TotalLabel.AutoSize = true;
            ProfitLabel.AutoSize = true;
            ChangeDollarLabel.AutoSize = true;
            ChangePercentLabel.AutoSize = true;
            Change1HrPercentLabel.AutoSize = true;
            Change24HrPercentLabel.AutoSize = true;
            Change7DayPercentLabel.AutoSize = true;

            CoinIndexLabel.Font = new Font(CoinIndexLabel.Font.FontFamily, 6f);

            CoinIndexLabel.Size = new Size(15, 13);
            CoinLabel.Size = new Size(38, 13);
            PriceLabel.Size = new Size(58, 13);
            BoughtTextBox.Size = new Size(80, 20);
            BoughtPriceLabel.Size = new Size(78, 13);
            TotalLabel.Size = new Size(58, 13);
            PaidTextBox.Size = new Size(80, 20);
            ProfitLabel.Size = new Size(58, 13);
            ChangeDollarLabel.Size = new Size(58, 13);
            ChangePercentLabel.Size = new Size(54, 13);
            Change1HrPercentLabel.Size = new Size(42, 13);
            Change24HrPercentLabel.Size = new Size(52, 13);
            Change7DayPercentLabel.Size = new Size(52, 13);

            CoinIndexLabel.Location = new Point(3, yindex + 2);
            CoinLabel.Location = new Point(12, yindex);
            PriceLabel.Location = new Point(CoinLabel.Location.X + CoinLabel.Width + Spacing, yindex);
            BoughtTextBox.Location = new Point(PriceLabel.Location.X + PriceLabel.Width + Spacing, yindex -3);
            BoughtPriceLabel.Location = new Point(BoughtTextBox.Location.X + BoughtTextBox.Width + Spacing, yindex);
            TotalLabel.Location = new Point(BoughtPriceLabel.Location.X + BoughtPriceLabel.Width + Spacing, yindex);
            PaidTextBox.Location = new Point(TotalLabel.Location.X + TotalLabel.Width + Spacing, yindex - 3);
            ProfitLabel.Location = new Point(PaidTextBox.Location.X + PaidTextBox.Width + Spacing, yindex);
            ChangeDollarLabel.Location = new Point(ProfitLabel.Location.X + ProfitLabel.Width + Spacing, yindex);
            ChangePercentLabel.Location = new Point(ChangeDollarLabel.Location.X + ChangeDollarLabel.Width + Spacing, yindex);
            Change1HrPercentLabel.Location = new Point(ChangePercentLabel.Location.X + ChangePercentLabel.Width + Spacing, yindex);
            Change24HrPercentLabel.Location = new Point(Change1HrPercentLabel.Location.X + Change1HrPercentLabel.Width + Spacing, yindex);
            Change7DayPercentLabel.Location = new Point(Change24HrPercentLabel.Location.X + Change24HrPercentLabel.Width + Spacing, yindex);

            ChangePercentLabel.TextChanged += new EventHandler(ChangePercentLabel_TextChanged);
            Change1HrPercentLabel.TextChanged += new EventHandler(ChangeTestPercentLabel_TextChanged);
            Change24HrPercentLabel.TextChanged += new EventHandler(ChangeTestPercentLabel_TextChanged);
            Change7DayPercentLabel.TextChanged += new EventHandler(ChangeTestPercentLabel_TextChanged);
        }

        public void Dispose()
        {
            CoinIndexLabel.Dispose();
            CoinLabel.Dispose();
            PriceLabel.Dispose();
            BoughtTextBox.Dispose();
            BoughtPriceLabel.Dispose();
            TotalLabel.Dispose();
            PaidTextBox.Dispose();
            ProfitLabel.Dispose();
            ChangeDollarLabel.Dispose();
            ChangePercentLabel.Dispose();
            Change1HrPercentLabel.Dispose();
            Change24HrPercentLabel.Dispose();
            Change7DayPercentLabel.Dispose();
        }

        private void ChangeTestPercentLabel_TextChanged(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            decimal changePercent = Convert.ToDecimal(label.Text.Replace("%", string.Empty));

            //Set color
            if (changePercent >= 0)
                label.ForeColor = Color.Green;
            else
                label.ForeColor = Color.Red;

            //Set weight
            if (changePercent >= 10 || changePercent <= -10)
                label.Font = new Font(label.Font, FontStyle.Bold);
            else
                label.Font = new Font(label.Font, FontStyle.Regular);
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
