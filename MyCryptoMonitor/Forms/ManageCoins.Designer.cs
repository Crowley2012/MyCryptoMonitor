namespace MyCryptoMonitor.Forms
{
    partial class ManageCoins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageCoins));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cbCoins = new System.Windows.Forms.ComboBox();
            this.cbCoinIndex = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(244, 11);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 21);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Add";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // cbCoins
            // 
            this.cbCoins.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbCoins.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCoins.FormattingEnabled = true;
            this.cbCoins.Location = new System.Drawing.Point(10, 11);
            this.cbCoins.Name = "cbCoins";
            this.cbCoins.Size = new System.Drawing.Size(176, 21);
            this.cbCoins.TabIndex = 0;
            this.cbCoins.SelectedIndexChanged += new System.EventHandler(this.cbCoins_SelectedIndexChanged);
            // 
            // cbCoinIndex
            // 
            this.cbCoinIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCoinIndex.FormattingEnabled = true;
            this.cbCoinIndex.Location = new System.Drawing.Point(190, 11);
            this.cbCoinIndex.Name = "cbCoinIndex";
            this.cbCoinIndex.Size = new System.Drawing.Size(48, 21);
            this.cbCoinIndex.TabIndex = 1;
            // 
            // ManageCoins
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 40);
            this.Controls.Add(this.cbCoinIndex);
            this.Controls.Add(this.cbCoins);
            this.Controls.Add(this.btnSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageCoins";
            this.Text = "Add Coin";
            this.Load += new System.EventHandler(this.ManageCoins_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cbCoins;
        private System.Windows.Forms.ComboBox cbCoinIndex;
    }
}