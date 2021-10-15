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
        public delegate bool Trade_OnRequest(Trade t);
        public Trade_OnRequest Trade_TradeOpen_OnRequest;
        private master _master { get { return (master)this.Owner; } }

        public frmTradeMasterFile()
        {
            InitializeComponent();

            cmbFilterStatus.SelectedIndex = 0;
        }

        private void frmTradeMasterFile_Load(object sender, EventArgs e)
        {
            dgvTrades.DataSource = _master.GetTrades_All();
        }

        private void btnViewCalculator_Click(object sender, EventArgs e)
        {
            Trade_TradeOpen_OnRequest((Trade)dgvTrades.CurrentRow.DataBoundItem);
        }
    }
}
