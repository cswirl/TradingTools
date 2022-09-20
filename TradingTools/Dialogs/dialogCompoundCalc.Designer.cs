namespace TradingTools.Dialogs
{
    partial class dialogCompoundCalc
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTargetPercentage = new System.Windows.Forms.TextBox();
            this.txtTradeCap = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtCapital = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTargetPercentage);
            this.groupBox1.Controls.Add(this.txtTradeCap);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnUse);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtResult);
            this.groupBox1.Controls.Add(this.txtCapital);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compund Calculator";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(229, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "^";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Target %";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Trade Cap";
            // 
            // txtTargetPercentage
            // 
            this.txtTargetPercentage.Location = new System.Drawing.Point(157, 41);
            this.txtTargetPercentage.Name = "txtTargetPercentage";
            this.txtTargetPercentage.Size = new System.Drawing.Size(66, 23);
            this.txtTargetPercentage.TabIndex = 1;
            this.txtTargetPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTargetPercentage.TextChanged += new System.EventHandler(this.Compute_TextChanged);
            this.txtTargetPercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            // 
            // txtTradeCap
            // 
            this.txtTradeCap.Location = new System.Drawing.Point(250, 41);
            this.txtTradeCap.Name = "txtTradeCap";
            this.txtTradeCap.Size = new System.Drawing.Size(66, 23);
            this.txtTradeCap.TabIndex = 2;
            this.txtTradeCap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTradeCap.TextChanged += new System.EventHandler(this.Compute_TextChanged);
            this.txtTradeCap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(407, 114);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnUse
            // 
            this.btnUse.Location = new System.Drawing.Point(326, 114);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(75, 23);
            this.btnUse.TabIndex = 3;
            this.btnUse.Text = "Use";
            this.btnUse.UseVisualStyleBackColor = true;
            this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Capital";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(31, 115);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(285, 23);
            this.txtResult.TabIndex = 1;
            this.txtResult.TabStop = false;
            // 
            // txtCapital
            // 
            this.txtCapital.Location = new System.Drawing.Point(31, 41);
            this.txtCapital.Name = "txtCapital";
            this.txtCapital.Size = new System.Drawing.Size(100, 23);
            this.txtCapital.TabIndex = 0;
            this.txtCapital.TextChanged += new System.EventHandler(this.Compute_TextChanged);
            this.txtCapital.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Decimal_KeyPress);
            // 
            // dialogCompoundCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(537, 194);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "dialogCompoundCalc";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "dialogCapitalCalc";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCapital;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTargetPercentage;
        private System.Windows.Forms.TextBox txtTradeCap;
        private System.Windows.Forms.TextBox txtResult;
    }
}