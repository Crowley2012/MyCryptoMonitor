namespace MyCryptoMonitor
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
            this.txtMngCoin = new System.Windows.Forms.TextBox();
            this.btnMngCoin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMngCoin
            // 
            this.txtMngCoin.Location = new System.Drawing.Point(12, 11);
            this.txtMngCoin.Name = "txtMngCoin";
            this.txtMngCoin.Size = new System.Drawing.Size(232, 22);
            this.txtMngCoin.TabIndex = 0;
            // 
            // btnMngCoin
            // 
            this.btnMngCoin.Location = new System.Drawing.Point(250, 11);
            this.btnMngCoin.Name = "btnMngCoin";
            this.btnMngCoin.Size = new System.Drawing.Size(75, 23);
            this.btnMngCoin.TabIndex = 1;
            this.btnMngCoin.Text = "Add";
            this.btnMngCoin.UseVisualStyleBackColor = true;
            // 
            // ManageCoins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 45);
            this.Controls.Add(this.btnMngCoin);
            this.Controls.Add(this.txtMngCoin);
            this.Name = "ManageCoins";
            this.Text = "ManageCoins";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMngCoin;
        private System.Windows.Forms.Button btnMngCoin;
    }
}