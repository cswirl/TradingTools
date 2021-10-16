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

namespace TradingTools
{
    public partial class frmTradeMasterFile : Form
    {
        private StatusFilter _statusFilter;
        private BindingList<Trade> _trade_bindingList;

        public delegate bool Trade_OnRequest(Trade t);
        public Trade_OnRequest Trade_TradeOpen_OnRequest;
        private master _master { get { return (master)this.Owner; } }

        public frmTradeMasterFile()
        {
            InitializeComponent();

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
            if (_master == default) return;
            // filter status
            switch (_statusFilter)
            {
                default:
                case StatusFilter.All:
                    DataGridView_SetDataSource(_master.GetTrades_All());
                    break;

                case StatusFilter.Closed:
                    DataGridView_SetDataSource(_master.GetTrades_Closed());
                    break;

                case StatusFilter.Open:
                    DataGridView_SetDataSource(_master.GetTrades_Open());
                    break;
            }
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
    }

    enum StatusFilter
    {
        All,
        Closed,
        Open
    }

}
