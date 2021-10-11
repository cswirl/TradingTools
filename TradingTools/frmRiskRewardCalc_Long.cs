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

        public RiskRewardCalcState State { get; set; } = RiskRewardCalcState.Empty;

        public CalculatorState CalculatorState { get; set; }

        public frmRiskRewardCalc_Long()
        {
            InitializeComponent();

            panelBandTop.Height = 3;
            panelBandBottom.Height = 3;
            // timer
            timer1.Interval = Presentation.INTERNAL_TIMER_REFRESH_VALUE;
        }

        private void btnReCalculate_Click(object sender, EventArgs e)
        {
            // _calc.Calculate() internal do the step 2 and 3
            // step 2: Collect data -Receptors
            // step 2-B: Validation 
            // step 3: Process Data collected including others supporting data
            string msg;
            var r = _calc.Calculate(
                InputConverter.Decimal(txtCapital.Text),
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

        private bool callOnLoad()
        {
            // State independent controls
            txtOpeningTradingFee_percent.Text = Constant.TRADING_FEE.ToString();

            setBand();
            // State Implementations
            if (State == RiskRewardCalcState.Empty)
            {
                CalculatorState = new();

                // Initalize UI controls
                txtBorrowAmount.Text = Constant.MONEY_FORMAT;
                nudDayCount.Value = 1;
                nudDailyInterestRate.Value = Constant.DAILY_INTEREST_RATE;

                txtPEP_Note.Text = "take-profit: inactive";
                txtLEP_Note.Text = "stop-loss: inactive";
            }
            else if (State == RiskRewardCalcState.Loaded)
            {
                if (CalculatorState == null)
                {
                    statusMessage.Text = "CalculatorState instance was not forwarded.";
                    MessageBox.Show(statusMessage.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.Close();
                    return false;
                }
                else
                {
                    // sys flow 1
                    txtCapital.Text = CalculatorState.Capital.ToString("0.00");     // dont change format
                    txtLeverage.Text = CalculatorState.Leverage.ToString("0");      // dont change format
                    txtEntryPrice.Text = CalculatorState.EntryPriceAvg.ToString();
                    nudDayCount.Value = CalculatorState.DayCount;
                    nudDailyInterestRate.Value = CalculatorState.DailyInterestRate;
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
                    txtTicker.Text = CalculatorState.Ticker;
                    txtStrategy.Text = CalculatorState.Strategy;
                    txtNote.Text = CalculatorState.Note;

                }
            }

            return true;
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
            decimal profit = StringToNumeric.Decimal(txtPEP_Profit.Text);
            decimal loss = StringToNumeric.Decimal(txtLEP_Loss.Text);
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
            Save();
        }

        private void captureState()
        {
            CalculatorState.Capital = InputConverter.Decimal(txtCapital.Text);
            CalculatorState.Leverage = InputConverter.Decimal(txtLeverage.Text);
            CalculatorState.EntryPriceAvg = InputConverter.Decimal(txtEntryPrice.Text);
            CalculatorState.DayCount = (int)nudDayCount.Value <= 0 ? 1 : (int)nudDayCount.Value;
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

            // 2-B Validation - the implementation may be incomplete but suffice for nowInputConverter.Decimal
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
                    State = RiskRewardCalcState.Loaded;
                    this.Text = CalculatorState.Ticker;
                    setBand();
                    statusMessage.Text = "State save successfully.";
                }
                else statusMessage.Text = "Saving state failed.";
            }
            else if (State == RiskRewardCalcState.Loaded)
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

        private void setBand()
        {
            switch (State)
            {
                case RiskRewardCalcState.Empty:
                    panelBandTop.BackColor = BandColor.Empty;
                    panelBandBottom.BackColor = BandColor.Empty;
                    break;

                case RiskRewardCalcState.Loaded:
                    panelBandTop.BackColor = BandColor.Loaded;
                    panelBandBottom.BackColor = BandColor.Loaded;
                    break;

                case RiskRewardCalcState.TradeOpen:
                    panelBandTop.BackColor = BandColor.OpenPosition;
                    panelBandBottom.BackColor = BandColor.OpenPosition;
                    break;

                case RiskRewardCalcState.TradeClosed:
                    panelBandTop.BackColor = BandColor.ClosedPosition;
                    panelBandBottom.BackColor = BandColor.ClosedPosition;
                    break;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (State == RiskRewardCalcState.Empty) return;
            // Deletion not allowed when Trade was Officialized
            if (State == RiskRewardCalcState.TradeOpen) return;
            if (State == RiskRewardCalcState.TradeClosed) return;

            DialogResult objDialog = MessageBox.Show("Are you sure you want to DELETE this State", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (objDialog == DialogResult.Yes)
            {
                var o = (master)this.Owner;
                // Remove from the Owner's List
                if (o.CalculatorState_Delete(CalculatorState))
                {
                    State = RiskRewardCalcState.Deleted;
                    statusMessage.Text = "State was deleted successfully. \n\nThis form will now close.";
                    MessageBox.Show(statusMessage.Text, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // just close the form
                    this.Close();
                }
                else
                {
                    statusMessage.Text = "Deleting state failed.";
                    MessageBox.Show(statusMessage.Text, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            setBand();
            statusMessage.Text = "Status message . . .";
        }

        private void btnOfficializedTrade_Click(object sender, EventArgs e)
        {
            if (State == RiskRewardCalcState.Empty | State == RiskRewardCalcState.Loaded)
            {
                officializedTrade();
            }
            else if (State == RiskRewardCalcState.TradeClosed)
            {

                //else if (State == RiskRewardCalcState.TradeOpen)
                //{
                //    if (o.Trade_ClosePosition(CalculatorState))
                //    {
                //        statusMessage.Text = "Trade is marked as closed.";
                //    }
                //    else statusMessage.Text = "Marking trade as closed failed.";
                //}
            }
        }

        private bool officializedTrade()
        {
            // Capture state
            // 1
            captureState();        // calculator state must be captured first
            var c = CalculatorState;
            var p = _calc.Position;
            var oc = _calc.OpeningCost;
            var b = _calc.Borrow;
            var t = new Trade
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

            // 2-B Validation - the implementation may be incomplete but suffice for nowInputConverter.Decimal
            string msg;
            var _trade_service = new Trade_Serv();
            if (!_trade_service.Trade_Validate(t, out msg) | !RiskRewardCalc_Serv.CalculatorState_Validate(this.CalculatorState, out msg))
            {
                statusMessage.Text = msg;
                MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            // 3 - process data collected - this will save data into a data store - no need for step 4 which is to Display Data 
            var o = (master)this.Owner;
            // Add to the Owner's List
            if (o.Trade_Add(t))
            {
                State = RiskRewardCalcState.TradeOpen;
                this.Text = CalculatorState.Ticker;
                setBand();
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
    }
}
