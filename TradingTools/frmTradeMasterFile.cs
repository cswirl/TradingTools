using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Model;
using TradingTools.Services;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Validation;

namespace TradingTools
{
    public partial class frmTradeMasterFile : Form
    {
        private StatusFilter _statusFilter;
        private BindingList<Trade> _trade_bindingList;

        public delegate bool Trade_OnRequest(Trade t);
        public Trade_OnRequest Trade_TradeOpen_OnRequest;
        private master? _master { get { return (master)this.Owner; } }

        public frmTradeMasterFile()
        {
            InitializeComponent();

            _statusFilter = StatusFilter.Closed;
            // initialize controls
            btnDelete.Visible = false;
            dtpDateEnter.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
            dtpDateExit.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);

            cmbFilterStatus.DataSource = Enum.GetValues(typeof(StatusFilter));
        }

        private void frmTradeMasterFile_Load(object sender, EventArgs e)
        {
            cmbFilterStatus.SelectedIndex = 1;  // status = closed
            // timer
            timer1.Interval = Presentation.INTERNAL_TIMER_REFRESH_VALUE;
        }

        private void DataGridView_SetDataSource(BindingList<Trade> bindingList)
        {
            _trade_bindingList = bindingList;
            dgvTrades.DataSource = _trade_bindingList;
        }

        private void btnViewCalculator_Click(object sender, EventArgs e)
        {
            Trade_TradeOpen_OnRequest((Trade)dgvTrades.CurrentRow.DataBoundItem);
        }

        private void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse<StatusFilter>(cmbFilterStatus.SelectedValue.ToString(), out _statusFilter);

            // 
            if (_master == default) return; // _master was changed to nullable so this check may be remove 
            // filter status
            switch (_statusFilter)
            {
                default:
                case StatusFilter.All:
                    DataGridView_SetDataSource(_master?.GetTrades_All());
                    break;

                case StatusFilter.Closed:
                    DataGridView_SetDataSource(_master?.GetTrades_Closed());
                    break;

                case StatusFilter.Open:
                    DataGridView_SetDataSource(_master?.GetTrades_Open());
                    break;
            }
            dgvTrades.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var t = (Trade)dgvTrades.CurrentRow.DataBoundItem;
            DialogResult objDialog = MyMessageBox.Question_YesNo($"Confirmation: DELETE Trade No. {t.Id}?", "Delete");
            if (objDialog == DialogResult.Yes)
            {
                if (_master.Trade_Delete(t))
                {
                    statusMessage.Text = $"Trade No. {t.Id} was deleted successfully.";
                    MyMessageBox.Inform(statusMessage.Text, "Delete");
                    _trade_bindingList.Remove(t);
                }
                else
                {
                    statusMessage.Text = $"An error occur while deleting Trade No. {t.Id}.";
                    MyMessageBox.Error(statusMessage.Text, "Delete");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            statusMessage.Text = "Status message . . .";
        }

        public void Trade_Officialized(Trade t)
        {
            if (_statusFilter == StatusFilter.Open) _trade_bindingList.Add(t);
        }

        public void Trade_Closed(Trade t)
        {
            if (_statusFilter == StatusFilter.Closed) _trade_bindingList.Add(t);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!cbCorrection.Checked) return;

            var t = (Trade)dgvTrades.CurrentRow.DataBoundItem;
            DialogResult objDialog = MyMessageBox.Question_YesNo($"Confirmation: UPDATE Trade No. {t.Id}?", "Update");
            if (objDialog == DialogResult.Yes)
            {
                // collect
                t.DateEnter = dtpDateEnter.Value;
                t.Ticker = txtTicker.Text;
                t.Capital = StringToNumeric.MoneyToDecimal(txtCapital.Text);
                t.LotSize = InputConverter.Decimal(txtLotSize.Text);
                t.EntryPriceAvg = InputConverter.Decimal(txtEntryPrice.Text);
                t.Leverage = InputConverter.Decimal(txtLeverage.Text);
                
                // process
                string msg;
                decimal d = SafeConvert.ToDecimalSafe((DateTime.Now - t.DateEnter).TotalDays);
                int dayCount = Convert.ToInt32(d);
                t.DayCount = dayCount == 0 ? 1 : dayCount;

                var calcDetail = CalculationDetails.GetCalculationDetails(t.Capital, t.Leverage, t.Leverage, t.LotSize, dayCount, t.DailyInterestRate, out msg);
                if (calcDetail == default)
                {
                    statusMessage.Text = msg;
                    MyMessageBox.Error(statusMessage.Text, "");
                    return;
                }

                // assign processed data
                var p = calcDetail.Position;
                var oc = calcDetail.OpeningCost;
                var b = calcDetail.Borrow;

                t.LeveragedCapital = p.LeveragedCapital;
                t.OpeningTradingFee = oc.TradingFee;
                t.OpeningTradingCost = oc.TradingFee;
                t.BorrowAmount = b.Amount;
                t.InterestCost = b.InterestCost;
                t.DailyInterestRate = b.DailyInterestRate;
                
                // validate
                if (!Trade_Serv.TradeOpening_Validate(t, out msg))
                {
                    statusMessage.Text = msg;
                    MyMessageBox.Error(msg);
                    return;
                }

                // for status = closed
                if (t.Status.Equals("closed"))
                {
                    t.DateExit = dtpDateExit.Value;

                    // validate
                    if (!Trade_Serv.TradeClosing_Validate(t, out msg))
                    {
                        statusMessage.Text = msg;
                        MyMessageBox.Error(msg);
                        return;
                    }
                }

                // update database
                if (_master.Trade_Update(t))
                {
                    statusMessage.Text = $"Trade No. {t.Id} was updated successfully.";
                    MyMessageBox.Inform(statusMessage.Text, "Update");
                    dgvTrades.Invalidate();
                }
                else
                {
                    statusMessage.Text = $"An error occur while updating Trade No. {t.Id}.";
                    MyMessageBox.Error(statusMessage.Text, "Update");
                }
            }
        }

        private void dgvTrades_SelectionChanged(object sender, EventArgs e)
        {
            var t = (Trade)dgvTrades.CurrentRow.DataBoundItem;
            if (t == default) return;
            txtTradeNum.Text = t.Id.ToString();
            txtTicker.Text = t.Ticker;
            dtpDateEnter.Value = t.DateEnter;
            txtCapital.Text = t.Capital.ToString(Constant.MONEY_FORMAT);
            txtLotSize.Text = t.LotSize.ToString();
            txtEntryPrice.Text = t.EntryPriceAvg.ToString();
            txtLeverage.Text = t.Leverage.ToString(Constant.PERCENTAGE_FORMAT);
      
            
            if (t.Status.Equals("closed"))
            {
                panelTradeClosed.Visible = true;
                dtpDateExit.Value = t.DateExit ?? t.DateEnter;
                // PCP, PnL etc
                txtPCP.Text = RiskRewardCalc_Serv.PCP(t.EntryPriceAvg, t.ExitPriceAvg).ToString(Constant.PERCENTAGE_FORMAT);
                txtPnL_percentage.Text = RiskRewardCalc_Serv.PnL_percentage(t.Capital, t.PnL).ToString(Constant.PERCENTAGE_FORMAT);
                txtPnL.Text = t.PnL == default ? "0" : t.PnL?.ToString(Constant.MONEY_FORMAT);
                decimal finalPositionValue = RiskRewardCalc_Serv.FinalPositionValue(t.LotSize, t.ExitPriceAvg);
                txtFinalPositionValue.Text = finalPositionValue.ToString(Constant.MONEY_FORMAT);
                txtAccountEquity.Text = RiskRewardCalc_Serv.AccountEquity(finalPositionValue, t.BorrowAmount).ToString(Constant.MONEY_FORMAT);
            }
            else
            {
                panelTradeClosed.Visible = false;
                // PCP, PnL etc
                txtPCP.Text = "0";
                txtPnL_percentage.Text = "0";
                txtPnL.Text = "0";
                txtFinalPositionValue.Text = "0";
                txtAccountEquity.Text = "0";
            }
        }

        private void dgvTrades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Trade_TradeOpen_OnRequest((Trade)dgvTrades.CurrentRow.DataBoundItem);
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

        private void TextBox_Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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

            if (!Format.isInteger(tb.Text, out msg))
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

        private void cbEditMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCorrection.Checked)
            {
                btnDelete.Visible = true;

                txtTicker.ReadOnly = false;
                dtpDateEnter.Enabled = true;
                txtCapital.ReadOnly = false;
                txtLotSize.ReadOnly = false;
                txtEntryPrice.ReadOnly = false;
                txtLeverage.ReadOnly = false;

                dtpDateExit.Enabled = true;
            }
            else
            {
                btnDelete.Visible = false;

                txtTicker.ReadOnly = true;
                dtpDateEnter.Enabled = false;
                txtCapital.ReadOnly = true;
                txtLotSize.ReadOnly = true;
                txtEntryPrice.ReadOnly = true;
                txtLeverage.ReadOnly = true;

                dtpDateExit.Enabled = false;
                // cancel changes
                dgvTrades_SelectionChanged(default, EventArgs.Empty);
            }
        }
    }

    enum StatusFilter
    {
        All,
        Closed,
        Open
    }

}
