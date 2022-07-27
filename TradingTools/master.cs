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
        Action<Trade> tradeOfficialized;
        Action<Trade> tradeClosed;
        Action<Trade> tradeDeleted;
        Action<Trade> tradeUpdated;
        //
        Action<TradeChallenge> tradeChallengeDeleted;

        private List<frmRiskRewardCalc> _listOf_frmRiskRewardCalc;
        private BindingList<CalculatorState> _calculatorStates_unofficial_bindingList;
        
        public TradingToolsDbContext DbContext { get; set; }
        public frmCalculatorStates _frmCalcStates { get; set; }
        public frmTradeMasterFile _frmTradeMasterFile { get; set; }
        public frmTradeChallengeMasterFile _frmTradeChallengeMasterFile { get; set; }
        public TradeService TradeService { get; private set; }
        public DelegateHandlers DelegateHandlers { get; set; }

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
            _listOf_frmRiskRewardCalc = new();
            this.DelegateHandlers = new(this);


            // gateway form
            _frmCalcStates = new();
            _frmCalcStates.Owner = this;
            _frmCalcStates.StartPosition = FormStartPosition.CenterScreen;
            _frmCalcStates.Show();
            // delegates
            _frmCalcStates.CalculatorState_Loaded_OnRequest += this.FormRRC_Loaded_Spawn;
            _frmCalcStates.Trade_TradeOpen_OnRequest += this.FormRRC_Trade_Spawn;
            //
            _frmCalcStates.FormTradeMasterFile += this.FormTradeMasterFile;
            _frmCalcStates.FormTradeChallengeMasterFile += this.FormTradeChallengeMasterFile;
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

        private void FormTradeChallengeMasterFile(object sender, EventArgs e)
        {
            if (_frmTradeChallengeMasterFile == default || _frmTradeChallengeMasterFile.IsDisposed)
            {
                _frmTradeChallengeMasterFile = new();
                _frmTradeChallengeMasterFile.Owner = this;
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
            form.Owner = this;
            form.FormClosed += this.FormRRC_FormClosed;
            form.Trade_Officializing_Cancelled += this.DelegateHandlers.Trade_Officializing_Cancelled;
            form.Trade_Closing_Cancelled += this.DelegateHandlers.Trade_Closing_Cancelled;
            this.tradeDeleted += form.MarkAsDeleted;
            this.tradeUpdated += form.Trade_Updated_Handler;
            
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
            // 1 means this is the last form
            if (this.OwnedForms.Length == 1) Application.Exit();
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
                var form = new frmRiskRewardCalc(riskRewardCalc);
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
                var form = new frmRiskRewardCalc(riskRewardCalc);
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
            var form = new frmRiskRewardCalc(riskRewardCalc);
            registerNewRiskRewardCalcForm(form);
            form.Show();

            return form;
        }
        #endregion


        public BindingList<CalculatorState> GetCalculatorStates_Unofficial_BindingList()
        {
            _calculatorStates_unofficial_bindingList = new BindingList<CalculatorState>(DbContext.CalculatorState
                .Where(x => x.TradeId == null)
                .ToList());

            return _calculatorStates_unofficial_bindingList;
        }

        // temporary fix - an event listener sounds perfect for this - maybe a queueu
        // Return True if no error
        public bool CalculatorState_Add(CalculatorState calculatorState)
        {
            DbContext.CalculatorState.Add(calculatorState);    // order matters here
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
            DbContext.CalculatorState.Remove(calculatorState);
            DbContext.SaveChanges();
            _calculatorStates_unofficial_bindingList.Remove(calculatorState);

            return true;
        }

        public BindingList<Trade> GetTrades_All()
        {
            return new BindingList<Trade>(DbContext.Trade
                .Include(x => x.CalculatorState).ToList());
        }

        public BindingList<Trade> GetTrades_Closed()
        {
            return new BindingList<Trade>(DbContext.Trade
                .Where(x => x.Status.Equals("closed"))
                .Include(x => x.CalculatorState).ToList());
        }

        public BindingList<Trade> GetTrades_Open()
        {
            return new BindingList<Trade>(DbContext.Trade
                .Where(x => x.Status.Equals("open"))
                .Include(x => x.CalculatorState).ToList());
        }


        internal bool Trade_Add(Trade t)
        {
            DbContext.Trade.Add(t);
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
            DbContext.Trade.Remove(t);
            DbContext.CalculatorState.Remove(t.CalculatorState);
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

        /// <summary>
        ///  Trade Challenge
        /// </summary>
        /// 
        public bool TradeChallenge_Create(TradeChallenge tc)
        {
            DbContext.TradeChallenge.Add(tc);
            DbContext.SaveChanges();

            return true;
        }

        public bool TradeChallenge_Update(TradeChallenge tc)
        {
            DbContext.TradeChallenge.Update(tc);
            DbContext.SaveChanges();

            return true;
        }

        public bool TradeChallenge_Delete(TradeChallenge tc)
        {
            DbContext.TradeChallenge.Remove(tc);
            DbContext.SaveChanges();
            tradeChallengeDeleted?.Invoke(tc);
            return true;
        }

        public List<TradeChallenge> GetTradeChallenges_Open()
        {
            return DbContext.TradeChallenge.Where(x => x.IsOpen).ToList();
                
        }
        public List<TradeChallenge> GetTradeChallenges_Closed()
        {
            return DbContext.TradeChallenge.Where(x => !x.IsOpen).ToList();

        }

        /// <summary>
        ///  Trade Thread
        /// </summary>
        /// 
        public bool TradeThread_Create(TradeThread tr)
        {
            DbContext.TradeThread.Add(tr);
            DbContext.SaveChanges();

            return true;
        }

        public List<Trade> TradeThread_GetActiveTrade(int tradeChallengeId)
        {
            //var x = DbContext.Trade
            //    .Where(x => x.Status.Equals("open"))
            //    .Include(x => x.TradeThreadTail).ThenInclude(tr => tr.TradeChallenge).Where(tc => tc.Id == tradeChallengeId)
            //    .Include(x => x.TradeThreadHead).ThenInclude(tr => tr.TradeChallenge).Where(tc => tc.Id == tradeChallengeId)
            //    .ToList();

            return DbContext.TradeThread
                .Include(tr => tr.Trade_head).ThenInclude(t => t.CalculatorState).Where(tr => tr.Trade_head.Status.Equals("open"))
                .Where(tr => tr.TradeChallengeId == tradeChallengeId)
                .Select(tr => tr.Trade_head)
                .ToList();
        }

        public List<Trade> TradeThread_GetTradeHistory(int tradeChallengeId)
        {
            //var a = DbContext.Trade
            //    .Include(x => x.TradeThreadHead).Where(t => t.TradeThreadHead.TradeChallengeId == tradeChallengeId)
            //    .ThenInclude(x => x.TradeChallenge).Where(tc => tc.Id == tradeChallengeId)
            //    .Where(x => x.Status.Equals("closed"))
            //    .ToList();

            return DbContext.TradeThread
                .Include(tr => tr.Trade_head).ThenInclude(t => t.CalculatorState).Where(tr => tr.Trade_head.Status.Equals("closed"))
                .Where(tr => tr.TradeChallengeId == tradeChallengeId)
                .Select(tr => tr.Trade_head)
                .OrderBy(t => t.DateExit)
                .ToList();
        }

        public List<Trade> TradeThread_GetAllTrades(int tradeChallengeId)
        {

            return DbContext.TradeThread
                .Include(tr => tr.Trade_head).ThenInclude(t => t.CalculatorState)
                .Where(tr => tr.TradeChallengeId == tradeChallengeId)
                .Select(tr => tr.Trade_head)
                .ToList();
        }

        public Trade TradeThread_GetNextTail(int tradeChallengeId)
        {
            return DbContext.TradeThread
                .Include(tr => tr.Trade_head).ThenInclude(t => t.CalculatorState).Where(tr => tr.Trade_head.Status.Equals("closed"))
                .Where(tr => tr.TradeChallengeId == tradeChallengeId)
                .Select(tr => tr.Trade_head)
                .OrderBy(t => t.DateExit)
                .LastOrDefault();
        }



        /// <summary>
        ///  Trade Challenge Prospect
        /// </summary>
        /// 
        public bool TradeChallengeProspect_Create(TradeChallengeProspect tcp)
        {
            DbContext.TradeChallengeProspect.Add(tcp);
            DbContext.SaveChanges();
            return true;
        }

        public bool TradeChallengeProspect_Delete(TradeChallengeProspect[] tcp)
        {
            DbContext.TradeChallengeProspect.RemoveRange(tcp);
            DbContext.SaveChanges();
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
            return true;
        }

        public List<CalculatorState> TradeChallengeProspect_GetAll(int tradeChallengeId)
        {
            return DbContext.CalculatorState
                .Include(c => c.TradeChallengeProspect)
                .Where(c => c.TradeChallengeProspect.TradeChallengeId == tradeChallengeId
                    && c.TradeId == default || c.TradeId < 1)
                .ToList();
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
    }
}