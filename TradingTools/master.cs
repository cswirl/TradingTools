using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.DAL;
using TradingTools.Services;
using TradingTools.Services.Interface;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;

namespace TradingTools
{
    public class master : Form
    {
        Action<Trade> tradeOfficialized;
        Action<Trade> tradeClosed;
        Action<Trade> tradeDeleted;
        Action<Trade> tradeUpdated;

        private List<frmRiskRewardCalc> _listOf_frmRRC_Long;
        private BindingList<CalculatorState> _calculatorStates_unofficial_bindingList;
        
        public TradingToolsDbContext DbContext { get; set; }
        public frmCalculatorStates _frmCalcStates { get; set; }
        public frmTradeMasterFile _frmTradeMasterFile { get; set; }
        public TradeService TradeService { get; private set; }

        public master()
        {
            // make master invisible to the user
            this.Visible = false;
            this.Size = new System.Drawing.Size(1, 1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Load += new EventHandler(master_Load);

            // Initialize Components
            InitializeDbContext();
            _listOf_frmRRC_Long = new();


            // gateway form
            _frmCalcStates = new();
            _frmCalcStates.Owner = this;
            _frmCalcStates.StartPosition = FormStartPosition.CenterScreen;
            _frmCalcStates.Show();
            // delegates
            _frmCalcStates.FormRRCLong_Empty_Open += this.FormRRCLong_Empty_Spawn;
            _frmCalcStates.FormRRC_Short_Empty_Open += this.FormRRC_Short_Empty_Spawn;
            _frmCalcStates.CalculatorState_Loaded_OnRequest += this.FormRRC_Loaded_Spawn;
            _frmCalcStates.Trade_TradeOpen_OnRequest += this.FormRRC_Trade_Spawn;
            //
            _frmCalcStates.FormTradeMasterFile += this.FormTradeMasterFile;
            //
            this.tradeOfficialized += _frmCalcStates.Trade_Officialized;
            this.tradeClosed += _frmCalcStates.Trade_Closed;
            this.tradeDeleted += _frmCalcStates.Trade_Deleted;
            this.tradeUpdated += _frmCalcStates.Trade_Updated;

        }

        private void master_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(0, 0);

        }

        private void InitializeDbContext()
        {
            DbContext = new();
            // This will load all the CalculatorStates and all the Trades records - Except to the closed Trades - enough for current application requirement
            //Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load(DbContext.CalculatorStates
            //    .Where(x => x.Trade.Status.Equals("open"))
            //    .Include(x => x.Trade));

            //_trades_open_bindingList = DbContext.Trades.Local.ToBindingList();
        }

        private void FormTradeMasterFile(object sender, EventArgs e)
        {
            if (_frmTradeMasterFile == default || _frmTradeMasterFile.IsDisposed)
            {
                _frmTradeMasterFile = new();
                _frmTradeMasterFile.Owner = this;
                _frmTradeMasterFile.Show();
                // delegates
                _frmTradeMasterFile.Trade_TradeOpen_OnRequest += this.FormRRC_Trade_Spawn;
                this.tradeOfficialized += _frmTradeMasterFile.Trade_Officialized;
                this.tradeClosed += _frmTradeMasterFile.Trade_Closed;
            }
            else
            {
                if (_frmTradeMasterFile.WindowState == FormWindowState.Minimized) _frmTradeMasterFile.WindowState = FormWindowState.Normal;
                _frmTradeMasterFile.Focus();
            }
        }

        private bool FormRRC_Trade_Spawn(Trade t)
        {
            // TODO: the EF core list is being renew whenever a Trade or CalculatorState is Updated fron the 
            // - maybe use id or something, maybe hash
            //var rrc = _listOf_frmRRC_Long.Find(x => x.CalculatorState.GetHashCode().Equals(c.GetHashCode()));
            var rrc = _listOf_frmRRC_Long.Find(x => x.Trade?.Equals(t) ?? false);       // THOUGH THIS IS WORKING FINE
            if (rrc != null)
            {
                rrc.WindowState = FormWindowState.Normal;
                rrc.Focus();
            }
            else
            {
                var riskRewardCalc = TradeService.RiskRewardCalcGetInstance(t.Side);
                var form = new frmRiskRewardCalc(riskRewardCalc);
                form.State = t.Status.Equals("open") ? RiskRewardCalcState.TradeOpen : RiskRewardCalcState.TradeClosed;
                form.Owner = this;
                form.Trade = t;
                form.CalculatorState = t.CalculatorState;
                form.Show();
                //Delegates assignment here
                form.FormClosing += (object sender, FormClosingEventArgs e) => _listOf_frmRRC_Long.Remove((frmRiskRewardCalc)sender);
                this.tradeDeleted += form.MarkAsDeleted;
                this.tradeUpdated += form.Trade_Updated;

                _listOf_frmRRC_Long.Add(form);
            }

            return true;
        }

        private bool FormRRC_Loaded_Spawn(CalculatorState c)
        {
            var rrc = _listOf_frmRRC_Long.Find(x => x.CalculatorState?.Equals(c) ?? false);
            if (rrc != null)
            {
                rrc.WindowState = FormWindowState.Normal;
                rrc.Focus();
            }
            else
            {
                var riskRewardCalc = TradeService.RiskRewardCalcGetInstance(c.Side);
                var form = new frmRiskRewardCalc(riskRewardCalc);
                form.State = RiskRewardCalcState.Loaded;
                form.Owner = this;
                form.CalculatorState = c;
                form.Show();
                //Delegates assignment here
                form.FormClosing += (object sender, FormClosingEventArgs e) => _listOf_frmRRC_Long.Remove((frmRiskRewardCalc)sender);
                this.tradeDeleted += form.MarkAsDeleted;
                this.tradeUpdated += form.Trade_Updated;

                _listOf_frmRRC_Long.Add(form);
            }

            return true;
        }

        private void FormRRCLong_Empty_Spawn(object sender, EventArgs e)
        {
            var rrc = TradeService.RiskRewardCalcGetInstance("long");
            FormRRC_Empty_Spawn(rrc);
        }

        private void FormRRC_Short_Empty_Spawn(object sender, EventArgs e)
        {
            var rrc = TradeService.RiskRewardCalcGetInstance("short");
            FormRRC_Empty_Spawn(rrc);
        }

        private void FormRRC_Empty_Spawn(IRiskRewardCalc riskRewardCalc)
        {
            var form = new frmRiskRewardCalc(riskRewardCalc);
            form.Owner = this;
            form.Show();
            //Delegates assignment here
            form.FormClosing += (object sender, FormClosingEventArgs e) => _listOf_frmRRC_Long.Remove((frmRiskRewardCalc)sender);
            this.tradeDeleted += form.MarkAsDeleted;
            this.tradeUpdated += form.Trade_Updated;

            _listOf_frmRRC_Long.Add(form);
        }

        public BindingList<CalculatorState> GetCalculatorStates_Unofficial_BindingList()
        {
            _calculatorStates_unofficial_bindingList = new BindingList<CalculatorState>(DbContext.CalculatorStates
                .Where(x => x.TradeId == null)
                .ToList());

            return _calculatorStates_unofficial_bindingList;
        }

        // temporary fix - an event listener sounds perfect for this - maybe a queueu
        // Return True if no error
        public bool CalculatorState_Add(CalculatorState calculatorState)
        {
            DbContext.CalculatorStates.Add(calculatorState);    // order matters here
            DbContext.SaveChanges();
            _calculatorStates_unofficial_bindingList.Add(calculatorState);

            return true;
        }

        public bool CalculatorState_Update()
        {
            // This one line code is sufficed sinced the CalculatorState objects are referenced properly
            DbContext.SaveChanges();
            _frmCalcStates.dgvUnofficial_Invalidate();       // Refereshes the DataGridView

            return true;
        }

        public bool CalculatorState_Delete(CalculatorState calculatorState)
        {
            DbContext.CalculatorStates.Remove(calculatorState);
            DbContext.SaveChanges();
            _calculatorStates_unofficial_bindingList.Remove(calculatorState);

            return true;
        }

        public BindingList<Trade> GetTrades_All()
        {
            return new BindingList<Trade>(DbContext.Trades
                .Include(x => x.CalculatorState).ToList());
        }

        public BindingList<Trade> GetTrades_Closed()
        {
            return new BindingList<Trade>(DbContext.Trades
                .Where(x => x.Status.Equals("closed"))
                .Include(x => x.CalculatorState).ToList());
        }

        public BindingList<Trade> GetTrades_Open()
        {
            return new BindingList<Trade>(DbContext.Trades
                .Where(x => x.Status.Equals("open"))
                .Include(x => x.CalculatorState).ToList());
        }


        internal bool Trade_Add(Trade t)
        {
            DbContext.Trades.Add(t);
            DbContext.SaveChanges();
            tradeOfficialized?.Invoke(t);

            return true;
        }

        internal bool Trade_Close(Trade t)
        {
            DbContext.SaveChanges();
            tradeClosed?.Invoke(t);

            return true;
        }

        internal bool Trade_Delete(Trade t)
        {
            DbContext.Trades.Remove(t);
            DbContext.CalculatorStates.Remove(t.CalculatorState);
            DbContext.SaveChanges();
            tradeDeleted?.Invoke(t);

            return true;
        }

        internal bool Trade_Update(Trade t)
        {
            DbContext.SaveChanges();
            tradeUpdated?.Invoke(t);

            return true;
        }


        #region UNUSED
        private BindingSource _calculatorStates_unsaved;
        public BindingSource GetCalculatorStates_Unsaved_BindingSource()
        {
            _calculatorStates_unsaved.DataSource = _listOf_frmRRC_Long
                .Where(x => x.State == RiskRewardCalcState.Empty)
                .Select(x => x.CalculatorState)
                .ToList();

            return _calculatorStates_unsaved;


            //// Another implementation for a list of Unsaved RRC Form i.e. their Id == default(int) - maybe create a dedicated List() for this
            //// They will have their own DataGridView - same functionality as the first dgv
            //var rrc_forms = Array.ConvertAll(this.OwnedForms, form => (frmRiskRewardCalc_Long)form).ToList();
            //var rrc = rrc_forms.Find(x => x.CalculatorState.Id == default(int));
            //if (rrc != null)
            //{
            //    rrc.WindowState = FormWindowState.Normal;
            //    rrc.Focus();
            //}
            //else
            //{

            //}
        }
        #endregion
    }
}