
namespace TradingTools
{
    partial class frmCalculatorStates
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnOpenCalc_Empty = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTradeMasterFile = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvUnofficial = new System.Windows.Forms.DataGridView();
            this.btnViewUnofficial_Loaded = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvTrades_Open = new System.Windows.Forms.DataGridView();
            this.btnViewOfficial = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnofficial)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrades_Open)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1176, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(112, 642);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.btnOpenCalc_Empty);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(112, 60);
            this.panel4.TabIndex = 0;
            // 
            // btnOpenCalc_Empty
            // 
            this.btnOpenCalc_Empty.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnOpenCalc_Empty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenCalc_Empty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenCalc_Empty.ForeColor = System.Drawing.Color.White;
            this.btnOpenCalc_Empty.Location = new System.Drawing.Point(3, 3);
            this.btnOpenCalc_Empty.Name = "btnOpenCalc_Empty";
            this.btnOpenCalc_Empty.Size = new System.Drawing.Size(106, 54);
            this.btnOpenCalc_Empty.TabIndex = 5;
            this.btnOpenCalc_Empty.Text = "CREATE NEW";
            this.btnOpenCalc_Empty.UseVisualStyleBackColor = false;
            this.btnOpenCalc_Empty.Click += new System.EventHandler(this.btnOpenCalc_Empty_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1288, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTradeMasterFile});
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
            // dgvUnofficial
            // 
            this.dgvUnofficial.AllowUserToAddRows = false;
            this.dgvUnofficial.AllowUserToDeleteRows = false;
            this.dgvUnofficial.AllowUserToResizeRows = false;
            this.dgvUnofficial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgvUnofficial, 2);
            this.dgvUnofficial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnofficial.Location = new System.Drawing.Point(13, 408);
            this.dgvUnofficial.Name = "dgvUnofficial";
            this.dgvUnofficial.ReadOnly = true;
            this.dgvUnofficial.RowTemplate.Height = 25;
            this.dgvUnofficial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnofficial.Size = new System.Drawing.Size(1150, 170);
            this.dgvUnofficial.TabIndex = 2;
            // 
            // btnViewUnofficial_Loaded
            // 
            this.btnViewUnofficial_Loaded.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewUnofficial_Loaded.Location = new System.Drawing.Point(898, 584);
            this.btnViewUnofficial_Loaded.Name = "btnViewUnofficial_Loaded";
            this.btnViewUnofficial_Loaded.Size = new System.Drawing.Size(265, 31);
            this.btnViewUnofficial_Loaded.TabIndex = 4;
            this.btnViewUnofficial_Loaded.Text = "View Selected > > >";
            this.btnViewUnofficial_Loaded.UseVisualStyleBackColor = true;
            this.btnViewUnofficial_Loaded.Click += new System.EventHandler(this.btnOpenCalc_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgvUnofficial, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvTrades_Open, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnViewUnofficial_Loaded, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnViewOfficial, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1176, 642);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(13, 385);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Unofficial";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(13, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Official Trade - Open";
            // 
            // dgvTrades_Open
            // 
            this.dgvTrades_Open.AllowUserToAddRows = false;
            this.dgvTrades_Open.AllowUserToDeleteRows = false;
            this.dgvTrades_Open.AllowUserToResizeRows = false;
            this.dgvTrades_Open.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgvTrades_Open, 2);
            this.dgvTrades_Open.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrades_Open.Location = new System.Drawing.Point(13, 63);
            this.dgvTrades_Open.Name = "dgvTrades_Open";
            this.dgvTrades_Open.ReadOnly = true;
            this.dgvTrades_Open.RowTemplate.Height = 25;
            this.dgvTrades_Open.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrades_Open.Size = new System.Drawing.Size(1150, 259);
            this.dgvTrades_Open.TabIndex = 7;
            // 
            // btnViewOfficial
            // 
            this.btnViewOfficial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewOfficial.Location = new System.Drawing.Point(898, 328);
            this.btnViewOfficial.Name = "btnViewOfficial";
            this.btnViewOfficial.Size = new System.Drawing.Size(265, 30);
            this.btnViewOfficial.TabIndex = 10;
            this.btnViewOfficial.Text = "View Selected > > >";
            this.btnViewOfficial.UseVisualStyleBackColor = true;
            this.btnViewOfficial.Click += new System.EventHandler(this.btnViewOfficial_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 666);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1288, 22);
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
            // frmCalculatorStates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 688);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmCalculatorStates";
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCalculatorStates_FormClosed);
            this.Load += new System.EventHandler(this.frmCalculatorStates_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnofficial)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrades_Open)).EndInit();
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
        private System.Windows.Forms.Button btnViewUnofficial_Loaded;
        private System.Windows.Forms.Button btnOpenCalc_Empty;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTrades_Open;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnViewOfficial;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuTradeMasterFile;
    }
}