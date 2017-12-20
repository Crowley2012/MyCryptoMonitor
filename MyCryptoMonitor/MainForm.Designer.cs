namespace MyCryptoMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.totalProfit = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReset = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.totalProfitChange = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.refreshLabel = new System.Windows.Forms.Label();
            this.btcPrice = new System.Windows.Forms.Label();
            this.ethPrice = new System.Windows.Forms.Label();
            this.ltcPrice = new System.Windows.Forms.Label();
            this.xrpPrice = new System.Windows.Forms.Label();
            this.xlmPrice = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btcBoughtLabel = new System.Windows.Forms.TextBox();
            this.ethBoughtLabel = new System.Windows.Forms.TextBox();
            this.ltcBoughtLabel = new System.Windows.Forms.TextBox();
            this.xrpBoughtLabel = new System.Windows.Forms.TextBox();
            this.xlmBoughtLabel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.xlmTotalLabel = new System.Windows.Forms.Label();
            this.xrpTotalLabel = new System.Windows.Forms.Label();
            this.ltcTotalLabel = new System.Windows.Forms.Label();
            this.ethTotalLabel = new System.Windows.Forms.Label();
            this.btcTotalLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xlmPaid = new System.Windows.Forms.TextBox();
            this.xrpPaid = new System.Windows.Forms.TextBox();
            this.ltcPaid = new System.Windows.Forms.TextBox();
            this.ethPaid = new System.Windows.Forms.TextBox();
            this.btcPaid = new System.Windows.Forms.TextBox();
            this.xlmProfit = new System.Windows.Forms.Label();
            this.xrpProfit = new System.Windows.Forms.Label();
            this.ltcProfit = new System.Windows.Forms.Label();
            this.ethProfit = new System.Windows.Forms.Label();
            this.btcProfit = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.xlmChange = new System.Windows.Forms.Label();
            this.xrpChange = new System.Windows.Forms.Label();
            this.ltcChange = new System.Windows.Forms.Label();
            this.ethChange = new System.Windows.Forms.Label();
            this.btcChange = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // totalProfit
            // 
            this.totalProfit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalProfit.AutoSize = true;
            this.totalProfit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalProfit.Location = new System.Drawing.Point(3, 0);
            this.totalProfit.Name = "totalProfit";
            this.totalProfit.Size = new System.Drawing.Size(88, 22);
            this.totalProfit.TabIndex = 0;
            this.totalProfit.Text = "$0.00";
            this.totalProfit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "BTC";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 50);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btnReset, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(295, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(186, 44);
            this.tableLayoutPanel5.TabIndex = 45;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(105, 12);
            this.btnReset.Margin = new System.Windows.Forms.Padding(12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(69, 20);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.totalProfit, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.totalProfitChange, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(195, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(94, 44);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // totalProfitChange
            // 
            this.totalProfitChange.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalProfitChange.AutoSize = true;
            this.totalProfitChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalProfitChange.Location = new System.Drawing.Point(3, 22);
            this.totalProfitChange.Name = "totalProfitChange";
            this.totalProfitChange.Size = new System.Drawing.Size(88, 22);
            this.totalProfitChange.TabIndex = 1;
            this.totalProfitChange.Text = "($0.00)";
            this.totalProfitChange.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(186, 44);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.statusLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.refreshLabel, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(180, 38);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(3, 19);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(69, 19);
            this.statusLabel.TabIndex = 8;
            this.statusLabel.Text = "Status: Loading";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // refreshLabel
            // 
            this.refreshLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.refreshLabel.AutoSize = true;
            this.refreshLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshLabel.Location = new System.Drawing.Point(3, 0);
            this.refreshLabel.Name = "refreshLabel";
            this.refreshLabel.Size = new System.Drawing.Size(58, 19);
            this.refreshLabel.TabIndex = 18;
            this.refreshLabel.Text = "Refreshes: 0";
            this.refreshLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btcPrice
            // 
            this.btcPrice.AutoSize = true;
            this.btcPrice.Location = new System.Drawing.Point(57, 101);
            this.btcPrice.Name = "btcPrice";
            this.btcPrice.Size = new System.Drawing.Size(34, 13);
            this.btcPrice.TabIndex = 3;
            this.btcPrice.Text = "$0.00";
            // 
            // ethPrice
            // 
            this.ethPrice.AutoSize = true;
            this.ethPrice.Location = new System.Drawing.Point(57, 127);
            this.ethPrice.Name = "ethPrice";
            this.ethPrice.Size = new System.Drawing.Size(34, 13);
            this.ethPrice.TabIndex = 4;
            this.ethPrice.Text = "$0.00";
            // 
            // ltcPrice
            // 
            this.ltcPrice.AutoSize = true;
            this.ltcPrice.Location = new System.Drawing.Point(57, 153);
            this.ltcPrice.Name = "ltcPrice";
            this.ltcPrice.Size = new System.Drawing.Size(34, 13);
            this.ltcPrice.TabIndex = 5;
            this.ltcPrice.Text = "$0.00";
            // 
            // xrpPrice
            // 
            this.xrpPrice.AutoSize = true;
            this.xrpPrice.Location = new System.Drawing.Point(57, 179);
            this.xrpPrice.Name = "xrpPrice";
            this.xrpPrice.Size = new System.Drawing.Size(34, 13);
            this.xrpPrice.TabIndex = 6;
            this.xrpPrice.Text = "$0.00";
            // 
            // xlmPrice
            // 
            this.xlmPrice.AutoSize = true;
            this.xlmPrice.Location = new System.Drawing.Point(57, 205);
            this.xlmPrice.Name = "xlmPrice";
            this.xlmPrice.Size = new System.Drawing.Size(34, 13);
            this.xlmPrice.TabIndex = 7;
            this.xlmPrice.Text = "$0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "ETH";
            // 
            // btcBoughtLabel
            // 
            this.btcBoughtLabel.Location = new System.Drawing.Point(130, 98);
            this.btcBoughtLabel.Name = "btcBoughtLabel";
            this.btcBoughtLabel.Size = new System.Drawing.Size(66, 20);
            this.btcBoughtLabel.TabIndex = 10;
            // 
            // ethBoughtLabel
            // 
            this.ethBoughtLabel.Location = new System.Drawing.Point(130, 124);
            this.ethBoughtLabel.Name = "ethBoughtLabel";
            this.ethBoughtLabel.Size = new System.Drawing.Size(66, 20);
            this.ethBoughtLabel.TabIndex = 11;
            // 
            // ltcBoughtLabel
            // 
            this.ltcBoughtLabel.Location = new System.Drawing.Point(130, 150);
            this.ltcBoughtLabel.Name = "ltcBoughtLabel";
            this.ltcBoughtLabel.Size = new System.Drawing.Size(66, 20);
            this.ltcBoughtLabel.TabIndex = 12;
            // 
            // xrpBoughtLabel
            // 
            this.xrpBoughtLabel.Location = new System.Drawing.Point(130, 176);
            this.xrpBoughtLabel.Name = "xrpBoughtLabel";
            this.xrpBoughtLabel.Size = new System.Drawing.Size(66, 20);
            this.xrpBoughtLabel.TabIndex = 13;
            // 
            // xlmBoughtLabel
            // 
            this.xlmBoughtLabel.Location = new System.Drawing.Point(130, 202);
            this.xlmBoughtLabel.Name = "xlmBoughtLabel";
            this.xlmBoughtLabel.Size = new System.Drawing.Size(66, 20);
            this.xlmBoughtLabel.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "LTC";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "XRP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "XLM";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Coin";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(57, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Price";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(127, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Bought";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(216, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Total";
            // 
            // xlmTotalLabel
            // 
            this.xlmTotalLabel.AutoSize = true;
            this.xlmTotalLabel.Location = new System.Drawing.Point(216, 205);
            this.xlmTotalLabel.Name = "xlmTotalLabel";
            this.xlmTotalLabel.Size = new System.Drawing.Size(34, 13);
            this.xlmTotalLabel.TabIndex = 26;
            this.xlmTotalLabel.Text = "$0.00";
            // 
            // xrpTotalLabel
            // 
            this.xrpTotalLabel.AutoSize = true;
            this.xrpTotalLabel.Location = new System.Drawing.Point(216, 179);
            this.xrpTotalLabel.Name = "xrpTotalLabel";
            this.xrpTotalLabel.Size = new System.Drawing.Size(34, 13);
            this.xrpTotalLabel.TabIndex = 25;
            this.xrpTotalLabel.Text = "$0.00";
            // 
            // ltcTotalLabel
            // 
            this.ltcTotalLabel.AutoSize = true;
            this.ltcTotalLabel.Location = new System.Drawing.Point(216, 153);
            this.ltcTotalLabel.Name = "ltcTotalLabel";
            this.ltcTotalLabel.Size = new System.Drawing.Size(34, 13);
            this.ltcTotalLabel.TabIndex = 24;
            this.ltcTotalLabel.Text = "$0.00";
            // 
            // ethTotalLabel
            // 
            this.ethTotalLabel.AutoSize = true;
            this.ethTotalLabel.Location = new System.Drawing.Point(216, 127);
            this.ethTotalLabel.Name = "ethTotalLabel";
            this.ethTotalLabel.Size = new System.Drawing.Size(34, 13);
            this.ethTotalLabel.TabIndex = 23;
            this.ethTotalLabel.Text = "$0.00";
            // 
            // btcTotalLabel
            // 
            this.btcTotalLabel.AutoSize = true;
            this.btcTotalLabel.Location = new System.Drawing.Point(216, 101);
            this.btcTotalLabel.Name = "btcTotalLabel";
            this.btcTotalLabel.Size = new System.Drawing.Size(34, 13);
            this.btcTotalLabel.TabIndex = 22;
            this.btcTotalLabel.Text = "$0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(280, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Paid";
            // 
            // xlmPaid
            // 
            this.xlmPaid.Location = new System.Drawing.Point(283, 202);
            this.xlmPaid.Name = "xlmPaid";
            this.xlmPaid.Size = new System.Drawing.Size(66, 20);
            this.xlmPaid.TabIndex = 31;
            // 
            // xrpPaid
            // 
            this.xrpPaid.Location = new System.Drawing.Point(283, 176);
            this.xrpPaid.Name = "xrpPaid";
            this.xrpPaid.Size = new System.Drawing.Size(66, 20);
            this.xrpPaid.TabIndex = 30;
            // 
            // ltcPaid
            // 
            this.ltcPaid.Location = new System.Drawing.Point(283, 150);
            this.ltcPaid.Name = "ltcPaid";
            this.ltcPaid.Size = new System.Drawing.Size(66, 20);
            this.ltcPaid.TabIndex = 29;
            // 
            // ethPaid
            // 
            this.ethPaid.Location = new System.Drawing.Point(283, 124);
            this.ethPaid.Name = "ethPaid";
            this.ethPaid.Size = new System.Drawing.Size(66, 20);
            this.ethPaid.TabIndex = 28;
            // 
            // btcPaid
            // 
            this.btcPaid.Location = new System.Drawing.Point(283, 98);
            this.btcPaid.Name = "btcPaid";
            this.btcPaid.Size = new System.Drawing.Size(66, 20);
            this.btcPaid.TabIndex = 27;
            // 
            // xlmProfit
            // 
            this.xlmProfit.AutoSize = true;
            this.xlmProfit.Location = new System.Drawing.Point(370, 205);
            this.xlmProfit.Name = "xlmProfit";
            this.xlmProfit.Size = new System.Drawing.Size(34, 13);
            this.xlmProfit.TabIndex = 38;
            this.xlmProfit.Text = "$0.00";
            // 
            // xrpProfit
            // 
            this.xrpProfit.AutoSize = true;
            this.xrpProfit.Location = new System.Drawing.Point(370, 179);
            this.xrpProfit.Name = "xrpProfit";
            this.xrpProfit.Size = new System.Drawing.Size(34, 13);
            this.xrpProfit.TabIndex = 37;
            this.xrpProfit.Text = "$0.00";
            // 
            // ltcProfit
            // 
            this.ltcProfit.AutoSize = true;
            this.ltcProfit.Location = new System.Drawing.Point(370, 153);
            this.ltcProfit.Name = "ltcProfit";
            this.ltcProfit.Size = new System.Drawing.Size(34, 13);
            this.ltcProfit.TabIndex = 36;
            this.ltcProfit.Text = "$0.00";
            // 
            // ethProfit
            // 
            this.ethProfit.AutoSize = true;
            this.ethProfit.Location = new System.Drawing.Point(370, 127);
            this.ethProfit.Name = "ethProfit";
            this.ethProfit.Size = new System.Drawing.Size(34, 13);
            this.ethProfit.TabIndex = 35;
            this.ethProfit.Text = "$0.00";
            // 
            // btcProfit
            // 
            this.btcProfit.AutoSize = true;
            this.btcProfit.Location = new System.Drawing.Point(370, 101);
            this.btcProfit.Name = "btcProfit";
            this.btcProfit.Size = new System.Drawing.Size(34, 13);
            this.btcProfit.TabIndex = 34;
            this.btcProfit.Text = "$0.00";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(370, 75);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 13);
            this.label16.TabIndex = 33;
            this.label16.Text = "Profit";
            // 
            // xlmChange
            // 
            this.xlmChange.AutoSize = true;
            this.xlmChange.Location = new System.Drawing.Point(427, 205);
            this.xlmChange.Name = "xlmChange";
            this.xlmChange.Size = new System.Drawing.Size(58, 13);
            this.xlmChange.TabIndex = 44;
            this.xlmChange.Text = "$0.000000";
            // 
            // xrpChange
            // 
            this.xrpChange.AutoSize = true;
            this.xrpChange.Location = new System.Drawing.Point(427, 179);
            this.xrpChange.Name = "xrpChange";
            this.xrpChange.Size = new System.Drawing.Size(58, 13);
            this.xrpChange.TabIndex = 43;
            this.xrpChange.Text = "$0.000000";
            // 
            // ltcChange
            // 
            this.ltcChange.AutoSize = true;
            this.ltcChange.Location = new System.Drawing.Point(427, 153);
            this.ltcChange.Name = "ltcChange";
            this.ltcChange.Size = new System.Drawing.Size(58, 13);
            this.ltcChange.TabIndex = 42;
            this.ltcChange.Text = "$0.000000";
            // 
            // ethChange
            // 
            this.ethChange.AutoSize = true;
            this.ethChange.Location = new System.Drawing.Point(427, 127);
            this.ethChange.Name = "ethChange";
            this.ethChange.Size = new System.Drawing.Size(58, 13);
            this.ethChange.TabIndex = 41;
            this.ethChange.Text = "$0.000000";
            // 
            // btcChange
            // 
            this.btcChange.AutoSize = true;
            this.btcChange.Location = new System.Drawing.Point(427, 101);
            this.btcChange.Name = "btcChange";
            this.btcChange.Size = new System.Drawing.Size(58, 13);
            this.btcChange.TabIndex = 40;
            this.btcChange.Text = "$0.000000";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(427, 75);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 13);
            this.label17.TabIndex = 39;
            this.label17.Text = "Change";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 234);
            this.Controls.Add(this.xlmChange);
            this.Controls.Add(this.xrpChange);
            this.Controls.Add(this.ltcChange);
            this.Controls.Add(this.ethChange);
            this.Controls.Add(this.btcChange);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.xlmProfit);
            this.Controls.Add(this.xrpProfit);
            this.Controls.Add(this.ltcProfit);
            this.Controls.Add(this.ethProfit);
            this.Controls.Add(this.btcProfit);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.xlmPaid);
            this.Controls.Add(this.xrpPaid);
            this.Controls.Add(this.ltcPaid);
            this.Controls.Add(this.ethPaid);
            this.Controls.Add(this.btcPaid);
            this.Controls.Add(this.xlmTotalLabel);
            this.Controls.Add(this.xrpTotalLabel);
            this.Controls.Add(this.ltcTotalLabel);
            this.Controls.Add(this.ethTotalLabel);
            this.Controls.Add(this.btcTotalLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.xlmBoughtLabel);
            this.Controls.Add(this.xrpBoughtLabel);
            this.Controls.Add(this.ltcBoughtLabel);
            this.Controls.Add(this.ethBoughtLabel);
            this.Controls.Add(this.btcBoughtLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.xlmPrice);
            this.Controls.Add(this.xrpPrice);
            this.Controls.Add(this.ltcPrice);
            this.Controls.Add(this.ethPrice);
            this.Controls.Add(this.btcPrice);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Name = "MainForm";
            this.Text = "My Crypto Monitor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label totalProfit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label btcPrice;
        private System.Windows.Forms.Label ethPrice;
        private System.Windows.Forms.Label ltcPrice;
        private System.Windows.Forms.Label xrpPrice;
        private System.Windows.Forms.Label xlmPrice;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox btcBoughtLabel;
        private System.Windows.Forms.TextBox ethBoughtLabel;
        private System.Windows.Forms.TextBox ltcBoughtLabel;
        private System.Windows.Forms.TextBox xrpBoughtLabel;
        private System.Windows.Forms.TextBox xlmBoughtLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label refreshLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label xlmTotalLabel;
        private System.Windows.Forms.Label xrpTotalLabel;
        private System.Windows.Forms.Label ltcTotalLabel;
        private System.Windows.Forms.Label ethTotalLabel;
        private System.Windows.Forms.Label btcTotalLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xlmPaid;
        private System.Windows.Forms.TextBox xrpPaid;
        private System.Windows.Forms.TextBox ltcPaid;
        private System.Windows.Forms.TextBox ethPaid;
        private System.Windows.Forms.TextBox btcPaid;
        private System.Windows.Forms.Label xlmProfit;
        private System.Windows.Forms.Label xrpProfit;
        private System.Windows.Forms.Label ltcProfit;
        private System.Windows.Forms.Label ethProfit;
        private System.Windows.Forms.Label btcProfit;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label xlmChange;
        private System.Windows.Forms.Label xrpChange;
        private System.Windows.Forms.Label ltcChange;
        private System.Windows.Forms.Label ethChange;
        private System.Windows.Forms.Label btcChange;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label totalProfitChange;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    }
}

