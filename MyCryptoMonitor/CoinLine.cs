using System.Windows.Forms;

namespace MyCryptoMonitor
{
    public class CoinGuiLine
    {
        public string Coin;
        public Label coinLabel;
        public Label priceLabel;
        public Label totalLabel;
        public Label profitLabel;
        public Label changeDollarLabel;
        public Label changePercentLabel;
        public TextBox boughtTextBox;
        public TextBox paidTextBox;

        public CoinGuiLine(string coin, int index)
        {
            Coin = coin;

            coinLabel = new Label();
            priceLabel = new Label();
            boughtTextBox = new TextBox();
            totalLabel = new Label();
            paidTextBox = new TextBox();
            profitLabel = new Label();
            changeDollarLabel = new Label();
            changePercentLabel = new Label();

            int yindex = index * 25;

            coinLabel.AutoSize = true;
            priceLabel.AutoSize = true;
            totalLabel.AutoSize = true;
            profitLabel.AutoSize = true;
            changeDollarLabel.AutoSize = true;
            changePercentLabel.AutoSize = true;

            coinLabel.Size = new System.Drawing.Size(28, 13);
            priceLabel.Size = new System.Drawing.Size(34, 13);
            boughtTextBox.Size = new System.Drawing.Size(66, 20);
            totalLabel.Size = new System.Drawing.Size(34, 13);
            paidTextBox.Size = new System.Drawing.Size(66, 20);
            profitLabel.Size = new System.Drawing.Size(34, 13);
            changeDollarLabel.Size = new System.Drawing.Size(58, 13);
            changePercentLabel.Size = new System.Drawing.Size(36, 13);

            coinLabel.Location = new System.Drawing.Point(12, 101 + yindex);
            priceLabel.Location = new System.Drawing.Point(57, 101 + yindex);
            boughtTextBox.Location = new System.Drawing.Point(130, 98 + yindex);
            totalLabel.Location = new System.Drawing.Point(216, 101 + yindex);
            paidTextBox.Location = new System.Drawing.Point(283, 98 + yindex);
            profitLabel.Location = new System.Drawing.Point(370, 101 + yindex);
            changeDollarLabel.Location = new System.Drawing.Point(427, 101 + yindex);
            changePercentLabel.Location = new System.Drawing.Point(510, 101 + yindex);
        }
    }
}
