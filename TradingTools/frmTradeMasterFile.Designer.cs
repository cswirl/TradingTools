
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
            this.cbCorrection = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnViewCalculator = new System.Windows.Forms.Button();
            this.cbxFilterStatus = new System.Windows.Forms.ComboBox();
            this.FILTER = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtTradeNum = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtTicker = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxTradingStyle = new System.Windows.Forms.ComboBox();
            this.txtLeverage = new System.Windows.Forms.TextBox();
            this.panelTradeClosed = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFinalCapital = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDateExit = new System.Windows.Forms.DateTimePicker();
            this.txtExitPrice = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label39 = new System.Windows.Forms.Label();
            this.txtPnL = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.txtPnL_percentage = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.txtPCP = new System.Windows.Forms.TextBox();
            this.txtFinalPositionValue = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtEntryPrice = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtLotSize = new System.Windows.Forms.TextBox();
            this.dtpDateEnter = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCapital = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dgvTrades = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelTradeClosed.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.cbCorrection);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnViewCalculator);
            this.panel1.Controls.Add(this.cbxFilterStatus);
            this.panel1.Controls.Add(this.FILTER);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(242, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1242, 76);
            this.panel1.TabIndex = 0;
            // 
            // cbCorrection
            // 
            this.cbCorrection.AutoSize = true;
            this.cbCorrection.Location = new System.Drawing.Point(7, 50);
            this.cbCorrection.Name = "cbCorrection";
            this.cbCorrection.Size = new System.Drawing.Size(82, 19);
            this.cbCorrection.TabIndex = 5;
            this.cbCorrection.Text = "Correction";
            this.cbCorrection.UseVisualStyleBackColor = true;
            this.cbCorrection.CheckedChanged += new System.EventHandler(this.cbEditMode_CheckedChanged);
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
            // cbxFilterStatus
            // 
            this.cbxFilterStatus.BackColor = System.Drawing.Color.White;
            this.cbxFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFilterStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxFilterStatus.FormattingEnabled = true;
            this.cbxFilterStatus.Location = new System.Drawing.Point(53, 10);
            this.cbxFilterStatus.Name = "cbxFilterStatus";
            this.cbxFilterStatus.Size = new System.Drawing.Size(94, 23);
            this.cbxFilterStatus.TabIndex = 1;
            this.cbxFilterStatus.TabStop = false;
            this.cbxFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cmbFilterStatus_SelectedIndexChanged);
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
            this.txtTradeNum.Location = new System.Drawing.Point(111, 76);
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
            this.label22.Location = new System.Drawing.Point(36, 79);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(69, 15);
            this.label22.TabIndex = 22;
            this.label22.Text = "TRADE NO.";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTicker
            // 
            this.txtTicker.Location = new System.Drawing.Point(111, 105);
            this.txtTicker.Name = "txtTicker";
            this.txtTicker.ReadOnly = true;
            this.txtTicker.Size = new System.Drawing.Size(111, 23);
            this.txtTicker.TabIndex = 1;
            this.txtTicker.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTicker.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Ticker_Validating);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(36, 112);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(69, 15);
            this.label30.TabIndex = 27;
            this.label30.Text = "Ticker / Pair";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cbxTradingStyle);
            this.panel3.Controls.Add(this.txtLeverage);
            this.panel3.Controls.Add(this.panelTradeClosed);
            this.panel3.Controls.Add(this.txtEntryPrice);
            this.panel3.Controls.Add(this.btnUpdate);
            this.panel3.Controls.Add(this.txtLotSize);
            this.panel3.Controls.Add(this.dtpDateEnter);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtTicker);
            this.panel3.Controls.Add(this.label30);
            this.panel3.Controls.Add(this.txtCapital);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.txtTradeNum);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(242, 794);
            this.panel3.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 42;
            this.label1.Text = "Trading Style";
            // 
            // cbxTradingStyle
            // 
            this.cbxTradingStyle.BackColor = System.Drawing.Color.White;
            this.cbxTradingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTradingStyle.Enabled = false;
            this.cbxTradingStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxTradingStyle.FormattingEnabled = true;
            this.cbxTradingStyle.Location = new System.Drawing.Point(111, 322);
            this.cbxTradingStyle.Name = "cbxTradingStyle";
            this.cbxTradingStyle.Size = new System.Drawing.Size(111, 23);
            this.cbxTradingStyle.TabIndex = 7;
            // 
            // txtLeverage
            // 
            this.txtLeverage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtLeverage.ForeColor = System.Drawing.Color.Red;
            this.txtLeverage.Location = new System.Drawing.Point(111, 293);
            this.txtLeverage.Name = "txtLeverage";
            this.txtLeverage.ReadOnly = true;
            this.txtLeverage.Size = new System.Drawing.Size(111, 23);
            this.txtLeverage.TabIndex = 6;
            this.txtLeverage.Tag = "";
            this.txtLeverage.Text = "1";
            this.txtLeverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLeverage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtLeverage.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Decimal_Validating);
            // 
            // panelTradeClosed
            // 
            this.panelTradeClosed.BackColor = System.Drawing.SystemColors.Control;
            this.panelTradeClosed.Controls.Add(this.label4);
            this.panelTradeClosed.Controls.Add(this.txtFinalCapital);
            this.panelTradeClosed.Controls.Add(this.label57);
            this.panelTradeClosed.Controls.Add(this.label3);
            this.panelTradeClosed.Controls.Add(this.dtpDateExit);
            this.panelTradeClosed.Controls.Add(this.txtExitPrice);
            this.panelTradeClosed.Controls.Add(this.tableLayoutPanel1);
            this.panelTradeClosed.Controls.Add(this.txtFinalPositionValue);
            this.panelTradeClosed.Controls.Add(this.label27);
            this.panelTradeClosed.Location = new System.Drawing.Point(3, 365);
            this.panelTradeClosed.Name = "panelTradeClosed";
            this.panelTradeClosed.Size = new System.Drawing.Size(233, 312);
            this.panelTradeClosed.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 15);
            this.label4.TabIndex = 41;
            this.label4.Text = "Exit Price (Avg)";
            // 
            // txtFinalCapital
            // 
            this.txtFinalCapital.Location = new System.Drawing.Point(108, 90);
            this.txtFinalCapital.Name = "txtFinalCapital";
            this.txtFinalCapital.ReadOnly = true;
            this.txtFinalCapital.Size = new System.Drawing.Size(111, 23);
            this.txtFinalCapital.TabIndex = 10;
            this.txtFinalCapital.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFinalCapital.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtFinalCapital.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Money_Validating);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(30, 93);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(72, 15);
            this.label57.TabIndex = 40;
            this.label57.Text = "Final Capital";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "Date Exit";
            // 
            // dtpDateExit
            // 
            this.dtpDateExit.Enabled = false;
            this.dtpDateExit.Location = new System.Drawing.Point(19, 27);
            this.dtpDateExit.Name = "dtpDateExit";
            this.dtpDateExit.Size = new System.Drawing.Size(200, 23);
            this.dtpDateExit.TabIndex = 8;
            // 
            // txtExitPrice
            // 
            this.txtExitPrice.Location = new System.Drawing.Point(108, 61);
            this.txtExitPrice.Name = "txtExitPrice";
            this.txtExitPrice.ReadOnly = true;
            this.txtExitPrice.Size = new System.Drawing.Size(111, 23);
            this.txtExitPrice.TabIndex = 9;
            this.txtExitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExitPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtExitPrice.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Decimal_Validating);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.5814F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.76744F));
            this.tableLayoutPanel1.Controls.Add(this.label39, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPnL, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label38, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPnL_percentage, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label51, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPCP, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 186);
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
            this.label39.Size = new System.Drawing.Size(27, 15);
            this.label39.TabIndex = 0;
            this.label39.Text = "PnL";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPnL
            // 
            this.txtPnL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPnL.Location = new System.Drawing.Point(110, 30);
            this.txtPnL.Name = "txtPnL";
            this.txtPnL.ReadOnly = true;
            this.txtPnL.Size = new System.Drawing.Size(102, 23);
            this.txtPnL.TabIndex = 2;
            this.txtPnL.TabStop = false;
            this.txtPnL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // txtPnL_percentage
            // 
            this.txtPnL_percentage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPnL_percentage.Location = new System.Drawing.Point(56, 30);
            this.txtPnL_percentage.Name = "txtPnL_percentage";
            this.txtPnL_percentage.ReadOnly = true;
            this.txtPnL_percentage.Size = new System.Drawing.Size(48, 23);
            this.txtPnL_percentage.TabIndex = 1;
            this.txtPnL_percentage.TabStop = false;
            this.txtPnL_percentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // txtPCP
            // 
            this.txtPCP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPCP.Location = new System.Drawing.Point(3, 30);
            this.txtPCP.Name = "txtPCP";
            this.txtPCP.ReadOnly = true;
            this.txtPCP.Size = new System.Drawing.Size(47, 23);
            this.txtPCP.TabIndex = 1;
            this.txtPCP.TabStop = false;
            this.txtPCP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFinalPositionValue
            // 
            this.txtFinalPositionValue.Location = new System.Drawing.Point(125, 252);
            this.txtFinalPositionValue.Name = "txtFinalPositionValue";
            this.txtFinalPositionValue.ReadOnly = true;
            this.txtFinalPositionValue.Size = new System.Drawing.Size(92, 23);
            this.txtFinalPositionValue.TabIndex = 32;
            this.txtFinalPositionValue.TabStop = false;
            this.txtFinalPositionValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(10, 255);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(109, 15);
            this.label27.TabIndex = 37;
            this.label27.Text = "Final Position Value";
            // 
            // txtEntryPrice
            // 
            this.txtEntryPrice.Location = new System.Drawing.Point(111, 264);
            this.txtEntryPrice.Name = "txtEntryPrice";
            this.txtEntryPrice.ReadOnly = true;
            this.txtEntryPrice.Size = new System.Drawing.Size(111, 23);
            this.txtEntryPrice.TabIndex = 5;
            this.txtEntryPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEntryPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtEntryPrice.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Decimal_Validating);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(12, 13);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(210, 38);
            this.btnUpdate.TabIndex = 39;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtLotSize
            // 
            this.txtLotSize.Location = new System.Drawing.Point(111, 235);
            this.txtLotSize.Name = "txtLotSize";
            this.txtLotSize.ReadOnly = true;
            this.txtLotSize.Size = new System.Drawing.Size(111, 23);
            this.txtLotSize.TabIndex = 4;
            this.txtLotSize.TabStop = false;
            this.txtLotSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLotSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtLotSize.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Decimal_Validating);
            // 
            // dtpDateEnter
            // 
            this.dtpDateEnter.Enabled = false;
            this.dtpDateEnter.Location = new System.Drawing.Point(22, 165);
            this.dtpDateEnter.Name = "dtpDateEnter";
            this.dtpDateEnter.Size = new System.Drawing.Size(200, 23);
            this.dtpDateEnter.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 267);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "Entry Price (Avg)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 15);
            this.label5.TabIndex = 31;
            this.label5.Text = "Lot Size";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCapital
            // 
            this.txtCapital.Location = new System.Drawing.Point(111, 206);
            this.txtCapital.Name = "txtCapital";
            this.txtCapital.ReadOnly = true;
            this.txtCapital.Size = new System.Drawing.Size(111, 23);
            this.txtCapital.TabIndex = 3;
            this.txtCapital.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCapital.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtCapital.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Money_Validating);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label26.Location = new System.Drawing.Point(46, 296);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(59, 15);
            this.label26.TabIndex = 35;
            this.label26.Text = "Leverage";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 28;
            this.label2.Text = "Date Enter";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(61, 209);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 15);
            this.label15.TabIndex = 36;
            this.label15.Text = "Capital";
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
            this.dgvTrades.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrades_CellDoubleClick);
            this.dgvTrades.SelectionChanged += new System.EventHandler(this.dgvTrades_SelectionChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1510, 24);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
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
            this.panelTradeClosed.ResumeLayout(false);
            this.panelTradeClosed.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
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
        private System.Windows.Forms.TextBox txtPnL;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txtPnL_percentage;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txtPCP;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtFinalPositionValue;
        private System.Windows.Forms.ComboBox cbxFilterStatus;
        private System.Windows.Forms.Label FILTER;
        private System.Windows.Forms.DataGridView dgvTrades;
        private System.Windows.Forms.Button btnViewCalculator;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel panelTradeClosed;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtLeverage;
        private System.Windows.Forms.TextBox txtEntryPrice;
        private System.Windows.Forms.TextBox txtLotSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCapital;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox cbCorrection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxTradingStyle;
        private System.Windows.Forms.TextBox txtFinalCapital;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox txtExitPrice;
        private System.Windows.Forms.Label label4;
    }
}