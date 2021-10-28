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
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Validation;

namespace TradingTools
{
    public partial class frmRiskRewardCalc_Long : Form
    {
        private RiskRewardCalc_Serv _rrc_serv = new();
        private CalculationDetails _calculationDetails = new();
        public EventHandler<RiskRewardCalcState> OnStateChanged;
        private TradingStyle _tradingStyle;

        public RiskRewardCalcState State { get; set; } = RiskRewardCalcState.Empty;

        public CalculatorState? CalculatorState { get; set; }
        public Trade? Trade { get; set; }
        public string Side { get; set; }
        public string PositionSide { get; private set; }



        public frmRiskRewardCalc_Long()
        {
            InitializeComponent();

            panelBandTop.Height = 3;
            panelBandBottom.Height = 3;
            cbxTradingStyle.DataSource = Enum.GetValues(typeof(TradingStyle));
            cbxTradingStyle.SelectedIndex = 1;  // swing

            PCP_Table_Formatting(dgvPriceIncreaseTable);
            PCP_Table_Formatting(dgvPriceDecreaseTable);
            // timer
            timer1.Interval = Presentation.INTERNAL_TIMER_REFRESH_VALUE;
            // events
        }

        private void btnReCalculate_Click(object sender, EventArgs e)
        {

            if (State == RiskRewardCalcState.Deleted) return;

            string msg;
            if (!captureCalculationDetails(out msg))
            {
                statusMessage.Text = msg;
                MyMessageBox.Error(statusMessage.Text, "");
                return;
            }

            // step 4: Represent data back to UI
            // the function captureCalculationDetails is able to handle the appropriate value for Lot Size from the textbox - given it is initialized properly
            txtLotSize.Text = _calculationDetails.Position.LotSize.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
            txtLeveragedCapital.Text = _calculationDetails.Position.LeveragedCapital.ToString(Constant.MONEY_FORMAT);
            txtOpeningTradingFee_dollar.Text = _calculationDetails.OpeningCost.TradingFee.ToString(Constant.MONEY_FORMAT);
            txtOpeningTradingCost.Text = _calculationDetails.OpeningCost.TradingFee.ToString(Constant.MONEY_FORMAT);


            txtBorrowAmount.Text = _calculationDetails.Borrow.Amount.ToString(Constant.MONEY_FORMAT);
            txtInterestCost.Text = _calculationDetails.Borrow.InterestCost.ToString(Constant.MONEY_FORMAT);


            //Closing Position - information
            var pit = _rrc_serv.PriceIncreaseTable.GenerateTable(
                _calculationDetails.Position.EntryPriceAvg,
                _calculationDetails.Position.LotSize,
                _calculationDetails.Borrow.InterestCost,
                _calculationDetails.Position.Capital
                ).OrderByDescending(o => o.PCP).ToList();

            if (pit != null) dgvPriceIncreaseTable.DataSource = pit;


            var pdt = _rrc_serv.PriceDecreaseTable.GenerateTable(
                _calculationDetails.Position.EntryPriceAvg,
                _calculationDetails.Position.LotSize,
                _calculationDetails.Borrow.InterestCost,
                _calculationDetails.Position.Capital
                ).OrderByDescending(o => o.PCP).ToList();

            if (pdt != null) dgvPriceDecreaseTable.DataSource = pdt;

            // compute buttons
            CustomPriceIncrease_Compute(null, null);
            CustomPriceDecrease_Compute(null, null);
            PEP_Compute(null, null);
            LEP_Compute(null, null);
            TradeExit_Compute(null, null);
            PerfectEntry_Compute(null, null);
        }

        private void frmRRC_Long_Load(object sender, EventArgs e)
        {
            // Todo: rename to InitStateDependentComponents then change to public. Place in the constructor.
            callOnLoad();
        }


        private void CustomPriceIncrease_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = InputConverter.Decimal(txtPriceIncrease_target.Text);
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc_serv.PriceIncreaseTable.GeneratePriceIncreaseRecord(
                priceTarget,
                _calculationDetails.Position.EntryPriceAvg,
                _calculationDetails.Position.LotSize,
                _calculationDetails.Borrow.InterestCost,
                _calculationDetails.Position.Capital);

            // 4
            if (rec == null) return;
            txtPriceIncreasePercentage.Text = rec.PCP.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
            txtPriceIncrease_profit.Text = rec.PnL.ToString(Constant.MONEY_FORMAT);
            txtProfitPercentage.Text = rec.PnL_Percentage.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
        }

        private void CustomPriceDecrease_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = InputConverter.Decimal(txtPriceDecrease_target.Text);
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc_serv.PriceDecreaseTable.GeneratePriceDecreaseRecord(
                priceTarget,
                _calculationDetails.Position.EntryPriceAvg,
                _calculationDetails.Position.LotSize,
                _calculationDetails.Borrow.InterestCost,
                _calculationDetails.Position.Capital);

            // 4
            if (rec == null) return;
            txtPriceDecreasePercentage.Text = rec.PCP.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
            txtPriceDecrease_loss.Text = rec.PnL.ToString(Constant.MONEY_FORMAT);
            txtLossPercentage.Text = rec.PnL_Percentage.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
        }

        private void updateRRR()
        {
            decimal profit = StringToNumeric.MoneyToDecimal(txtPEP_Profit.Text);
            decimal loss = StringToNumeric.MoneyToDecimal(txtLEP_Loss.Text);
            decimal? rrr = null;

            if (loss != 0) rrr = profit / Math.Abs(loss);


            // Display RRR
            txtRRR.Text = "1 / " + (rrr == null ? "NA" : rrr?.ToString("0.0"));
        }

        private void PEP_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = InputConverter.Decimal(txtPEP_ExitPrice.Text);
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc_serv.PriceIncreaseTable.GeneratePriceIncreaseRecord(
                priceTarget,
                _calculationDetails.Position.EntryPriceAvg,
                _calculationDetails.Position.LotSize,
                _calculationDetails.Borrow.InterestCost,
                _calculationDetails.Position.Capital);

            // 4
            if (rec == null) return;
            txtPEP_sPV.Text = _calculationDetails.GetSpeculativePositionValue(priceTarget).ToString(Constant.MONEY_FORMAT);
            txtPEP_AccountEquity.Text = _calculationDetails.GetSpeculativeAccountEquity(priceTarget).ToString(Constant.MONEY_FORMAT);

            txtPEP_PCP.Text = rec.PCP.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
            txtPEP_RealProfit_percent.Text = rec.PnL_Percentage.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
            txtPEP_Profit.Text = rec.PnL.ToString(Constant.MONEY_FORMAT);
            txtPEP_TradingCost.Text = rec.TradingCost.ToString(Constant.MONEY_FORMAT);

            updateRRR();
        }


        private void LEP_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = InputConverter.Decimal(txtLEP_ExitPrice.Text);
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc_serv.PriceDecreaseTable.GeneratePriceDecreaseRecord(
                priceTarget,
                _calculationDetails.Position.EntryPriceAvg,
                _calculationDetails.Position.LotSize,
                _calculationDetails.Borrow.InterestCost,
                _calculationDetails.Position.Capital);

            // 4
            if (rec == null) return;
            txtLEP_sPV.Text = _calculationDetails.GetSpeculativePositionValue(priceTarget).ToString(Constant.MONEY_FORMAT);
            txtLEP_AccountEquity.Text = _calculationDetails.GetSpeculativeAccountEquity(priceTarget).ToString(Constant.MONEY_FORMAT);

            txtLEP_PCP.Text = rec.PCP.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
            txtLEP_RealLoss_percent.Text = rec.PnL_Percentage.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
            txtLEP_Loss.Text = rec.PnL.ToString(Constant.MONEY_FORMAT);
            txtLEP_TradingCost.Text = rec.TradingCost.ToString(Constant.MONEY_FORMAT);

            updateRRR();
        }

        private void TradeExit_Compute(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = InputConverter.Decimal(txtTradeExit_ExitPrice.Text);
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc_serv.PriceIncreaseTable.GeneratePriceIncreaseRecord(
                priceTarget,
                _calculationDetails.Position.EntryPriceAvg,
                _calculationDetails.Position.LotSize,
                _calculationDetails.Borrow.InterestCost,
                _calculationDetails.Position.Capital);

            // 4
            if (rec == null) return;
            txtTradeExit_PV.Text = _calculationDetails.GetSpeculativePositionValue(priceTarget).ToString(Constant.MONEY_FORMAT);
            txtFinalCapital.Text = _calculationDetails.GetSpeculativeAccountEquity(priceTarget).ToString(Constant.MONEY_FORMAT);

            txtTradeExit_PCP.Text = rec.PCP.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
            txtTradeExit_PnL_percentage.Text = rec.PnL_Percentage.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
            txtTradeExit_PnL.Text = rec.PnL.ToString(Constant.MONEY_FORMAT);
            txtTradeExit_TC.Text = rec.TradingCost.ToString(Constant.MONEY_FORMAT);
        }

        private void PerfectEntry_Compute(object sender, EventArgs e)
        {
            // 2
            decimal exitPrice = InputConverter.Decimal(txtPerfectEntry_ExitPrice.Text);
            decimal entryPrice = InputConverter.Decimal(txtPerfectEntry_EntryPrice.Text);
            if (entryPrice <= 0 || exitPrice <= 0) return;

            // 3
            var rec = _rrc_serv.PriceIncreaseTable.GeneratePriceIncreaseRecord(
                exitPrice,
                entryPrice,
                _calculationDetails.Position.LotSize,
                _calculationDetails.Borrow.InterestCost,
                _calculationDetails.Position.Capital);

            // 4
            if (rec == null) return;
            txtPerfectEntry_PCP.Text = rec.PCP.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
            txtPerfectEntry_PnL_percentage.Text = rec.PnL_Percentage.ToString(Constant.PERCENTAGE_FORMAT_SINGLE);
            txtPerfectEntry_PnL.Text = rec.PnL.ToString(Constant.MONEY_FORMAT);
            txtPerfectEntry_TC.Text = rec.TradingCost.ToString(Constant.MONEY_FORMAT);
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

        private bool captureCalculationDetails(out string msg)
        {
            // The _calculationDetails will do the step 2 and 3 internally
            // step 2: Collect data -Receptors
            // step 2-B: Validation 
            // step 3: Process Data collected including others supporting data
            bool r = false;
            if (txtLotSize.Text.Length < 1 | txtLotSize.Text.Equals(string.Empty))
            {
                r = _calculationDetails.Trade_Unofficial_Calculate(
            StringToNumeric.MoneyToDecimal(txtCapital.Text),
            InputConverter.Decimal(txtLeverage.Text),
            InputConverter.Decimal(txtEntryPrice.Text),
            (int)nudDayCount.Value,
            nudDailyInterestRate.Value, out msg);
            }
            else
            {
                r = _calculationDetails.Trade_Official_Calculate(
            StringToNumeric.MoneyToDecimal(txtCapital.Text),
            InputConverter.Decimal(txtLeverage.Text),
            InputConverter.Decimal(txtLotSize.Text),
            InputConverter.Decimal(txtEntryPrice.Text),
            (int)nudDayCount.Value,
            nudDailyInterestRate.Value, out msg);
            }

            return true;
        }
        private void captureCalculatorState()
        {
            var c = CalculatorState;
            c.Capital = InputConverter.MoneyToDecimal(txtCapital.Text);
            c.Leverage = InputConverter.Decimal(txtLeverage.Text);
            c.EntryPriceAvg = InputConverter.Decimal(txtEntryPrice.Text);
            c.LotSize = InputConverter.Decimal(txtLotSize.Text);
            c.DayCount = (int)nudDayCount.Value;
            c.DailyInterestRate = nudDailyInterestRate.Value;
            c.PriceIncreaseTarget = InputConverter.Decimal(txtPriceIncrease_target.Text);
            c.PriceDecreaseTarget = InputConverter.Decimal(txtPriceDecrease_target.Text);
            c.PEP_ExitPrice = InputConverter.Decimal(txtPEP_ExitPrice.Text);
            c.PEP_Note = txtPEP_Note.Text;
            c.LEP_ExitPrice = InputConverter.Decimal(txtLEP_ExitPrice.Text);
            c.LEP_Note = txtLEP_Note.Text;
            c.Ticker = txtTicker.Text;
            c.ReasonForEntry = txtReasonForEntry.Text;
            c.Note = txtNote.Text;
            //
            c.CounterBias = txtCounterBias.Text;
            c.InterestCost = InputConverter.Decimal(txtInterestCost.Text);
            c.ExchangeFee = InputConverter.Decimal(txtExchangeFee.Text);
            c.OpeningTradingFee = InputConverter.Decimal(txtOpeningTradingFee_dollar.Text);
            c.OpeningTradingCost = InputConverter.Decimal(txtOpeningTradingCost.Text);

            c.TradingStyle = cbxTradingStyle.SelectedValue.ToString();
            //
            c.TradeExit_ExitPrice = InputConverter.Decimal(txtTradeExit_ExitPrice.Text);
            c.ReasonForExit = txtReasonForExit.Text;
            c.PerfectEntry_EntryPrice = InputConverter.Decimal(txtPerfectEntry_EntryPrice.Text);
            c.PerfectEntry_ExitPrice = InputConverter.Decimal(txtPerfectEntry_ExitPrice.Text);
            c.PerfectEntry_Note = txtPerfectEntry_Note.Text;
            //

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
            bool r = captureCalculationDetails(out msg);
            if (r == false)
            {
                statusMessage.Text = msg;
                MyMessageBox.Error(statusMessage.Text, "");
                return false;
            }

            var c = CalculatorState;
            var p = _calculationDetails.Position;
            var oc = _calculationDetails.OpeningCost;
            var b = _calculationDetails.Borrow;

            this.Trade = new Trade
            {
                Ticker = c.Ticker,
                Capital = c.Capital,
                Leverage = c.Leverage,
                EntryPriceAvg = c.EntryPriceAvg,
                LotSize = c.LotSize,
                TradingStyle = cbxTradingStyle.SelectedValue.ToString(),
                //
                PositionSide = "long",
                Status = "open",
                DateEnter = dateEnter,
                //
                LeveragedCapital = p.LeveragedCapital,
                OpeningTradingFee = oc.TradingFee,
                OpeningTradingCost = oc.TradingFee,
                BorrowAmount = b.Amount,
                DayCount = b.DayCount,
                DailyInterestRate = b.DailyInterestRate,
                InterestCost = b.InterestCost,
                //
                CalculatorState = this.CalculatorState
            };

            // 3 - Validation - the implementation may be incomplete but suffice for now - InputConverter.MoneyToDecimal
            if (!RiskRewardCalc_Serv.CalculatorState_Validate(this.CalculatorState, out msg) || !Trade_Serv.TradeOpening_Validate(this.Trade, out msg))
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

            ChangeState(this.State);
        }
        private void ChangeState(RiskRewardCalcState s)
        {
            State = s;

            TradingStyle tradeStyle;

            switch (State)
            {
                case RiskRewardCalcState.Empty:
                    panelBandTop.BackColor = BandColor.Empty;
                    panelBandBottom.BackColor = BandColor.Empty;

                    CalculatorState = new();
                    // Initalize UI controls
                    txtLeverage.Text = "3";
                    txtBorrowAmount.Text = "0";
                    nudDayCount.Value = 1;
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
                        panelBandTop.BackColor = BandColor.Loaded;
                        panelBandBottom.BackColor = BandColor.Loaded;

                        // sys flow 1
                        txtCapital.Text = c.Capital.ToString("0.00");     // dont change format
                        txtLeverage.Text = c.Leverage.ToString("0");      // dont change format
                        txtEntryPrice.Text = c.EntryPriceAvg.ToString();
                        txtLotSize.Text = c.LotSize.ToString();
                        nudDayCount.Value = c.DayCount < nudDayCount.Minimum ? nudDayCount.Minimum : CalculatorState.DayCount;
                        nudDailyInterestRate.Value = c.DailyInterestRate < nudDailyInterestRate.Minimum ? nudDailyInterestRate.Minimum : CalculatorState.DailyInterestRate;
                        // caveat: the CalculationDetails object will always use the Constance Exchange Fee 0.001 - it is not aware of any external source such as CalculatorState.ExchangeFee
                        txtExchangeFee.Text = c.ExchangeFee.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        btnReCalculate_Click(null, null);

                        // sys flow 2
                        txtPriceIncrease_target.Text = c.PriceIncreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtPriceDecrease_target.Text = c.PriceDecreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);

                        // sys flow 3
                        txtPEP_ExitPrice.Text = c.PEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtPEP_Note.Text = c.PEP_Note;

                        // sys flow
                        txtLEP_ExitPrice.Text = c.LEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtLEP_Note.Text = c.LEP_Note;
                        
                        //
                        txtTradeExit_ExitPrice.Text = c.TradeExit_ExitPrice?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtReasonForExit.Text = c.ReasonForExit;
                        txtPerfectEntry_EntryPrice.Text = c.PerfectEntry_EntryPrice?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtPerfectEntry_ExitPrice.Text = c.PerfectEntry_ExitPrice?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtPerfectEntry_Note.Text = c.PerfectEntry_Note;

                        // Independent data
                        this.Text = c.Ticker;
                        txtTicker.Text = c.Ticker;
                        txtReasonForEntry.Text = c.ReasonForEntry;
                        txtCounterBias.Text = c.CounterBias;
                        txtNote.Text = c.Note;
                        Enum.TryParse<TradingStyle>(c.TradingStyle, out tradeStyle);
                        cbxTradingStyle.SelectedItem = tradeStyle;

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

                        // load Trade detail
                        // Independent data
                        txtTradeNum.Text = Trade.Id.ToString();
                        txtTicker.Text = Trade.Ticker;
                        Enum.TryParse<TradingStyle>(Trade.TradingStyle, out tradeStyle);
                        cbxTradingStyle.SelectedItem = tradeStyle;

                        // sys flow 1
                        txtCapital.Text = Trade.Capital.ToString(Constant.MONEY_FORMAT);     // Money Format Regex implemented
                        txtLeverage.Text = Trade.Leverage.ToString(Constant.LEVERAGE_DECIMAL_PLACE);
                        txtEntryPrice.Text = Trade.EntryPriceAvg.ToString();
                        txtLotSize.Text = Trade.LotSize.ToString();

                        //Numeric Up Down control throws exception when assigned value less then their Minimum value
                        decimal d = Trade_Serv.GetTrading_ElaspsedTime_Days(Trade.DateEnter, DateTime.Now);
                        nudDayCount.Value = d < nudDayCount.Minimum ? nudDayCount.Minimum : d;
                        nudDailyInterestRate.Value = Trade.DailyInterestRate < nudDailyInterestRate.Value ? nudDailyInterestRate.Minimum : Trade.DailyInterestRate;
                        
                        txtExchangeFee.Text = CalculatorState.ExchangeFee.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);

                        btnReCalculate_Click(null, null);

                        #region Purely CalculatorState
                        // sys flow
                        txtPriceIncrease_target.Text = CalculatorState.PriceIncreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtPriceDecrease_target.Text = CalculatorState.PriceDecreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);

                        // sys flow
                        txtPEP_ExitPrice.Text = CalculatorState.PEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtPEP_Note.Text = CalculatorState.PEP_Note;

                        // sys flow
                        txtLEP_ExitPrice.Text = CalculatorState.LEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtLEP_Note.Text = CalculatorState.LEP_Note;

                        // sys flow
                        txtTradeExit_ExitPrice.Text = CalculatorState.TradeExit_ExitPrice?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtReasonForExit.Text = CalculatorState.ReasonForExit;

                        txtPerfectEntry_EntryPrice.Text = CalculatorState.PerfectEntry_EntryPrice?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtPerfectEntry_ExitPrice.Text = CalculatorState.PerfectEntry_ExitPrice?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtPerfectEntry_Note.Text = CalculatorState.PerfectEntry_Note;
                        //
                        txtReasonForEntry.Text = CalculatorState.ReasonForEntry;
                        txtCounterBias.Text = CalculatorState.CounterBias;
                        txtNote.Text = CalculatorState.Note;
                        #endregion
                        //
                        lblHeader.Text = Trade.Ticker + $" - OPEN";
                        this.Text = Trade.Ticker + $" - Trade No. { Trade?.Id}";

                        txtTicker.ReadOnly = true;
                        txtCapital.ReadOnly = true;
                        txtLeverage.ReadOnly = true;
                        txtEntryPrice.ReadOnly = true;
                        txtLotSize.ReadOnly = true;
                        cbxTradingStyle.Enabled = false;
                        nudDayCount.ReadOnly = true;
                        nudDailyInterestRate.ReadOnly = true;

                        btnReCalculate.Visible = false;
                        btnDelete.Visible = false;
                        btnOfficializedTrade.Enabled = false;
                        btnOfficializedTrade.Visible = false;
                        btnCloseTheTrade.Visible = true;
                    }
                    break;

                case RiskRewardCalcState.TradeClosed:
                    panelBandTop.BackColor = BandColor.TradeClosed;
                    panelBandBottom.BackColor = BandColor.TradeClosed;


                    //Numeric Up Down control throws exception when assigned value less then their Minimum value
                    decimal e = Trade_Serv.GetTrading_ElaspsedTime_Days(Trade.DateEnter, Trade.DateExit);
                    nudDayCount.Value = e < nudDayCount.Minimum ? nudDayCount.Minimum : e;

                    #region Trade Common
                    // load trade details
                    // Independent data
                    txtTradeNum.Text = Trade.Id.ToString();
                    txtTicker.Text = Trade.Ticker;
                    Enum.TryParse<TradingStyle>(Trade.TradingStyle, out tradeStyle);
                    cbxTradingStyle.SelectedItem = tradeStyle;

                    // sys flow 1
                    txtCapital.Text = Trade.Capital.ToString(Constant.MONEY_FORMAT);
                    txtLeverage.Text = Trade.Leverage.ToString(Constant.LEVERAGE_DECIMAL_PLACE);      
                    txtEntryPrice.Text = Trade.EntryPriceAvg.ToString();
                    txtLotSize.Text = Trade.LotSize.ToString();

                    nudDailyInterestRate.Value = Trade.DailyInterestRate < nudDailyInterestRate.Value ? nudDailyInterestRate.Minimum : Trade.DailyInterestRate;

                    txtExchangeFee.Text = CalculatorState.ExchangeFee.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);

                    btnReCalculate_Click(null, null);

                    #region Almost All CalculatorState
                    // sys flow
                    txtPriceIncrease_target.Text = CalculatorState.PriceIncreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    txtPriceDecrease_target.Text = CalculatorState.PriceDecreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);

                    // sys flow
                    txtPEP_ExitPrice.Text = CalculatorState.PEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    txtPEP_Note.Text = CalculatorState.PEP_Note;

                    // sys flow
                    txtLEP_ExitPrice.Text = CalculatorState.LEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    txtLEP_Note.Text = CalculatorState.LEP_Note;

                    // sys flow
                    txtTradeExit_ExitPrice.Text = Trade.ExitPriceAvg?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);  // Trade
                    txtReasonForExit.Text = CalculatorState.ReasonForExit;

                    txtPerfectEntry_EntryPrice.Text = CalculatorState.PerfectEntry_EntryPrice?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    txtPerfectEntry_ExitPrice.Text = CalculatorState.PerfectEntry_ExitPrice?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    txtPerfectEntry_Note.Text = CalculatorState.PerfectEntry_Note;
                    //
                    txtReasonForEntry.Text = CalculatorState.ReasonForEntry;
                    txtCounterBias.Text = CalculatorState.CounterBias;
                    txtNote.Text = CalculatorState.Note;
                    #endregion

                    #endregion

                    //
                    lblHeader.Text = Trade.Ticker + " - CLOSED"; ;
                    lblHeader.ForeColor = Color.LightSteelBlue;
                    this.Text = Trade.Ticker + $" - Trade No. { Trade?.Id}";

                    // same as trade open
                    txtTicker.ReadOnly = true;
                    txtCapital.ReadOnly = true;
                    txtLeverage.ReadOnly = true;
                    txtEntryPrice.ReadOnly = true;
                    txtLotSize.ReadOnly = true;
                    cbxTradingStyle.Enabled = false;
                    nudDayCount.ReadOnly = true;
                    nudDailyInterestRate.ReadOnly = true;

                    btnDelete.Visible = false;
                    btnOfficializedTrade.Enabled = false;
                    btnOfficializedTrade.Visible = false;

                    // trade close
                    txtPEP_ExitPrice.ReadOnly = true;
                    txtLEP_ExitPrice.ReadOnly = true;
                    txtPerfectEntry_EntryPrice.ReadOnly = true;
                    txtPerfectEntry_ExitPrice.ReadOnly = true;
                    txtTradeExit_ExitPrice.ReadOnly = true;
                    txtFinalCapital.ReadOnly = true;

                    btnSetPEP.Visible = false;
                    btnSetLEP.Visible = false;
                    btnReCalculate.Visible = false;
                    //btnSave.Visible = false;
                    btnCloseTheTrade.Visible = false;

                    gbxNotes.Height = 800;
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

        private void btnCloseTheTrade_Click(object sender, EventArgs e)
        {
            if (State != RiskRewardCalcState.TradeOpen) return;

            // Prepare Trade object for Closing
            Trade.ExitPriceAvg = InputConverter.Decimal(txtTradeExit_ExitPrice.Text);
            Trade.FinalCapital = StringToNumeric.MoneyToDecimal(txtFinalCapital.Text);
            Trade.CalculatorState.ReasonForExit = txtReasonForExit.Text;

            //var tradeClosing = new TradeClosing(Trade);
            //tradeClosing.Trade_Close += this.closeTheTrade;

            var result = new TradeClosing(Trade).ShowDialog();
            if (result == DialogResult.Yes)
            {
                // 1 - Input and Sanitize - done on the TradeClosing Dialog
                // Update this form if any changes made from TradeClosing Dialog prior to captureCalculatorState()
                txtTradeExit_ExitPrice.Text = Trade.ExitPriceAvg?.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                txtFinalCapital.Text = Trade.FinalCapital?.ToString(Constant.MONEY_FORMAT);
                txtReasonForExit.Text = Trade.CalculatorState.ReasonForExit;
                // Need to capture calculatorstate to be updated automatically by dbcontext
                captureCalculatorState();
                //decimal exitPrice = radPEP_ExitPrice.Checked ? StringToNumeric.MoneyToDecimal(txtPEP_ExitPrice.Text) : StringToNumeric.MoneyToDecimal(txtLEP_ExitPrice.Text);

                // 2 - Process
                string msg;
                if (!captureCalculationDetails(out msg))
                {
                    statusMessage.Text = msg;
                    MyMessageBox.Error(statusMessage.Text, "");
                    return;
                }


                var rec = PriceChangeTable.GeneratePriceChangeRecord(
               Trade.ExitPriceAvg ?? 0,
               _calculationDetails.Position.EntryPriceAvg,
               _calculationDetails.Position.LotSize,
               _calculationDetails.Borrow.InterestCost,
               _calculationDetails.Position.Capital);

                // Collect -
                // Trade Closing Data are captured in TradeClosing Dialog
                //Trade.DateExit
                //Trade.ExitPriceAvg
                //Trade.FinalCapital

                // auto-compute
                Trade.DayCount = _calculationDetails.Borrow.DayCount;
                Trade.DailyInterestRate = _calculationDetails.Borrow.DailyInterestRate;
                Trade.InterestCost = _calculationDetails.Borrow.InterestCost;
                Trade.ClosingTradingFee = PriceChangeTable.SpeculativeTradingFee(Trade.ExitPriceAvg ?? 0, Trade.LotSize);
                Trade.ClosingTradingCost = rec.TradingCost;

                Trade.PnL = Trade.FinalCapital - Trade.Capital;
                Trade.PnL_percentage = Trade.PnL / Trade.Capital * 100;
                //
                Trade.Status = "closed";

                
                // 3 - Validation
                if (!Trade_Serv.TradeClosing_Validate(Trade, out msg))
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
            exitPrice.DefaultCellStyle.Format = Constant.MAX_DECIMAL_PLACE_FORMAT;
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
                    txtPriceIncrease_target.Text = x.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    CustomPriceIncrease_Compute(null, null);
                }
                else if (s == dgvPriceDecreaseTable)
                {
                    txtPriceDecrease_target.Text = x.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    CustomPriceDecrease_Compute(null, null);
                }
            }
        }

        private void cbxTradingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse<TradingStyle>(cbxTradingStyle.SelectedValue.ToString(), out _tradingStyle);
        }
    }

    
}
