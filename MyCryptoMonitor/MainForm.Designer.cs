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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblTotalProfit = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblResetTime = new System.Windows.Forms.Label();
            this.lblRefreshTime = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalProfitChange = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePortfolioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portfolio1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portfolio2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portfolio3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPortfolioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portfolio1ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.portfolio2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.portfolio3ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTotalProfit
            // 
            this.lblTotalProfit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalProfit.AutoSize = true;
            this.lblTotalProfit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProfit.Location = new System.Drawing.Point(3, 0);
            this.lblTotalProfit.Name = "lblTotalProfit";
            this.lblTotalProfit.Size = new System.Drawing.Size(118, 27);
            this.lblTotalProfit.TabIndex = 0;
            this.lblTotalProfit.Text = "$0.00";
            this.lblTotalProfit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTips.SetToolTip(this.lblTotalProfit, "Total Paid + Total Profits");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(670, 60);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblResetTime, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblRefreshTime, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblStatus, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(264, 54);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lblResetTime
            // 
            this.lblResetTime.AutoSize = true;
            this.lblResetTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResetTime.Location = new System.Drawing.Point(3, 0);
            this.lblResetTime.Name = "lblResetTime";
            this.lblResetTime.Size = new System.Drawing.Size(100, 12);
            this.lblResetTime.TabIndex = 18;
            this.lblResetTime.Text = "Time since reset: 00:00";
            this.lblResetTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRefreshTime
            // 
            this.lblRefreshTime.AutoSize = true;
            this.lblRefreshTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefreshTime.Location = new System.Drawing.Point(3, 17);
            this.lblRefreshTime.Name = "lblRefreshTime";
            this.lblRefreshTime.Size = new System.Drawing.Size(108, 12);
            this.lblRefreshTime.TabIndex = 19;
            this.lblRefreshTime.Text = "Time since refresh: 00:00";
            this.lblRefreshTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(3, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(69, 12);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status: Loading";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblTotalProfit, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblTotalProfitChange, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(273, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(124, 54);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // lblTotalProfitChange
            // 
            this.lblTotalProfitChange.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalProfitChange.AutoSize = true;
            this.lblTotalProfitChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProfitChange.Location = new System.Drawing.Point(3, 27);
            this.lblTotalProfitChange.Name = "lblTotalProfitChange";
            this.lblTotalProfitChange.Size = new System.Drawing.Size(118, 27);
            this.lblTotalProfitChange.TabIndex = 1;
            this.lblTotalProfitChange.Text = "($0.00)";
            this.lblTotalProfitChange.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTips.SetToolTip(this.lblTotalProfitChange, "Total Profits");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Coin";
            this.toolTips.SetToolTip(this.label7, "Coin short code");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(57, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Price";
            this.toolTips.SetToolTip(this.label8, "Current price of coin");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(131, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Bought";
            this.toolTips.SetToolTip(this.label9, "Amount of coin bought");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(230, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Total";
            this.toolTips.SetToolTip(this.label10, "Price x Bought");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(304, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Paid";
            this.toolTips.SetToolTip(this.label1, "The amount of fiat paid");
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(406, 91);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 13);
            this.label16.TabIndex = 33;
            this.label16.Text = "Profit";
            this.toolTips.SetToolTip(this.label16, "Total - Paid");
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(481, 91);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 13);
            this.label17.TabIndex = 39;
            this.label17.Text = "Change ($)";
            this.toolTips.SetToolTip(this.label17, "Change in dollars since reset");
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(555, 91);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "Change (%)";
            this.toolTips.SetToolTip(this.label18, "Change in percent since reset");
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.coinsToolStripMenuItem,
            this.savePortfolioToolStripMenuItem,
            this.loadPortfolioToolStripMenuItem,
            this.donateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(694, 24);
            this.menuStrip1.TabIndex = 46;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.Reset_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit_Click);
            // 
            // coinsToolStripMenuItem
            // 
            this.coinsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.removeAllToolStripMenuItem});
            this.coinsToolStripMenuItem.Name = "coinsToolStripMenuItem";
            this.coinsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.coinsToolStripMenuItem.Text = "Coins";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.addToolStripMenuItem.Text = "Add Coin";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.AddCoin_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.removeToolStripMenuItem.Text = "Remove Coin";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveCoin_Click);
            // 
            // removeAllToolStripMenuItem
            // 
            this.removeAllToolStripMenuItem.Name = "removeAllToolStripMenuItem";
            this.removeAllToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.removeAllToolStripMenuItem.Text = "Remove All";
            this.removeAllToolStripMenuItem.Click += new System.EventHandler(this.RemoveAllCoins_Click);
            // 
            // savePortfolioToolStripMenuItem
            // 
            this.savePortfolioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.portfolio1ToolStripMenuItem,
            this.portfolio2ToolStripMenuItem,
            this.portfolio3ToolStripMenuItem});
            this.savePortfolioToolStripMenuItem.Name = "savePortfolioToolStripMenuItem";
            this.savePortfolioToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.savePortfolioToolStripMenuItem.Text = "Save Portfolio";
            // 
            // portfolio1ToolStripMenuItem
            // 
            this.portfolio1ToolStripMenuItem.Name = "portfolio1ToolStripMenuItem";
            this.portfolio1ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.portfolio1ToolStripMenuItem.Tag = "Portfolio1";
            this.portfolio1ToolStripMenuItem.Text = "Portfolio 1";
            this.portfolio1ToolStripMenuItem.Click += new System.EventHandler(this.SavePortfolio_Click);
            // 
            // portfolio2ToolStripMenuItem
            // 
            this.portfolio2ToolStripMenuItem.Name = "portfolio2ToolStripMenuItem";
            this.portfolio2ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.portfolio2ToolStripMenuItem.Tag = "Portfolio2";
            this.portfolio2ToolStripMenuItem.Text = "Portfolio 2";
            this.portfolio2ToolStripMenuItem.Click += new System.EventHandler(this.SavePortfolio_Click);
            // 
            // portfolio3ToolStripMenuItem
            // 
            this.portfolio3ToolStripMenuItem.Name = "portfolio3ToolStripMenuItem";
            this.portfolio3ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.portfolio3ToolStripMenuItem.Tag = "Portfolio3";
            this.portfolio3ToolStripMenuItem.Text = "Portfolio 3";
            this.portfolio3ToolStripMenuItem.Click += new System.EventHandler(this.SavePortfolio_Click);
            // 
            // loadPortfolioToolStripMenuItem
            // 
            this.loadPortfolioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.portfolio1ToolStripMenuItem1,
            this.portfolio2ToolStripMenuItem1,
            this.portfolio3ToolStripMenuItem1});
            this.loadPortfolioToolStripMenuItem.Name = "loadPortfolioToolStripMenuItem";
            this.loadPortfolioToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.loadPortfolioToolStripMenuItem.Text = "Load Portfolio";
            // 
            // portfolio1ToolStripMenuItem1
            // 
            this.portfolio1ToolStripMenuItem1.Name = "portfolio1ToolStripMenuItem1";
            this.portfolio1ToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.portfolio1ToolStripMenuItem1.Tag = "Portfolio1";
            this.portfolio1ToolStripMenuItem1.Text = "Portfolio 1";
            this.portfolio1ToolStripMenuItem1.Click += new System.EventHandler(this.LoadPortfolio_Click);
            // 
            // portfolio2ToolStripMenuItem1
            // 
            this.portfolio2ToolStripMenuItem1.Name = "portfolio2ToolStripMenuItem1";
            this.portfolio2ToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.portfolio2ToolStripMenuItem1.Tag = "Portfolio2";
            this.portfolio2ToolStripMenuItem1.Text = "Portfolio 2";
            this.portfolio2ToolStripMenuItem1.Click += new System.EventHandler(this.LoadPortfolio_Click);
            // 
            // portfolio3ToolStripMenuItem1
            // 
            this.portfolio3ToolStripMenuItem1.Name = "portfolio3ToolStripMenuItem1";
            this.portfolio3ToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.portfolio3ToolStripMenuItem1.Tag = "Portfolio3";
            this.portfolio3ToolStripMenuItem1.Text = "Portfolio 3";
            this.portfolio3ToolStripMenuItem1.Click += new System.EventHandler(this.LoadPortfolio_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(629, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "24/hr (%)";
            this.toolTips.SetToolTip(this.label2, "Change in percent for last 24 hours");
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.donateToolStripMenuItem.Text = "Donate";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 124);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "My Crypto Monitor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalProfit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblResetTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblTotalProfitChange;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coinsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem savePortfolioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portfolio1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portfolio2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portfolio3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPortfolioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portfolio1ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem portfolio2ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem portfolio3ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllToolStripMenuItem;
        private System.Windows.Forms.Label lblRefreshTime;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
    }
}

