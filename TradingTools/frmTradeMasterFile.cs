using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Services;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;

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
                    _trade_bindingList.Remove(t);
                    statusMessage.Text = $"Trade No. {t.Id} was deleted successfully.";
                    MyMessageBox.Inform(statusMessage.Text, "Delete");
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

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDelete.Visible = true;
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDelete.Visible = false;
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
            var t = (Trade)dgvTrades.CurrentRow.DataBoundItem;
            DialogResult objDialog = MyMessageBox.Question_YesNo($"Confirmation: UPDATE Trade No. {t.Id}?", "Update");
            if (objDialog == DialogResult.Yes)
            {
                // collect
                t.DateEnter = dtpDateEnter.Value;
                //open trade must not be able to update DateExit
                if (!t.Status.Equals("open")) t.DateExit = dtpDateExit.Value;

                // validate
                string msg;
                if (!Trade_Serv.TradeOpening_Validate(t, out msg))
                {
                    statusMessage.Text = msg;
                    MyMessageBox.Error(msg);
                    return;
                }

                // update database
                if (_master.Trade_Update(t))
                {
                    dgvTrades.Invalidate();
                    statusMessage.Text = $"Trade No. {t.Id} was updated successfully.";
                    MyMessageBox.Inform(statusMessage.Text, "Update");
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
            dtpDateExit.Value = t.DateExit ?? t.DateEnter;

            if (t.Status.Equals("closed"))
            {
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
    }

    enum StatusFilter
    {
        All,
        Closed,
        Open
    }

}
