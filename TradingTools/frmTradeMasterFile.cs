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
using TradingTools.Model;
using TradingTools.Services;
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

        private master _master;

        public frmTradeMasterFile(master master)
        {
            InitializeComponent();

            _master = master;

            appInitialize();
        }

        private void appInitialize()
        {
            // delegates
            _master.RefreshTimer.Tick += this.timer1_Tick;
            //
            _master.Trade_Officialized += this.Trade_Officialized;
            _master.Trade_Closed += this.Trade_Closed;
            _master.Trade_Deleted += this.Trade_Deleted;

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
        }

        private void DataGridView_SetDataSource(List<Trade> list)
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
                    DataGridView_SetDataSource(_master?.Trades_GetAll(true));
                    break;

                case StatusFilter.Closed:
                    DataGridView_SetDataSource(_master?.Trades_GetClosed(true));
                    break;

                case StatusFilter.Open:
                    DataGridView_SetDataSource(_master?.Trades_GetOpen(true));
                    break;
                case StatusFilter.Deleted:
                    DataGridView_SetDataSource(_master?.Trades_GetDeleted(true));
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
                if (_master.Trade_Delete(t))
                {
                    statusMessage.Text = $"Trade No. {t.Id} was deleted successfully.";
                    AppMessageBox.Inform(statusMessage.Text, "Delete");
                    _list.Remove(t);
                }
                else
                {
                    statusMessage.Text = $"An error occur while deleting Trade No. {t.Id}.";
                    AppMessageBox.Error(statusMessage.Text, "Delete");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
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
            else if (_statusFilter == StatusFilter.Open) _list.Remove(t);
            else if (_statusFilter == StatusFilter.All)
            {
                dgvTrades.Invalidate();
                dgvTrades_SelectionChanged(default, default);
            }
        }

        private void Trade_Deleted(Trade t)
        {
            if (_statusFilter == StatusFilter.Deleted) _list.Insert(0, t);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!checkCorrection.Checked) return;
            if (isListEmpty()) return;

            var tradeClone = new Trade();
            var t = (Trade)dgvTrades.CurrentRow.DataBoundItem;
            t.CopyProperties(tradeClone);

            DialogResult objDialog = AppMessageBox.Question_YesNo($"Confirmation: UPDATE Trade No. {t.Id}?", "Update");
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
                // validate
                // for status = open
                if (!TradeService.TradeOpening_Validate(tradeClone, out msg))
                {
                    statusMessage.Text = msg;
                    AppMessageBox.Error(msg);
                    return;
                }

                // for status = closed
                if (t.Status.Equals("closed"))
                {
                    tradeClone.DateExit = Validation.DateExit_PreDate_Fixer(dtpDateExit.Value);
                    tradeClone.ExitPriceAvg = txtExitPrice.Text.ToDecimal();
                    tradeClone.FinalCapital = txtFinalCapital.Text.ToDecimal();
                    // auto-compute

                    // validate
                    if (!TradeService.TradeClosing_Validate(t, out msg))
                    {
                        statusMessage.Text = msg;
                        AppMessageBox.Error(msg);
                        return;
                    }
                }

                tradeClone.CopyProperties(t);

                // update database
                if (_master.Trade_Update(t))    // Trade object must be the original
                {
                    statusMessage.Text = $"Trade No. {t.Id} was updated successfully.";
                    AppMessageBox.Inform(statusMessage.Text, "Update");
                    dgvTrades.Invalidate();
                    dgvTrades_SelectionChanged(null, null);
                }
                else
                {
                    statusMessage.Text = $"An error occur while updating Trade No. {t.Id}.";
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
            Presentation.DateTimePicker_MaxDate_SafeAssign(dtpDateEnter, t.DateEnter); 

            txtCapital.Text = t.Capital.ToMoney();
            txtLotSize.Text = t.LotSize.ToString();
            txtEntryPrice.Text = t.EntryPriceAvg.ToString();
            txtLeverage.Text = t.Leverage.ToString_UptoTwoDecimal();
            Enum.TryParse<TradingStyle>(t.TradingStyle, out tradeStyle);
            cbxTradingStyle.SelectedItem = tradeStyle;

            if (t.Status.Equals("closed"))
            {
                panelTradeClosed.Visible = true;
                Presentation.DateTimePicker_MaxDate_SafeAssign(dtpDateExit, t.DateExit ?? dtpDateExit.Value);
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
                Presentation.DateTimePicker_MaxDate_SafeAssign(dtpDateExit, DateTime.Now);
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
    }

    enum StatusFilter
    {
        All,
        Closed,
        Open,
        Deleted
    }

}
