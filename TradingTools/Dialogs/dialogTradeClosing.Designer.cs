
namespace TradingTools
{
    partial class dialogTradeClosing
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
            this.dtpDateExit = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExitPrice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFinalCapital = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnCloseTrade = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtReasonForExit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDateExit
            // 
            this.dtpDateExit.Location = new System.Drawing.Point(29, 36);
            this.dtpDateExit.Name = "dtpDateExit";
            this.dtpDateExit.Size = new System.Drawing.Size(181, 23);
            this.dtpDateExit.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 33;
            this.label2.Text = "Date Exit";
            // 
            // txtExitPrice
            // 
            this.txtExitPrice.Location = new System.Drawing.Point(29, 113);
            this.txtExitPrice.Name = "txtExitPrice";
            this.txtExitPrice.Size = new System.Drawing.Size(111, 23);
            this.txtExitPrice.TabIndex = 4;
            this.txtExitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExitPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtExitPrice.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Decimal_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 15);
            this.label7.TabIndex = 39;
            this.label7.Text = "Exit Price (Avg)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFinalCapital
            // 
            this.txtFinalCapital.Location = new System.Drawing.Point(161, 113);
            this.txtFinalCapital.Name = "txtFinalCapital";
            this.txtFinalCapital.Size = new System.Drawing.Size(111, 23);
            this.txtFinalCapital.TabIndex = 2;
            this.txtFinalCapital.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFinalCapital.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            this.txtFinalCapital.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Money_Validating);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(159, 95);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 15);
            this.label15.TabIndex = 42;
            this.label15.Text = "Final Capital";
            // 
            // btnCloseTrade
            // 
            this.btnCloseTrade.Location = new System.Drawing.Point(397, 108);
            this.btnCloseTrade.Name = "btnCloseTrade";
            this.btnCloseTrade.Size = new System.Drawing.Size(108, 31);
            this.btnCloseTrade.TabIndex = 6;
            this.btnCloseTrade.Text = "Close the Trade";
            this.btnCloseTrade.UseVisualStyleBackColor = true;
            this.btnCloseTrade.Click += new System.EventHandler(this.btnCloseTrade_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(302, 108);
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
            // txtReasonForExit
            // 
            this.txtReasonForExit.Location = new System.Drawing.Point(29, 183);
            this.txtReasonForExit.Multiline = true;
            this.txtReasonForExit.Name = "txtReasonForExit";
            this.txtReasonForExit.Size = new System.Drawing.Size(476, 118);
            this.txtReasonForExit.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 44;
            this.label1.Text = "Reason for Exit";
            // 
            // TradeClosing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(541, 313);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReasonForExit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCloseTrade);
            this.Controls.Add(this.txtExitPrice);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFinalCapital);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dtpDateExit);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TradeClosing";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trade Officialize";
            this.Load += new System.EventHandler(this.TradeClosing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDateExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExitPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFinalCapital;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnCloseTrade;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReasonForExit;
    }
}