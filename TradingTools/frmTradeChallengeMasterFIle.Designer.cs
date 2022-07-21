namespace TradingTools
{
    partial class frmTradeChallengeMasterFile
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvClosed = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCurrent = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClosed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrent)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(637, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // createNewToolStripMenuItem
            // 
            this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
            this.createNewToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.createNewToolStripMenuItem.Text = "Create New";
            this.createNewToolStripMenuItem.Click += new System.EventHandler(this.createNewToolStripMenuItem_Click);
            // 
            // dgvClosed
            // 
            this.dgvClosed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClosed.Location = new System.Drawing.Point(12, 291);
            this.dgvClosed.Name = "dgvClosed";
            this.dgvClosed.RowTemplate.Height = 25;
            this.dgvClosed.Size = new System.Drawing.Size(471, 177);
            this.dgvClosed.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Closed";
            // 
            // dgvCurrent
            // 
            this.dgvCurrent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrent.Location = new System.Drawing.Point(12, 74);
            this.dgvCurrent.Name = "dgvCurrent";
            this.dgvCurrent.RowTemplate.Height = 25;
            this.dgvCurrent.Size = new System.Drawing.Size(471, 122);
            this.dgvCurrent.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Current";
            // 
            // frmTradeChallengeMasterFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 534);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvCurrent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvClosed);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmTradeChallengeMasterFile";
            this.Text = "Trade Challeng Master File";
            this.Load += new System.EventHandler(this.frmTradeChallengeMasterFile_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClosed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvClosed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCurrent;
        private System.Windows.Forms.Label label2;
    }
}