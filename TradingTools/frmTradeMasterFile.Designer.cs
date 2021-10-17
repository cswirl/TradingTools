
namespace TradingTools
{
    partial class frmTradeMasterFile
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnViewCalculator = new System.Windows.Forms.Button();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.FILTER = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtTradeNum = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtTicker = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label39 = new System.Windows.Forms.Label();
            this.txtPEP_Profit = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.txtPEP_RealProfit_percent = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.txtPEP_PCP = new System.Windows.Forms.TextBox();
            this.txtPEP_sPV = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.dtpDateExit = new System.Windows.Forms.DateTimePicker();
            this.dtpDateEnter = new System.Windows.Forms.DateTimePicker();
            this.txtPEP_AccountEquity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvTrades = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correrctionModeToggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrades)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnViewCalculator);
            this.panel1.Controls.Add(this.cmbFilterStatus);
            this.panel1.Controls.Add(this.FILTER);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(242, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1242, 76);
            this.panel1.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.Red;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(1086, 39);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(150, 31);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnViewCalculator
            // 
            this.btnViewCalculator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewCalculator.Location = new System.Drawing.Point(506, 40);
            this.btnViewCalculator.Name = "btnViewCalculator";
            this.btnViewCalculator.Size = new System.Drawing.Size(230, 31);
            this.btnViewCalculator.TabIndex = 3;
            this.btnViewCalculator.Text = "Selected Row >> View Calculator";
            this.btnViewCalculator.UseVisualStyleBackColor = true;
            this.btnViewCalculator.Click += new System.EventHandler(this.btnViewCalculator_Click);
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStatus.FormattingEnabled = true;
            this.cmbFilterStatus.Location = new System.Drawing.Point(53, 10);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(94, 23);
            this.cmbFilterStatus.TabIndex = 1;
            this.cmbFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cmbFilterStatus_SelectedIndexChanged);
            // 
            // FILTER
            // 
            this.FILTER.AutoSize = true;
            this.FILTER.Location = new System.Drawing.Point(7, 13);
            this.FILTER.Name = "FILTER";
            this.FILTER.Size = new System.Drawing.Size(40, 15);
            this.FILTER.TabIndex = 0;
            this.FILTER.Text = "FILTER";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1484, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(26, 794);
            this.panel2.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 818);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1510, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusMessage
            // 
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(106, 17);
            this.statusMessage.Text = "Status Message . . .";
            // 
            // txtTradeNum
            // 
            this.txtTradeNum.BackColor = System.Drawing.SystemColors.Control;
            this.txtTradeNum.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtTradeNum.ForeColor = System.Drawing.Color.Blue;
            this.txtTradeNum.Location = new System.Drawing.Point(104, 25);
            this.txtTradeNum.Name = "txtTradeNum";
            this.txtTradeNum.ReadOnly = true;
            this.txtTradeNum.Size = new System.Drawing.Size(111, 23);
            this.txtTradeNum.TabIndex = 12;
            this.txtTradeNum.TabStop = false;
            this.txtTradeNum.Text = "###";
            this.txtTradeNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label22.Location = new System.Drawing.Point(29, 28);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(69, 15);
            this.label22.TabIndex = 22;
            this.label22.Text = "TRADE NO.";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTicker
            // 
            this.txtTicker.Location = new System.Drawing.Point(104, 54);
            this.txtTicker.Name = "txtTicker";
            this.txtTicker.ReadOnly = true;
            this.txtTicker.Size = new System.Drawing.Size(111, 23);
            this.txtTicker.TabIndex = 26;
            this.txtTicker.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(29, 61);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(69, 15);
            this.label30.TabIndex = 27;
            this.label30.Text = "Ticker / Pair";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.btnUpdate);
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Controls.Add(this.txtPEP_sPV);
            this.panel3.Controls.Add(this.label27);
            this.panel3.Controls.Add(this.dtpDateExit);
            this.panel3.Controls.Add(this.dtpDateEnter);
            this.panel3.Controls.Add(this.txtTicker);
            this.panel3.Controls.Add(this.txtPEP_AccountEquity);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label28);
            this.panel3.Controls.Add(this.label30);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtTradeNum);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(242, 794);
            this.panel3.TabIndex = 26;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(122, 218);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(93, 23);
            this.btnUpdate.TabIndex = 39;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.5814F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.76744F));
            this.tableLayoutPanel1.Controls.Add(this.label39, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPEP_Profit, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label38, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPEP_RealProfit_percent, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label51, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPEP_PCP, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 271);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(215, 55);
            this.tableLayoutPanel1.TabIndex = 38;
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(110, 12);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(36, 15);
            this.label39.TabIndex = 0;
            this.label39.Text = "Profit";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPEP_Profit
            // 
            this.txtPEP_Profit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPEP_Profit.Location = new System.Drawing.Point(110, 30);
            this.txtPEP_Profit.Name = "txtPEP_Profit";
            this.txtPEP_Profit.ReadOnly = true;
            this.txtPEP_Profit.Size = new System.Drawing.Size(102, 23);
            this.txtPEP_Profit.TabIndex = 2;
            this.txtPEP_Profit.TabStop = false;
            this.txtPEP_Profit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(56, 12);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(40, 15);
            this.label38.TabIndex = 0;
            this.label38.Text = "PnL %";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPEP_RealProfit_percent
            // 
            this.txtPEP_RealProfit_percent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPEP_RealProfit_percent.Location = new System.Drawing.Point(56, 30);
            this.txtPEP_RealProfit_percent.Name = "txtPEP_RealProfit_percent";
            this.txtPEP_RealProfit_percent.ReadOnly = true;
            this.txtPEP_RealProfit_percent.Size = new System.Drawing.Size(48, 23);
            this.txtPEP_RealProfit_percent.TabIndex = 1;
            this.txtPEP_RealProfit_percent.TabStop = false;
            this.txtPEP_RealProfit_percent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label51
            // 
            this.label51.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(3, 12);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(29, 15);
            this.label51.TabIndex = 0;
            this.label51.Text = "PCP";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPEP_PCP
            // 
            this.txtPEP_PCP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPEP_PCP.Location = new System.Drawing.Point(3, 30);
            this.txtPEP_PCP.Name = "txtPEP_PCP";
            this.txtPEP_PCP.ReadOnly = true;
            this.txtPEP_PCP.Size = new System.Drawing.Size(47, 23);
            this.txtPEP_PCP.TabIndex = 1;
            this.txtPEP_PCP.TabStop = false;
            this.txtPEP_PCP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPEP_sPV
            // 
            this.txtPEP_sPV.Location = new System.Drawing.Point(130, 337);
            this.txtPEP_sPV.Name = "txtPEP_sPV";
            this.txtPEP_sPV.ReadOnly = true;
            this.txtPEP_sPV.Size = new System.Drawing.Size(92, 23);
            this.txtPEP_sPV.TabIndex = 32;
            this.txtPEP_sPV.TabStop = false;
            this.txtPEP_sPV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(15, 340);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(109, 15);
            this.label27.TabIndex = 37;
            this.label27.Text = "Final Position Value";
            // 
            // dtpDateExit
            // 
            this.dtpDateExit.Location = new System.Drawing.Point(15, 175);
            this.dtpDateExit.Name = "dtpDateExit";
            this.dtpDateExit.Size = new System.Drawing.Size(200, 23);
            this.dtpDateExit.TabIndex = 31;
            // 
            // dtpDateEnter
            // 
            this.dtpDateEnter.Location = new System.Drawing.Point(15, 114);
            this.dtpDateEnter.Name = "dtpDateEnter";
            this.dtpDateEnter.Size = new System.Drawing.Size(200, 23);
            this.dtpDateEnter.TabIndex = 30;
            // 
            // txtPEP_AccountEquity
            // 
            this.txtPEP_AccountEquity.Location = new System.Drawing.Point(130, 366);
            this.txtPEP_AccountEquity.Name = "txtPEP_AccountEquity";
            this.txtPEP_AccountEquity.ReadOnly = true;
            this.txtPEP_AccountEquity.Size = new System.Drawing.Size(92, 23);
            this.txtPEP_AccountEquity.TabIndex = 34;
            this.txtPEP_AccountEquity.TabStop = false;
            this.txtPEP_AccountEquity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "Date Exit";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(36, 369);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(88, 15);
            this.label28.TabIndex = 36;
            this.label28.Text = "Account Equity";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 28;
            this.label2.Text = "Date Enter";
            // 
            // dgvTrades
            // 
            this.dgvTrades.AllowUserToAddRows = false;
            this.dgvTrades.AllowUserToDeleteRows = false;
            this.dgvTrades.AllowUserToOrderColumns = true;
            this.dgvTrades.AllowUserToResizeRows = false;
            this.dgvTrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrades.Location = new System.Drawing.Point(242, 100);
            this.dgvTrades.MultiSelect = false;
            this.dgvTrades.Name = "dgvTrades";
            this.dgvTrades.ReadOnly = true;
            this.dgvTrades.RowTemplate.Height = 25;
            this.dgvTrades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrades.Size = new System.Drawing.Size(1242, 718);
            this.dgvTrades.TabIndex = 27;
            this.dgvTrades.SelectionChanged += new System.EventHandler(this.dgvTrades_SelectionChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1510, 24);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.correrctionModeToggleToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // correrctionModeToggleToolStripMenuItem
            // 
            this.correrctionModeToggleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem,
            this.offToolStripMenuItem});
            this.correrctionModeToggleToolStripMenuItem.Name = "correrctionModeToggleToolStripMenuItem";
            this.correrctionModeToggleToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.correrctionModeToggleToolStripMenuItem.Text = "Correrction Mode";
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.onToolStripMenuItem_Click);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.offToolStripMenuItem_Click);
            // 
            // frmTradeMasterFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1510, 840);
            this.Controls.Add(this.dgvTrades);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmTradeMasterFile";
            this.Text = "Trade Master File";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTradeMasterFile_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrades)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.TextBox txtTradeNum;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtTicker;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDateExit;
        private System.Windows.Forms.DateTimePicker dtpDateEnter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txtPEP_Profit;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txtPEP_RealProfit_percent;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txtPEP_PCP;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtPEP_sPV;
        private System.Windows.Forms.TextBox txtPEP_AccountEquity;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.Label FILTER;
        private System.Windows.Forms.DataGridView dgvTrades;
        private System.Windows.Forms.Button btnViewCalculator;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem correrctionModeToggleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.Button btnUpdate;
    }
}