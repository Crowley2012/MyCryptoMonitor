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
            this.totalLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btcPrice = new System.Windows.Forms.Label();
            this.ethPrice = new System.Windows.Forms.Label();
            this.ltcPrice = new System.Windows.Forms.Label();
            this.xrpPrice = new System.Windows.Forms.Label();
            this.xlmPrice = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btcBought = new System.Windows.Forms.TextBox();
            this.ethBought = new System.Windows.Forms.TextBox();
            this.ltcBought = new System.Windows.Forms.TextBox();
            this.xrpBought = new System.Windows.Forms.TextBox();
            this.xlmBought = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.refreshLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.xlmTotal = new System.Windows.Forms.Label();
            this.xrpTotal = new System.Windows.Forms.Label();
            this.ltcTotal = new System.Windows.Forms.Label();
            this.ethTotal = new System.Windows.Forms.Label();
            this.btcTotal = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // totalLabel
            // 
            this.totalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalLabel.AutoSize = true;
            this.totalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLabel.Location = new System.Drawing.Point(123, 0);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(94, 58);
            this.totalLabel.TabIndex = 0;
            this.totalLabel.Text = "$0.00";
            this.totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 121);
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
            this.tableLayoutPanel1.Controls.Add(this.totalLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(340, 58);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btcPrice
            // 
            this.btcPrice.AutoSize = true;
            this.btcPrice.Location = new System.Drawing.Point(57, 121);
            this.btcPrice.Name = "btcPrice";
            this.btcPrice.Size = new System.Drawing.Size(34, 13);
            this.btcPrice.TabIndex = 3;
            this.btcPrice.Text = "$0.00";
            // 
            // ethPrice
            // 
            this.ethPrice.AutoSize = true;
            this.ethPrice.Location = new System.Drawing.Point(57, 147);
            this.ethPrice.Name = "ethPrice";
            this.ethPrice.Size = new System.Drawing.Size(34, 13);
            this.ethPrice.TabIndex = 4;
            this.ethPrice.Text = "$0.00";
            // 
            // ltcPrice
            // 
            this.ltcPrice.AutoSize = true;
            this.ltcPrice.Location = new System.Drawing.Point(57, 173);
            this.ltcPrice.Name = "ltcPrice";
            this.ltcPrice.Size = new System.Drawing.Size(34, 13);
            this.ltcPrice.TabIndex = 5;
            this.ltcPrice.Text = "$0.00";
            // 
            // xrpPrice
            // 
            this.xrpPrice.AutoSize = true;
            this.xrpPrice.Location = new System.Drawing.Point(57, 199);
            this.xrpPrice.Name = "xrpPrice";
            this.xrpPrice.Size = new System.Drawing.Size(34, 13);
            this.xrpPrice.TabIndex = 6;
            this.xrpPrice.Text = "$0.00";
            // 
            // xlmPrice
            // 
            this.xlmPrice.AutoSize = true;
            this.xlmPrice.Location = new System.Drawing.Point(57, 225);
            this.xlmPrice.Name = "xlmPrice";
            this.xlmPrice.Size = new System.Drawing.Size(34, 13);
            this.xlmPrice.TabIndex = 7;
            this.xlmPrice.Text = "$0.00";
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(3, 26);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(108, 26);
            this.statusLabel.TabIndex = 8;
            this.statusLabel.Text = "Status: Loading";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "ETH";
            // 
            // btcBought
            // 
            this.btcBought.Location = new System.Drawing.Point(130, 118);
            this.btcBought.Name = "btcBought";
            this.btcBought.Size = new System.Drawing.Size(100, 20);
            this.btcBought.TabIndex = 10;
            // 
            // ethBought
            // 
            this.ethBought.Location = new System.Drawing.Point(130, 144);
            this.ethBought.Name = "ethBought";
            this.ethBought.Size = new System.Drawing.Size(100, 20);
            this.ethBought.TabIndex = 11;
            // 
            // ltcBought
            // 
            this.ltcBought.Location = new System.Drawing.Point(130, 170);
            this.ltcBought.Name = "ltcBought";
            this.ltcBought.Size = new System.Drawing.Size(100, 20);
            this.ltcBought.TabIndex = 12;
            // 
            // xrpBought
            // 
            this.xrpBought.Location = new System.Drawing.Point(130, 196);
            this.xrpBought.Name = "xrpBought";
            this.xrpBought.Size = new System.Drawing.Size(100, 20);
            this.xrpBought.TabIndex = 13;
            // 
            // xlmBought
            // 
            this.xlmBought.Location = new System.Drawing.Point(130, 222);
            this.xlmBought.Name = "xlmBought";
            this.xlmBought.Size = new System.Drawing.Size(100, 20);
            this.xlmBought.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "LTC";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "XRP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "XLM";
            // 
            // refreshLabel
            // 
            this.refreshLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshLabel.AutoSize = true;
            this.refreshLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshLabel.Location = new System.Drawing.Point(3, 0);
            this.refreshLabel.Name = "refreshLabel";
            this.refreshLabel.Size = new System.Drawing.Size(108, 26);
            this.refreshLabel.TabIndex = 18;
            this.refreshLabel.Text = "Refreshes: 0";
            this.refreshLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.statusLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.refreshLabel, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(114, 52);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Coin";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(57, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Price";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(127, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Bought";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(248, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Total";
            // 
            // xlmTotal
            // 
            this.xlmTotal.AutoSize = true;
            this.xlmTotal.Location = new System.Drawing.Point(248, 225);
            this.xlmTotal.Name = "xlmTotal";
            this.xlmTotal.Size = new System.Drawing.Size(34, 13);
            this.xlmTotal.TabIndex = 26;
            this.xlmTotal.Text = "$0.00";
            // 
            // xrpTotal
            // 
            this.xrpTotal.AutoSize = true;
            this.xrpTotal.Location = new System.Drawing.Point(248, 199);
            this.xrpTotal.Name = "xrpTotal";
            this.xrpTotal.Size = new System.Drawing.Size(34, 13);
            this.xrpTotal.TabIndex = 25;
            this.xrpTotal.Text = "$0.00";
            // 
            // ltcTotal
            // 
            this.ltcTotal.AutoSize = true;
            this.ltcTotal.Location = new System.Drawing.Point(248, 173);
            this.ltcTotal.Name = "ltcTotal";
            this.ltcTotal.Size = new System.Drawing.Size(34, 13);
            this.ltcTotal.TabIndex = 24;
            this.ltcTotal.Text = "$0.00";
            // 
            // ethTotal
            // 
            this.ethTotal.AutoSize = true;
            this.ethTotal.Location = new System.Drawing.Point(248, 147);
            this.ethTotal.Name = "ethTotal";
            this.ethTotal.Size = new System.Drawing.Size(34, 13);
            this.ethTotal.TabIndex = 23;
            this.ethTotal.Text = "$0.00";
            // 
            // btcTotal
            // 
            this.btcTotal.AutoSize = true;
            this.btcTotal.Location = new System.Drawing.Point(248, 121);
            this.btcTotal.Name = "btcTotal";
            this.btcTotal.Size = new System.Drawing.Size(34, 13);
            this.btcTotal.TabIndex = 22;
            this.btcTotal.Text = "$0.00";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 259);
            this.Controls.Add(this.xlmTotal);
            this.Controls.Add(this.xrpTotal);
            this.Controls.Add(this.ltcTotal);
            this.Controls.Add(this.ethTotal);
            this.Controls.Add(this.btcTotal);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.xlmBought);
            this.Controls.Add(this.xrpBought);
            this.Controls.Add(this.ltcBought);
            this.Controls.Add(this.ethBought);
            this.Controls.Add(this.btcBought);
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
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label btcPrice;
        private System.Windows.Forms.Label ethPrice;
        private System.Windows.Forms.Label ltcPrice;
        private System.Windows.Forms.Label xrpPrice;
        private System.Windows.Forms.Label xlmPrice;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox btcBought;
        private System.Windows.Forms.TextBox ethBought;
        private System.Windows.Forms.TextBox ltcBought;
        private System.Windows.Forms.TextBox xrpBought;
        private System.Windows.Forms.TextBox xlmBought;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label refreshLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label xlmTotal;
        private System.Windows.Forms.Label xrpTotal;
        private System.Windows.Forms.Label ltcTotal;
        private System.Windows.Forms.Label ethTotal;
        private System.Windows.Forms.Label btcTotal;
    }
}

