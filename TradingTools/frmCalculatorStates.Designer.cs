
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dgvCalculatorStates = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOpenCalc_Loaded = new System.Windows.Forms.Button();
            this.btnOpenCalc_Empty = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculatorStates)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(142, 422);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(142, 118);
            this.panel4.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1288, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dgvCalculatorStates
            // 
            this.dgvCalculatorStates.AllowUserToAddRows = false;
            this.dgvCalculatorStates.AllowUserToDeleteRows = false;
            this.dgvCalculatorStates.AllowUserToResizeRows = false;
            this.dgvCalculatorStates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalculatorStates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCalculatorStates.Location = new System.Drawing.Point(0, 10);
            this.dgvCalculatorStates.Name = "dgvCalculatorStates";
            this.dgvCalculatorStates.ReadOnly = true;
            this.dgvCalculatorStates.RowTemplate.Height = 25;
            this.dgvCalculatorStates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCalculatorStates.Size = new System.Drawing.Size(1136, 304);
            this.dgvCalculatorStates.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvCalculatorStates);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(142, 132);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 10, 10, 0);
            this.panel2.Size = new System.Drawing.Size(1146, 314);
            this.panel2.TabIndex = 3;
            // 
            // btnOpenCalc_Loaded
            // 
            this.btnOpenCalc_Loaded.Location = new System.Drawing.Point(1023, 80);
            this.btnOpenCalc_Loaded.Name = "btnOpenCalc_Loaded";
            this.btnOpenCalc_Loaded.Size = new System.Drawing.Size(265, 46);
            this.btnOpenCalc_Loaded.TabIndex = 4;
            this.btnOpenCalc_Loaded.Text = "Open Selected >> Risk Reward Calculator";
            this.btnOpenCalc_Loaded.UseVisualStyleBackColor = true;
            this.btnOpenCalc_Loaded.Click += new System.EventHandler(this.btnOpenCalc_Click);
            // 
            // btnOpenCalc_Empty
            // 
            this.btnOpenCalc_Empty.Location = new System.Drawing.Point(1023, 33);
            this.btnOpenCalc_Empty.Name = "btnOpenCalc_Empty";
            this.btnOpenCalc_Empty.Size = new System.Drawing.Size(265, 41);
            this.btnOpenCalc_Empty.TabIndex = 5;
            this.btnOpenCalc_Empty.Text = "New Risk Reward Calculator";
            this.btnOpenCalc_Empty.UseVisualStyleBackColor = true;
            this.btnOpenCalc_Empty.Click += new System.EventHandler(this.btnOpenCalc_Empty_Click);
            // 
            // frmCalculatorStates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 446);
            this.Controls.Add(this.btnOpenCalc_Empty);
            this.Controls.Add(this.btnOpenCalc_Loaded);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MaximizeBox = false;
            this.Name = "frmCalculatorStates";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHome";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCalculatorStates_FormClosed);
            this.Load += new System.EventHandler(this.frmCalculatorStates_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculatorStates)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridView dgvCalculatorStates;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOpenCalc_Loaded;
        private System.Windows.Forms.Button btnOpenCalc_Empty;
    }
}