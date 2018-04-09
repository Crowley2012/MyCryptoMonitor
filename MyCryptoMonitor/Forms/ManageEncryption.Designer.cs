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
            this.lblInstructions = new System.Windows.Forms.Label();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDetails
            // 
            this.lblDetails.AutoSize = true;
            this.lblDetails.Location = new System.Drawing.Point(13, 13);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(387, 26);
            this.lblDetails.TabIndex = 0;
            this.lblDetails.Text = "Enabling encryption will encrypt any data that this program saves and will requir" +
    "e \r\nyou to enter your password each time the program launches.";
            // 
            // cbEnableEncryption
            // 
            this.cbEnableEncryption.AutoCheck = false;
            this.cbEnableEncryption.AutoSize = true;
            this.cbEnableEncryption.Location = new System.Drawing.Point(16, 83);
            this.cbEnableEncryption.Name = "cbEnableEncryption";
            this.cbEnableEncryption.Size = new System.Drawing.Size(76, 17);
            this.cbEnableEncryption.TabIndex = 1;
            this.cbEnableEncryption.Text = "Encryption";
            this.cbEnableEncryption.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(98, 81);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(213, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(13, 57);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(201, 13);
            this.lblInstructions.TabIndex = 3;
            this.lblInstructions.Text = "Type in a password to enable encryption.";
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(317, 79);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 5;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // Encrypt
            // 
            this.AcceptButton = this.btnEncrypt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 117);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cbEnableEncryption);
            this.Controls.Add(this.lblDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Encrypt";
            this.Text = "Encrypt";
            this.Load += new System.EventHandler(this.Encrypt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.CheckBox cbEnableEncryption;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Button btnEncrypt;
    }
}