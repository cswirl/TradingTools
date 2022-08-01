using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Trunk.Extensions;

namespace TradingTools.Dialogs
{
    public partial class dialogCapitalCalc : Form
    {
        public Action<(string capital, string lotSize, string price)> UseCapital;

        public dialogCapitalCalc((string capital, string lotSize, string price) tuple)
        {
            InitializeComponent();

            txtCapital.Text = tuple.capital;
            txtLotSize.Text = tuple.lotSize;  
            txtPrice.Text = tuple.price;
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
            decimal capital, lotSize, price;
            price = txtPrice.Text.ToDecimal();
            lotSize = txtLotSize.Text.ToDecimal();
            capital = price * lotSize;
            txtCapital.Text = capital.ToMoney();
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            UseCapital?.Invoke((txtCapital.Text, txtLotSize.Text, txtPrice.Text));
            this.Hide();
        }
    }
}
