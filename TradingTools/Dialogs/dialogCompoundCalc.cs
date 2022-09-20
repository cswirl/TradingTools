using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Extensions;

namespace TradingTools.Dialogs
{
    public partial class dialogCompoundCalc : Form
    {
        public Action<(string targetPercent, string tradeCap)> UseData;

        public dialogCompoundCalc((string initialCapital, string targetPercent, string tradeCap) tuple)
        {
            InitializeComponent();

            txtCapital.Text = tuple.initialCapital;
            txtTargetPercentage.Text = tuple.targetPercent;  
            txtTradeCap.Text = tuple.tradeCap;
        }

        private void button2_Click(object sender, EventArgs e) => this.Hide();

        private void TextBox_Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void Compute_TextChanged(object sender, EventArgs e)
        {
            decimal initialCapital, targetPercent;
            initialCapital = txtCapital.Text.ToDecimal();
            targetPercent = txtTargetPercentage.Text.ToDecimal();
            // Goal
            double baseNum = targetPercent.SolveBaseNumber();
            double exponent = txtTradeCap.Text.ToDouble();
            double compound = Math.Pow(baseNum, exponent);
            double goal = decimal.ToDouble(initialCapital) * compound;
            txtResult.Text = $"{initialCapital.ToMoney()} x " +
                $"{baseNum}" +
                $"^{exponent} = {goal.ToMoney()}";
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            UseData?.Invoke((txtTargetPercentage.Text, txtTradeCap.Text));
            this.Hide();
        }
    }
}
