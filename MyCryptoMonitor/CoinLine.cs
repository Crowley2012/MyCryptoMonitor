using System.Drawing;
using System.Windows.Forms;

namespace MyCryptoMonitor
{
    public class CoinGuiLine
    {
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
            BoughtTextBox.Size = new Size(66, 20);
            TotalLabel.Size = new Size(34, 13);
            PaidTextBox.Size = new Size(66, 20);
            ProfitLabel.Size = new Size(34, 13);
            ChangeDollarLabel.Size = new Size(58, 13);
            ChangePercentLabel.Size = new Size(36, 13);
            Change24HrPercentLabel.Size = new Size(36, 13);

            CoinLabel.Location = new Point(12, 116 + yindex);
            PriceLabel.Location = new Point(57, 116 + yindex);
            BoughtTextBox.Location = new Point(130, 113 + yindex);
            TotalLabel.Location = new Point(216, 116 + yindex);
            PaidTextBox.Location = new Point(283, 113 + yindex);
            ProfitLabel.Location = new Point(370, 116 + yindex);
            ChangeDollarLabel.Location = new Point(427, 116 + yindex);
            ChangePercentLabel.Location = new Point(510, 116 + yindex);
            Change24HrPercentLabel.Location = new Point(593, 116 + yindex);
        }
    }
}
