namespace MyCryptoMonitor.Forms
{
    partial class FrmManageCoins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageCoins));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.bsViewModel = new System.Windows.Forms.BindingSource(this.components);
            this.cbCoins = new System.Windows.Forms.ComboBox();
            this.bsCoins = new System.Windows.Forms.BindingSource(this.components);
            this.cbCoinIndex = new System.Windows.Forms.ComboBox();
            this.bsCoinIndexes = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsViewModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCoins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCoinIndexes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsViewModel, "ButtonText", true));
            this.btnSubmit.Location = new System.Drawing.Point(244, 11);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 21);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Add";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // bsViewModel
            // 
            this.bsViewModel.DataSource = typeof(MyCryptoMonitor.ViewModels.FrmManageCoinsViewModel);
            // 
            // cbCoins
            // 
            this.cbCoins.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbCoins.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCoins.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bsViewModel, "SelectedCoin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbCoins.DataSource = this.bsCoins;
            this.cbCoins.FormattingEnabled = true;
            this.cbCoins.Location = new System.Drawing.Point(10, 11);
            this.cbCoins.Name = "cbCoins";
            this.cbCoins.Size = new System.Drawing.Size(176, 21);
            this.cbCoins.TabIndex = 0;
            // 
            // bsCoins
            // 
            this.bsCoins.DataMember = "Coins";
            this.bsCoins.DataSource = this.bsViewModel;
            // 
            // cbCoinIndex
            // 
            this.cbCoinIndex.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsViewModel, "SelectedCoinIndex", true));
            this.cbCoinIndex.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bsViewModel, "CoinIndexEnabled", true));
            this.cbCoinIndex.DataSource = this.bsCoinIndexes;
            this.cbCoinIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCoinIndex.FormattingEnabled = true;
            this.cbCoinIndex.Location = new System.Drawing.Point(190, 11);
            this.cbCoinIndex.Name = "cbCoinIndex";
            this.cbCoinIndex.Size = new System.Drawing.Size(48, 21);
            this.cbCoinIndex.TabIndex = 1;
            // 
            // bsCoinIndexes
            // 
            this.bsCoinIndexes.DataMember = "CoinIndexes";
            this.bsCoinIndexes.DataSource = this.bsViewModel;
            // 
            // FrmManageCoins
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 40);
            this.Controls.Add(this.cbCoinIndex);
            this.Controls.Add(this.cbCoins);
            this.Controls.Add(this.btnSubmit);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsViewModel, "Title", true));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmManageCoins";
            this.Text = "Add Coin";
            this.Load += new System.EventHandler(this.ManageCoins_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsViewModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCoins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCoinIndexes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cbCoins;
        private System.Windows.Forms.ComboBox cbCoinIndex;
        private System.Windows.Forms.BindingSource bsViewModel;
        private System.Windows.Forms.BindingSource bsCoins;
        private System.Windows.Forms.BindingSource bsCoinIndexes;
    }
}