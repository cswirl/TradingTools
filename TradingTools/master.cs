using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Repository;
using TradingTools.Dialogs;
using TradingTools.Helpers;
using TradingTools.Services;
using TradingTools.Services.Interface;
using TradingTools.Trunk;
using TradingTools.Trunk.Contracts;
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
        public Timer RefreshTimer;
        public DelegateHandlers DelegateHandlers { get; set; }

        public TradingToolsDbContext DbContext { get; set; }
        public ILoggerManager LoggerManager { get; set; }

        private List<frmRiskRewardCalc> _listOf_frmRiskRewardCalc;
        private List<frmTradeChallenge> _listOf_frmTradeChallenge;

        public frmDashboard _frmDashboard { get; set; }
        public frmTradeMasterFile _frmTradeMasterFile { get; set; }
        public frmTradeChallengeMasterFile _frmTradeChallengeMasterFile { get; set; }

        // Service
        public IServiceManager ServiceManager { get; private set; }

        public master()
        {
            InitializeComponent();

            this.Visible = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Load += new EventHandler(master_Load);
            ///
            // Logger
            LogManager.LoadConfiguration(Path.Combine(Directory.GetCurrentDirectory(), "nlog.config"));
            this.LoggerManager = new LoggerManager();

            InitializeServiceManager();
            _listOf_frmRiskRewardCalc = new();
            _listOf_frmTradeChallenge = new();
            this.DelegateHandlers = new(this);
            //
            RefreshTimer = new();
            RefreshTimer.Interval = Presentation.INTERNAL_TIMER_REFRESH_VALUE;
            RefreshTimer.Start();

            // Dashboard (gateway form)
            _frmDashboard = new(this);
            _frmDashboard.StartPosition = FormStartPosition.CenterScreen;
            _frmDashboard.Show();
        }

        private void master_Load(object sender, EventArgs e)
        {
            /// make master invisible to the user
            this.Size = new System.Drawing.Size(0, 0);
            LoggerManager.LogInfo("master form loaded");
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

        
        #region Trade Challenge
        public void FormTradeChallengeMasterFile()
        {
            if (_frmTradeChallengeMasterFile == default || _frmTradeChallengeMasterFile.IsDisposed)
            {
                _frmTradeChallengeMasterFile = new(this);
                _frmTradeChallengeMasterFile.Show();
            }
            else
            {
                if (_frmTradeChallengeMasterFile.WindowState == FormWindowState.Minimized) _frmTradeChallengeMasterFile.WindowState = FormWindowState.Normal;
                _frmTradeChallengeMasterFile.Focus();
            }
        }

        public frmTradeChallenge TradeChallenge_Spawn(TradeChallenge tradeChallenge)
        {
            var tc = _listOf_frmTradeChallenge.Find(x => x.TradeChallenge.Id == tradeChallenge.Id);
            if (tc != null)
            {
                activateForm(tc);
                return tc;
            }
            else
            {
                var form = new frmTradeChallenge(this, tradeChallenge);

                // register
                form.FormClosed += FormTradeChallenge_FormClosed;
                _listOf_frmTradeChallenge.Add(form);
                //

                form.Show();

                return form;
            }
        }

        private void FormTradeChallenge_FormClosed(object sender, FormClosedEventArgs e)
        {
            _listOf_frmTradeChallenge.Remove((frmTradeChallenge)sender);
            // another contingency exit - to avoid master hidden in the background
            if (_frmDashboard == default && _listOf_frmRiskRewardCalc.Count < 1 && _listOf_frmTradeChallenge.Count < 1) Application.Exit();
        }
        #endregion

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

        private void activateForm(Form form)
        {
            form.WindowState = FormWindowState.Normal;
            form.Focus();
        }

        private void FormRRC_FormClosed(object sender, FormClosedEventArgs e)
        {
            _listOf_frmRiskRewardCalc.Remove((frmRiskRewardCalc)sender);
            // another contingency exit - to avoid master hidden in the background
            if (_frmDashboard == default && _listOf_frmRiskRewardCalc.Count < 1 && _listOf_frmTradeChallenge.Count < 1) Application.Exit();
        }

        public frmRiskRewardCalc FormRRC_Trade_Spawn(Trade t)
        {
            var rrc = _listOf_frmRiskRewardCalc.Find(x => x.Trade?.Id == t.Id);
            if (rrc != null)
            {
                activateForm(rrc);
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
            var rrc = _listOf_frmRiskRewardCalc.Find(x => x.CalculatorState.Id == c.Id);
            if (rrc != null)
            {
                activateForm (rrc);
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

        public frmRiskRewardCalc FormRRC_Long_Empty_Spawn(decimal capital = 0)
        {
            var rrc = TradeService.RiskRewardCalcGetInstance("long");
            return FormRRC_Empty_Spawn(rrc, capital);
        }

        public frmRiskRewardCalc FormRRC_Short_Empty_Spawn(decimal capital = 0)
        {
            var rrc = TradeService.RiskRewardCalcGetInstance("short");
            return FormRRC_Empty_Spawn(rrc, capital); ;
        }

        private frmRiskRewardCalc FormRRC_Empty_Spawn(IRiskRewardCalc riskRewardCalc, decimal capital)
        {
            var form = new frmRiskRewardCalc(riskRewardCalc, this);
            if (capital > 0) form.SetCapital(capital);
            registerNewRiskRewardCalcForm(form);
            form.Show();

            return form;
        }
        #endregion

        // Use two sources: Database records and External text file
        public string[] TickerAutoCompleteSource()
        {
            // database source
            var tradeHist = ServiceManager.TradeService.GetTickers();
            var sources = tradeHist.ToList();

            // file source
            string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string fileName = "Assets\\ticker-source.txt";
            var path = Path.Combine(exePath, fileName);
            if (!File.Exists(path)) return sources.ToArray();
            IEnumerable<string> lines = File.ReadLines(path);
            var textFile = lines.Select(x => x.Trim().Replace('-', '/').ToUpper());

            // combine sources
            sources.AddRange(textFile);

            return sources.ToArray();
        }

        private void InitializeServiceManager()
        {
            DbContext = new();
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ServiceManager = new ServiceManager(new RepositoryManager(DbContext), LoggerManager);

            try
            {
                // This will run the ef core database update command - if it detects pending migration
                DbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                // Log
                LoggerManager.LogError($"Something went wrong in the {nameof(InitializeServiceManager)} service method {ex}");
                AppMessageBox.Error("An error occurred during migration.", "Critical Error");
                Environment.Exit(-1);
            }
            
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // master
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "master";
            this.ResumeLayout(false);

        }     
    }
}