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
using TradingTools.Trunk.Extensions;
using TradingTools.Trunk.Validation;

namespace TradingTools
{
    public partial class TradeClosing : Form
    {
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
            Trade.DateExit = Validation.DateExit_PreDate_Fixer(dtpDateExit.Value);
            Trade.ExitPriceAvg = txtExitPrice.Text.ToDecimal();
            Trade.FinalCapital = txtFinalCapital.Text.ToDecimal();
            Trade.CalculatorState.ReasonForExit = txtReasonForExit.Text;
            Trade.Status = "closed";

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
            if (Trade == null) return;
            Presentation.DateTimePicker_MaxDate_SafeAssign(dtpDateExit, DateTime.Now);
            txtExitPrice.Text = Trade?.ExitPriceAvg?.ToString_UptoMaxDecimal();
            txtFinalCapital.Text = Trade?.FinalCapital?.ToMoney();
            txtReasonForExit.Text = Trade?.CalculatorState.ReasonForExit;
        }
    }
}
