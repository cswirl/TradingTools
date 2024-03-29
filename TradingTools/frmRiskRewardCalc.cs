using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Dialogs;
using TradingTools.Model;
using TradingTools.Services;
using TradingTools.Services.Interface;
using TradingTools.Services.Models;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Extensions;
using TradingTools.Trunk.Validation;

namespace TradingTools
{
    public partial class frmRiskRewardCalc : Form
    {
        //delegates
        public CalculatorStateCreated CalculatorState_Added;
        public CalculatorStateUpdating CalculatorState_Officializing_IsCancelled;
        //
        public TradeCreated Trade_Officialized;
        public TradeOfficializing Trade_Officializing;
        public TradeUpdating Trade_Closing_IsCancelled;
        //
        public EventHandler<RiskRewardCalcState> OnStateChanged;

        private IRiskRewardCalc _rrc;
        private TradingStyle _tradingStyle;
        private string _headerMetadata;
        private string _lastSavedStateHash;
        private dialogCapitalCalc _dialogCapital;
        private bool _flagCalculate = false;

        private master _master;
        private IServiceManager _service;


        // Properties
        public RiskRewardCalcState State { get; set; } = RiskRewardCalcState.Empty;

        public CalculatorState? CalculatorState { get; set; }
        public Trade? Trade { get; set; }
        public string Side { get; set; }
        public Position Position { get; set; }
        public ISideTheme Theme { get; private set; }

        public frmRiskRewardCalc(IRiskRewardCalc riskRewardCalc, master master)
        {
            InitializeComponent();

            // Priority
            // Dependency Injection of RiskRewardCalc
            _rrc = riskRewardCalc;
            Side = riskRewardCalc.Side;
            _master = master;
            _service = master.ServiceManager;

            this.Theme = TradingTools.Theme.SideTheme_GetInstance(riskRewardCalc.Side);

            // delegates
            if (riskRewardCalc.Side == "short")
            {
                btnSetPEP.Click += new EventHandler(delegate (Object o, EventArgs a) { txtLEP_ExitPrice.Text = txtPriceIncrease_target.Text; });
                btnSetLEP.Click += new EventHandler(delegate (Object o, EventArgs a) { txtPEP_ExitPrice.Text = txtPriceDecrease_target.Text; });
            }
            else
            {
                btnSetPEP.Click += new EventHandler(delegate (Object o, EventArgs a) { txtPEP_ExitPrice.Text = txtPriceIncrease_target.Text; });
                btnSetLEP.Click += new EventHandler(delegate (Object o, EventArgs a) { txtLEP_ExitPrice.Text = txtPriceDecrease_target.Text; });
            }
            this.OnStateChanged += this.OnStateChanged_Invoked;
            //

            myInitializeComponent();
        }

        public void SetCapital(decimal capital) => txtCapital.Text = capital.ToMoney();

        private void OnStateChanged_Invoked(object sender, RiskRewardCalcState e)
        {
            lblHeader.Text = $"{Theme.Title} - {_headerMetadata}";
        }

        private void myInitializeComponent()
        {
            splitContainer1.IsSplitterFixed = true;
            panelBandTop.Height = 3;
            panelBandBottom.Height = 3;
            cbxTradingStyle.DataSource = Enum.GetValues(typeof(TradingStyle));
            cbxTradingStyle.SelectedIndex = 1;  // swing

            Theme.Format_PriceIncreaseTable(dgvPriceIncreaseTable);
            Theme.Format_PriceDecreaseTable(dgvPriceDecreaseTable);

            // timer
            timer1.Interval = Presentation.INTERNAL_TIMER_REFRESH_VALUE;

            // Ticker auto complete
            var autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(_master.TickerAutoCompleteSource());
            txtTicker.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTicker.AutoCompleteCustomSource = autoComplete;
            txtTicker.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void btnReCalculate_Click(object sender, EventArgs e)
        {

            if (State == RiskRewardCalcState.Deleted) return;

            string msg;

            // Position
            Position = GetPositionData(out msg);
            if (Position  == default)
            {
                statusMessage.Text = msg;
                return;
            }

            
            // step 4: Represent data back to UI
            // the function captureCalculationDetails is able to handle the appropriate value for Lot Size from the textbox - given it is initialized properly
            txtLeverage.Text = Position.Leverage.ToString_UptoTwoDecimal();
            txtLotSize.Text = Position.LotSize.ToString_UptoMaxDecimal();
            txtLeveragedCapital.Text = Position.LeveragedCapital.ToMoney();

            txtBorrowAmount.Text = Formula.BorrowedAmount(Position.Leverage, Position.Capital).ToMoney();
            var fee = Formula.TradingFee(Position.LeveragedCapital, Constant.TRADING_FEE);
            txtOpeningTradingFee_dollar.Text = fee.ToMoney();
            txtOpeningTradingCost.Text = fee.ToMoney();

            // PnL Tables
            var pit = _rrc.GeneratePriceIncreaseTable(Position);
            if (pit != null) dgvPriceIncreaseTable.DataSource = pit;

            var pdt = _rrc.GeneratePriceDecreaseTable(Position);
            if (pdt != null) dgvPriceDecreaseTable.DataSource = pdt;

            // compute buttons
            CustomPriceIncrease_Compute(null, null);
            CustomPriceDecrease_Compute(null, null);
            PEP_Compute(null, null);
            LEP_Compute(null, null);
            TradeExit_Compute(null, null);
            PerfectEntry_Compute(null, null);

            // enable controls
            if (!_flagCalculate)
            {
                gbTradeExit.Enabled = true;
                gbPEP.Enabled = true;
                gbLEP.Enabled = true;
                txtPriceIncrease_target.Enabled = true;
                txtPriceDecrease_target.Enabled=true;
                _flagCalculate = true;
            }

            this.ValidateChildren();
        }

        private Position GetPositionData(out string msg)
        {
            var p = new Position();
            msg = string.Empty;

            // Collect Data - The collection process will assign default values - no exception is expected
            p.Capital = txtCapital.Text.ToDecimal();
            p.EntryPriceAvg = txtEntryPrice.Text.ToDecimal();
            p.Leverage = txtLeverage.Text.ToDecimal();
            p.LotSize = txtLotSize.Text.ToDecimal();

            // Validation 
            // divide by zero screener
            if (p.Capital == 0 || p.EntryPriceAvg == 0)
            {
                msg = "Capital and Entry Price needed.";
                return default;
            }

            if (radioLeverage.Checked && p.LotSize == 0) {
                msg = "Lot Size value is needed.";
                return default;
            }

            if (radioLotSize.Checked && p.Leverage <= 0) {
                msg = "Leverage value is needed.";
                return default;
            }

            // Process - Do not process Official Trades
            if (State == RiskRewardCalcState.Empty || State == RiskRewardCalcState.Loaded)
            {
                p.Leverage = radioLeverage.Checked
                ? Formula.Leverage(p.EntryPriceAvg, p.LotSize, p.Capital)
                : p.Leverage;
                p.LotSize = radioLotSize.Checked
                    ? Formula.LotSize(p.Capital, p.Leverage, p.EntryPriceAvg)
                    : p.LotSize;
            }
            
            // Final Validation - Strict
            // Hence, Minimum value 
            if (p.Capital <= 0 | p.LotSize <= 0 | p.EntryPriceAvg <= 0)
            {
                msg = "Invalid input data";
                return default;
            }

            return p;
        }

        private void frmRiskRewardCalc_Load(object sender, EventArgs e)
        {
            // Proposal: rename to InitStateDependentComponents then change to public. Place in the constructor.
            callOnLoad();

            SetLastSavedCalculatorHash();    // this must be called last on form load
        }


        private void CustomPriceIncrease_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = txtPriceIncrease_target.Text.ToDecimal();
            if (priceTarget <= 0) return;

            // 3
            PnLRecord pnl;
            try { pnl = _rrc.ComputePnL(priceTarget, Position); }
            catch (NullReferenceException) { return; }

            // 4
            if (pnl == null) return;
            txtPriceIncreasePercentage.Text = pnl.PCP.ToPercentageSingle();
            txtPriceIncrease_profit.Text = pnl.PnL.ToMoney();
            txtProfitPercentage.Text = pnl.PnL_Percentage.ToPercentageSingle();
            }

        private void CustomPriceDecrease_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = txtPriceDecrease_target.Text.ToDecimal();
            if (priceTarget <= 0) return;

            // 3
            PnLRecord pnl;
            try { pnl = _rrc.ComputePnL(priceTarget, Position); }
            catch (NullReferenceException) { return; }

            // 4
            if (pnl == null) return;
            txtPriceDecreasePercentage.Text = pnl.PCP.ToPercentageSingle();
            txtPriceDecrease_loss.Text = pnl.PnL.ToMoney();
            txtLossPercentage.Text = pnl.PnL_Percentage.ToPercentageSingle();
        }

        private void updateRRR()
        {
            decimal profit = txtPEP_Profit.Text.ToDecimal();
            decimal loss = txtLEP_Loss.Text.ToDecimal();
            decimal? rrr = null;

            if (loss != 0) rrr = profit / Math.Abs(loss);


            // Display RRR
            txtRRR.Text = (rrr == null ? "NA" : rrr?.ToString("0.0"));
        }

        private void PEP_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = txtPEP_ExitPrice.Text.ToDecimal();
            if (priceTarget <= 0) return;

            // 3
            if (Position == default) return;
            var exitPnl = _rrc.PnlExitPlan(priceTarget, Position);

            var rec = exitPnl.Item1;
            var pv = exitPnl.Item2;
            var equity = exitPnl.Item3;

            // 4
            if (rec == null) return;
            txtPEP_sPV.Text = pv.ToMoney();
            txtPEP_AccountEquity.Text = equity.ToMoney();

            txtPEP_PCP.Text = rec.PCP.ToPercentageSingle();
            txtPEP_RealProfit_percent.Text = rec.PnL_Percentage.ToPercentageSingle();
            txtPEP_Profit.Text = rec.PnL.ToMoney();
            txtPEP_TradingCost.Text = "--";

            updateRRR();
            }


        private void LEP_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = txtLEP_ExitPrice.Text.ToDecimal();
            if (priceTarget <= 0) return;

            // 3
            if (Position == default) return;
            var exitPnl = _rrc.PnlExitPlan(priceTarget, Position);

            var rec = exitPnl.Item1;
            var sPV = exitPnl.Item2;
            var equity = exitPnl.Item3;

            // 4
            if (exitPnl == null) return;
            txtLEP_sPV.Text = sPV.ToMoney();
            txtLEP_AccountEquity.Text = equity.ToMoney();

            txtLEP_PCP.Text = rec.PCP.ToPercentageSingle();
            txtLEP_RealLoss_percent.Text = rec.PnL_Percentage.ToPercentageSingle();
            txtLEP_Loss.Text = rec.PnL.ToMoney();
            txtLEP_TradingCost.Text = "--";

            updateRRR();
        }

        private void TradeExit_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = txtTradeExit_ExitPrice.Text.ToDecimal();
            if (priceTarget <= 0) return;

            // 3
            if (Position == default) return;
            var exitPnl = _rrc.PnlExitPlan(priceTarget, Position);

            var rec = exitPnl.Item1;
            var pv = exitPnl.Item2;
            var equity = exitPnl.Item3;

            // 4
            if (rec == null) return;
            txtTradeExit_PV.Text = pv.ToMoney();
            txtFinalCapital.Text = equity.ToMoney();
            txtTradeExit_PCP.Text = rec.PCP.ToPercentageSingle();
            txtTradeExit_PnL_percentage.Text = rec.PnL_Percentage.ToPercentageSingle();
            txtTradeExit_PnL.Text = rec.PnL.ToMoney();
            txtTradeExit_TC.Text = "--";
        }

        private void PerfectEntry_Compute(object sender, EventArgs e)
        {
            // 2
            decimal entryPrice = txtPerfectEntry_EntryPrice.Text.ToDecimal();
            decimal exitPrice = txtPerfectEntry_ExitPrice.Text.ToDecimal();
            if (entryPrice <= 0 || exitPrice <= 0) return;

            //
            decimal pcp = Formula.PCP(entryPrice, exitPrice);
            txtPerfectEntry_PCP.Text = pcp.ToPercentageSingle();
            txtMultiple.Text = (1 + (pcp / 100)).ToString_UptoTwoDecimal() + "x";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (State == RiskRewardCalcState.Deleted) return;
            Save();
        }

        private CalculatorState captureCalculatorState()
        {
            var c = new CalculatorState();
            // Clone First - to copy the keys such as TradeId foreign key
            this.CalculatorState.CopyProperties(c);
            // Collect Receptors
            c.Capital = txtCapital.Text.ToDecimal();
            c.Leverage = txtLeverage.Text.ToDecimal();
            c.EntryPriceAvg = txtEntryPrice.Text.ToDecimal();
            c.LotSize = txtLotSize.Text.ToDecimal();
            //c.DayCount = txtDayCount.Text.ToDecimal();
            c.DailyInterestRate = nudDailyInterestRate.Value;
            c.PriceIncreaseTarget = txtPriceIncrease_target.Text.ToDecimal();
            c.PriceDecreaseTarget = txtPriceDecrease_target.Text.ToDecimal();
            c.PEP_ExitPrice = txtPEP_ExitPrice.Text.ToDecimal();
            c.PEP_Note = txtPEP_Note.Text;
            c.LEP_ExitPrice = txtLEP_ExitPrice.Text.ToDecimal();
            c.LEP_Note = txtLEP_Note.Text;
            c.Ticker = txtTicker.Text;
            c.ReasonForEntry = txtReasonForEntry.Text;
            c.Note = txtNote.Text;
            // Processed
            c.CounterBias = txtCounterBias.Text;
            c.ExchangeFee = txtExchangeFee.Text.ToDecimal();

            c.TradingStyle = cbxTradingStyle.SelectedValue.ToString();
            //
            c.TradeExit_ExitPrice = txtTradeExit_ExitPrice.Text.ToDecimal();
            c.ReasonForExit = txtReasonForExit.Text;
            c.PerfectEntry_EntryPrice = txtPerfectEntry_EntryPrice.Text.ToDecimal();
            c.PerfectEntry_ExitPrice = txtPerfectEntry_ExitPrice.Text.ToDecimal();
            c.PerfectEntry_Note = txtPerfectEntry_Note.Text;
            //
            c.IsLotSizeChecked = radioLotSize.Checked;
            c.Side = _rrc.Side;

            return c;
        }

        private bool Save()
        {
            // Capture state
            captureCalculatorState().CopyProperties(this.CalculatorState);

            // Process data collected - this will save data into a data store - no need for step 4 which is to Display Data 
            if (_master == default) return false;
            if (State == RiskRewardCalcState.Empty)
            {
                if (_service.CalculatorStateService.Add(CalculatorState))
                {
                    statusMessage.Text = "State save successfully.";
                    ChangeState(RiskRewardCalcState.Loaded);
                    CalculatorState_Added?.Invoke(CalculatorState);
                    SetLastSavedCalculatorHash();
                    _master.CalculatorState_Added?.Invoke(CalculatorState);
                }
                else statusMessage.Text = "Saving state failed.";
            }
            else if (State == RiskRewardCalcState.Loaded | State == RiskRewardCalcState.TradeOpen | State == RiskRewardCalcState.TradeClosed)
            {
                if (_service.CalculatorStateService.Update(CalculatorState))
                {
                    statusMessage.Text = "State updated successfully.";
                    SetLastSavedCalculatorHash();
                    _master.CalculatorState_Updated?.Invoke(CalculatorState);
                }
                else statusMessage.Text = "Updating state failed.";
            }
            
            return true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (State == RiskRewardCalcState.Deleted) return;
            if (State == RiskRewardCalcState.Empty) return;
            // Deletion not allowed when Trade was Officialized
            if (State == RiskRewardCalcState.TradeOpen) return;
            if (State == RiskRewardCalcState.TradeClosed) return;

            DialogResult objDialog = AppMessageBox.Question_YesNo("This action is not reversible\n\n Confirm DELETE?", "Delete", this);
            if (objDialog == DialogResult.Yes)
            {
                if (_service.CalculatorStateService.Delete(CalculatorState))
                {
                    ChangeState(RiskRewardCalcState.Deleted);
                    statusMessage.Text = "State was deleted successfully. \n\nThis form will now close.";
                    MessageBox.Show(statusMessage.Text, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _master.CalculatorState_Deleted?.Invoke(CalculatorState);
                    // just close the form
                    this.Close();
                }
                else
                {
                    statusMessage.Text = "Deleting state failed.";
                    AppMessageBox.Error(statusMessage.Text, "Delete", this);
                }
            }


        }

        private void frmRiskRewardCalc_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Just ignore and close the form
            if (State == RiskRewardCalcState.Deleted) return;
            if (DoNotAskSave()) return;

            // show the form in case was minimized and closing was came from external such as from a parent form
            this.WindowState = FormWindowState.Normal;
            this.Focus();

            var msgBoxButtons = (e.CloseReason == CloseReason.ApplicationExitCall) ? MessageBoxButtons.YesNo : MessageBoxButtons.YesNoCancel;
            DialogResult objDialog = MessageBox.Show("Do you want to save before closing ?", this.Text, msgBoxButtons, MessageBoxIcon.Question);

            if (objDialog == DialogResult.Cancel)
            {
                // keep the form open
                e.Cancel = true;
            }
            else if (objDialog == DialogResult.No)
            {
                // let the form close gracefully
            }
            else if (objDialog == DialogResult.Yes)
            {
                // try saving state then let it close gracefully
                // BUT keep the form open if is any are complication while saving
                e.Cancel = !Save();
            }
        }

        private bool DoNotAskSave() => _lastSavedStateHash == captureCalculatorState().CreateHash();

        private void SetLastSavedCalculatorHash() => _lastSavedStateHash = captureCalculatorState().CreateHash();

        private void timer1_Tick(object sender, EventArgs e) => statusMessage.Text = "Status message . . .";

        private void btnOfficializedTrade_Click(object sender, EventArgs e)
        {
            if (State == RiskRewardCalcState.Deleted) return;

            // delegate cancel
            string msg;
            if (CalculatorState_Officializing_IsCancelled?.Invoke(this.CalculatorState, out msg) ?? false)
            {
                AppMessageBox.Error(msg, "Officializing a Trade Denied", this);
                return;
            }

            if (State == RiskRewardCalcState.Empty | State == RiskRewardCalcState.Loaded)
            {
                dynamic d = new ExpandoObject();
                d.Ticker = txtTicker.Text;
                d.Capital = txtCapital.Text;
                d.Leverage = txtLeverage.Text;
                d.LotSize = txtLotSize.Text;
                d.EntryPrice = txtEntryPrice.Text;

                var officializeDialog = new dialogTradeOfficialize(_master);
                officializeDialog.MyProperty = d;
                officializeDialog.Trade_Officialize += this.officializedTrade;
                officializeDialog.ShowDialog();
            }
        }

        private bool officializedTrade(dynamic obj)
        {
            // 1 - Input
            txtTicker.Text = obj.Ticker;
            txtCapital.Text = obj.Capital.ToString();
            txtLeverage.Text = obj.Leverage.ToString();
            txtEntryPrice.Text = obj.EntryPrice.ToString();
            txtLotSize.Text = Validation.HasProperty(obj, "LotSize") ? obj.LotSize.ToString() : string.Empty;

            // 2 - Collect Data
            var dateEnter = obj.DateEnter;
            // Need to capture calculatorstate to be updated automatically by dbcontext
            captureCalculatorState().CopyProperties(this.CalculatorState);
            string msg;
            // 2 - Process
            var c = CalculatorState;

            this.Trade = new Trade
            {
                Side = c.Side,
                Ticker = c.Ticker,
                Capital = c.Capital,
                Leverage = c.Leverage,
                EntryPriceAvg = c.EntryPriceAvg,
                LotSize = c.LotSize,
                TradingStyle = cbxTradingStyle.SelectedValue.ToString(),
                //
                Status = "open",
                DateEnter = dateEnter,
                //
                CalculatorState = this.CalculatorState
            };

            // 3 - Validation
            if (!TradeService.TradeOpening_Validate(this.Trade, out msg))
            {
                statusMessage.Text = msg;
                AppMessageBox.Error(statusMessage.Text, "", this);
                return false;
            }

            // This is a hook to add additioanl properties to Trade instance prior to ef core update
            // ex. attaching TradeThread instance to Trade property for ef core update and auto INSERT thru relationship
            Trade_Officializing?.Invoke(Trade);

            // 4 - Save data into a data store - ChangeState will Display Data 
            if (_service.TradeService.Create(Trade))
            {
                statusMessage.Text = $"Ticker: {Trade.Ticker} has been officialized successfully.";
                AppMessageBox.Inform(statusMessage.Text, $"Trade No. {Trade.Id} is Official", this);
                ChangeState(RiskRewardCalcState.TradeOpen);
                Trade_Officialized?.Invoke(Trade);
                _master.Trade_Officialized?.Invoke(Trade);
                SetLastSavedCalculatorHash();
            }
            else
            {
                statusMessage.Text = "Officializing a Trade failure";
                AppMessageBox.Error(statusMessage.Text, "", this);
                return false;
            }

            return true;
        }


        public void MarkAsDeleted(Trade t)
        {
            if (Trade != default && Trade.Id == t.Id) ChangeState(RiskRewardCalcState.Deleted);
        }

        private void callOnLoad()
        {
            // State independent controls
            txtExchangeFee.Text = Constant.TRADING_FEE.ToString();

            panelTitleBand.BackColor = Theme.Default;
            panelFooterBand.BackColor = Theme.Default;
            lblHeader.Text = Theme.Title;

            ChangeState(this.State);
        }
        private void ChangeState(RiskRewardCalcState s)
        {
            State = s;

            TradingStyle tradeStyle;

            switch (State)
            {
                case RiskRewardCalcState.Empty:
                    _headerMetadata = "unsaved";
                    panelBandTop.BackColor = Theme.Empty;
                    panelBandBottom.BackColor = Theme.Empty;

                    CalculatorState = new();
                    // Initalize UI controls
                    txtLeverage.Text = "1";
                    txtBorrowAmount.Text = "0";
                    txtDayCount.Text = "0";

                    nudDailyInterestRate.Value = Constant.DAILY_INTEREST_RATE;

                    txtPEP_Note.Text = "take-profit: inactive";
                    txtLEP_Note.Text = "stop-loss: inactive";

                    btnCloseTheTrade.Visible = false;

                    // disable controls
                    gbTradeExit.Enabled = false;
                    gbPEP.Enabled = false;
                    gbLEP.Enabled = false;
                    txtPriceIncrease_target.Enabled = false;
                    txtPriceDecrease_target.Enabled = false;

                    break;

                case RiskRewardCalcState.Loaded:
                    var c = CalculatorState;
                    if (CalculatorState == null)
                    {
                        statusMessage.Text = "Internal error: CalculatorState instance was not forwarded.";
                        _master.LoggerManager.LogError(statusMessage.Text);
                        MessageBox.Show(statusMessage.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        this.Close();
                        return;
                    }
                    else
                    {
                        _headerMetadata = "saved - unofficial";
                        panelBandTop.BackColor = Theme.Loaded;
                        panelBandBottom.BackColor = Theme.Loaded;

                        
                        this.Text = c.Ticker;
                        txtTicker.Text = c.Ticker;
                        // Common
                        // Independent data
                        txtReasonForEntry.Text = c.ReasonForEntry;
                        txtCounterBias.Text = c.CounterBias;
                        txtNote.Text = c.Note;
                        Enum.TryParse<TradingStyle>(c.TradingStyle, out tradeStyle);
                        cbxTradingStyle.SelectedItem = tradeStyle;
                        radioLotSize.Checked = c.IsLotSizeChecked;

                        // sys flow 1
                        txtCapital.Text = c.Capital.ToString_TwoDecimal();     // dont change format
                        txtLeverage.Text = c.Leverage.ToString_UptoOneDecimal();      // dont change format
                        txtEntryPrice.Text = c.EntryPriceAvg.ToString_UptoMaxDecimal();
                        txtLotSize.Text = c.LotSize.ToString_UptoMaxDecimal();
                        
                        nudDailyInterestRate.Value = Math.Max(c.DailyInterestRate, nudDailyInterestRate.Minimum);
                        txtExchangeFee.Text = c.ExchangeFee.ToString_UptoMaxDecimal();
                        btnReCalculate_Click(null, null);

                        // sys flow 2
                        txtPriceIncrease_target.Text = c.PriceIncreaseTarget?.ToString_UptoMaxDecimal();
                        txtPriceDecrease_target.Text = c.PriceDecreaseTarget?.ToString_UptoMaxDecimal();

                        // sys flow 3
                        txtPEP_ExitPrice.Text = c.PEP_ExitPrice?.ToString_UptoMaxDecimal();
                        txtPEP_Note.Text = c.PEP_Note;

                        // sys flow
                        txtLEP_ExitPrice.Text = c.LEP_ExitPrice?.ToString_UptoMaxDecimal();
                        txtLEP_Note.Text = c.LEP_Note;
                        
                        //
                        txtTradeExit_ExitPrice.Text = c.TradeExit_ExitPrice?.ToString_UptoMaxDecimal();
                        txtReasonForExit.Text = c.ReasonForExit;
                        txtPerfectEntry_EntryPrice.Text = c.PerfectEntry_EntryPrice?.ToString_UptoMaxDecimal();
                        txtPerfectEntry_ExitPrice.Text = c.PerfectEntry_ExitPrice?.ToString_UptoMaxDecimal();
                        txtPerfectEntry_Note.Text = c.PerfectEntry_Note;

                        btnCloseTheTrade.Visible = false;
                    }
                    break;

                case RiskRewardCalcState.TradeOpen:
                    if (CalculatorState == null)
                    {
                        statusMessage.Text = "Internal error: CalculatorState instance was not forwarded.";
                        _master.LoggerManager.LogError(statusMessage.Text);
                        AppMessageBox.Error(statusMessage.Text, "", this);
                        this.Close();
                        return;
                    }
                    else if (Trade == null)
                    {
                        statusMessage.Text = "Internal error: Trade instance was not forwarded.";
                        _master.LoggerManager.LogError(statusMessage.Text);
                        AppMessageBox.Error(statusMessage.Text, "", this);
                        this.Close();
                        return;
                    }
                    else
                    {
                        panelBandTop.BackColor = Theme.TradeOpen;
                        panelBandBottom.BackColor = Theme.TradeOpen;
                        lblHeader.Text = Trade.Ticker;
                        _headerMetadata = "Trade OPEN";
                        btnCloseTheTrade.Visible = true;

                        TradeCommon_Load(Trade);
                    }
                    break;

                case RiskRewardCalcState.TradeClosed:
                    panelBandTop.BackColor = Theme.TradeClosed;
                    panelBandBottom.BackColor = Theme.TradeClosed;
                    lblHeader.Text = Trade.Ticker;
                    lblHeader.ForeColor = Color.LightSteelBlue;
                    _headerMetadata = $"Trade CLOSED - {this.Trade.DateExit?.ToString("F")}";
                    // Controls
                    txtPEP_ExitPrice.ReadOnly = true;
                    txtLEP_ExitPrice.ReadOnly = true;
                    txtPerfectEntry_EntryPrice.ReadOnly = true;
                    txtPerfectEntry_ExitPrice.ReadOnly = true;
                    txtTradeExit_ExitPrice.ReadOnly = true;
                    txtFinalCapital.ReadOnly = true;
                    //
                    btnSetPEP.Visible = false;
                    btnSetLEP.Visible = false;
                    btnCloseTheTrade.Visible = false;
                    gbxNotes.Height = 800;

                    TradeCommon_Load(Trade);

                    // Trade Exit - Override Values
                    //txtTradeExit_PV.Text = Formula.PositionValue(Trade.LotSize, Trade.ExitPriceAvg ?? 0).ToMoney();
                    //txtTradeExit_PCP.Text = Formula.PCP(Trade.EntryPriceAvg, Trade.ExitPriceAvg).ToPercentageSingle();
                    txtTradeExit_ExitPrice.Text = Trade.ExitPriceAvg?.ToString_UptoMaxDecimal();
                    txtFinalCapital.Text = Trade.FinalCapital?.ToMoney();

                    txtTradeExit_PnL_percentage.Text = Trade.PnL_percentage?.ToPercentageSingle();
                    txtTradeExit_PnL.Text = Trade.PnL?.ToMoney();
                    // Transaction Cost
                    txtTradeExit_TC.Text = "--"; // (pv - fc).ToString(Constant.MONEY_FORMAT);
                    break;

                case RiskRewardCalcState.Deleted:
                    _headerMetadata = " < < DELETED > >";
                    this.Text += _headerMetadata;
                    
                    // disable the form from interacting to database
                    btnSave.Visible = false;
                    btnDelete.Visible = false;
                    btnOfficializedTrade.Visible = false;
                    btnCloseTheTrade.Visible = false;
                    break;
            }

            OnStateChanged?.Invoke(this, s);
        }

        private void TradeCommon_Load(Trade t)
        {
            // trade
            this.Text = Trade.Ticker + $" - Trade No. {Trade?.Id}";
            txtTradeNum.Text = Trade.Id.ToString();
            txtTicker.Text = Trade.Ticker;
            txtTradeDates.Visible = true;
            txtTradeDates.Text = $"Enter: {this.Trade.DateEnter.ToFull()}{Environment.NewLine}" +
                $"Exit: {this.Trade.DateExit?.ToFull()}";

            // Independent data
            txtReasonForEntry.Text = CalculatorState.ReasonForEntry;
            txtCounterBias.Text = CalculatorState.CounterBias;
            txtNote.Text = CalculatorState.Note;
            TradingStyle tradeStyle;
            Enum.TryParse<TradingStyle>(Trade.TradingStyle, out tradeStyle);    //
            cbxTradingStyle.SelectedItem = tradeStyle;
            radioLotSize.Checked = CalculatorState.IsLotSizeChecked;

            // sys flow 1
            txtCapital.Text = Trade.Capital.ToMoney();     // Money Format Regex implemented
            txtLeverage.Text = Trade.Leverage.ToString_UptoTwoDecimal();
            txtEntryPrice.Text = Trade.EntryPriceAvg.ToString_UptoMaxDecimal();
            txtLotSize.Text = Trade.LotSize.ToString_UptoMaxDecimal();
            txtDayCount.Text = Trade.DayCount?.ToString_UptoTwoDecimal() ?? "0";

            //Numeric Up Down control throws exception when assigned value less then their Minimum value
            nudDailyInterestRate.Value = Math.Max(CalculatorState.DailyInterestRate, nudDailyInterestRate.Minimum);

            txtExchangeFee.Text = CalculatorState.ExchangeFee.ToString_UptoMaxDecimal();

            btnReCalculate_Click(null, null);

            #region Purely CalculatorState
            // sys flow
            txtPriceIncrease_target.Text = CalculatorState.PriceIncreaseTarget?.ToString_UptoMaxDecimal();
            txtPriceDecrease_target.Text = CalculatorState.PriceDecreaseTarget?.ToString_UptoMaxDecimal();

            // sys flow
            txtPEP_ExitPrice.Text = CalculatorState.PEP_ExitPrice?.ToString_UptoMaxDecimal();
            txtPEP_Note.Text = CalculatorState.PEP_Note;

            // sys flow
            txtLEP_ExitPrice.Text = CalculatorState.LEP_ExitPrice?.ToString_UptoMaxDecimal();
            txtLEP_Note.Text = CalculatorState.LEP_Note;

            // sys flow
            txtTradeExit_ExitPrice.Text = CalculatorState.TradeExit_ExitPrice?.ToString_UptoMaxDecimal();
            txtReasonForExit.Text = CalculatorState.ReasonForExit;

            txtPerfectEntry_EntryPrice.Text = CalculatorState.PerfectEntry_EntryPrice?.ToString_UptoMaxDecimal();
            txtPerfectEntry_ExitPrice.Text = CalculatorState.PerfectEntry_ExitPrice?.ToString_UptoMaxDecimal();
            txtPerfectEntry_Note.Text = CalculatorState.PerfectEntry_Note;
            //
            #endregion

            // Controls
            txtTicker.ReadOnly = true;
            txtCapital.ReadOnly = true;
            txtLeverage.ReadOnly = true;
            txtEntryPrice.ReadOnly = true;
            txtLotSize.ReadOnly = true;
            cbxTradingStyle.Enabled = false;
            nudDailyInterestRate.Enabled = false;
            radioLeverage.Visible = false;
            radioLotSize.Visible = false;
            btnDelete.Visible = false;
            btnOfficializedTrade.Enabled = false;
            btnOfficializedTrade.Visible = false;
            btnReCalculate.Visible = false;
        }

        private void btnCloseTheTrade_Click(object sender, EventArgs e)
        {
            if (State != RiskRewardCalcState.TradeOpen) return;

            // delegate cancel
            string msg;
            if (Trade_Closing_IsCancelled?.Invoke(this.Trade, out msg) ?? false)
            {
                AppMessageBox.Error(msg, "Closing a Trade Denied", this);
                return;
            }
            
            // Prepare a proxy trade object for Closing
            var t = new Trade();
            Trade?.CopyProperties(t);
            t.CalculatorState = new();
            t.ExitPriceAvg = txtTradeExit_ExitPrice.Text.ToDecimal();
            t.FinalCapital = txtFinalCapital.Text.ToDecimal();
            t.CalculatorState.ReasonForExit = txtReasonForExit.Text;

            // 1 - Input and Sanitize - done on the TradeClosing Dialog
            var result = new dialogTradeClosing(t).ShowDialog();
            if (result == DialogResult.Yes)
            {
                // Validate the data gathered by the proxy trade object
                if (!TradeService.TradeClosing_Validate(t, out msg))
                {
                    statusMessage.Text = msg;
                    AppMessageBox.Error(msg, "", this);
                    return;
                }

                // Update this form if any changes made from TradeClosing Dialog before calling captureCalculatorState()
                txtTradeExit_ExitPrice.Text = t.ExitPriceAvg?.ToString_UptoMaxDecimal();
                txtFinalCapital.Text = t.FinalCapital?.ToMoney();
                txtReasonForExit.Text = t.CalculatorState.ReasonForExit;

                // Need to capture calculatorstate to be updated automatically by dbcontext
                captureCalculatorState().CopyProperties(this.CalculatorState);

                // 2 - Process
                // Collect -
                Trade.DateExit = t.DateExit;
                Trade.ExitPriceAvg = t.ExitPriceAvg;
                Trade.FinalCapital = t.FinalCapital;
                Trade.Status = t.Status;

                // 4 - store and set display
                // This method will save the Trade object together with its CalculatorState object
                // and will automatically register to the master form internal List
                if (_service.TradeService.Close(Trade))
                {
                    statusMessage.Text = $"Trade Id: {Trade.Id} - {Trade.Ticker} has been closed successfully.";
                    ChangeState(RiskRewardCalcState.TradeClosed);
                    _master.Trade_Closed?.Invoke(Trade);
                    SetLastSavedCalculatorHash();
                }
                else
                {
                    statusMessage.Text = "An error occur while closing a trade.";
                    MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

            }
        }

        public void Trade_Updated_Handler(Trade t)
        {
            if (Trade != default && Trade.Id == t.Id)
            {
                this.Trade = t;
                this.CalculatorState = t.CalculatorState;
                ChangeState(State);
                Save();
            }
        }

        #region Controls Validation
        private void TextBox_Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        

        private void TextBox_Money_Validating(object sender, CancelEventArgs e)
        {
            var tb = (TextBox)sender;
            string msg;

            if (!Format.isMoney(tb.Text, out msg))
            {
                //e.Cancel = true;
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tb, null);
            }
        }

        private void TextBox_Decimal_Validating(object sender, CancelEventArgs e)
        {
            var tb = (TextBox)sender;
            string msg;

            if (!Format.isDecimal(tb.Text, out msg))
            {
                //e.Cancel = true;
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tb, null);
            }
        }

        private void TextBox_Ticker_Validating(object sender, CancelEventArgs e)
        {
            var tb = (TextBox)sender;
            string msg;

            if (!Format.isTicker(tb.Text, out msg))
            {
                //e.Cancel = true;
                errorProvider1.SetError(tb, msg);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tb, null);
            }
        }
        #endregion

        // Obsolete - See Theme
        #region DataGridView Formatting
        private void PCP_Table_Formatting(DataGridView d)
        {

            //d.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            //d.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            //d.DefaultCellStyle.SelectionForeColor = Color.White;
            d.AutoGenerateColumns = false;
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.AllowUserToOrderColumns = true;
            d.AllowUserToDeleteRows = false;
            d.AllowUserToAddRows = false;
            d.AllowUserToResizeColumns = false;
            d.AllowUserToResizeRows = false;
            d.ReadOnly = true;
            d.MultiSelect = false;
            d.Font = new Font(new FontFamily("tahoma"), 8.5f, FontStyle.Regular);
            d.Columns.Clear();

            // PCP
            var pcp = new DataGridViewTextBoxColumn();
            pcp.DataPropertyName = "PCP";
            pcp.Name = "PCP";
            pcp.HeaderText = "PCP";
            pcp.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            pcp.DefaultCellStyle.Format = Constant.PERCENTAGE_FORMAT_NONE;
            pcp.Width = 80;
            pcp.ReadOnly = true;
            pcp.SortMode = DataGridViewColumnSortMode.NotSortable;
            pcp.Visible = true;
            d.Columns.Add(pcp);
            // PnL %
            var PnL_percentage = new DataGridViewTextBoxColumn();
            PnL_percentage.DataPropertyName = "PnL_Percentage";
            PnL_percentage.Name = "PnL_Percentage";
            PnL_percentage.HeaderText = "PnL %";
            PnL_percentage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PnL_percentage.DefaultCellStyle.Format = Constant.PERCENTAGE_FORMAT_SINGLE;
            PnL_percentage.Width = 80;
            PnL_percentage.ReadOnly = true;
            PnL_percentage.SortMode = DataGridViewColumnSortMode.NotSortable;
            PnL_percentage.Visible = true;
            d.Columns.Add(PnL_percentage);
            // PnL
            var PnL = new DataGridViewTextBoxColumn();
            PnL.DataPropertyName = "PnL";
            PnL.Name = "PnL";
            PnL.HeaderText = "PnL";
            PnL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            PnL.DefaultCellStyle.Format = Constant.MONEY_FORMAT;
            PnL.Width = 100;
            PnL.ReadOnly = true;
            PnL.SortMode = DataGridViewColumnSortMode.NotSortable;
            PnL.Visible = true;
            d.Columns.Add(PnL);
            // ExitPrice
            var exitPrice = new DataGridViewTextBoxColumn();
            exitPrice.DataPropertyName = "ExitPrice";
            exitPrice.Name = "ExitPrice";
            exitPrice.HeaderText = "Exit Price";
            exitPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            exitPrice.DefaultCellStyle.Format = Constant.DECIMAL_UPTO_MAX;
            exitPrice.DefaultCellStyle.ForeColor = Color.White;
            //exitPrice.DefaultCellStyle.Font = new Font(new FontFamily("tahoma"), 8.5f, FontStyle.Bold);
            exitPrice.DefaultCellStyle.BackColor = Color.SteelBlue;
            //exitPrice.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            //exitPrice.DefaultCellStyle.SelectionForeColor = Color.White;
            exitPrice.Width = 100;
            exitPrice.ReadOnly = true;
            exitPrice.SortMode = DataGridViewColumnSortMode.NotSortable;
            exitPrice.Visible = true;
            d.Columns.Add(exitPrice);

            //var button = new DataGridViewButtonColumn
            //{
            //    //Text = "ExitPrice",
            //    DataPropertyName = "ExitPrice",
            //    Name = "ExitPrice",
            //    HeaderText = "Exit Price",
            //    Width = 100,
            //    //UseColumnTextForButtonValue = true,

            //    SortMode = DataGridViewColumnSortMode.NotSortable,
            //    Visible = true,
            //};
            //d.Columns.Add(button);
        }
        #endregion

        private void PCP_Table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var s = (DataGridView)sender;
            if (e.ColumnIndex == s.Columns["ExitPrice"].Index)
            {
                if ((s.CurrentCell.Value == default)) return;
                decimal x = (decimal)(s.CurrentCell.Value ?? 0m);
                if (s == dgvPriceIncreaseTable)
                {
                    txtPriceIncrease_target.Text = x.ToString(Constant.DECIMAL_UPTO_MAX);
                    CustomPriceIncrease_Compute(null, null);
                }
                else if (s == dgvPriceDecreaseTable)
                {
                    txtPriceDecrease_target.Text = x.ToString(Constant.DECIMAL_UPTO_MAX);
                    CustomPriceDecrease_Compute(null, null);
                }
            }
        }

        private void cbxTradingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse<TradingStyle>(cbxTradingStyle.SelectedValue.ToString(), out _tradingStyle);
        }


        private void radioLotSize_CheckedChanged(object sender, EventArgs e)
        {
            var radio = (RadioButton)sender;

            txtLotSize.ReadOnly = radio.Checked;

            if (radio.Checked)
            {
                txtLotSize.Text = "auto";
                radio.Text = "auto";
            }
            else
            {
                txtLotSize.Clear();
                radio.Text = "";
            }
        }

        private void radioLeverage_CheckedChanged(object sender, EventArgs e)
        {
            var radio = (RadioButton)sender;

            txtLeverage.ReadOnly = radio.Checked;

            if (radio.Checked)
            {
                txtLeverage.Text = "auto";
                radio.Text = "auto";
            }
            else
            {
                txtLeverage.Text = "1";
                radio.Text = "";
            }
        }

        private void PositionTextboxes_KeyDown(object sender, KeyEventArgs e)
        {
            var tb = (TextBox)sender;
            if (State == RiskRewardCalcState.TradeOpen || State == RiskRewardCalcState.TradeClosed) return;

            if (e.KeyCode == Keys.Enter)
            {
                btnReCalculate_Click(null, null);
            }
        }

        private void txtLeverage_KeyDown(object sender, KeyEventArgs e)
        {
            if (State == RiskRewardCalcState.TradeOpen || State == RiskRewardCalcState.TradeClosed) 
            {
                // Don't do anything
            }
            else
            {
                var tb = (TextBox)sender;
                var val = tb.Text.ToDecimal();
                if (e.KeyCode == Keys.Up)
                {
                    tb.Text = (val + 1).ToString_UptoMaxDecimal();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if ((val - 1) < 1) { tb.Text = "1"; return; }
                    else tb.Text = (val - 1).ToString_UptoTwoDecimal();
                }
            }
              
            PositionTextboxes_KeyDown(sender, e);
        }

        private void linkCapital_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.State == RiskRewardCalcState.TradeOpen
                || this.State == RiskRewardCalcState.TradeClosed
                || this.State == RiskRewardCalcState.Deleted) return;

            (string capital, string lotSize, string price) arg = (txtCapital.Text, txtLotSize.Text, txtEntryPrice.Text);
            if (_dialogCapital == default) _dialogCapital = new dialogCapitalCalc(arg);
            _dialogCapital.StartPosition = FormStartPosition.Manual;
            _dialogCapital.Left = this.Left + txtCapital.Left + txtCapital.Width + 50;
            _dialogCapital.Top = this.Top + txtCapital.Top + 100;
            _dialogCapital.UseCapital = (pos) => { 
                txtCapital.Text = pos.capital;
                txtEntryPrice.Text = pos.price;
                txtLotSize.Text = pos.lotSize;
                btnReCalculate_Click(null, null);
            };
            _dialogCapital.ShowDialog();
        }

        private void frmRiskRewardCalc_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }
    }

 
}
