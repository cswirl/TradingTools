using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.DAL;
using TradingTools.Helpers;
//using TradingTools.DAL.Migrations;
using TradingTools.Services;
using TradingTools.Services.Interface;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;

namespace TradingTools
{
    public class master : Form
    {
        //  delegates
        // CalculatorState
        public CalculatorStateCreated CalculatorState_Added;
        public CalculatorStateUpdated CalculatorState_Updated;
        public CalculatorStateDeleted CalculatorState_Deleted;
        // Trade
        public TradeCreated Trade_Officialized;
        public TradeUpdated Trade_Updated;
        public TradeUpdated Trade_Closed;
        public TradeDeleted Trade_Deleted;
        // Trade Challenge
        public TradeChallengeCreated TradeChallenge_Created;
        public TradeChallengeUpdated TradeChallenge_Updated;
        public TradeChallengeUpdated TradeChallenge_Closed;
        public TradeChallengeDeleted TradeChallenge_Deleted;
        // Trade Thread
        public TradeThreadCreated TradeThread_Created;
        // Trade Challenge Prospect
        public TradeChallengeProspectCreated TradeChallengeProspect_Created;
        public TradeChallengeProspectDeleted TradeChallengeProspect_Deleted;
        public TradeChallengeProspectDeletedRange TradeChallengeProspect_DeletedRange;
        //
        public Timer Clock;
        public DelegateHandlers DelegateHandlers { get; set; }

        public TradingToolsDbContext DbContext { get; set; }

        private List<frmRiskRewardCalc> _listOf_frmRiskRewardCalc;
        
        private IContainer components;

        public frmDashboard _frmDashboard { get; set; }
        public frmTradeMasterFile _frmTradeMasterFile { get; set; }
        public frmTradeChallengeMasterFile _frmTradeChallengeMasterFile { get; set; }


        public master()
        {
            InitializeComponent();

            /// make master invisible to the user
            this.Visible = false;
            this.Size = new System.Drawing.Size(0, 0);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Load += new EventHandler(master_Load);
            ///

            InitializeDbContext();
            _listOf_frmRiskRewardCalc = new();
            this.DelegateHandlers = new(this);
            //
            Clock.Interval = Presentation.INTERNAL_TIMER_REFRESH_VALUE;

            // Dashboard (gateway form)
            _frmDashboard = new(this);
            _frmDashboard.StartPosition = FormStartPosition.CenterScreen;
            _frmDashboard.Show();
        }

        private void master_Load(object sender, EventArgs e)
        {

        }

        private void InitializeDbContext()
        {
            DbContext = new();
            // This will load all the CalculatorStates and all the Trades records - Except to the closed Trades - enough for current application requirement
            //Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load(DbContext.CalculatorStates
            //    .Where(x => x.Trade.Status.Equals("open"))
            //    .Include(x => x.Trade));

            //_trades_open_bindingList = DbContext.Trades.Local.ToBindingList();

            //DbContext.Database.Migrate();
        }

        public void FormTradeMasterFile()
        {
            if (_frmTradeMasterFile == default || _frmTradeMasterFile.IsDisposed)
            {
                _frmTradeMasterFile = new(this);
                _frmTradeMasterFile.Show();
                
            }
            else
            {
                if (_frmTradeMasterFile.WindowState == FormWindowState.Minimized) _frmTradeMasterFile.WindowState = FormWindowState.Normal;
                _frmTradeMasterFile.Focus();
            }
        }

        public void FormTradeChallengeMasterFile()
        {
            if (_frmTradeChallengeMasterFile == default || _frmTradeChallengeMasterFile.IsDisposed)
            {
                _frmTradeChallengeMasterFile = new(this);
                _frmTradeChallengeMasterFile.Show();
                // delegates
            }
            else
            {
                if (_frmTradeChallengeMasterFile.WindowState == FormWindowState.Minimized) _frmTradeChallengeMasterFile.WindowState = FormWindowState.Normal;
                _frmTradeChallengeMasterFile.Focus();
            }
        }

        #region Risk Reward Calculator Forms
        private void registerNewRiskRewardCalcForm(frmRiskRewardCalc form)
        {
            //Delegates assignment here
            form.FormClosed += this.FormRRC_FormClosed;
            form.CalculatorState_Officializing_IsCancelled += this.DelegateHandlers.CalculatorState_Officializing_IsCancelled_Handler;
            form.Trade_Closing_IsCancelled += this.DelegateHandlers.Trade_Closing_IsCancelled_Handler;
            this.Trade_Deleted += form.MarkAsDeleted;
            this.Trade_Updated += form.Trade_Updated_Handler;
            
            _listOf_frmRiskRewardCalc.Add(form);
        }

        private void activateRiskRewardCalcForm(frmRiskRewardCalc rrc)
        {
            rrc.WindowState = FormWindowState.Normal;
            rrc.Focus();
        }

        private void FormRRC_FormClosed(object sender, FormClosedEventArgs e)
        {
            _listOf_frmRiskRewardCalc.Remove((frmRiskRewardCalc)sender);
            // another contingency exit - to avoid master hidden in the background
            if (_frmDashboard == default && _listOf_frmRiskRewardCalc.Count < 1) Application.Exit();
        }

        public frmRiskRewardCalc FormRRC_Trade_Spawn(Trade t)
        {
            // TODO: the EF core list is being renew whenever a Trade or CalculatorState is Updated fron the 
            // - maybe use id or something, maybe hash
            //var rrc = _listOf_frmRRC_Long.Find(x => x.CalculatorState.GetHashCode().Equals(c.GetHashCode()));
            var rrc = _listOf_frmRiskRewardCalc.Find(x => x.Trade?.Equals(t) ?? false);       // THOUGH THIS IS WORKING FINE
            if (rrc != null)
            {
                activateRiskRewardCalcForm(rrc);
                return rrc;
            }
            else
            {
                var riskRewardCalc = TradeService.RiskRewardCalcGetInstance(t.Side);
                var form = new frmRiskRewardCalc(riskRewardCalc, this);
                form.State = t.Status.Equals("open") ? RiskRewardCalcState.TradeOpen : RiskRewardCalcState.TradeClosed;
                form.Trade = t;
                form.CalculatorState = t.CalculatorState;

                registerNewRiskRewardCalcForm(form);
                form.Show();

                return form;
            }
        }

        public frmRiskRewardCalc FormRRC_Loaded_Spawn(CalculatorState c)
        {
            var rrc = _listOf_frmRiskRewardCalc.Find(x => x.CalculatorState?.Equals(c) ?? false);
            if (rrc != null)
            {
                activateRiskRewardCalcForm (rrc);
                return rrc;
            }
            else
            {
                var riskRewardCalc = TradeService.RiskRewardCalcGetInstance(c.Side);
                var form = new frmRiskRewardCalc(riskRewardCalc, this);
                form.State = RiskRewardCalcState.Loaded;
                form.CalculatorState = c;

                registerNewRiskRewardCalcForm(form);
                form.Show();

                return form;
            }
        }

        public frmRiskRewardCalc FormRRC_Long_Empty_Spawn()
        {
            var rrc = TradeService.RiskRewardCalcGetInstance("long");
            return FormRRC_Empty_Spawn(rrc);
        }

        public frmRiskRewardCalc FormRRC_Short_Empty_Spawn()
        {
            var rrc = TradeService.RiskRewardCalcGetInstance("short");
            return FormRRC_Empty_Spawn(rrc); ;
        }

        private frmRiskRewardCalc FormRRC_Empty_Spawn(IRiskRewardCalc riskRewardCalc)
        {
            var form = new frmRiskRewardCalc(riskRewardCalc, this);
            registerNewRiskRewardCalcForm(form);
            form.Show();

            return form;
        }
        #endregion


        #region Repository
        /// <summary>
        /// CalculatorState
        /// </summary>
        // Return True for sucess
        public bool CalculatorState_Create(CalculatorState calculatorState)
        {
            DbContext.CalculatorState.Add(calculatorState);
            DbContext.SaveChanges();
            CalculatorState_Added?.Invoke(calculatorState);

            return true;
        }

        public bool CalculatorState_Update(CalculatorState calculatorState)
        {
            DbContext.CalculatorState.Update(calculatorState);
            DbContext.SaveChanges();
            CalculatorState_Updated?.Invoke(calculatorState);

            return true;
        }

        public bool CalculatorState_Delete(CalculatorState calculatorState)
        {
            DbContext.CalculatorState.Remove(calculatorState);
            DbContext.SaveChanges();
            CalculatorState_Deleted?.Invoke(calculatorState);

            return true;
        }

        public List<CalculatorState> CalculatorStates_GetAll(bool descending = false)
        {
            var c = DbContext.CalculatorState
                .Where(x => x.TradeId == null)
                .AsQueryable();
            
            if (descending) c = c.OrderByDescending(c => c.Id);

            return c.ToList();
        }

        /// <summary>
        /// Trade
        /// </summary>
        internal bool Trade_Create(Trade t)
        {
            DbContext.Trade.Add(t);
            DbContext.SaveChanges();
            Trade_Officialized?.Invoke(t);

            return true;
        }

        internal bool Trade_Update(Trade t)
        {
            DbContext.Trade.Update(t);
            DbContext.SaveChanges();

            Trade_Updated?.Invoke(t);

            return true;
        }

        internal bool Trade_Close(Trade t)
        {
            DbContext.Trade.Update(t);
            DbContext.SaveChanges();
            Trade_Closed?.Invoke(t);

            return true;
        }

        internal bool Trade_Delete(Trade t)
        {
            t.IsDeleted = true;
            DbContext.Trade.Update(t);
            DbContext.SaveChanges();
            Trade_Deleted?.Invoke(t);

            return true;
        }

        public List<Trade> Trades_GetAll(bool descending = false)
        {
            var t = DbContext.Trade
                .Include(x => x.CalculatorState)
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            if (descending) t = t.OrderByDescending(x => x.Id);

            return t.ToList();
        }

        public List<Trade> Trades_GetOpen(bool descending = false)
        {
            var t = DbContext.Trade
                .Where(x => x.Status.Equals("open") && !x.IsDeleted)
                .Include(x => x.CalculatorState)
                .AsQueryable();

            if (descending) t = t.OrderByDescending(x => x.DateEnter);

            return t.ToList();
        }

        public List<Trade> Trades_GetClosed(bool descending = false)
        {
            var t = DbContext.Trade
                .Where(x => x.Status.Equals("closed") && !x.IsDeleted)
                .Include(x => x.CalculatorState)
                .AsQueryable();

            if (descending) t = t.OrderByDescending(x => x.DateExit);

            return t.ToList();
        }

        public List<Trade> Trades_GetDeleted(bool descending = false)
        {
            var t = DbContext.Trade
                .Where(x => x.IsDeleted)
                .Include(x => x.CalculatorState)
                .AsQueryable();

            if (descending) t = t.OrderByDescending(x => x.Id);

            return t.ToList();
        }

        /// <summary>
        ///  Trade Challenge
        /// </summary>
        /// 
        public bool TradeChallenge_Create(TradeChallenge tc)
        {
            DbContext.TradeChallenge.Add(tc);
            DbContext.SaveChanges();
            TradeChallenge_Created?.Invoke(tc);

            return true;
        }

        public bool TradeChallenge_Update(TradeChallenge tc)
        {
            DbContext.TradeChallenge.Update(tc);
            DbContext.SaveChanges();
            TradeChallenge_Updated?.Invoke(tc);

            return true;
        }

        public bool TradeChallenge_Close(TradeChallenge tc)
        {
            tc.IsOpen = false;
            DbContext.TradeChallenge.Update(tc);
            DbContext.SaveChanges();
            TradeChallenge_Closed?.Invoke(tc);

            return true;
        }

        public bool TradeChallenge_Delete(TradeChallenge tc)
        {
            DbContext.TradeChallenge.Remove(tc);
            DbContext.SaveChanges();
            TradeChallenge_Deleted?.Invoke(tc);

            return true;
        }

        public List<TradeChallenge> TradeChallenge_GetOpen(bool descending = false)
        {
            var tc = DbContext.TradeChallenge.Where(x => x.IsOpen).AsQueryable();

            if (descending) tc = tc.OrderByDescending(x => x.Id);

            return tc.ToList();
                
        }
        public List<TradeChallenge> TradeChallenge_GetClosed(bool descending = false)
        {
            var tc = DbContext.TradeChallenge.Where(x => !x.IsOpen).AsQueryable();

            if (descending) tc = tc.OrderByDescending(x => x.Id);

            return tc.ToList();

        }

        /// <summary>
        ///  Trade Thread
        /// </summary>
        /// 
        public bool TradeThread_Create(TradeThread tr)
        {
            DbContext.TradeThread.Add(tr);
            DbContext.SaveChanges();
            TradeThread_Created?.Invoke(tr);

            return true;
        }

        public List<Trade> TradeThread_GetAllTrades(int tradeChallengeId)
        {
            return DbContext.Trade
                .Include(x => x.CalculatorState)
                .Include(x => x.TradeThread)
                .Where(x => !x.IsDeleted && x.TradeThread.TradeChallengeId == tradeChallengeId)
                .ToList();

            //return DbContext.TradeThread
            //    .Include(tr => tr.Trade).ThenInclude(t => t.CalculatorState)
            //    .Where(tr => tr.TradeChallengeId == tradeChallengeId && tr.TradeId != default)
            //    .Select(tr => tr.Trade).Where(t => !t.IsDeleted)
            //    .ToList();
        }

        public List<Trade> TradeThread_GetActiveTrade(int tradeChallengeId)
        {
            return DbContext.Trade
                .Include(x => x.CalculatorState)
                .Include(x => x.TradeThread).ThenInclude(tr => tr.TradeChallenge)
                .Where(x => !x.IsDeleted && x.Status.Equals("open") && x.TradeThread.TradeChallengeId == tradeChallengeId)
                .ToList();
                

            //return DbContext.TradeThread
            //    .Include(tr => tr.Trade).ThenInclude(t => t.CalculatorState).Where(tr => tr.Trade.Status.Equals("open"))
            //    .Where(tr => tr.TradeChallengeId == tradeChallengeId && tr.TradeId != default)
            //    .Select(tr => tr.Trade).Where(t => !t.IsDeleted)
            //    .ToList();
        }

        public List<Trade> TradeThread_GetTradeHistory(int tradeChallengeId, bool descending = false)
        {
            var q = DbContext.Trade
                .Include(x => x.CalculatorState)
                .Include(x => x.TradeThread).ThenInclude(tr => tr.TradeChallenge)
                .Where(x => !x.IsDeleted && x.Status.Equals("closed") && x.TradeThread.TradeChallengeId == tradeChallengeId)
                .AsQueryable();

            if (descending) q = q.OrderByDescending(x => x.DateExit);

            return q.ToList();

            //return DbContext.TradeThread
            //    .Include(tr => tr.Trade).ThenInclude(t => t.CalculatorState).Where(tr => tr.Trade.Status.Equals("closed"))
            //    .Where(tr => tr.TradeChallengeId == tradeChallengeId && tr.TradeId != default)
            //    .Select(tr => tr.Trade).Where(t => !t.IsDeleted)
            //    .OrderBy(t => t.DateExit)
            //    .ToList();
        }

        public int TradeThread_GetTradeChallengeId(int tradeId)
        {
            var x = DbContext.TradeThread
                .Where(tr => tr.TradeId == tradeId)
                .FirstOrDefault();

            // return zero if no match found
            return x?.TradeChallengeId ?? 0;
        }

        /// <summary>
        ///  Trade Challenge Prospect
        /// </summary>
        /// 
        public bool TradeChallengeProspect_Create(TradeChallengeProspect tcp)
        {
            DbContext.TradeChallengeProspect.Add(tcp);
            DbContext.SaveChanges();
            TradeChallengeProspect_Created?.Invoke(tcp);

            return true;
        }

        public bool TradeChallengeProspect_Delete(TradeChallengeProspect[] tcp)
        {
            DbContext.TradeChallengeProspect.RemoveRange(tcp);
            DbContext.SaveChanges();
            TradeChallengeProspect_DeletedRange?.Invoke(tcp);

            return true;
        }

        public bool TradeChallengeProspect_Delete(CalculatorState c)
        {
            var tcp = DbContext.TradeChallengeProspect
                .Where(tcp => tcp.CalculatorStateId == c.Id)
                .FirstOrDefault();
            // when there's nothing to remove - just return success/true
            if (tcp == default) return true;

            DbContext.TradeChallengeProspect.Remove(tcp);
            DbContext.SaveChanges();
            TradeChallengeProspect_Deleted?.Invoke(tcp);

            return true;
        }

        public List<CalculatorState> TradeChallengeProspect_GetAll(int tradeChallengeId, bool descending = false)
        {
            var c = DbContext.CalculatorState
                .Include(c => c.TradeChallengeProspect)
                .Where(c => c.TradeChallengeProspect.TradeChallengeId == tradeChallengeId
                    && c.TradeId == default)
                .AsQueryable();

            if (descending) c = c.OrderByDescending(c => c.TradeId);

            return c.ToList();
        }

        public int TradeChallengeProspect_GetTradeChallengeId(int calculatorStateId)
        {
            var p = DbContext.TradeChallengeProspect
                .Include(tcp => tcp.CalculatorState)
                .Where(tcp => tcp.CalculatorStateId == calculatorStateId)
                .FirstOrDefault();
   
            if (p != default) 
                return p.TradeChallengeId;

            return 0;
        }
        #endregion

        #region UNUSED
        private BindingSource _calculatorStates_unsaved;
        public BindingSource GetCalculatorStates_Unsaved_BindingSource()
        {
            _calculatorStates_unsaved.DataSource = _listOf_frmRiskRewardCalc
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Clock = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Clock
            // 
            this.Clock.Enabled = true;
            // 
            // master
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "master";
            this.ResumeLayout(false);

        }
    }
}