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

            DataGridView_TradeOpen_Format(dgvTrades_Open);
            DataGridView_Unofficial_Format(dgvUnofficial);
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

        #region DataGridView Format
        private void DataGridView_Common(DataGridView d)
        {
            d.DefaultCellStyle.SelectionForeColor = Color.White;
            d.AutoGenerateColumns = false;
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.AllowUserToOrderColumns = true;
            d.AllowUserToDeleteRows = false;
            d.AllowUserToAddRows = false;
            d.AllowUserToResizeColumns = true;
            d.AllowUserToResizeRows = false;
            d.ReadOnly = true;
            d.MultiSelect = false;
            d.Font = new Font(new FontFamily("tahoma"), 8.5f, FontStyle.Regular);
            d.Columns.Clear();

            // Id
            var id = new DataGridViewTextBoxColumn();
            id.DataPropertyName = "Id";
            id.Name = "Id";
            id.HeaderText = "Id";
            id.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            id.DefaultCellStyle.Format = Constant.WHOLE_NUMBER;
            id.Width = 80;
            id.ReadOnly = true;
            id.SortMode = DataGridViewColumnSortMode.NotSortable;
            id.Visible = true;
            d.Columns.Add(id);

            // Side
            var Side = new DataGridViewTextBoxColumn();
            Side.DataPropertyName = "Side";
            Side.Name = "Side";
            Side.HeaderText = "Side";
            Side.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Side.Width = 80;
            Side.ReadOnly = true;
            Side.SortMode = DataGridViewColumnSortMode.NotSortable;
            Side.Visible = true;
            d.Columns.Add(Side);

            // Ticker
            var tick = new DataGridViewTextBoxColumn();
            tick.DataPropertyName = "Ticker";
            tick.Name = "Ticker";
            tick.HeaderText = "Ticker";
            tick.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tick.Width = 150;
            tick.ReadOnly = true;
            tick.SortMode = DataGridViewColumnSortMode.NotSortable;
            tick.Visible = true;
            d.Columns.Add(tick);

            // Capital
            var cap = new DataGridViewTextBoxColumn();
            cap.DataPropertyName = "Capital";
            cap.Name = "Capital";
            cap.HeaderText = "Capital";
            cap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            cap.DefaultCellStyle.Format = Constant.MONEY_FORMAT;
            cap.Width = 100;
            cap.ReadOnly = true;
            cap.SortMode = DataGridViewColumnSortMode.NotSortable;
            cap.Visible = true;
            d.Columns.Add(cap);

            // Entry Price Average
            var entryPrice = new DataGridViewTextBoxColumn();
            entryPrice.DataPropertyName = "EntryPriceAvg";
            entryPrice.Name = "EntryPriceAvg";
            entryPrice.HeaderText = "Entry Price Avg.";
            entryPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            entryPrice.DefaultCellStyle.Format = Constant.DECIMAL_UPTO_MAX;
            entryPrice.Width = 100;
            entryPrice.ReadOnly = true;
            entryPrice.SortMode = DataGridViewColumnSortMode.NotSortable;
            entryPrice.Visible = true;
            d.Columns.Add(entryPrice);

            // Lot Size
            var lotSize = new DataGridViewTextBoxColumn();
            lotSize.DataPropertyName = "LotSize";
            lotSize.Name = "lotSize";
            lotSize.HeaderText = "Lot Size";
            lotSize.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            lotSize.DefaultCellStyle.Format = Constant.DECIMAL_UPTO_MAX;
            lotSize.Width = 120;
            lotSize.ReadOnly = true;
            lotSize.SortMode = DataGridViewColumnSortMode.NotSortable;
            lotSize.Visible = true;
            d.Columns.Add(lotSize);

            // Leverage
            var leverage = new DataGridViewTextBoxColumn();
            leverage.DataPropertyName = "Leverage";
            leverage.Name = "Leverage";
            leverage.HeaderText = "Leverage";
            leverage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            leverage.DefaultCellStyle.Format = Constant.DECIMAL_UPTO_TWO;
            leverage.Width = 80;
            leverage.ReadOnly = true;
            leverage.SortMode = DataGridViewColumnSortMode.NotSortable;
            leverage.Visible = true;
            d.Columns.Add(leverage);

            // LeverageCapital
            var levCap = new DataGridViewTextBoxColumn();
            levCap.DataPropertyName = "LeveragedCapital";
            levCap.Name = "LeveragedCapital";
            levCap.HeaderText = "L. Capital";
            levCap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            levCap.DefaultCellStyle.Format = Constant.MONEY_FORMAT;
            levCap.Width = 100;
            levCap.ReadOnly = true;
            levCap.SortMode = DataGridViewColumnSortMode.NotSortable;
            levCap.Visible = true;
            d.Columns.Add(levCap);

            // Trading Style
            var style = new DataGridViewTextBoxColumn();
            style.DataPropertyName = "TradingStyle";
            style.Name = "TradingStyle";
            style.HeaderText = "Trading Style";
            style.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            style.Width = 120;
            style.ReadOnly = true;
            style.SortMode = DataGridViewColumnSortMode.NotSortable;
            style.Visible = true;
            d.Columns.Add(style);
        }
        private void DataGridView_TradeOpen_Format(DataGridView d)
        {
            DataGridView_Common(d);

            // Date Enter
            var dateEnter = new DataGridViewTextBoxColumn();
            dateEnter.DataPropertyName = "DateEnter";
            dateEnter.Name = "DateEnter";
            dateEnter.HeaderText = "Date Enter";
            dateEnter.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dateEnter.DefaultCellStyle.Format = "F"; //Constant.DATE_MMMM_DD_YYYY;
            dateEnter.Width = 200;
            dateEnter.ReadOnly = true;
            dateEnter.SortMode = DataGridViewColumnSortMode.NotSortable;
            dateEnter.Visible = true;
            d.Columns.Add(dateEnter);

            // Day Count
            var days = new DataGridViewTextBoxColumn();
            days.DataPropertyName = "DayCount";
            days.Name = "DayCount";
            days.HeaderText = "Days";
            days.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            days.DefaultCellStyle.Format = Constant.DECIMAL_UPTO_TWO;
            days.Width = 100;
            days.ReadOnly = true;
            days.SortMode = DataGridViewColumnSortMode.NotSortable;
            days.Visible = true;
            d.Columns.Add(days);

            // Side
            var status = new DataGridViewTextBoxColumn();
            status.DataPropertyName = "Status";
            status.Name = "Status";
            status.HeaderText = "Status";
            status.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            status.Width = 100;
            status.ReadOnly = true;
            status.SortMode = DataGridViewColumnSortMode.NotSortable;
            status.Visible = true;
            d.Columns.Add(status);
        }

        private void DataGridView_Unofficial_Format(DataGridView d)
        {
            DataGridView_Common(d);

            // Reason for Entry
            var reason = new DataGridViewTextBoxColumn();
            reason.DataPropertyName = "ReasonForEntry";
            reason.Name = "ReasonForEntry";
            reason.HeaderText = "Reason For Entry";
            reason.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            reason.Width = 160;
            reason.ReadOnly = true;
            reason.SortMode = DataGridViewColumnSortMode.NotSortable;
            reason.Visible = true;
            d.Columns.Add(reason);

            // Counter Bias
            var counter = new DataGridViewTextBoxColumn();
            counter.DataPropertyName = "CounterBias";
            counter.Name = "CounterBias";
            counter.HeaderText = "Counter Bias";
            counter.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            counter.Width = 160;
            counter.ReadOnly = true;
            counter.SortMode = DataGridViewColumnSortMode.NotSortable;
            counter.Visible = true;
            d.Columns.Add(counter);

            // Note
            var note = new DataGridViewTextBoxColumn();
            note.DataPropertyName = "Note";
            note.Name = "Note";
            note.HeaderText = "Note";
            note.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            note.Width = 200;
            note.ReadOnly = true;
            note.SortMode = DataGridViewColumnSortMode.NotSortable;
            note.Visible = true;
            d.Columns.Add(note);

        }
        #endregion
    }
}
