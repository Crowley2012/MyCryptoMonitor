using MyCryptoMonitor.Statics;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyCryptoMonitor.Gui
{
    public sealed class CoinLine : IDisposable
    {
        #region Public Variables
        public string CoinName;
        public int CoinIndex;
        public Label CoinLabel;
        public Label CoinIndexLabel;
        public Label PriceLabel;
        public TextBox BoughtTextBox;
        public Label BoughtPriceLabel;
        public Label TotalLabel;
        public TextBox PaidTextBox;
        public Label ProfitLabel;
        public Label RatioLabel;
        public Label ChangeDollarLabel;
        public Label ChangePercentLabel;
        public Label Change1HrPercentLabel;
        public Label Change24HrPercentLabel;
        public Label Change7DayPercentLabel;
        public TableLayoutPanel Table;
        #endregion

        #region Constructor
        public CoinLine(string coinName, int coinIndex, int lineIndex, int formWidth)
        {
            CoinName = coinName;
            CoinIndex = coinIndex;

            CoinIndexLabel = new Label();
            CoinLabel = new Label();
            PriceLabel = new Label();
            BoughtTextBox = new TextBox();
            BoughtPriceLabel = new Label();
            TotalLabel = new Label();
            PaidTextBox = new TextBox();
            ProfitLabel = new Label();
            RatioLabel = new Label();
            ChangeDollarLabel = new Label();
            ChangePercentLabel = new Label();
            Change1HrPercentLabel = new Label();
            Change24HrPercentLabel = new Label();
            Change7DayPercentLabel = new Label();
            Table = new TableLayoutPanel();

            CoinIndexLabel.AutoSize = false;
            CoinLabel.AutoSize = false;
            PriceLabel.AutoSize = false;
            BoughtPriceLabel.AutoSize = false;
            TotalLabel.AutoSize = false;
            ProfitLabel.AutoSize = false;
            RatioLabel.AutoSize = false;
            ChangeDollarLabel.AutoSize = false;
            ChangePercentLabel.AutoSize = false;
            Change1HrPercentLabel.AutoSize = false;
            Change24HrPercentLabel.AutoSize = false;
            Change7DayPercentLabel.AutoSize = false;

            CoinIndexLabel.TextAlign = ContentAlignment.MiddleLeft;
            CoinLabel.TextAlign = ContentAlignment.MiddleLeft;
            PriceLabel.TextAlign = ContentAlignment.MiddleLeft;
            BoughtPriceLabel.TextAlign = ContentAlignment.MiddleLeft;
            TotalLabel.TextAlign = ContentAlignment.MiddleLeft;
            ProfitLabel.TextAlign = ContentAlignment.MiddleLeft;
            RatioLabel.TextAlign = ContentAlignment.MiddleLeft;
            ChangeDollarLabel.TextAlign = ContentAlignment.MiddleLeft;
            ChangePercentLabel.TextAlign = ContentAlignment.MiddleLeft;
            Change1HrPercentLabel.TextAlign = ContentAlignment.MiddleLeft;
            Change24HrPercentLabel.TextAlign = ContentAlignment.MiddleLeft;
            Change7DayPercentLabel.TextAlign = ContentAlignment.MiddleLeft;

            CoinIndexLabel.Dock = DockStyle.Fill;
            CoinLabel.Dock = DockStyle.Fill;
            PriceLabel.Dock = DockStyle.Fill;
            BoughtPriceLabel.Dock = DockStyle.Fill;
            TotalLabel.Dock = DockStyle.Fill;
            ProfitLabel.Dock = DockStyle.Fill;
            RatioLabel.Dock = DockStyle.Fill;
            ChangeDollarLabel.Dock = DockStyle.Fill;
            ChangePercentLabel.Dock = DockStyle.Fill;
            Change1HrPercentLabel.Dock = DockStyle.Fill;
            Change24HrPercentLabel.Dock = DockStyle.Fill;
            Change7DayPercentLabel.Dock = DockStyle.Fill;
            BoughtTextBox.Dock = DockStyle.Fill;
            PaidTextBox.Dock = DockStyle.Fill;

            CoinIndexLabel.Font = new Font(CoinIndexLabel.Font.FontFamily, 6f);

            Table.RowCount = 1;
            Table.ColumnCount = 14;
            Table.Anchor = ((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right);
            Table.Location = new Point(0, 123 + 25 * lineIndex);
            Table.Size = new Size(formWidth - 15, 23);
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 19F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.109272F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.196688F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.180848F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.989424F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.522156F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.522156F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.522156F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.522156F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.247383F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.247383F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.980124F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.980124F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.980124F));
            Table.Controls.Add(CoinIndexLabel, 0, 0);
            Table.Controls.Add(CoinLabel, 1, 0);
            Table.Controls.Add(PriceLabel, 2, 0);
            Table.Controls.Add(BoughtTextBox, 3, 0);
            Table.Controls.Add(BoughtPriceLabel, 4, 0);
            Table.Controls.Add(TotalLabel, 5, 0);
            Table.Controls.Add(PaidTextBox, 6, 0);
            Table.Controls.Add(ProfitLabel, 7, 0);
            Table.Controls.Add(RatioLabel, 8, 0);
            Table.Controls.Add(ChangeDollarLabel, 9, 0);
            Table.Controls.Add(ChangePercentLabel, 10, 0);
            Table.Controls.Add(Change1HrPercentLabel, 11, 0);
            Table.Controls.Add(Change24HrPercentLabel, 12, 0);
            Table.Controls.Add(Change7DayPercentLabel, 13, 0);

            ProfitLabel.TextChanged += new EventHandler(Label_TextChanged);
            RatioLabel.TextChanged += new EventHandler(Label_TextChanged);
            ChangeDollarLabel.TextChanged += new EventHandler(Label_TextChanged);
            ChangePercentLabel.TextChanged += new EventHandler(LabelBold_TextChanged);
            Change1HrPercentLabel.TextChanged += new EventHandler(LabelBold_TextChanged);
            Change24HrPercentLabel.TextChanged += new EventHandler(LabelBold_TextChanged);
            Change7DayPercentLabel.TextChanged += new EventHandler(LabelBold_TextChanged);
            BoughtTextBox.TextChanged += new EventHandler(Input_TextChanged);
            PaidTextBox.TextChanged += new EventHandler(Input_TextChanged);
        }
        #endregion

        #region Methods
        public void SetBoughtText(string bought)
        {
            BoughtTextBox.TextChanged -= new EventHandler(Input_TextChanged);
            BoughtTextBox.Text = bought;
            BoughtTextBox.TextChanged += new EventHandler(Input_TextChanged);
        }

        public void SetPaidText(string paid)
        {
            PaidTextBox.TextChanged -= new EventHandler(Input_TextChanged);
            PaidTextBox.Text = paid;
            PaidTextBox.TextChanged += new EventHandler(Input_TextChanged);
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
            RatioLabel.Dispose();
            ChangeDollarLabel.Dispose();
            ChangePercentLabel.Dispose();
            Change1HrPercentLabel.Dispose();
            Change24HrPercentLabel.Dispose();
            Change7DayPercentLabel.Dispose();
            Table.Dispose();
        }
        #endregion

        #region Events
        private void Input_TextChanged(object sender, EventArgs e)
        {
            MainService.Unsaved = true;
        }

        private void Label_TextChanged(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            decimal changePercent = label.Text.Replace("%", string.Empty).Replace(MainService.CurrencySymbol, string.Empty).ConvertToDecimal();

            label.ForeColor = changePercent >= 0 ? ColorTranslator.FromHtml(Globals.PositiveColor) : ColorTranslator.FromHtml(Globals.NegativeColor);
        }

        private void LabelBold_TextChanged(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            decimal changePercent = label.Text.Replace("%", string.Empty).Replace(MainService.CurrencySymbol, string.Empty).ConvertToDecimal();

            label.ForeColor = changePercent >= 0 ? ColorTranslator.FromHtml(Globals.PositiveColor) : ColorTranslator.FromHtml(Globals.NegativeColor);
            label.Font = changePercent >= 10 || changePercent <= -10 ? new Font(label.Font, FontStyle.Bold) : new Font(label.Font, FontStyle.Regular);
        }
        #endregion
    }
}
