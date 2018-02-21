namespace MyCryptoMonitor.Forms
{
    partial class Encrypt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Encrypt));
            this.lblDetails = new System.Windows.Forms.Label();
            this.cbEnableEncryption = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblEnableEncryption = new System.Windows.Forms.Label();
            this.lblDisableEncryption = new System.Windows.Forms.Label();
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
            this.cbEnableEncryption.AutoSize = true;
            this.cbEnableEncryption.Enabled = false;
            this.cbEnableEncryption.Location = new System.Drawing.Point(16, 83);
            this.cbEnableEncryption.Name = "cbEnableEncryption";
            this.cbEnableEncryption.Size = new System.Drawing.Size(112, 17);
            this.cbEnableEncryption.TabIndex = 1;
            this.cbEnableEncryption.Text = "Enable Encryption";
            this.cbEnableEncryption.UseVisualStyleBackColor = true;
            this.cbEnableEncryption.CheckedChanged += new System.EventHandler(this.cbEnableEncryption_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(134, 81);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(258, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // lblEnableEncryption
            // 
            this.lblEnableEncryption.AutoSize = true;
            this.lblEnableEncryption.Location = new System.Drawing.Point(13, 57);
            this.lblEnableEncryption.Name = "lblEnableEncryption";
            this.lblEnableEncryption.Size = new System.Drawing.Size(201, 13);
            this.lblEnableEncryption.TabIndex = 3;
            this.lblEnableEncryption.Text = "Type in a password to enable encryption.";
            // 
            // lblDisableEncryption
            // 
            this.lblDisableEncryption.AutoSize = true;
            this.lblDisableEncryption.Location = new System.Drawing.Point(13, 57);
            this.lblDisableEncryption.Name = "lblDisableEncryption";
            this.lblDisableEncryption.Size = new System.Drawing.Size(216, 13);
            this.lblDisableEncryption.TabIndex = 4;
            this.lblDisableEncryption.Text = "Type in your password to disable encryption.";
            // 
            // Encrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 117);
            this.Controls.Add(this.lblDisableEncryption);
            this.Controls.Add(this.lblEnableEncryption);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cbEnableEncryption);
            this.Controls.Add(this.lblDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Encrypt";
            this.Text = "Encrypt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.CheckBox cbEnableEncryption;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblEnableEncryption;
        private System.Windows.Forms.Label lblDisableEncryption;
    }
}