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
using TradingTools.Trunk.Entity;

namespace TradingTools
{
    public partial class frmCalculatorStates : Form
    {
        //delegates
        public delegate frmRiskRewardCalc CalculatorState_OnRequest(CalculatorState c);
        public CalculatorState_OnRequest CalculatorState_Loaded_OnRequest;

        public delegate frmRiskRewardCalc Trade_OnRequest(Trade t);
        public Trade_OnRequest Trade_TradeOpen_OnRequest;

        //public event EventHandler FormRRC_Long_Empty_Open;
        //public event EventHandler FormRRC_Short_Empty_Open;
        public event EventHandler FormTradeMasterFile;
        public event EventHandler FormTradeChallengeMasterFile;

        // fields
        private BindingList<CalculatorState> _calculatorStates_unofficial_bindingList;
        private BindingList<Trade> _trades_open_bindingList;

        private master _master { get { return (master)this.Owner; } }

        public frmCalculatorStates()
        {
            InitializeComponent();
        }

        private void frmCalculatorStates_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnOpenCalc_Click(object sender, EventArgs e)
        {
            CalculatorState_Loaded_OnRequest((CalculatorState)dgvUnofficial.CurrentRow.DataBoundItem);
        }

        public void dgvUnofficial_SetDatasource()
        {
            _calculatorStates_unofficial_bindingList = _master.GetCalculatorStates_Unofficial_BindingList();
            dgvUnofficial.DataSource = _calculatorStates_unofficial_bindingList;
        }

        public void dgvUnofficial_Invalidate()
        {
            dgvUnofficial.Invalidate();
        }

        public void dgvTrades_Open_SetDatasource()
        {
            _trades_open_bindingList = _master.GetTrades_Open();
            dgvTrades_Open.DataSource = _trades_open_bindingList;
        }

        public void dgvTrades_Open_Invalidate()
        {
            dgvTrades_Open.Invalidate();
        }

        private void btnOpenCalc_Empty_Click(object sender, EventArgs e)
        {
            _master.FormRRC_Long_Empty_Spawn();
        }

        private void btnOpenCalcShort_Empty_Click(object sender, EventArgs e)
        {
            _master.FormRRC_Short_Empty_Spawn();
        }

        private void frmCalculatorStates_Load(object sender, EventArgs e)
        {
            dgvTrades_Open_SetDatasource();
            dgvUnofficial_SetDatasource();
        }

        private void btnViewOfficial_Click(object sender, EventArgs e)
        {
            Trade_TradeOpen_OnRequest?.Invoke((Trade)dgvTrades_Open.CurrentRow.DataBoundItem);
        }

        private void menuTradeMasterFile_Click(object sender, EventArgs e)
        {
            FormTradeMasterFile?.Invoke(this, EventArgs.Empty);
        }

        public void Trade_Officialized(Trade t)
        {
            _calculatorStates_unofficial_bindingList.Remove(t.CalculatorState);
            _trades_open_bindingList.Add(t);
        }

        public void Trade_Closed(Trade t)
        {
            _trades_open_bindingList.Remove(t);
        }

        public void Trade_Deleted(Trade t)
        {
            if (t.Status.Equals("open")) _trades_open_bindingList.Remove(t);
        }

        public void Trade_Updated(Trade t)
        {
            if (t.Status.Equals("open")) dgvTrades_Open.Invalidate();
        }

        private void dgvTrades_Open_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Trade_TradeOpen_OnRequest?.Invoke((Trade)dgvTrades_Open.CurrentRow.DataBoundItem);
        }

        private void dgvUnofficial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CalculatorState_Loaded_OnRequest((CalculatorState)dgvUnofficial.CurrentRow.DataBoundItem);
        }

        private void frmCalculatorStates_FormClosing(object sender, FormClosingEventArgs e)
        {
            // This will just close the form without asking again - when triggered by Application.Exit()
            if (e.CloseReason == System.Windows.Forms.CloseReason.FormOwnerClosing) return;

            DialogResult objDialog = MessageBox.Show("This will close the application\n\nDo you want to proceed ?"
                , "Terminating the program",  MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (objDialog == DialogResult.Cancel)
            {
                // keep the form open
                e.Cancel = true;
            }
        }

        private void menuTradeChallenge_Click(object sender, EventArgs e)
        {
            FormTradeChallengeMasterFile?.Invoke(null, null);
        }
    }
}
