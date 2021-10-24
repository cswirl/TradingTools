using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Model;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Validation;

namespace TradingTools
{
    public partial class TradeClosing : Form
    {
        public delegate bool Trade_OnClosing(Trade t);
        public Trade_OnClosing Trade_Close;
        //public event EventHandler Trade_Officialize;

        public dynamic MyProperty { get; set; }

        public Trade? Trade { get; set; }

        public TradeClosing()
        {
            InitializeComponent();

            dtpDateExit.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
        }

        public TradeClosing(Trade t)
        {
            this.Trade = t;

            InitializeComponent();
            dtpDateExit.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
        }

        private void btnCloseTrade_Click(object sender, EventArgs e)
        {
            //dynamic d = new ExpandoObject();
            //d.DateEnter = dtpDateExit.Value;
            //d.Capital = StringToNumeric.MoneyToDecimal(txtFinalCapital.Text);
            //d.EntryPrice = InputConverter.Decimal(txtExitPrice.Text);

            Trade.DateEnter = dtpDateExit.Value;
            Trade.Capital = StringToNumeric.MoneyToDecimal(txtFinalCapital.Text);
            Trade.EntryPriceAvg = InputConverter.Decimal(txtExitPrice.Text);
            Trade.CalculatorState.ReasonForExit = txtReasonForExit.Text;

            //Trade_Close?.Invoke(this.Trade);
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }


        #region Controls Validation
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

        private void TextBox_Money_Validating(object sender, CancelEventArgs e)
        {
            var tb = (TextBox)sender;
            string msg;

            if (!Format.isMoney(tb.Text, out msg))
            {
                //e.Cancel = true;
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tb, null);
            }
        }

        private void TextBox_Decimal_Validating(object sender, CancelEventArgs e)
        {
            var tb = (TextBox)sender;
            string msg;

            if (!Format.isDecimal(tb.Text, out msg))
            {
                //e.Cancel = true;
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tb, null);
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TradeClosing_Load(object sender, EventArgs e)
        {
            
            //if (MyProperty == null) return;
            //dtpDateExit.Value = DateTime.Now;
            //txtFinalCapital.Text = MyProperty.Capital;
            //txtExitPrice.Text = MyProperty.EntryPrice;

            if (Trade == null) return;
            dtpDateExit.Value = DateTime.Now;
            txtFinalCapital.Text = Trade.FinalCapital?.ToString(Constant.MONEY_FORMAT);
            txtExitPrice.Text = Trade.ExitPriceAvg?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
            txtReasonForExit.Text = Trade.CalculatorState.ReasonForExit;
        }
    }
}
