
namespace TradingTools
{
    partial class frmDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDashboard));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnOpenCalcShort_Empty = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnOpenCalcLong_Empty = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTradeMasterFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTradeChallenge = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvUnofficial = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTrades_Open = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnofficial)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrades_Open)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1563, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(132, 924);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.btnOpenCalcShort_Empty);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 57);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(132, 57);
            this.panel5.TabIndex = 1;
            // 
            // btnOpenCalcShort_Empty
            // 
            this.btnOpenCalcShort_Empty.BackColor = System.Drawing.Color.Red;
            this.btnOpenCalcShort_Empty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenCalcShort_Empty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenCalcShort_Empty.ForeColor = System.Drawing.Color.White;
            this.btnOpenCalcShort_Empty.Location = new System.Drawing.Point(3, 3);
            this.btnOpenCalcShort_Empty.Name = "btnOpenCalcShort_Empty";
            this.btnOpenCalcShort_Empty.Size = new System.Drawing.Size(126, 51);
            this.btnOpenCalcShort_Empty.TabIndex = 5;
            this.btnOpenCalcShort_Empty.Text = "NEW SHORT";
            this.btnOpenCalcShort_Empty.UseVisualStyleBackColor = false;
            this.btnOpenCalcShort_Empty.Click += new System.EventHandler(this.btnOpenCalcShort_Empty_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.btnOpenCalcLong_Empty);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(132, 57);
            this.panel4.TabIndex = 0;
            // 
            // btnOpenCalcLong_Empty
            // 
            this.btnOpenCalcLong_Empty.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnOpenCalcLong_Empty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenCalcLong_Empty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenCalcLong_Empty.ForeColor = System.Drawing.Color.White;
            this.btnOpenCalcLong_Empty.Location = new System.Drawing.Point(3, 3);
            this.btnOpenCalcLong_Empty.Name = "btnOpenCalcLong_Empty";
            this.btnOpenCalcLong_Empty.Size = new System.Drawing.Size(126, 51);
            this.btnOpenCalcLong_Empty.TabIndex = 5;
            this.btnOpenCalcLong_Empty.Text = "NEW LONG";
            this.btnOpenCalcLong_Empty.UseVisualStyleBackColor = false;
            this.btnOpenCalcLong_Empty.Click += new System.EventHandler(this.btnOpenCalc_Empty_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1695, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTradeMasterFile,
            this.menuTradeChallenge});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(50, 20);
            this.toolStripMenuItem1.Text = "Menu";
            // 
            // menuTradeMasterFile
            // 
            this.menuTradeMasterFile.Name = "menuTradeMasterFile";
            this.menuTradeMasterFile.Size = new System.Drawing.Size(162, 22);
            this.menuTradeMasterFile.Text = "Trade Master File";
            this.menuTradeMasterFile.Click += new System.EventHandler(this.menuTradeMasterFile_Click);
            // 
            // menuTradeChallenge
            // 
            this.menuTradeChallenge.Name = "menuTradeChallenge";
            this.menuTradeChallenge.Size = new System.Drawing.Size(162, 22);
            this.menuTradeChallenge.Text = "Trade Challenge";
            this.menuTradeChallenge.Click += new System.EventHandler(this.menuTradeChallenge_Click);
            // 
            // dgvUnofficial
            // 
            this.dgvUnofficial.AllowUserToAddRows = false;
            this.dgvUnofficial.AllowUserToDeleteRows = false;
            this.dgvUnofficial.AllowUserToResizeRows = false;
            this.dgvUnofficial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnofficial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnofficial.Location = new System.Drawing.Point(33, 422);
            this.dgvUnofficial.Name = "dgvUnofficial";
            this.dgvUnofficial.ReadOnly = true;
            this.dgvUnofficial.RowTemplate.Height = 25;
            this.dgvUnofficial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnofficial.Size = new System.Drawing.Size(1517, 438);
            this.dgvUnofficial.TabIndex = 2;
            this.dgvUnofficial.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUnofficial_CellDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvUnofficial, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dgvTrades_Open, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(30, 0, 10, 0);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1563, 924);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // dgvTrades_Open
            // 
            this.dgvTrades_Open.AllowUserToAddRows = false;
            this.dgvTrades_Open.AllowUserToDeleteRows = false;
            this.dgvTrades_Open.AllowUserToResizeRows = false;
            this.dgvTrades_Open.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrades_Open.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrades_Open.Location = new System.Drawing.Point(33, 63);
            this.dgvTrades_Open.Name = "dgvTrades_Open";
            this.dgvTrades_Open.ReadOnly = true;
            this.dgvTrades_Open.RowTemplate.Height = 25;
            this.dgvTrades_Open.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrades_Open.Size = new System.Drawing.Size(1517, 290);
            this.dgvTrades_Open.TabIndex = 7;
            this.dgvTrades_Open.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrades_Open_CellDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(33, 359);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1517, 57);
            this.panel2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(3, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Unofficial";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(33, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1517, 54);
            this.panel3.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(3, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Official Trade - Open";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 948);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1695, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusMessage
            // 
            this.statusMessage.BackColor = System.Drawing.Color.Transparent;
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(106, 17);
            this.statusMessage.Text = "Status message . . .";
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1695, 970);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDashboard";
            this.Text = "Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDashboard_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDashboard_FormClosed);
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnofficial)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrades_Open)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridView dgvUnofficial;
        private System.Windows.Forms.Button btnOpenCalcLong_Empty;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTrades_Open;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuTradeMasterFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnOpenCalcShort_Empty;
        private System.Windows.Forms.ToolStripMenuItem menuTradeChallenge;
    }
}