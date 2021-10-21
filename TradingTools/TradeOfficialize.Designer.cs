
namespace TradingTools
{
    partial class TradeOfficialize
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
            this.dtpDateEnter = new System.Windows.Forms.DateTimePicker();
            this.txtTicker = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLeverage = new System.Windows.Forms.TextBox();
            this.txtEntryPrice = new System.Windows.Forms.TextBox();
            this.txtLotSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCapital = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnOfficialize = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDateEnter
            // 
            this.dtpDateEnter.Location = new System.Drawing.Point(29, 36);
            this.dtpDateEnter.Name = "dtpDateEnter";
            this.dtpDateEnter.Size = new System.Drawing.Size(181, 23);
            this.dtpDateEnter.TabIndex = 8;
            // 
            // txtTicker
            // 
            this.txtTicker.Location = new System.Drawing.Point(27, 113);
            this.txtTicker.Name = "txtTicker";
            this.txtTicker.Size = new System.Drawing.Size(111, 23);
            this.txtTicker.TabIndex = 1;
            this.txtTicker.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTicker.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Ticker_Validating);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(29, 95);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(69, 15);
            this.label30.TabIndex = 32;
            this.label30.Text = "Ticker / Pair";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 33;
            this.label2.Text = "Date Enter";
            // 
            // txtLeverage
            // 
            this.txtLeverage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtLeverage.ForeColor = System.Drawing.Color.Red;
            this.txtLeverage.Location = new System.Drawing.Point(295, 113);
            this.txtLeverage.Name = "txtLeverage";
            this.txtLeverage.Size = new System.Drawing.Size(111, 23);
            this.txtLeverage.TabIndex = 3;
            this.txtLeverage.Tag = "";
            this.txtLeverage.Text = "1";
            this.txtLeverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLeverage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Numeric_KeyPress);
            this.txtLeverage.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Integer_Validating);
            // 
            // txtEntryPrice
            // 
            this.txtEntryPrice.Location = new System.Drawing.Point(429, 113);
            this.txtEntryPrice.Name = "txtEntryPrice";
            this.txtEntryPrice.Size = new System.Drawing.Size(111, 23);
            this.txtEntryPrice.TabIndex = 4;
            this.txtEntryPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEntryPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtEntryPrice.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Decimal_Validating);
            // 
            // txtLotSize
            // 
            this.txtLotSize.Location = new System.Drawing.Point(563, 113);
            this.txtLotSize.Name = "txtLotSize";
            this.txtLotSize.Size = new System.Drawing.Size(111, 23);
            this.txtLotSize.TabIndex = 5;
            this.txtLotSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLotSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtLotSize.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Decimal_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(565, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 15);
            this.label5.TabIndex = 37;
            this.label5.Text = "Lot Size";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(426, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 15);
            this.label7.TabIndex = 39;
            this.label7.Text = "Entry Price (Avg)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCapital
            // 
            this.txtCapital.Location = new System.Drawing.Point(161, 113);
            this.txtCapital.Name = "txtCapital";
            this.txtCapital.Size = new System.Drawing.Size(111, 23);
            this.txtCapital.TabIndex = 2;
            this.txtCapital.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCapital.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtCapital.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Money_Validating);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label26.Location = new System.Drawing.Point(295, 95);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(59, 15);
            this.label26.TabIndex = 41;
            this.label26.Text = "Leverage";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(159, 95);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 15);
            this.label15.TabIndex = 42;
            this.label15.Text = "Capital";
            // 
            // btnOfficialize
            // 
            this.btnOfficialize.Location = new System.Drawing.Point(582, 168);
            this.btnOfficialize.Name = "btnOfficialize";
            this.btnOfficialize.Size = new System.Drawing.Size(89, 31);
            this.btnOfficialize.TabIndex = 6;
            this.btnOfficialize.Text = "Officialize";
            this.btnOfficialize.UseVisualStyleBackColor = true;
            this.btnOfficialize.Click += new System.EventHandler(this.btnOfficialize_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(487, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 31);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // TradeOfficialize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(701, 213);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOfficialize);
            this.Controls.Add(this.txtLeverage);
            this.Controls.Add(this.txtEntryPrice);
            this.Controls.Add(this.txtLotSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCapital);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dtpDateEnter);
            this.Controls.Add(this.txtTicker);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TradeOfficialize";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trade Officialize";
            this.Load += new System.EventHandler(this.TradeOfficialize_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDateEnter;
        private System.Windows.Forms.TextBox txtTicker;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLeverage;
        private System.Windows.Forms.TextBox txtEntryPrice;
        private System.Windows.Forms.TextBox txtLotSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCapital;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnOfficialize;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}