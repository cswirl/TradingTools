using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private CalculationDetails _calc = new();
        public EventHandler<RiskRewardCalcState> OnStateChanged;

        public RiskRewardCalcState State { get; set; } = RiskRewardCalcState.Empty;

        public CalculatorState CalculatorState { get; set; }
        public Trade Trade { get; set; }
        public string Side { get; set; }

        public frmRiskRewardCalc_Long()
        {
            InitializeComponent();

            panelBandTop.Height = 3;
            panelBandBottom.Height = 3;
            // timer
            timer1.Interval = Presentation.INTERNAL_TIMER_REFRESH_VALUE;
            // events
        }

        private void btnReCalculate_Click(object sender, EventArgs e)
        {
            // _calc.Calculate() internal do the step 2 and 3
            // step 2: Collect data -Receptors
            // step 2-B: Validation 
            // step 3: Process Data collected including others supporting data
            string msg;
            var r = _calc.Calculate(
                StringToNumeric.MoneyToDecimal(txtCapital.Text),
                InputConverter.Decimal(txtLeverage.Text),
                InputConverter.Decimal(txtEntryPrice.Text),
                (int)nudDayCount.Value,
                nudDailyInterestRate.Value, out msg);

            if (r == false)
            {
                statusMessage.Text = msg;
                MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            // step 4: Represent data back to UI
            txtLotSize.Text = _calc.Position.LotSize.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
            txtLeveragedCapital.Text = _calc.Position.LeveragedCapital.ToString(Constant.MONEY_FORMAT);
            txtInitalPositionValue.Text = _calc.Position.InitialPositionValue.ToString(Constant.MONEY_FORMAT);
            txtOpeningTradingFee_dollar.Text = _calc.OpeningCost.TradingFee.ToString(Constant.MONEY_FORMAT);
            txtOpeningTradingCost.Text = _calc.OpeningCost.TradingFee.ToString(Constant.MONEY_FORMAT);

            txtBorrowAmount.Text = _calc.Borrow.Amount.ToString(Constant.MONEY_FORMAT);
            txtInterestCost.Text = _calc.Borrow.InterestCost.ToString(Constant.MONEY_FORMAT);


            //Closing Position - information
            var pit = _rrc_serv.PriceIncreaseTable.GenerateTable(
                _calc.Position.EntryPriceAvg,
                _calc.Position.LotSize,
                _calc.Borrow.InterestCost,
                _calc.Position.Capital
                ).OrderByDescending(o => o.PriceChangePercentage).ToList();

            if (pit != null) dgvPriceIncreaseTable.DataSource = pit;


            var pdt = _rrc_serv.PriceDecreaseTable.GenerateTable(
                _calc.Position.EntryPriceAvg,
                _calc.Position.LotSize,
                _calc.Borrow.InterestCost,
                _calc.Position.Capital
                ).OrderByDescending(o => o.PriceChangePercentage).ToList();

            if (pdt != null) dgvPriceDecreaseTable.DataSource = pdt;
        }

        private void frmRRC_Long_Load(object sender, EventArgs e)
        {
            // Todo: rename to InitStateDependentComponents then change to public. Place in the constructor.
            callOnLoad();
        }


        private void btnPriceIncrease_custom_Click(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = InputConverter.Decimal(txtPriceIncrease_target.Text);
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc_serv.PriceIncreaseTable.GeneratePriceIncreaseRecord(
                priceTarget,
                _calc.Position.EntryPriceAvg,
                _calc.Position.LotSize,
                _calc.Borrow.InterestCost,
                _calc.Position.Capital);

            // 4
            if (rec == null) return;
            txtPriceIncreasePercentage.Text = rec.PriceChangePercentage.ToString(Constant.PERCENTAGE_FORMAT);
            txtPriceIncrease_profit.Text = rec.PnL.ToString(Constant.MONEY_FORMAT);
            txtProfitPercentage.Text = rec.PnL_Percentage.ToString(Constant.PERCENTAGE_FORMAT);
        }

        private void btnPriceDecrease_custom_Click(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = InputConverter.Decimal(txtPriceDecrease_target.Text);
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc_serv.PriceDecreaseTable.GeneratePriceDecreaseRecord(
                priceTarget,
                _calc.Position.EntryPriceAvg,
                _calc.Position.LotSize,
                _calc.Borrow.InterestCost,
                _calc.Position.Capital);

            // 4
            if (rec == null) return;
            txtPriceDecreasePercentage.Text = rec.PriceChangePercentage.ToString(Constant.PERCENTAGE_FORMAT);
            txtPriceDecrease_loss.Text = rec.PnL.ToString(Constant.MONEY_FORMAT);
            txtLossPercentage.Text = rec.PnL_Percentage.ToString(Constant.PERCENTAGE_FORMAT);
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

        private void button1_Click(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = InputConverter.Decimal(txtPEP_ExitPrice.Text);
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc_serv.PriceIncreaseTable.GeneratePriceIncreaseRecord(
                priceTarget,
                _calc.Position.EntryPriceAvg,
                _calc.Position.LotSize,
                _calc.Borrow.InterestCost,
                _calc.Position.Capital);

            // 4
            if (rec == null) return;
            txtPEP_sPV.Text = _calc.GetSpeculativePositionValue(priceTarget).ToString(Constant.MONEY_FORMAT);
            txtPEP_AccountEquity.Text = _calc.GetSpeculativeAccountEquity(priceTarget).ToString(Constant.MONEY_FORMAT);

            txtPEP_PCP.Text = rec.PriceChangePercentage.ToString(Constant.PERCENTAGE_FORMAT);
            txtPEP_RealProfit_percent.Text = rec.PnL_Percentage.ToString(Constant.PERCENTAGE_FORMAT);
            txtPEP_Profit.Text = rec.PnL.ToString(Constant.MONEY_FORMAT);
            txtPEP_TradingCost.Text = rec.TradingCost.ToString(Constant.MONEY_FORMAT);

            updateRRR();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = InputConverter.Decimal(txtLEP_ExitPrice.Text);
            if (priceTarget <= 0) return;

            // 3
            var rec = _rrc_serv.PriceDecreaseTable.GeneratePriceDecreaseRecord(
                priceTarget,
                _calc.Position.EntryPriceAvg,
                _calc.Position.LotSize,
                _calc.Borrow.InterestCost,
                _calc.Position.Capital);

            // 4
            if (rec == null) return;
            txtLEP_sPV.Text = _calc.GetSpeculativePositionValue(priceTarget).ToString(Constant.MONEY_FORMAT);
            txtLEP_AccountEquity.Text = _calc.GetSpeculativeAccountEquity(priceTarget).ToString(Constant.MONEY_FORMAT);

            txtLEP_PCP.Text = rec.PriceChangePercentage.ToString(Constant.PERCENTAGE_FORMAT);
            txtLEP_RealLoss_percent.Text = rec.PnL_Percentage.ToString(Constant.PERCENTAGE_FORMAT);
            txtLEP_Loss.Text = rec.PnL.ToString(Constant.MONEY_FORMAT);
            txtLEP_TradingCost.Text = rec.TradingCost.ToString(Constant.MONEY_FORMAT);

            updateRRR();
        }

        private void btnSetLEP_Click(object sender, EventArgs e)
        {
            txtLEP_ExitPrice.Text = txtPriceDecrease_target.Text;
            btnLEP_compute.PerformClick();
        }

        private void btnSetPEP_Click(object sender, EventArgs e)
        {
            txtPEP_ExitPrice.Text = txtPriceIncrease_target.Text;
            btnPEP_compute.PerformClick();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (State == RiskRewardCalcState.Deleted) return;
            Save();
        }

        private void captureState()
        {
            CalculatorState.Capital = InputConverter.Decimal(txtCapital.Text);
            CalculatorState.Leverage = InputConverter.Decimal(txtLeverage.Text);
            CalculatorState.EntryPriceAvg = InputConverter.Decimal(txtEntryPrice.Text);
            CalculatorState.DayCount = (int)nudDayCount.Value;
            CalculatorState.DailyInterestRate = nudDailyInterestRate.Value;
            CalculatorState.PriceIncreaseTarget = InputConverter.Decimal(txtPriceIncrease_target.Text);
            CalculatorState.PriceDecreaseTarget = InputConverter.Decimal(txtPriceDecrease_target.Text);
            CalculatorState.PEP_ExitPrice = InputConverter.Decimal(txtPEP_ExitPrice.Text);
            CalculatorState.PEP_Note = txtPEP_Note.Text;
            CalculatorState.LEP_ExitPrice = InputConverter.Decimal(txtLEP_ExitPrice.Text);
            CalculatorState.LEP_Note = txtLEP_Note.Text;
            CalculatorState.Ticker = txtTicker.Text;
            CalculatorState.Strategy = txtStrategy.Text;
            CalculatorState.Note = txtNote.Text;
        }

        private bool Save()
        {
            // Capture state
            // 1
            captureState();

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
                    this.Text = CalculatorState.Ticker;
                    ChangeState(RiskRewardCalcState.Loaded);
                    statusMessage.Text = "State save successfully.";
                }
                else statusMessage.Text = "Saving state failed.";
            }
            else if (State == RiskRewardCalcState.Loaded | State == RiskRewardCalcState.TradeOpen)
            {
                if (o.CalculatorState_Update())
                {
                    this.Text = CalculatorState.Ticker;
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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

        private void TextBox_Integer_Validating(object sender, CancelEventArgs e)
        {
            var tb = (TextBox)sender;
            string msg;

            if (!Format.isInteger(tb.Text, out msg))
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

        private void frmRiskRewardCalc_Long_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Just ignore if the reason for closing was cause by deletion
            if (State == RiskRewardCalcState.Deleted) return;

            // Saving not allowed when Trade is Closed
            if (State == RiskRewardCalcState.TradeClosed) return;

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
                var result = MyMessageBox.Question_YesNo("Confirm save this Trade as Official?", "Trade Officialize");
                if (result == DialogResult.Yes)
                {
                    
                    if (State == RiskRewardCalcState.Empty) btnReCalculate.PerformClick();
                    officializedTrade();
                }
            }
        }

        private bool officializedTrade()
        {
            // Capture state
            // 2 - Process
            captureState();        // calculator state must be captured first
            var c = CalculatorState;
            var p = _calc.Position;
            var oc = _calc.OpeningCost;
            var b = _calc.Borrow;
            this.Trade = new Trade
            {
                Ticker = c.Ticker,
                PositionSide = "long",
                Status = "open",
                DateEnter = DateTime.Now,
                Capital = c.Capital,
                Leverage = c.Leverage,
                LeveragedCapital = p.LeveragedCapital,
                EntryPriceAvg = c.EntryPriceAvg,
                LotSize = p.LotSize,
                OpeningTradingFee = oc.TradingFee,
                OpeningTradingCost = oc.TradingFee,
                BorrowAmount = b.Amount,
                DayCount = b.DayCount,
                DailyInterestRate = b.DailyInterestRate,
                InterestCost = b.InterestCost,
                CalculatorState = this.CalculatorState
            };

            // 3 - Validation - the implementation may be incomplete but suffice for nowInputConverter.MoneyToDecimal
            string msg;
            if (!RiskRewardCalc_Serv.CalculatorState_Validate(this.CalculatorState, out msg) || !Trade_Serv.TradeOpening_Validate(this.Trade, out msg))
            {
                statusMessage.Text = msg;
                MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            // 4 - this will save data into a data store - no need to Display Data 
            var o = (master)this.Owner;
            // Add to the Owner's List
            if (o.Trade_Add(this.Trade))
            {
                txtTradeNum.Text = this.Trade.Id.ToString();
                this.Text = this.Trade.Ticker + " - OPEN";
                ChangeState(RiskRewardCalcState.TradeOpen);
                statusMessage.Text = "Trade has been officialized successfully.";
            }
            else
            {
                statusMessage.Text = "Officializing trade failure";
                MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }


            return true;
        }

        public void MarkAsDeleted()
        {
            ChangeState(RiskRewardCalcState.Deleted);
        }

        private void callOnLoad()
        {
            // State independent controls
            txtOpeningTradingFee_percent.Text = Constant.TRADING_FEE.ToString();

            ChangeState(this.State);
        }
        private void ChangeState(RiskRewardCalcState s)
        {
            State = s;
            switch (State)
            {
                case RiskRewardCalcState.Empty:
                    panelBandTop.BackColor = BandColor.Empty;
                    panelBandBottom.BackColor = BandColor.Empty;

                    CalculatorState = new();
                    // Initalize UI controls
                    txtBorrowAmount.Text = "0";
                    nudDayCount.Value = 1;
                    nudDailyInterestRate.Value = Constant.DAILY_INTEREST_RATE;

                    txtPEP_Note.Text = "take-profit: inactive";
                    txtLEP_Note.Text = "stop-loss: inactive";

                    btnCloseTheTrade.Visible = false;
                    break;

                case RiskRewardCalcState.Loaded:
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
                        txtCapital.Text = CalculatorState.Capital.ToString("0.00");     // dont change format
                        txtLeverage.Text = CalculatorState.Leverage.ToString("0");      // dont change format
                        txtEntryPrice.Text = CalculatorState.EntryPriceAvg.ToString();
                        nudDayCount.Value = CalculatorState.DayCount < nudDayCount.Minimum ? nudDayCount.Minimum : CalculatorState.DayCount;
                        nudDailyInterestRate.Value = CalculatorState.DailyInterestRate < nudDailyInterestRate.Minimum ? nudDailyInterestRate.Minimum : CalculatorState.DailyInterestRate;
                        btnReCalculate.PerformClick();

                        // sys flow 2
                        txtPriceIncrease_target.Text = CalculatorState.PriceIncreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        btnPriceIncrease_custom.PerformClick();
                        txtPriceDecrease_target.Text = CalculatorState.PriceDecreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        btnPriceDecrease_custom.PerformClick();

                        // sys flow 3
                        txtPEP_ExitPrice.Text = CalculatorState.PEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtPEP_Note.Text = CalculatorState.PEP_Note;
                        btnPEP_compute.PerformClick();

                        // sys flow
                        txtLEP_ExitPrice.Text = CalculatorState.LEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtLEP_Note.Text = CalculatorState.LEP_Note;
                        btnLEP_compute.PerformClick();

                        // Independent data
                        this.Text = CalculatorState.Ticker;
                        txtTicker.Text = CalculatorState.Ticker;
                        txtStrategy.Text = CalculatorState.Strategy;
                        txtNote.Text = CalculatorState.Note;

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
                        panelBandTop.BackColor = BandColor.OpenPosition;
                        panelBandBottom.BackColor = BandColor.OpenPosition;
                        panelExitPrice.Visible = true;

                        // load Trade detail
                        // sys flow 1
                        txtCapital.Text = Trade.Capital.ToString("0.00");     // dont change format
                        txtLeverage.Text = Trade.Leverage.ToString("0");      // dont change format
                        txtEntryPrice.Text = Trade.EntryPriceAvg.ToString();
                        //Numeric Up Down control throws exception when assigned value less then their Minimum value
                        decimal d = SafeConvert.ToDecimalSafe((DateTime.Now - Trade.DateEnter).TotalDays);
                        nudDayCount.Value = d < nudDayCount.Minimum ? nudDayCount.Minimum : d;
                        nudDailyInterestRate.Value = Trade.DailyInterestRate < nudDailyInterestRate.Value ? nudDailyInterestRate.Minimum : Trade.DailyInterestRate;
                        btnReCalculate.PerformClick();

                        // sys flow 2
                        txtPriceIncrease_target.Text = CalculatorState.PriceIncreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        btnPriceIncrease_custom.PerformClick();
                        txtPriceDecrease_target.Text = CalculatorState.PriceDecreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        btnPriceDecrease_custom.PerformClick();

                        // sys flow 3
                        txtPEP_ExitPrice.Text = CalculatorState.PEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtPEP_Note.Text = CalculatorState.PEP_Note;
                        btnPEP_compute.PerformClick();

                        // sys flow
                        txtLEP_ExitPrice.Text = CalculatorState.LEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                        txtLEP_Note.Text = CalculatorState.LEP_Note;
                        btnLEP_compute.PerformClick();

                        // Independent data
                        txtTradeNum.Text = Trade.Id.ToString();
                        txtTicker.Text = Trade.Ticker;
                        txtStrategy.Text = CalculatorState.Strategy;
                        txtNote.Text = CalculatorState.Note;

                        //
                        lblHeader.Text = Trade.Ticker + " - OPEN";
                        this.Text = Trade.Ticker + " - OPEN";

                        txtCapital.ReadOnly = true;
                        txtLeverage.ReadOnly = true;
                        txtEntryPrice.ReadOnly = true;
                        txtTicker.ReadOnly = true;

                        
                        btnDelete.Visible = false;
                        btnOfficializedTrade.Enabled = false;
                        btnOfficializedTrade.Visible = false;
                        btnCloseTheTrade.Visible = true;
                    }
                    break;

                case RiskRewardCalcState.TradeClosed:
                    

                    panelBandTop.BackColor = BandColor.ClosedPosition;
                    panelBandBottom.BackColor = BandColor.ClosedPosition;

                    // load trade details
                    // sys flow 1
                    txtCapital.Text = Trade.Capital.ToString(Constant.MONEY_FORMAT);     // dont change format
                    txtLeverage.Text = Trade.Leverage.ToString("0");      // dont change format
                    txtEntryPrice.Text = Trade.EntryPriceAvg.ToString();
                    //txtLotSize.Text = Trade.LotSize.ToString();
                    //txtLeveragedCapital.Text = Trade.LeveragedCapital.ToString(Constant.MONEY_FORMAT);
                    //txtOpeningTradingFee_dollar.Text = Trade.OpeningTradingCost.ToString(Constant.MONEY_FORMAT);
                    //txtOpeningTradingCost.Text = Trade.OpeningTradingCost.ToString(Constant.MONEY_FORMAT);
                    //Numeric Up Down control throws exception when assigned value less then their Minimum value
                    nudDayCount.Value = Trade.DayCount < nudDayCount.Minimum ? nudDayCount.Minimum : Trade.DayCount;
                    nudDailyInterestRate.Value = Trade.DailyInterestRate < nudDailyInterestRate.Value ? nudDailyInterestRate.Minimum : Trade.DailyInterestRate;
                    btnReCalculate.PerformClick();

                    // sys flow 2
                    txtPriceIncrease_target.Text = CalculatorState.PriceIncreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    btnPriceIncrease_custom.PerformClick();
                    txtPriceDecrease_target.Text = CalculatorState.PriceDecreaseTarget.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    btnPriceDecrease_custom.PerformClick();

                    // sys flow 3
                    txtPEP_ExitPrice.Text = CalculatorState.PEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    txtPEP_Note.Text = CalculatorState.PEP_Note;
                    btnPEP_compute.PerformClick();

                    // sys flow
                    txtLEP_ExitPrice.Text = CalculatorState.LEP_ExitPrice.ToString(Constant.MAX_DECIMAL_PLACE_FORMAT);
                    txtLEP_Note.Text = CalculatorState.LEP_Note;
                    btnLEP_compute.PerformClick();

                    // Independent data
                    txtTradeNum.Text = Trade.Id.ToString();
                    txtTicker.Text = Trade.Ticker;
                    txtStrategy.Text = CalculatorState.Strategy;
                    txtNote.Text = CalculatorState.Note;


                    //
                    lblHeader.Text = Trade.Ticker + " - CLOSED"; ;
                    lblHeader.ForeColor = Color.LightSteelBlue;
                    this.Text = Trade.Ticker + " - CLOSED"; ;

                    // same as trade open
                    txtCapital.ReadOnly = true;
                    txtLeverage.ReadOnly = true;
                    txtEntryPrice.ReadOnly = true;
                    txtTicker.ReadOnly = true;
                    nudDayCount.ReadOnly = true;
                    nudDailyInterestRate.ReadOnly = true;

                    btnDelete.Visible = false;
                    btnOfficializedTrade.Enabled = false;
                    btnOfficializedTrade.Visible = false;

                    // trade close
                    txtPEP_ExitPrice.ReadOnly = true;
                    txtLEP_ExitPrice.ReadOnly = true;
                    btnSetPEP.Visible = false;
                    btnSetLEP.Visible = false;
                    panelExitPrice.Enabled = false;
                    btnReCalculate.Visible = false;
                    btnSave.Visible = false;
                    btnCloseTheTrade.Visible = false;
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
            if (State == RiskRewardCalcState.Deleted) return;

            var result = MyMessageBox.Question_YesNo("Confirm to close this Trade?", "Trade Closing");
            if (result == DialogResult.Yes)
            {
                // 1-2 - Input and Sanitize
                decimal exitPrice = radPEP_ExitPrice.Checked ? StringToNumeric.MoneyToDecimal(txtPEP_ExitPrice.Text) : StringToNumeric.MoneyToDecimal(txtLEP_ExitPrice.Text);

                // 3 - Process
                btnReCalculate.PerformClick();
                btnPEP_compute.PerformClick();
                btnLEP_compute.PerformClick();

                var rec = PriceChangeTable.GeneratePriceChangeRecord(
               exitPrice,
               _calc.Position.EntryPriceAvg,
               _calc.Position.LotSize,
               _calc.Borrow.InterestCost,
               _calc.Position.Capital);

                // Collect - Trade Closing Data
                //update borrow cost
                Trade.DayCount = _calc.Borrow.DayCount;
                Trade.DailyInterestRate = _calc.Borrow.DailyInterestRate;
                Trade.InterestCost = _calc.Borrow.InterestCost;

                Trade.DateExit = DateTime.Now;
                Trade.ExitPriceAvg = exitPrice;
                Trade.ClosingTradingFee = PriceChangeTable.SpeculativeTradingFee(exitPrice, Trade.LotSize);
                Trade.ClosingTradingCost = rec.TradingCost;
                Trade.PnL = rec.PnL;
                Trade.Status = "closed";

                
                // Validation
                string msg;
                if (!Trade_Serv.TradeClosing_Validate(Trade, out msg))
                {
                    statusMessage.Text = msg;
                    MyMessageBox.Error(msg);
                    return;
                }

                // 4 - display/store
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
    }
}
