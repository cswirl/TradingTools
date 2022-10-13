using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Dialogs;
using TradingTools.Extensions;
using TradingTools.Model;
using TradingTools.Services;
using TradingTools.Services.Interface;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Extensions;
using TradingTools.Trunk.Validation;

namespace TradingTools
{
    public partial class frmTradeMasterFile : Form
    {
        private BindingList<Trade> _list;
        private StatusFilter _statusFilter;
        private TradingStyle tradeStyle;
        private Timer _timer;

        private master _master;
        private IServiceManager _service;

        public frmTradeMasterFile(master master)
        {
            InitializeComponent();

            _master = master;
            _service = master.ServiceManager;
            _timer = new Timer();
            appInitialize();
        }

        private void appInitialize()
        {
            // delegates
            _timer.Interval = Presentation.INTERNAL_TIMER_REFRESH_VALUE;
            _timer.Tick += this.timer_Tick;
            _timer.Start();
            //
            _master.Trade_Officialized += this.Trade_Officialized;
            _master.Trade_Closed += this.Trade_Closed;

            // initialize controls
            _statusFilter = StatusFilter.Closed;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            dtpDateEnter.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
            dtpDateExit.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);

            cbxFilterStatus.DataSource = Enum.GetValues(typeof(StatusFilter));
            cbxTradingStyle.DataSource = Enum.GetValues(typeof(TradingStyle));
        }

        private void frmTradeMasterFile_Load(object sender, EventArgs e)
        {
            cbxFilterStatus.SelectedIndex = 1;  // status = closed
            _timer.Start();
        }

        private void DataGridView_SetDataSource(IList<Trade> list)
        {
            _list = new(list);
            dgvTrades.DataSource = _list;
        }

        private void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse<StatusFilter>(cbxFilterStatus.SelectedValue.ToString(), out _statusFilter);

            // 
            if (_master == default) return; // _master was changed to nullable type so this check may be remove 
            // filter status
            switch (_statusFilter)
            {
                default:
                case StatusFilter.All:
                    var list = _service.TradeService.GetAll(true);
                    if (list == null) return;
                    DataGridView_SetDataSource(list);
                    break;

                case StatusFilter.Closed:
                    list = _service.TradeService.GetStatusClosed(true);
                    if (list == null) return;
                    DataGridView_SetDataSource(list);
                    break;

                case StatusFilter.Open:
                    list = _service.TradeService.GetStatusOpen(true);
                    if (list == null) return;
                    DataGridView_SetDataSource(list);
                    break;
                case StatusFilter.Deleted:
                    list = _service.TradeService.GetDeleted(true);
                    if (list == null) return;
                    DataGridView_SetDataSource(list);
                    break;
            }
            dgvTrades.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (isListEmpty()) return;

            var t = (Trade)dgvTrades.CurrentRow.DataBoundItem;
            if (t.IsDeleted) { statusMessage.Text = $"Trade {t.Id} is already deleted"; return; }

            DialogResult objDialog = AppMessageBox.Question_YesNo($"Confirmation: DELETE Trade No. {t.Id}?", "Delete");
            if (objDialog == DialogResult.Yes)
            {
                if (_service.TradeService.Delete(t))
                {
                    statusMessage.Text = $"Trade Id: {t.Id} - {t.Ticker} was deleted successfully.";
                    AppMessageBox.Inform(statusMessage.Text, "Delete");
                    deleteTrade(t);
                    _master.Trade_Deleted?.Invoke(t);
                }
                else
                {
                    statusMessage.Text = $"An error occur while deleting Trade No. {t.Id}.";
                    AppMessageBox.Error(statusMessage.Text, "Delete");
                }
            }
        }

        private void deleteTrade(Trade trade)
        {
            switch(_statusFilter)
            {
                case StatusFilter.All:
                case StatusFilter.Open:
                case StatusFilter.Closed:
                    _list.Remove(trade);
                    break;
                case StatusFilter.Deleted:
                    _list.Insert(0, trade);
                    break;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            statusMessage.Text = "Status message . . .";
        }

        // Officialize a Trade means creating a new Trade record
        private void Trade_Officialized(Trade t)
        {
            if (_statusFilter == StatusFilter.Open || _statusFilter == StatusFilter.All) _list.Insert(0,t);
        }

        private void Trade_Closed(Trade t)
        {
            if (_statusFilter == StatusFilter.Closed) _list.Insert(0,t);
            else if (_statusFilter == StatusFilter.Open)
            {
                if (_list.Remove(x => x.Id == t.Id) != null)
                    statusMessage.Text = $"Trade Id: {t.Id} - {t.Ticker} was closed externally";
            }
            else if (_statusFilter == StatusFilter.All)
            {
                if (_list.Replace(t, x => x.Id == t.Id) != null)
                {
                    dgvTrades_SelectionChanged(default, default);
                    dgvTrades.Invalidate();
                    statusMessage.Text = $"Trade Id: {t.Id} - {t.Ticker} was closed externally";
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!checkCorrection.Checked) return;
            if (isListEmpty()) return;

            var tradeClone = new Trade();
            var trade = (Trade)dgvTrades.CurrentRow.DataBoundItem;
            trade.CopyProperties(tradeClone);

            DialogResult objDialog = AppMessageBox.Question_YesNo($"Confirmation: UPDATE Trade No. {trade.Id}?", "Update");
            if (objDialog == DialogResult.Yes)
            {
                // collect
                tradeClone.DateEnter = dtpDateEnter.Value;
                tradeClone.Ticker = txtTicker.Text;
                tradeClone.Capital = txtCapital.Text.ToDecimal();
                tradeClone.LotSize = txtLotSize.Text.ToDecimal();
                tradeClone.EntryPriceAvg = txtEntryPrice.Text.ToDecimal();
                tradeClone.Leverage = txtLeverage.Text.ToDecimal();
                tradeClone.TradingStyle = cbxTradingStyle.SelectedValue.ToString();

                string msg;
                // validate the clone
                // for status = open
                if (!TradeService.TradeOpening_Validate(tradeClone, out msg))
                {
                    statusMessage.Text = msg;
                    AppMessageBox.Error(msg);
                    return;
                }

                // for status = closed
                if (trade.Status.Equals("closed"))
                {
                    tradeClone.DateExit = dtpDateExit.Value;
                    tradeClone.ExitPriceAvg = txtExitPrice.Text.ToDecimal();
                    tradeClone.FinalCapital = txtFinalCapital.Text.ToDecimal();

                    // validate
                    if (!TradeService.TradeClosing_Validate(tradeClone, out msg))
                    {
                        statusMessage.Text = msg;
                        AppMessageBox.Error(msg);
                        return;
                    }
                }

                tradeClone.CopyProperties(trade);

                // update database
                if (_service.TradeService.Update(trade))    // Trade object must be the original
                {
                    statusMessage.Text = $"Trade No. {trade.Id} was updated successfully.";
                    AppMessageBox.Inform(statusMessage.Text, "Update");
                    dgvTrades.Invalidate();
                    dgvTrades_SelectionChanged(null, null);
                    _master.Trade_Updated?.Invoke(trade);
                }
                else
                {
                    statusMessage.Text = $"An error occur while updating Trade No. {trade.Id}.";
                    AppMessageBox.Error(statusMessage.Text, "Update");
                }
            }
        }

        private bool isListEmpty()
        {
            if (_list.Count < 1) return true;
            var dataBound = dgvTrades.CurrentRow?.DataBoundItem ?? default;
            if (dataBound == default) return true;

            return false;
        }

        private void dgvTrades_SelectionChanged(object sender, EventArgs e)
        {
            // null safety check
            if (isListEmpty()) return;

            var t = (Trade)dgvTrades.CurrentRow.DataBoundItem;
            if (t == default) return;
            txtTradeNum.Text = t.Id.ToString();
            txtTicker.Text = t.Ticker;
            // todo: refactor
            dtpDateEnter.SafeValueAssignment(t.DateEnter); 

            txtCapital.Text = t.Capital.ToMoney();
            txtLotSize.Text = t.LotSize.ToString();
            txtEntryPrice.Text = t.EntryPriceAvg.ToString();
            txtLeverage.Text = t.Leverage.ToString_UptoTwoDecimal();
            Enum.TryParse<TradingStyle>(t.TradingStyle, out tradeStyle);
            cbxTradingStyle.SelectedItem = tradeStyle;

            if (t.Status.Equals("closed"))
            {
                panelTradeClosed.Visible = true;
                dtpDateExit.SafeValueAssignment(t.DateExit ?? dtpDateExit.Value);
                txtExitPrice.Text = t.ExitPriceAvg?.ToString_UptoMaxDecimal();
                txtFinalCapital.Text = t.FinalCapital?.ToMoney();
                // PCP, PnL etc
                txtPCP.Text = Formula.PCP(t.EntryPriceAvg, t.ExitPriceAvg).ToPercentageSingle();
                txtPnL.Text = t.PnL?.ToMoney();
                txtPnL_percentage.Text = t.PnL_percentage?.ToPercentageSingle();
                txtFinalPositionValue.Text = Formula.PositionValue(t.LotSize, t.ExitPriceAvg ?? 0).ToMoney();
            }
            else
            {
                panelTradeClosed.Visible = false;
                dtpDateExit.SafeValueAssignment(DateTime.Now);
                txtExitPrice.Text = "0";
                txtFinalCapital.Text = "0";
                // PCP, PnL etc
                txtPCP.Text = "0";
                txtPnL.Text = "0";
                txtPnL_percentage.Text = "0";
                txtFinalPositionValue.Text = "0";
            }
        }

        private void dgvTrades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _master.FormRRC_Trade_Spawn((Trade)dgvTrades.CurrentRow.DataBoundItem);
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
            if (checkCorrection.Checked)
            {
                btnUpdate.Visible = true;
                btnDelete.Visible = true;

                txtTicker.ReadOnly = false;
                dtpDateEnter.Enabled = true;
                txtCapital.ReadOnly = false;
                txtLotSize.ReadOnly = false;
                txtEntryPrice.ReadOnly = false;
                txtLeverage.ReadOnly = false;
                cbxTradingStyle.Enabled = true;
                // status = close
                dtpDateExit.Enabled = true;
                txtExitPrice.ReadOnly = false;
                txtFinalCapital.ReadOnly = false;
            }
            else
            {
                btnUpdate.Visible = false;
                btnDelete.Visible = false;

                txtTicker.ReadOnly = true;
                dtpDateEnter.Enabled = false;
                txtCapital.ReadOnly = true;
                txtLotSize.ReadOnly = true;
                txtEntryPrice.ReadOnly = true;
                txtLeverage.ReadOnly = true;
                cbxTradingStyle.Enabled = false;
                // 
                dtpDateExit.Enabled = false;
                txtExitPrice.ReadOnly = true;
                txtFinalCapital.ReadOnly = true;
                // cancel changes
                dgvTrades_SelectionChanged(default, EventArgs.Empty);
            }
        }

        private void frmTradeMasterFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timer.Stop();
        }
    }

    enum StatusFilter
    {
        All,
        Closed,
        Open,
        Deleted
    }

}
