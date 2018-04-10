namespace MyCryptoMonitor.Forms
{
    partial class ManagePortfolios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePortfolios));
            this.grdPortfolios = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.bsPortfolios = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startupDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdPortfolios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPortfolios)).BeginInit();
            this.SuspendLayout();
            // 
            // grdPortfolios
            // 
            this.grdPortfolios.AllowUserToResizeColumns = false;
            this.grdPortfolios.AllowUserToResizeRows = false;
            this.grdPortfolios.AutoGenerateColumns = false;
            this.grdPortfolios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPortfolios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.startupDataGridViewCheckBoxColumn});
            this.grdPortfolios.DataSource = this.bsPortfolios;
            this.grdPortfolios.Location = new System.Drawing.Point(16, 36);
            this.grdPortfolios.Margin = new System.Windows.Forms.Padding(4);
            this.grdPortfolios.Name = "grdPortfolios";
            this.grdPortfolios.Size = new System.Drawing.Size(605, 535);
            this.grdPortfolios.TabIndex = 7;
            this.grdPortfolios.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdPortfolios_CellValidating);
            this.grdPortfolios.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.grdPortfolios_UserDeletingRow);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label1.Location = new System.Drawing.Point(17, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(518, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "To add, click empty cell. To edit, double click cell. To delete, click row header" +
    " and press delete.";
            // 
            // bsPortfolios
            // 
            this.bsPortfolios.DataSource = typeof(MyCryptoMonitor.DataSources.PortfolioDataSourceList);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // startupDataGridViewCheckBoxColumn
            // 
            this.startupDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.startupDataGridViewCheckBoxColumn.DataPropertyName = "Startup";
            this.startupDataGridViewCheckBoxColumn.HeaderText = "Startup";
            this.startupDataGridViewCheckBoxColumn.Name = "startupDataGridViewCheckBoxColumn";
            this.startupDataGridViewCheckBoxColumn.Width = 60;
            // 
            // ManagePortfolios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 586);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdPortfolios);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ManagePortfolios";
            this.Text = "Portfolio Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PortfolioManager_FormClosing);
            this.Load += new System.EventHandler(this.PortfolioManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdPortfolios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPortfolios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView grdPortfolios;
        private System.Windows.Forms.BindingSource bsPortfolios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn startupDataGridViewCheckBoxColumn;
    }
}