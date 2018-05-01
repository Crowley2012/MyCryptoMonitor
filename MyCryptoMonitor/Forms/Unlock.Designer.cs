namespace MyCryptoMonitor.Forms
{
    partial class Unlock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Unlock));
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnForgot = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(12, 12);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(249, 20);
            this.txtPassword.TabIndex = 0;
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(267, 11);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(75, 23);
            this.btnUnlock.TabIndex = 1;
            this.btnUnlock.Text = "Unlock";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnForgot
            // 
            this.btnForgot.Location = new System.Drawing.Point(348, 11);
            this.btnForgot.Name = "btnForgot";
            this.btnForgot.Size = new System.Drawing.Size(105, 23);
            this.btnForgot.TabIndex = 2;
            this.btnForgot.Text = "Forgot Password";
            this.btnForgot.UseVisualStyleBackColor = true;
            this.btnForgot.Click += new System.EventHandler(this.btnForgot_Click);
            // 
            // Unlock
            // 
            this.AcceptButton = this.btnUnlock;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 46);
            this.Controls.Add(this.btnForgot);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.txtPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Unlock";
            this.Text = "Unlock";
            this.Load += new System.EventHandler(this.Unlock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Button btnForgot;
    }
}