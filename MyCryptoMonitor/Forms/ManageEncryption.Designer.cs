namespace MyCryptoMonitor.Forms
{
    partial class ManageEncryption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageEncryption));
            this.lblDetails = new System.Windows.Forms.Label();
            this.cbEnableEncryption = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDetails
            // 
            this.lblDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetails.Location = new System.Drawing.Point(13, 13);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(418, 45);
            this.lblDetails.TabIndex = 0;
            this.lblDetails.Text = "Enabling encryption will encrypt all saved data and will require a password on la" +
    "unch.\r\n\r\nType in password to encrypt or decrypt.";
            // 
            // cbEnableEncryption
            // 
            this.cbEnableEncryption.AutoCheck = false;
            this.cbEnableEncryption.AutoSize = true;
            this.cbEnableEncryption.ForeColor = System.Drawing.Color.Green;
            this.cbEnableEncryption.Location = new System.Drawing.Point(16, 72);
            this.cbEnableEncryption.Name = "cbEnableEncryption";
            this.cbEnableEncryption.Size = new System.Drawing.Size(74, 17);
            this.cbEnableEncryption.TabIndex = 3;
            this.cbEnableEncryption.Text = "Encrypted";
            this.cbEnableEncryption.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(98, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(244, 20);
            this.txtPassword.TabIndex = 0;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(348, 69);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 22);
            this.btnEncrypt.TabIndex = 1;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // ManageEncryption
            // 
            this.AcceptButton = this.btnEncrypt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 103);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cbEnableEncryption);
            this.Controls.Add(this.lblDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageEncryption";
            this.Text = "Encrypt";
            this.Load += new System.EventHandler(this.Encrypt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.CheckBox cbEnableEncryption;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnEncrypt;
    }
}