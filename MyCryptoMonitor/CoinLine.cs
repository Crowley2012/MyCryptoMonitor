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

            CoinName = coin;
            int yindex = index * 25;

            CoinLabel.AutoSize = true;
            PriceLabel.AutoSize = true;
            TotalLabel.AutoSize = true;
            ProfitLabel.AutoSize = true;
            ChangeDollarLabel.AutoSize = true;
            ChangePercentLabel.AutoSize = true;

            CoinLabel.Size = new System.Drawing.Size(28, 13);
            PriceLabel.Size = new System.Drawing.Size(34, 13);
            BoughtTextBox.Size = new System.Drawing.Size(66, 20);
            TotalLabel.Size = new System.Drawing.Size(34, 13);
            PaidTextBox.Size = new System.Drawing.Size(66, 20);
            ProfitLabel.Size = new System.Drawing.Size(34, 13);
            ChangeDollarLabel.Size = new System.Drawing.Size(58, 13);
            ChangePercentLabel.Size = new System.Drawing.Size(36, 13);

            CoinLabel.Location = new System.Drawing.Point(12, 116 + yindex);
            PriceLabel.Location = new System.Drawing.Point(57, 116 + yindex);
            BoughtTextBox.Location = new System.Drawing.Point(130, 113 + yindex);
            TotalLabel.Location = new System.Drawing.Point(216, 116 + yindex);
            PaidTextBox.Location = new System.Drawing.Point(283, 113 + yindex);
            ProfitLabel.Location = new System.Drawing.Point(370, 116 + yindex);
            ChangeDollarLabel.Location = new System.Drawing.Point(427, 116 + yindex);
            ChangePercentLabel.Location = new System.Drawing.Point(510, 116 + yindex);
        }
    }
}
