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

        public delegate bool Trade_OnRequest(Trade t);
        public Trade_OnRequest Trade_TradeOpen_OnRequest;
        private master _master { get { return (master)this.Owner; } }

        public frmTradeMasterFile()
        {
            InitializeComponent();

            cmbFilterStatus.DataSource = Enum.GetValues(typeof(StatusFilter));
            cmbFilterStatus.SelectedIndex = 0;
            _statusFilter = StatusFilter.All;
        }

        private void frmTradeMasterFile_Load(object sender, EventArgs e)
        {
            dgvTrades.DataSource = _master.GetTrades_All();
        }

        private void btnViewCalculator_Click(object sender, EventArgs e)
        {
            Trade_TradeOpen_OnRequest((Trade)dgvTrades.CurrentRow.DataBoundItem);
        }

        private void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse<StatusFilter>(cmbFilterStatus.SelectedValue.ToString(), out _statusFilter);
        }

        public enum StatusFilter
        {
            All,
            Closed,
            Open

        }

        private void btnFilterByStatus_Click(object sender, EventArgs e)
        {
            filterTradeByStatus();
        }

        private void filterTradeByStatus()
        {
            switch (_statusFilter)
            {
                default:
                case StatusFilter.All:
                    dgvTrades.DataSource = _master.GetTrades_All();
                    break;

                case StatusFilter.Closed:
                    dgvTrades.DataSource = _master.GetTrades_Closed();
                    break;

                case StatusFilter.Open:
                    dgvTrades.DataSource = _master.GetTrades_Open();
                    break;
            }
                



                
        }
    }
}
