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
    public partial class frmDashboard : Form
    {
        // fields
        private BindingList<CalculatorState> _unofficialCalculatorStates;
        private BindingList<Trade> _openTrades;

        private master _master;

        public frmDashboard(master master)
        {
            InitializeComponent();

            _master = master;
            //
            appInitialize();
        }

        private void appInitialize()
        {
            // delegates
            _master.RefreshTimer.Tick += (s, e) => { statusMessage.Text = "Status . . ."; };
            /// Use delegate from the master - these are invoked right after DbContext CRUD statements
            _master.Trade_Officialized += this.Trade_Officialized;
            _master.Trade_Updated += this.Trade_Updated;
            _master.Trade_Closed += this.Trade_Closed;
            _master.Trade_Deleted += this.Trade_Deleted;
            _master.CalculatorState_Added += this.CalculatorState_Added;
            _master.CalculatorState_Updated += this.CalculatorState_Updated;
            _master.CalculatorState_Deleted += this.CalculatorState_Deleted;
            //
        }

        private void frmDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void dgvUnofficial_SetDatasource()
        {
            _unofficialCalculatorStates = new(_master.CalculatorStates_GetAll(true));
            dgvUnofficial.DataSource = _unofficialCalculatorStates;
        }

        public void dgvTrades_Open_SetDatasource()
        {
            _openTrades = new(_master.Trades_GetOpen(true));
            dgvTrades_Open.DataSource = _openTrades;
        }

        private void btnOpenCalc_Empty_Click(object sender, EventArgs e)
        {
            _master.FormRRC_Long_Empty_Spawn();
        }

        private void btnOpenCalcShort_Empty_Click(object sender, EventArgs e)
        {
            _master.FormRRC_Short_Empty_Spawn();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            dgvTrades_Open_SetDatasource();
            dgvUnofficial_SetDatasource();
        }

        private void menuTradeMasterFile_Click(object sender, EventArgs e)
        {
            _master.FormTradeMasterFile();
        }

        private void dgvTrades_Open_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _master.FormRRC_Trade_Spawn((Trade)dgvTrades_Open.CurrentRow.DataBoundItem);
        }

        private void dgvUnofficial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _master.FormRRC_Loaded_Spawn((CalculatorState)dgvUnofficial.CurrentRow.DataBoundItem);
        }

        private void frmDashboard_FormClosing(object sender, FormClosingEventArgs e)
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
            _master.FormTradeChallengeMasterFile();
        }

        #region Delegate Handlers
        public void Trade_Officialized(Trade t)
        {
            _unofficialCalculatorStates.Remove(t.CalculatorState);
            _openTrades.Insert(0, t);
        }

        public void Trade_Closed(Trade t)
        {
            if (_openTrades.Remove(t)) statusMessage.Text = $"Trade Id: {t.Id} was closed externally";
        }

        public void Trade_Deleted(Trade t)
        {
            if (_openTrades.Remove(t)) statusMessage.Text = $"Trade Id: {t.Id} was deleted externally";
        }

        public void Trade_Updated(Trade t)
        {
            if (t.Status.Equals("open"))
            {
                statusMessage.Text = $"Trade Id: {t.Id} was updated externally";
                dgvTrades_Open.Invalidate();
            }
        }

        private void CalculatorState_Added(CalculatorState c)
        {
            _unofficialCalculatorStates.Insert(0, c);
        }

        private void CalculatorState_Deleted(CalculatorState c)
        {
            if (_unofficialCalculatorStates.Remove(c)) statusMessage.Text = $"CalculatorState Id: {c.Id} was deleted externally";
        }

        private void CalculatorState_Updated(CalculatorState c)
        {
            dgvUnofficial.Invalidate();
            statusMessage.Text = $"CalculatorState Id: {c.Id} was updated externally";
        }
        #endregion
    }
}
