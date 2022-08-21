namespace TradingTools
{
    partial class frmTradeChallenge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTradeChallenge));
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtTargetPercentage = new System.Windows.Forms.TextBox();
            this.txtCap = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCompleted = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel_LongShortButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnOpenCalcShort_Empty = new System.Windows.Forms.Button();
            this.btnOpenCalcLong_Empty = new System.Windows.Forms.Button();
            this.lblTradeHist = new System.Windows.Forms.Label();
            this.dgvTradeHistory = new System.Windows.Forms.DataGridView();
            this.dgvActiveTrade = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvProspects = new System.Windows.Forms.DataGridView();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.monthCalendarDateEnter = new System.Windows.Forms.MonthCalendar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtInsight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel_LongShortButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTradeHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveTrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProspects)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(371, 335);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Active Trade";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtId);
            this.panel1.Controls.Add(this.txtStatus);
            this.panel1.Controls.Add(this.txtTargetPercentage);
            this.panel1.Controls.Add(this.txtCap);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(42, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 139);
            this.panel1.TabIndex = 13;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(78, 15);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(74, 23);
            this.txtId.TabIndex = 1;
            this.txtId.TabStop = false;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(78, 102);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(74, 23);
            this.txtStatus.TabIndex = 1;
            this.txtStatus.TabStop = false;
            this.txtStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStatus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCap_KeyPress);
            // 
            // txtTargetPercentage
            // 
            this.txtTargetPercentage.Location = new System.Drawing.Point(78, 73);
            this.txtTargetPercentage.Name = "txtTargetPercentage";
            this.txtTargetPercentage.Size = new System.Drawing.Size(74, 23);
            this.txtTargetPercentage.TabIndex = 1;
            this.txtTargetPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTargetPercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTargetPercentage_KeyPress);
            // 
            // txtCap
            // 
            this.txtCap.Location = new System.Drawing.Point(78, 44);
            this.txtCap.Name = "txtCap";
            this.txtCap.Size = new System.Drawing.Size(74, 23);
            this.txtCap.TabIndex = 0;
            this.txtCap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCap_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(52, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Target %";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trade Cap";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(371, 86);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(219, 202);
            this.txtDesc.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(371, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Description";
            // 
            // btnCompleted
            // 
            this.btnCompleted.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCompleted.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCompleted.ForeColor = System.Drawing.Color.White;
            this.btnCompleted.Location = new System.Drawing.Point(237, 165);
            this.btnCompleted.Name = "btnCompleted";
            this.btnCompleted.Size = new System.Drawing.Size(128, 38);
            this.btnCompleted.TabIndex = 6;
            this.btnCompleted.Text = "END Challenge";
            this.btnCompleted.UseVisualStyleBackColor = false;
            this.btnCompleted.Click += new System.EventHandler(this.btnCompleted_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(237, 125);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 34);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel_LongShortButtons
            // 
            this.tableLayoutPanel_LongShortButtons.ColumnCount = 2;
            this.tableLayoutPanel_LongShortButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_LongShortButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_LongShortButtons.Controls.Add(this.btnOpenCalcShort_Empty, 0, 0);
            this.tableLayoutPanel_LongShortButtons.Controls.Add(this.btnOpenCalcLong_Empty, 0, 0);
            this.tableLayoutPanel_LongShortButtons.Location = new System.Drawing.Point(641, 310);
            this.tableLayoutPanel_LongShortButtons.Name = "tableLayoutPanel_LongShortButtons";
            this.tableLayoutPanel_LongShortButtons.RowCount = 1;
            this.tableLayoutPanel_LongShortButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_LongShortButtons.Size = new System.Drawing.Size(266, 39);
            this.tableLayoutPanel_LongShortButtons.TabIndex = 0;
            // 
            // btnOpenCalcShort_Empty
            // 
            this.btnOpenCalcShort_Empty.BackColor = System.Drawing.Color.Red;
            this.btnOpenCalcShort_Empty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenCalcShort_Empty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenCalcShort_Empty.ForeColor = System.Drawing.Color.White;
            this.btnOpenCalcShort_Empty.Location = new System.Drawing.Point(136, 3);
            this.btnOpenCalcShort_Empty.Name = "btnOpenCalcShort_Empty";
            this.btnOpenCalcShort_Empty.Size = new System.Drawing.Size(127, 33);
            this.btnOpenCalcShort_Empty.TabIndex = 8;
            this.btnOpenCalcShort_Empty.Text = "NEW SHORT";
            this.btnOpenCalcShort_Empty.UseVisualStyleBackColor = false;
            this.btnOpenCalcShort_Empty.Click += new System.EventHandler(this.btnOpenCalcShort_Empty_Click);
            // 
            // btnOpenCalcLong_Empty
            // 
            this.btnOpenCalcLong_Empty.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnOpenCalcLong_Empty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenCalcLong_Empty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenCalcLong_Empty.ForeColor = System.Drawing.Color.White;
            this.btnOpenCalcLong_Empty.Location = new System.Drawing.Point(3, 3);
            this.btnOpenCalcLong_Empty.Name = "btnOpenCalcLong_Empty";
            this.btnOpenCalcLong_Empty.Size = new System.Drawing.Size(127, 33);
            this.btnOpenCalcLong_Empty.TabIndex = 7;
            this.btnOpenCalcLong_Empty.Text = "NEW LONG";
            this.btnOpenCalcLong_Empty.UseVisualStyleBackColor = false;
            this.btnOpenCalcLong_Empty.Click += new System.EventHandler(this.btnOpenCalcLong_Empty_Click);
            // 
            // lblTradeHist
            // 
            this.lblTradeHist.AutoSize = true;
            this.lblTradeHist.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTradeHist.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblTradeHist.Location = new System.Drawing.Point(42, 461);
            this.lblTradeHist.Name = "lblTradeHist";
            this.lblTradeHist.Size = new System.Drawing.Size(104, 20);
            this.lblTradeHist.TabIndex = 11;
            this.lblTradeHist.Text = "Trade History";
            // 
            // dgvTradeHistory
            // 
            this.dgvTradeHistory.AllowUserToAddRows = false;
            this.dgvTradeHistory.AllowUserToDeleteRows = false;
            this.dgvTradeHistory.AllowUserToResizeRows = false;
            this.dgvTradeHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTradeHistory.Location = new System.Drawing.Point(42, 484);
            this.dgvTradeHistory.Name = "dgvTradeHistory";
            this.dgvTradeHistory.ReadOnly = true;
            this.dgvTradeHistory.RowTemplate.Height = 25;
            this.dgvTradeHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTradeHistory.Size = new System.Drawing.Size(865, 405);
            this.dgvTradeHistory.TabIndex = 14;
            this.dgvTradeHistory.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrades_CellDoubleClick);
            this.dgvTradeHistory.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvTradeHistory_RowPostPaint);
            // 
            // dgvActiveTrade
            // 
            this.dgvActiveTrade.AllowUserToAddRows = false;
            this.dgvActiveTrade.AllowUserToDeleteRows = false;
            this.dgvActiveTrade.AllowUserToResizeRows = false;
            this.dgvActiveTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActiveTrade.Location = new System.Drawing.Point(371, 358);
            this.dgvActiveTrade.Name = "dgvActiveTrade";
            this.dgvActiveTrade.ReadOnly = true;
            this.dgvActiveTrade.RowTemplate.Height = 25;
            this.dgvActiveTrade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActiveTrade.Size = new System.Drawing.Size(536, 67);
            this.dgvActiveTrade.TabIndex = 15;
            this.dgvActiveTrade.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrades_CellDoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new System.Drawing.Point(42, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Prospects";
            // 
            // dgvProspects
            // 
            this.dgvProspects.AllowUserToAddRows = false;
            this.dgvProspects.AllowUserToDeleteRows = false;
            this.dgvProspects.AllowUserToResizeRows = false;
            this.dgvProspects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProspects.Location = new System.Drawing.Point(42, 250);
            this.dgvProspects.Name = "dgvProspects";
            this.dgvProspects.ReadOnly = true;
            this.dgvProspects.RowTemplate.Height = 25;
            this.dgvProspects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProspects.Size = new System.Drawing.Size(275, 175);
            this.dgvProspects.TabIndex = 14;
            this.dgvProspects.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProspects_CellDoubleClick);
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.SystemColors.Control;
            this.txtTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.txtTitle.Location = new System.Drawing.Point(42, 28);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(323, 33);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.Text = "25 Trade Challenge";
            this.txtTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // monthCalendarDateEnter
            // 
            this.monthCalendarDateEnter.BackColor = System.Drawing.SystemColors.Window;
            this.monthCalendarDateEnter.CalendarDimensions = new System.Drawing.Size(1, 6);
            this.monthCalendarDateEnter.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.monthCalendarDateEnter.Location = new System.Drawing.Point(919, 20);
            this.monthCalendarDateEnter.Name = "monthCalendarDateEnter";
            this.monthCalendarDateEnter.ScrollChange = 6;
            this.monthCalendarDateEnter.ShowToday = false;
            this.monthCalendarDateEnter.TabIndex = 18;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 940);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusMessage
            // 
            this.statusMessage.BackColor = System.Drawing.Color.Transparent;
            this.statusMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(63, 17);
            this.statusMessage.Text = "status . . .";
            // 
            // txtInsight
            // 
            this.txtInsight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtInsight.Location = new System.Drawing.Point(596, 67);
            this.txtInsight.Multiline = true;
            this.txtInsight.Name = "txtInsight";
            this.txtInsight.ReadOnly = true;
            this.txtInsight.Size = new System.Drawing.Size(311, 221);
            this.txtInsight.TabIndex = 2;
            this.txtInsight.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(596, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Insight Report";
            // 
            // frmTradeChallenge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1184, 962);
            this.Controls.Add(this.txtInsight);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvActiveTrade);
            this.Controls.Add(this.dgvProspects);
            this.Controls.Add(this.dgvTradeHistory);
            this.Controls.Add(this.btnCompleted);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tableLayoutPanel_LongShortButtons);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTradeHist);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.monthCalendarDateEnter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmTradeChallenge";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Trade Challenge";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTradeChallenge_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTradeChallenge_FormClosed);
            this.Load += new System.EventHandler(this.frmTradeChallenge_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel_LongShortButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTradeHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveTrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProspects)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_LongShortButtons;
        private System.Windows.Forms.Button btnOpenCalcLong_Empty;
        private System.Windows.Forms.Button btnOpenCalcShort_Empty;
        private System.Windows.Forms.TextBox txtCap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCompleted;
        private System.Windows.Forms.Label lblTradeHist;
        private System.Windows.Forms.DataGridView dgvTradeHistory;
        private System.Windows.Forms.DataGridView dgvActiveTrade;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvProspects;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.MonthCalendar monthCalendarDateEnter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtTargetPercentage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInsight;
        private System.Windows.Forms.Label label5;
    }
}