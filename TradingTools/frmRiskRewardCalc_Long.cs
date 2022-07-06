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
using TradingTools.Model;
using TradingTools.Services;
using TradingTools.Services.Interface;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Extensions;
using TradingTools.Trunk.Validation;

namespace TradingTools
{
    public partial class frmRiskRewardCalc_Long : Form
    {
        private IRiskRewardCalc _rrc;
        private RiskRewardCalc_Serv _rrc_serv = new();  // Obsolete: in favor of IRiskRewardCalc
        public EventHandler<RiskRewardCalcState> OnStateChanged;
        private TradingStyle _tradingStyle;

        public RiskRewardCalcState State { get; set; } = RiskRewardCalcState.Empty;

        public CalculatorState? CalculatorState { get; set; }
        public Trade? Trade { get; set; }
        public string Side { get; set; }
        public Position Position { get; set; }
        public ISideTheme Theme { get; private set; }

        public frmRiskRewardCalc_Long(IRiskRewardCalc riskRewardCalc)
        {
            InitializeComponent();

            // Priority
            // Dependency Injection of RiskRewardCalc
            _rrc = riskRewardCalc;
            Side = riskRewardCalc.Side;

            this.Theme = TradingTools.Theme.SideTheme_GetInstance(riskRewardCalc.Side);
            //

            myInitializeComponent();
        }

        // May be obsoleted - but this cannot be deleted as empty constructor is needed by .NET
        public frmRiskRewardCalc_Long()
        {
            InitializeComponent();

            myInitializeComponent();
        }

        private void myInitializeComponent()
        {
            panelBandTop.Height = 3;
            panelBandBottom.Height = 3;
            cbxTradingStyle.DataSource = Enum.GetValues(typeof(TradingStyle));
            cbxTradingStyle.SelectedIndex = 1;  // swing

            Theme.Format_PriceIncreaseTable(dgvPriceIncreaseTable);
            Theme.Format_PriceDecreaseTable(dgvPriceDecreaseTable);

            // timer
            timer1.Interval = Presentation.INTERNAL_TIMER_REFRESH_VALUE;
            // events
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
                MyMessageBox.Error(statusMessage.Text, "");
                return;
            }

            
            // step 4: Represent data back to UI
            // the function captureCalculationDetails is able to handle the appropriate value for Lot Size from the textbox - given it is initialized properly
            txtLeverage.Text = Position.Leverage.ToDecimalUptoTwo();
            txtLotSize.Text = Position.LotSize.ToDecimalUptoMax();
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
        }

        private Position GetPositionData(out string msg)
        {
            var p = new Position();
            msg = string.Empty;

            // Collect Data
            p.Capital = txtCapital.Text.ToDecimal();
            p.EntryPriceAvg = txtEntryPrice.Text.ToDecimal();
            p.Leverage = txtLeverage.Text.ToDecimal();
            p.LotSize = txtLotSize.Text.ToDecimal();

            // Validation - The collection process will assign default values - no null object is expected
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

            // Process
            p.Leverage = radioLeverage.Checked
                ? Formula.Leverage(p.EntryPriceAvg, p.LotSize, p.Capital)
                : p.Leverage;
            p.LotSize = radioLotSize.Checked
                ? Formula.LotSize(p.Capital, p.Leverage, p.EntryPriceAvg)
                : p.LotSize;


            // Final Validation - Strict
            // Hence, Minimum value 
            if (p.Capital <= 10 | p.LotSize <= 0 | p.EntryPriceAvg <= 0)
            {
                msg = "Invalid input data";
                return default;
            }

            return p;
        }

        private void frmRRC_Long_Load(object sender, EventArgs e)
        {
            // Proposal: rename to InitStateDependentComponents then change to public. Place in the constructor.
            callOnLoad();
        }


        private void CustomPriceIncrease_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = txtPriceIncrease_target.Text.ToDecimal();
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc.ComputePnL(priceTarget, Position);

            // 4
            if (rec == null) return;
            txtPriceIncreasePercentage.Text = rec.PCP.ToPercentageSingle();
            txtPriceIncrease_profit.Text = rec.PnL.ToMoney();
            txtProfitPercentage.Text = rec.PnL_Percentage.ToPercentageSingle();
        }

        private void CustomPriceDecrease_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = txtPriceDecrease_target.Text.ToDecimal();
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc.ComputePnL(priceTarget, Position);

            // 4
            if (rec == null) return;
            txtPriceDecreasePercentage.Text = rec.PCP.ToPercentageSingle();
            txtPriceDecrease_loss.Text = rec.PnL.ToMoney();
            txtLossPercentage.Text = rec.PnL_Percentage.ToPercentageSingle();
        }

        private void updateRRR()
        {
            decimal profit = txtPEP_Profit.Text.ToDecimal();
            decimal loss = txtLEP_Loss.Text.ToDecimal();
            decimal? rrr = null;

            if (loss != 0) rrr = profit / Math.Abs(loss);


            // Display RRR
            txtRRR.Text = "1 / " + (rrr == null ? "NA" : rrr?.ToString("0.0"));
        }

        private void PEP_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = txtPEP_ExitPrice.Text.ToDecimal();
            if (priceTarget <= 0) return;

            // 3
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
            var exitPlan = _rrc.PnlExitPlan(priceTarget, Position);
            var rec = exitPlan.Item1;
            var sPV = exitPlan.Item2;
            var equity = exitPlan.Item3;

            // 4
            if (exitPlan == null) return;
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
            txtMultiple.Text = (1 + (pcp / 100)).ToDecimalUptoTwo() + "x";
        }

        private void btnSetLEP_Click(object sender, EventArgs e)
        {
            txtLEP_ExitPrice.Text = txtPriceDecrease_target.Text;
            LEP_Compute(null, null);
        }

        private void btnSetPEP_Click(object sender, EventArgs e)
        {
            txtPEP_ExitPrice.Text = txtPriceIncrease_target.Text;
            PEP_Compute(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (State == RiskRewardCalcState.Deleted) return;
            Save();
        }

        private void captureCalculatorState()
        {
            var c = CalculatorState;
            // Collect Receptors
            c.Capital = txtCapital.Text.ToDecimal();
            c.Leverage = txtLeverage.Text.ToDecimal();
            c.EntryPriceAvg = txtEntryPrice.Text.ToDecimal();
            c.LotSize = txtLotSize.Text.ToDecimal();
            c.DayCount = txtDayCount.Text.ToDecimal();
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

        }

        private bool Save()
        {
            // Capture state
            // 1
            captureCalculatorState();

            // 2-B Validation - the implementation may be incomplete but suffice for nowInputConverter.MoneyToDecimal
            string msg;
            if (!RiskRewardCalc_Serv.CalculatorState_Validate(this.CalculatorState, out msg))
            {
                statusMessage.Text = msg;
                MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            // 3 - process data collected - this will save data into a data store - no need for step 4 which is to Display Data 
            var o = (master)this.Owner;
            if (State == RiskRewardCalcState.Empty)
            {
                // Add to the Owner's List
                if (o.CalculatorState_Add(CalculatorState))
                {
                    ChangeState(RiskRewardCalcState.Loaded);
                    statusMessage.Text = "State save successfully.";
                }
                else statusMessage.Text = "Saving state failed.";
            }
            else if (State == RiskRewardCalcState.Loaded | State == RiskRewardCalcState.TradeOpen | State == RiskRewardCalcState.TradeClosed)
            {
                if (o.CalculatorState_Update())
                {
                    statusMessage.Text = "State updated successfully.";
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

            DialogResult objDialog = MyMessageBox.Question_YesNo("Are you sure you want to DELETE this State", "Delete");
            if (objDialog == DialogResult.Yes)
            {
                var o = (master)this.Owner;
                // Remove from the Owner's List
                if (o.CalculatorState_Delete(CalculatorState))
                {
                    ChangeState(RiskRewardCalcState.Deleted);
                    statusMessage.Text = "State was deleted successfully. \n\nThis form will now close.";
                    MessageBox.Show(statusMessage.Text, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // just close the form
                    this.Close();
                }
                else
                {
                    statusMessage.Text = "Deleting state failed.";
                    MyMessageBox.Error(statusMessage.Text, "Delete");
                }
            }


        }

        private void frmRiskRewardCalc_Long_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Just ignore if the reason for closing was cause by deletion
            if (State == RiskRewardCalcState.Deleted) return;

            // show the form in case was minimized and closing was came from external such as from a parent form
            this.WindowState = FormWindowState.Normal;
            this.Focus();

            DialogResult objDialog = MessageBox.Show("Do you want to save before closing ?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            statusMessage.Text = "Status message . . .";
        }

        private void btnOfficializedTrade_Click(object sender, EventArgs e)
        {
            if (State == RiskRewardCalcState.Deleted) return;

            if (State == RiskRewardCalcState.Empty | State == RiskRewardCalcState.Loaded)
            {
                dynamic d = new ExpandoObject();
                d.Ticker = txtTicker.Text;
                d.Capital = txtCapital.Text;
                d.Leverage = txtLeverage.Text;
                d.LotSize = txtLotSize.Text;
                d.EntryPrice = txtEntryPrice.Text;

                var officializeDialog = new TradeOfficialize();
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
            captureCalculatorState();
            string msg;
            // 2 - Process
            var c = CalculatorState;

            this.Trade = new Trade
            {
                Ticker = c.Ticker,
                Capital = c.Capital,
                Leverage = c.Leverage,
                EntryPriceAvg = c.EntryPriceAvg,
                LotSize = c.LotSize,
                TradingStyle = cbxTradingStyle.SelectedValue.ToString(),
                //
                Side = "long",
                Status = "open",
                DateEnter = dateEnter,
                //
                CalculatorState = this.CalculatorState
            };

            // 3 - Validation - the implementation may be incomplete but suffice for now - InputConverter.MoneyToDecimal
            if (!RiskRewardCalc_Serv.CalculatorState_Validate(this.CalculatorState, out msg) || !TradeService.TradeOpening_Validate(this.Trade, out msg))
            {
                statusMessage.Text = msg;
                MyMessageBox.Error(statusMessage.Text, "");
                return false;
            }

            // 4 - Save data into a data store - ChangeState will Display Data 
            var o = (master)this.Owner;
            if (o.Trade_Add(this.Trade))
            {
                ChangeState(RiskRewardCalcState.TradeOpen);
                statusMessage.Text = $"Ticker: {Trade.Ticker} has been officialized successfully.";
                MyMessageBox.Inform(statusMessage.Text, $"Trade No. {Trade.Id} is Official");
            }
            else
            {
                statusMessage.Text = "Officializing a Trade failure";
                MyMessageBox.Error(statusMessage.Text, "");
                return false;
            }

            return true;
        }


        public void MarkAsDeleted(Trade t)
        {
            if (Trade != default && Trade.Equals(t)) ChangeState(RiskRewardCalcState.Deleted);
        }

        private void callOnLoad()
        {
            // State independent controls
            txtExchangeFee.Text = Constant.TRADING_FEE.ToString();

            panelTitleBand.BackColor = Theme.BandColor;
            panelFooterBand.BackColor = Theme.BandColor;
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
                    lblHeader.Text += " - Unsaved";
                    panelBandTop.BackColor = BandColor.Empty;
                    panelBandBottom.BackColor = BandColor.Empty;

                    CalculatorState = new();

                    // Initalize UI controls
                    txtLeverage.Text = "1";
                    txtBorrowAmount.Text = "0";
                    txtDayCount.Text = "0";

                    nudDailyInterestRate.Value = Constant.DAILY_INTEREST_RATE;

                    txtPEP_Note.Text = "take-profit: inactive";
                    txtLEP_Note.Text = "stop-loss: inactive";

                    btnCloseTheTrade.Visible = false;
                    break;

                case RiskRewardCalcState.Loaded:
                    var c = CalculatorState;
                    if (CalculatorState == null)
                    {
                        statusMessage.Text = "Internal error: CalculatorState instance was not forwarded.";
                        MessageBox.Show(statusMessage.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        this.Close();
                        return;
                    }
                    else
                    {
                        lblHeader.Text += " - Unofficial";
                        panelBandTop.BackColor = BandColor.Loaded;
                        panelBandBottom.BackColor = BandColor.Loaded;

                        
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
                        txtCapital.Text = c.Capital.ToDecimal_Two();     // dont change format
                        txtLeverage.Text = c.Leverage.ToDecimalUptoOne();      // dont change format
                        txtEntryPrice.Text = c.EntryPriceAvg.ToDecimalUptoMax();
                        txtLotSize.Text = c.LotSize.ToDecimalUptoMax();
                        
                        nudDailyInterestRate.Value = Math.Max(c.DailyInterestRate, nudDailyInterestRate.Minimum);
                        txtExchangeFee.Text = c.ExchangeFee.ToDecimalUptoMax();
                        btnReCalculate_Click(null, null);

                        // sys flow 2
                        txtPriceIncrease_target.Text = c.PriceIncreaseTarget?.ToDecimalUptoMax();
                        txtPriceDecrease_target.Text = c.PriceDecreaseTarget?.ToDecimalUptoMax();

                        // sys flow 3
                        txtPEP_ExitPrice.Text = c.PEP_ExitPrice?.ToDecimalUptoMax();
                        txtPEP_Note.Text = c.PEP_Note;

                        // sys flow
                        txtLEP_ExitPrice.Text = c.LEP_ExitPrice?.ToDecimalUptoMax();
                        txtLEP_Note.Text = c.LEP_Note;
                        
                        //
                        txtTradeExit_ExitPrice.Text = c.TradeExit_ExitPrice?.ToDecimalUptoMax();
                        txtReasonForExit.Text = c.ReasonForExit;
                        txtPerfectEntry_EntryPrice.Text = c.PerfectEntry_EntryPrice?.ToDecimalUptoMax();
                        txtPerfectEntry_ExitPrice.Text = c.PerfectEntry_ExitPrice?.ToDecimalUptoMax();
                        txtPerfectEntry_Note.Text = c.PerfectEntry_Note;

                        btnCloseTheTrade.Visible = false;
                    }
                    break;

                case RiskRewardCalcState.TradeOpen:
                    if (CalculatorState == null)
                    {
                        statusMessage.Text = "Internal error: CalculatorState instance was not forwarded.";
                        MyMessageBox.Error(statusMessage.Text);
                        this.Close();
                        return;
                    }
                    else if (Trade == null)
                    {
                        statusMessage.Text = "Internal error: Trade instance was not forwarded.";
                        MyMessageBox.Error(statusMessage.Text);
                        this.Close();
                        return;
                    }
                    else
                    {
                        panelBandTop.BackColor = BandColor.TradeOpen;
                        panelBandBottom.BackColor = BandColor.TradeOpen;
                        lblHeader.Text = Trade.Ticker + $" - OPEN";
                        btnCloseTheTrade.Visible = true;

                        TradeCommon_Load(Trade);
                    }
                    break;

                case RiskRewardCalcState.TradeClosed:
                    panelBandTop.BackColor = BandColor.TradeClosed;
                    panelBandBottom.BackColor = BandColor.TradeClosed;
                    lblHeader.Text = Trade.Ticker + " - CLOSED"; ;
                    lblHeader.ForeColor = Color.LightSteelBlue;
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
                    txtTradeExit_PV.Text = Formula.PositionValue(Trade.LotSize, Trade.ExitPriceAvg ?? 0).ToMoney();
                    txtFinalCapital.Text = Trade.FinalCapital?.ToMoney();

                    txtTradeExit_PCP.Text = Formula.PCP(Trade.EntryPriceAvg, Trade.ExitPriceAvg).ToPercentageSingle();
                    txtTradeExit_PnL_percentage.Text = Trade.PnL_percentage?.ToPercentageSingle();
                    txtTradeExit_PnL.Text = Trade.PnL?.ToMoney();
                    // Transaction Cost
                    txtTradeExit_TC.Text = "--"; // (pv - fc).ToString(Constant.MONEY_FORMAT);
                    break;

                case RiskRewardCalcState.Deleted:
                    this.Text += " < < DELETED > >";
                    lblHeader.Text += " < < DELETED > >";

                    // disable the form from interacting to database
                    btnSave.Visible = false;
                    btnDelete.Visible = false;
                    btnOfficializedTrade.Visible = false;
                    btnCloseTheTrade.Visible = false;
                    break;
            }
        }

        private void TradeCommon_Load(Trade t)
        {
            // trade
            this.Text = Trade.Ticker + $" - Trade No. {Trade?.Id}";
            txtTradeNum.Text = Trade.Id.ToString();
            txtTicker.Text = Trade.Ticker;

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
            txtLeverage.Text = Trade.Leverage.ToDecimalUptoTwo();
            txtEntryPrice.Text = Trade.EntryPriceAvg.ToDecimalUptoMax();
            txtLotSize.Text = Trade.LotSize.ToDecimalUptoMax();
            txtDayCount.Text = Trade.DayCount?.ToDecimalUptoTwo() ?? "0";

            //Numeric Up Down control throws exception when assigned value less then their Minimum value
            nudDailyInterestRate.Value = Math.Max(CalculatorState.DailyInterestRate, nudDailyInterestRate.Minimum);

            txtExchangeFee.Text = CalculatorState.ExchangeFee.ToDecimalUptoMax();

            btnReCalculate_Click(null, null);

            #region Purely CalculatorState
            // sys flow
            txtPriceIncrease_target.Text = CalculatorState.PriceIncreaseTarget?.ToDecimalUptoMax();
            txtPriceDecrease_target.Text = CalculatorState.PriceDecreaseTarget?.ToDecimalUptoMax();

            // sys flow
            txtPEP_ExitPrice.Text = CalculatorState.PEP_ExitPrice?.ToDecimalUptoMax();
            txtPEP_Note.Text = CalculatorState.PEP_Note;

            // sys flow
            txtLEP_ExitPrice.Text = CalculatorState.LEP_ExitPrice?.ToDecimalUptoMax();
            txtLEP_Note.Text = CalculatorState.LEP_Note;

            // sys flow
            txtTradeExit_ExitPrice.Text = CalculatorState.TradeExit_ExitPrice?.ToDecimalUptoMax();
            txtReasonForExit.Text = CalculatorState.ReasonForExit;

            txtPerfectEntry_EntryPrice.Text = CalculatorState.PerfectEntry_EntryPrice?.ToDecimalUptoMax();
            txtPerfectEntry_ExitPrice.Text = CalculatorState.PerfectEntry_ExitPrice?.ToDecimalUptoMax();
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

            // Prepare a trade object for Closing
            var t = new Trade();
            t.CalculatorState = new();
            t.ExitPriceAvg = InputConverter.Decimal(txtTradeExit_ExitPrice.Text);
            t.FinalCapital = StringToNumeric.MoneyToDecimal(txtFinalCapital.Text);
            t.CalculatorState.ReasonForExit = txtReasonForExit.Text;

            var result = new TradeClosing(t).ShowDialog();
            if (result == DialogResult.Yes)
            {
                // 1 - Input and Sanitize - done on the TradeClosing Dialog
                // Update this form if any changes made from TradeClosing Dialog before calling captureCalculatorState()
                txtTradeExit_ExitPrice.Text = t.ExitPriceAvg?.ToString(Constant.DECIMAL_UPTO_MAX);
                txtFinalCapital.Text = t.FinalCapital?.ToString(Constant.MONEY_FORMAT);
                txtReasonForExit.Text = t.CalculatorState.ReasonForExit;

                // Need to capture calculatorstate to be updated automatically by dbcontext
                captureCalculatorState();

                // 2 - Process
                // Collect -
                Trade.DateExit = t.DateExit;
                Trade.ExitPriceAvg = t.ExitPriceAvg;
                Trade.FinalCapital = t.FinalCapital;

                Trade.Status = "closed";

                string msg;
                // 3 - Validation
                if (!TradeService.TradeClosing_Validate(Trade, out msg))
                {
                    statusMessage.Text = msg;
                    MyMessageBox.Error(msg);
                    return;
                }

                // 4 - store and display
                var o = (master)this.Owner;
                // Add to the Owner's List
                if (o.Trade_Close(Trade))
                {
                    ChangeState(RiskRewardCalcState.TradeClosed);
                    statusMessage.Text = $"Trade No. '{Trade.Id}' has been closed successfully.";
                }
                else
                {
                    statusMessage.Text = "An error occur while closing a trade.";
                    MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

            }
        }

        public void Trade_Updated(Trade t)
        {
            if (Trade != default && Trade.Equals(t))
            {
                Save();
                ChangeState(State); 
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

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void TextBox_Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        private void TextBox_Integer_Validating(object sender, CancelEventArgs e)
        {

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
            if (e.KeyCode == Keys.Enter)
            {
                btnReCalculate_Click(null, null);
            }
        }
    }

 
}
