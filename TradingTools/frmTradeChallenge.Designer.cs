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
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioClosed = new System.Windows.Forms.RadioButton();
            this.radioOpen = new System.Windows.Forms.RadioButton();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.btnCompleted = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtCap = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel_LongShortButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnOpenCalcShort_Empty = new System.Windows.Forms.Button();
            this.btnOpenCalcLong_Empty = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvTradeHistory = new System.Windows.Forms.DataGridView();
            this.dgvActiveTrade = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvProspects = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel_LongShortButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTradeHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveTrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProspects)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(42, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Active Trade";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.radioClosed);
            this.panel1.Controls.Add(this.radioOpen);
            this.panel1.Controls.Add(this.txtDesc);
            this.panel1.Controls.Add(this.btnCompleted);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.txtId);
            this.panel1.Controls.Add(this.txtTitle);
            this.panel1.Controls.Add(this.txtCap);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(42, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 95);
            this.panel1.TabIndex = 13;
            // 
            // radioClosed
            // 
            this.radioClosed.AutoSize = true;
            this.radioClosed.Enabled = false;
            this.radioClosed.Location = new System.Drawing.Point(246, 35);
            this.radioClosed.Name = "radioClosed";
            this.radioClosed.Size = new System.Drawing.Size(59, 19);
            this.radioClosed.TabIndex = 3;
            this.radioClosed.Text = "closed";
            this.radioClosed.UseVisualStyleBackColor = true;
            // 
            // radioOpen
            // 
            this.radioOpen.AutoSize = true;
            this.radioOpen.Checked = true;
            this.radioOpen.Enabled = false;
            this.radioOpen.Location = new System.Drawing.Point(246, 10);
            this.radioOpen.Name = "radioOpen";
            this.radioOpen.Size = new System.Drawing.Size(52, 19);
            this.radioOpen.TabIndex = 2;
            this.radioOpen.TabStop = true;
            this.radioOpen.Text = "open";
            this.radioOpen.UseVisualStyleBackColor = true;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(409, 8);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(249, 80);
            this.txtDesc.TabIndex = 4;
            // 
            // btnCompleted
            // 
            this.btnCompleted.Location = new System.Drawing.Point(701, 38);
            this.btnCompleted.Name = "btnCompleted";
            this.btnCompleted.Size = new System.Drawing.Size(75, 23);
            this.btnCompleted.TabIndex = 6;
            this.btnCompleted.Text = "Completed";
            this.btnCompleted.UseVisualStyleBackColor = true;
            this.btnCompleted.Click += new System.EventHandler(this.btnCompleted_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(701, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(78, 6);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(49, 23);
            this.txtId.TabIndex = 1;
            this.txtId.TabStop = false;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(78, 64);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(297, 23);
            this.txtTitle.TabIndex = 1;
            // 
            // txtCap
            // 
            this.txtCap.Location = new System.Drawing.Point(78, 35);
            this.txtCap.Name = "txtCap";
            this.txtCap.Size = new System.Drawing.Size(49, 23);
            this.txtCap.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(52, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Title";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(201, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trade Cap";
            // 
            // tableLayoutPanel_LongShortButtons
            // 
            this.tableLayoutPanel_LongShortButtons.ColumnCount = 2;
            this.tableLayoutPanel_LongShortButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_LongShortButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_LongShortButtons.Controls.Add(this.btnOpenCalcShort_Empty, 0, 0);
            this.tableLayoutPanel_LongShortButtons.Controls.Add(this.btnOpenCalcLong_Empty, 0, 0);
            this.tableLayoutPanel_LongShortButtons.Location = new System.Drawing.Point(865, 24);
            this.tableLayoutPanel_LongShortButtons.Name = "tableLayoutPanel_LongShortButtons";
            this.tableLayoutPanel_LongShortButtons.RowCount = 1;
            this.tableLayoutPanel_LongShortButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_LongShortButtons.Size = new System.Drawing.Size(200, 66);
            this.tableLayoutPanel_LongShortButtons.TabIndex = 0;
            // 
            // btnOpenCalcShort_Empty
            // 
            this.btnOpenCalcShort_Empty.BackColor = System.Drawing.Color.Red;
            this.btnOpenCalcShort_Empty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenCalcShort_Empty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenCalcShort_Empty.ForeColor = System.Drawing.Color.White;
            this.btnOpenCalcShort_Empty.Location = new System.Drawing.Point(103, 3);
            this.btnOpenCalcShort_Empty.Name = "btnOpenCalcShort_Empty";
            this.btnOpenCalcShort_Empty.Size = new System.Drawing.Size(94, 60);
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
            this.btnOpenCalcLong_Empty.Size = new System.Drawing.Size(94, 60);
            this.btnOpenCalcLong_Empty.TabIndex = 7;
            this.btnOpenCalcLong_Empty.Text = "NEW LONG";
            this.btnOpenCalcLong_Empty.UseVisualStyleBackColor = false;
            this.btnOpenCalcLong_Empty.Click += new System.EventHandler(this.btnOpenCalcLong_Empty_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(42, 540);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Trade History";
            // 
            // dgvTradeHistory
            // 
            this.dgvTradeHistory.AllowUserToAddRows = false;
            this.dgvTradeHistory.AllowUserToDeleteRows = false;
            this.dgvTradeHistory.AllowUserToResizeRows = false;
            this.dgvTradeHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTradeHistory.Location = new System.Drawing.Point(42, 569);
            this.dgvTradeHistory.Name = "dgvTradeHistory";
            this.dgvTradeHistory.ReadOnly = true;
            this.dgvTradeHistory.RowTemplate.Height = 25;
            this.dgvTradeHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTradeHistory.Size = new System.Drawing.Size(803, 241);
            this.dgvTradeHistory.TabIndex = 14;
            // 
            // dgvActiveTrade
            // 
            this.dgvActiveTrade.AllowUserToAddRows = false;
            this.dgvActiveTrade.AllowUserToDeleteRows = false;
            this.dgvActiveTrade.AllowUserToResizeRows = false;
            this.dgvActiveTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActiveTrade.Location = new System.Drawing.Point(42, 181);
            this.dgvActiveTrade.Name = "dgvActiveTrade";
            this.dgvActiveTrade.ReadOnly = true;
            this.dgvActiveTrade.RowTemplate.Height = 25;
            this.dgvActiveTrade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActiveTrade.Size = new System.Drawing.Size(803, 119);
            this.dgvActiveTrade.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new System.Drawing.Point(42, 332);
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
            this.dgvProspects.Location = new System.Drawing.Point(42, 361);
            this.dgvProspects.Name = "dgvProspects";
            this.dgvProspects.ReadOnly = true;
            this.dgvProspects.RowTemplate.Height = 25;
            this.dgvProspects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProspects.Size = new System.Drawing.Size(803, 150);
            this.dgvProspects.TabIndex = 14;
            // 
            // frmTradeChallenge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1105, 866);
            this.Controls.Add(this.dgvActiveTrade);
            this.Controls.Add(this.dgvProspects);
            this.Controls.Add(this.dgvTradeHistory);
            this.Controls.Add(this.tableLayoutPanel_LongShortButtons);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmTradeChallenge";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Trade Challenge";
            this.Load += new System.EventHandler(this.frmTradeChallenge_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel_LongShortButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTradeHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveTrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProspects)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_LongShortButtons;
        private System.Windows.Forms.Button btnOpenCalcLong_Empty;
        private System.Windows.Forms.Button btnOpenCalcShort_Empty;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtCap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCompleted;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvTradeHistory;
        private System.Windows.Forms.DataGridView dgvActiveTrade;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvProspects;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioClosed;
        private System.Windows.Forms.RadioButton radioOpen;
    }
}