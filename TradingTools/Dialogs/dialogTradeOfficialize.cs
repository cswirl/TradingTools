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
using TradingTools.Dialogs;
using TradingTools.Model;
using TradingTools.Trunk.Extensions;
using TradingTools.Trunk.Validation;

namespace TradingTools
{
    public partial class dialogTradeOfficialize : Form
    {
        public delegate bool Trade_OnOfficializing(dynamic d);
        public Trade_OnOfficializing Trade_Officialize;

        public dynamic MyProperty { get; set; }

        public dialogTradeOfficialize(master master)
        {
            InitializeComponent();

            dtpDateEnter.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);

            // Ticker auto complete
            var autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(master.TickerAutoCompleteSource());
            txtTicker.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTicker.AutoCompleteCustomSource = autoComplete;
            txtTicker.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void btnOfficialize_Click(object sender, EventArgs e)
        {
            dynamic d = new ExpandoObject();
            d.DateEnter = dtpDateEnter.Value;
            d.Ticker = txtTicker.Text;
            d.Capital = StringToNumeric.MoneyToDecimal(txtCapital.Text);
            d.Leverage = InputConverter.Decimal(txtLeverage.Text);
            d.EntryPrice = InputConverter.Decimal(txtEntryPrice.Text);

            if (txtLotSize.Text.Length < 1 | txtLotSize.Text.Equals(string.Empty))
            {
               
            }
            else
            {
                decimal lotSize = InputConverter.Decimal(txtLotSize.Text);
                if (lotSize > 0) d.LotSize = lotSize;
            }

            if (Trade_Officialize?.Invoke(d)) this.Close();
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

        private void TextBox_Integer_Validating(object sender, CancelEventArgs e)
        {
            var tb = (TextBox)sender;
            string msg;

            if (!Format.isWholeNumberAboveZero(tb.Text, out msg))
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

        private void TextBox_Ticker_Validating(object sender, CancelEventArgs e)
        {
            var tb = (TextBox)sender;
            string msg;

            if (!Format.isTicker(tb.Text, out msg))
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
            this.Close();
        }

        private void TradeOfficialize_Load(object sender, EventArgs e)
        {
            if (MyProperty == null) return;
            Presentation.DateTimePicker_MaxDate_SafeAssign(dtpDateEnter, DateTime.Now);
            txtTicker.Text = MyProperty.Ticker;
            txtCapital.Text = MyProperty.Capital;
            txtLeverage.Text = MyProperty.Leverage;
            txtLotSize.Text = Validation.HasProperty(MyProperty, "LotSize") ? MyProperty.LotSize : "";
            txtEntryPrice.Text = MyProperty.EntryPrice;
        }

        private void txtLeverage_KeyDown(object sender, KeyEventArgs e)
        {
            var tb = (TextBox)sender;
            var val = tb.Text.ToDecimal();
            if (e.KeyCode == Keys.Up)
            {
                tb.Text = (val + 1).ToString_UptoMaxDecimal();
            }
            else if (e.KeyCode == Keys.Down)
            {
                if ((val - 1) < 1) { tb.Text = "1"; return; }
                else tb.Text = (val - 1).ToString_UptoTwoDecimal();
            }
        }
    }
}
