using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        }

        private bool calculate_main_validated()
        {
            //// Empty string
            //if (string.IsNullOrEmpty(txtCapital.Text) || string.IsNullOrEmpty(txtLeverage.Text) || string.IsNullOrEmpty(txtEntryPrice.Text))
            //{
            //    statusMessage.Text = "Please fill-up the required data";
            //    MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            // Minimum Value
            decimal capital = InputConverter.Decimal(txtCapital.Text);
            decimal leverage = InputConverter.Decimal(txtLeverage.Text);
            decimal entryPrice = InputConverter.Decimal(txtEntryPrice.Text);
            if (capital <= 10 || leverage < 1 || entryPrice <= 0)
            {
                statusMessage.Text = "Invalid input data";
                MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }
        private void btnReCalculate_Click(object sender, EventArgs e)
        {
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
            txtLotSize.Text = _calc.Position.LotSize.ToString();
            txtLeveragedCapital.Text = _calc.Position.LeveragedCapital.ToString();
            txtInitalPositionValue.Text = _calc.Position.InitialPositionValue.ToString();
            txtOpeningTradingFee_dollar.Text = _calc.OpeningCost.TradingFee.ToString();
            txtOpeningTradingCost.Text = _calc.OpeningCost.TradingFee.ToString();

            txtBorrowAmount.Text = _calc.Borrow.Amount.ToString();
            txtInterestCost.Text = _calc.Borrow.InterestCost.ToString();


            //Closing Position
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
            callOnLoad();
        }

        private void callOnLoad()
        {
            // State independent controls
            txtOpeningTradingFee_percent.Text = Constant.TRADING_FEE.ToString();


            // State Implementations
            if (State == RiskRewardCalcState.Empty)
            {
                CalculatorState = new();

                // Initalize UI controls
                txtBorrowAmount.Text = "0";
                nudDayCount.Value = 1;
                nudDailyInterestRate.Value = Constant.DAILY_INTEREST_RATE;
            }
            else if (State == RiskRewardCalcState.Loaded)
            {
                if (CalculatorState == null)
                {
                    statusMessage.Text = "CalculatorState instance was not forwarded.";
                    MessageBox.Show(statusMessage.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.Close();
                    return;
                }
                else
                {
                    // sys flow 1
                    txtCapital.Text = CalculatorState.Capital.ToString("0.00");
                    txtLeverage.Text = CalculatorState.Leverage.ToString("0");
                    txtEntryPrice.Text = CalculatorState.EntryPriceAvg.ToString();
                    nudDayCount.Value = CalculatorState.DayCount;
                    nudDailyInterestRate.Value = CalculatorState.DailyInterestRate;
                    btnReCalculate.PerformClick();

                    // sys flow 2
                    txtPriceIncrease_target.Text = CalculatorState.PriceIncreaseTarget.ToString();
                    btnPriceIncrease_custom.PerformClick();
                    txtPriceDecrease_target.Text = CalculatorState.PriceDecreaseTarget.ToString();
                    btnPriceDecrease_custom.PerformClick();

                    // sys flow 3
                    txtPEP_ExitPrice.Text = CalculatorState.PEP_ExitPrice.ToString();
                    txtPEP_Note.Text = CalculatorState.PEP_Note;
                    btnPEP_compute.PerformClick();

                    // sys flow
                    txtLEP_ExitPrice.Text = CalculatorState.LEP_ExitPrice.ToString();
                    txtLEP_Note.Text = CalculatorState.LEP_Note;
                    btnLEP_compute.PerformClick();

                    // Independent data
                    txtTicker.Text = CalculatorState.Ticker;
                    txtStrategy.Text = CalculatorState.Strategy;
                    txtNote.Text = CalculatorState.Note;

                }
            }
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
            txtPriceIncreasePercentage.Text = rec.PriceChangePercentage.ToString();
            txtPriceIncrease_profit.Text = rec.PnL.ToString();
            txtProfitPercentage.Text = rec.PnL_Percentage.ToString();
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
            txtPriceDecreasePercentage.Text = rec.PriceChangePercentage.ToString();
            txtPriceDecrease_loss.Text = rec.PnL.ToString();
            txtLossPercentage.Text = rec.PnL_Percentage.ToString();
        }

        private void updateRRR()
        {
            decimal profit;
            decimal loss;
            decimal? rrr = null;
            if (Decimal.TryParse(txtPEP_Profit.Text, out profit) && Decimal.TryParse(txtLEP_Loss.Text, out loss))
            {
                rrr = profit / Math.Abs(loss);
            }

            // Display RRR
            txtRRR.Text = "1 / " + (rrr == null ? "1" : rrr?.ToString("0.00"));
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
            txtPEP_sPV.Text = _calc.GetSpeculativePositionValue(priceTarget).ToString();
            txtPEP_AccountEquity.Text = _calc.GetSpeculativeAccountEquity(priceTarget).ToString();

            txtPEP_PCP.Text = rec.PriceChangePercentage.ToString();
            txtPEP_RealProfit_percent.Text = rec.PnL_Percentage.ToString();
            txtPEP_Profit.Text = rec.PnL.ToString();
            txtPEP_TradingCost.Text = rec.TradingCost.ToString();

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
            txtLEP_sPV.Text = _calc.GetSpeculativePositionValue(priceTarget).ToString();
            txtLEP_AccountEquity.Text = _calc.GetSpeculativeAccountEquity(priceTarget).ToString();

            txtLEP_PCP.Text = rec.PriceChangePercentage.ToString();
            txtLEP_RealLoss_percent.Text = rec.PnL_Percentage.ToString();
            txtLEP_Loss.Text = rec.PnL.ToString();
            txtLEP_TradingCost.Text = rec.TradingCost.ToString();

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

        private bool Save()
        {
            // Capture state
            // 1
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

            // 2-B Validation - the implementation may be incomplete but suffice for nowInputConverter.Decimal
            string msg;
            if (!_rrc_serv.CalculatorState_Validate(this.CalculatorState, out msg))
            {
                statusMessage.Text = msg;
                MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            // 3 - process data collected - this will save data into a data store - no need for step 4 which is to Display Data 
            var o = (frmCalculatorStates)this.Owner;
            if (State == RiskRewardCalcState.Empty)
            {
                // Add to the Owner's List
                if (o.CalculatorState_Add(CalculatorState))
                {
                    State = RiskRewardCalcState.Loaded;
                    statusMessage.Text = "State save successfully.";
                }
                else statusMessage.Text = "Saving state failed.";
            }
            else if (State == RiskRewardCalcState.Loaded)
            {
                if (o.CalculatorState_Update())
                {
                    statusMessage.Text = "State updated successfully.";
                }
                else statusMessage.Text = "Updating state failed.";
            }

            return true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (State == RiskRewardCalcState.Empty) return;

            DialogResult objDialog = MessageBox.Show("Are you sure you want to DELETE this State", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (objDialog == DialogResult.Yes)
            {
                var o = (frmCalculatorStates)this.Owner;
                // Remove from the Owner's List
                if (o.CalculatorState_Delete(CalculatorState))
                {
                    statusMessage.Text = "State was deleted successfully. \n\nThis form will now close.";
                    MessageBox.Show(statusMessage.Text, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetFromEmpty();
                }
                else
                {
                    statusMessage.Text = "Deleting state failed.";
                    MessageBox.Show(statusMessage.Text, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void resetFromEmpty()
        {
            // just close the form
            this.Close();
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

        private void frmRiskRewardCalc_Long_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult objDialog = MessageBox.Show("Do you want to save before closing ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
    }
}
