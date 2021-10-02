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
            if (string.IsNullOrEmpty(txtCapital.Text) || string.IsNullOrEmpty(txtLeverage.Text) || string.IsNullOrEmpty(txtEntryPrice.Text))
            {
                return false;
            }

            return true;
        }
        private void btnReCalculate_Click(object sender, EventArgs e)
        {
            //if (!ValidateChildren(ValidationConstraints.Enabled))
            //{
            //    statusMessage.Text = "Please fill-up the required data";
            //    MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}

            if (!calculate_main_validated())
            {
                statusMessage.Text = "Please fill-up the required data";
                MessageBox.Show(statusMessage.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            
            // step 2: Collect data from Receptors
            _calc.Position.Capital = Convert.ToDecimal(txtCapital.Text);
            _calc.Position.Leverage = Convert.ToDecimal(txtLeverage.Text);
            _calc.Position.EntryPriceAvg = Convert.ToDecimal(txtEntryPrice.Text);


            // step 3: Process Data collected including others supporting data
            _calc.OpeningCost.TradingFee = TradingCost.GetTradingFee_in_dollar(_calc.Position.LeveragedCapital);
            _calc.Position.InitialPositionValue = _calc.Position.LeveragedCapital - _calc.OpeningCost.TradingFee;
            
            _calc.Borrow.Amount = _calc.Position.LeveragedCapital - _calc.Position.Capital;
            _calc.Borrow.DayCount = (int)nudDayCount.Value;
            _calc.Borrow.DailyInterestRate = nudDailyInterestRate.Value;


            // step 4: Represent data back to UI
            txtLotSize.Text = _calc.Position.LotSize.ToString();
            txtLeveragedCapital.Text = _calc.Position.LeveragedCapital.ToString();
            txtInitalPositionValue.Text = _calc.Position.InitialPositionValue.ToString();
            txtOpeningTradingFee_dollar.Text = _calc.OpeningCost.TradingFee.ToString();
            txtOpeningTradingCost.Text = _calc.OpeningCost.TradingFee.ToString();

            txtBorrowAmount.Text = _calc.Borrow.Amount.ToString();
            txtInterestCost.Text = _calc.Borrow.InterestCost.ToString();
            txtPEP_AccountEquity.Text = (_calc.Position.InitialPositionValue - _calc.Borrow.Amount).ToString();


            //Closing Position
            dgvPriceIncreaseTable.DataSource = _rrc_serv.PriceIncreaseTable.GenerateTable(
                _calc.Position.EntryPriceAvg, 
                _calc.Position.LotSize,
                _calc.Borrow.InterestCost,
                _calc.Position.Capital
                ).OrderByDescending(o => o.PriceChangePercentage).ToList();

            dgvPriceDecreaseTable.DataSource = _rrc_serv.PriceDecreaseTable.GenerateTable(
                _calc.Position.EntryPriceAvg,
                _calc.Position.LotSize,
                _calc.Borrow.InterestCost,
                _calc.Position.Capital
                ).OrderByDescending(o => o.PriceChangePercentage).ToList();


        }

        private void frmRRC_Long_Load(object sender, EventArgs e)
        {
            callOnLoad();
        }

        private void callOnLoad()
        {
            // State independent controls
            txtOpeningTradingFee_percent.Text = Constant.TRADING_FEE.ToString();

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
                    txtCapital.Text = CalculatorState.Capital.ToString();
                    txtLeverage.Text = CalculatorState.Leverage.ToString();
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
            decimal priceTarget = Convert.ToDecimal(txtPriceIncrease_target.Text);
            //_position.EntryPriceAvg = Convert.ToDecimal(txtEntryPrice.Text);

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
            decimal priceTarget = Convert.ToDecimal(txtPriceDecrease_target.Text);
            //_position.EntryPriceAvg = Convert.ToDecimal(txtEntryPrice.Text);

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
            txtRRR.Text = "1 / " + (rrr == null ? "1" : rrr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = Convert.ToDecimal(txtPEP_ExitPrice.Text);

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
            decimal priceTarget = Convert.ToDecimal(txtLEP_ExitPrice.Text);

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
            // Todo: Validate First - make sure its clean before calling SaveState
            decimal def_out;
            // Capture state
            CalculatorState.Capital = _calc.Position.Capital;
            CalculatorState.Leverage = _calc.Position.Leverage;
            CalculatorState.EntryPriceAvg = _calc.Position.EntryPriceAvg;
            CalculatorState.DayCount = _calc.Borrow.DayCount;
            CalculatorState.DailyInterestRate = _calc.Borrow.DailyInterestRate;
            CalculatorState.PriceIncreaseTarget = Convert.ToDecimal(txtPriceIncrease_target.Text);
            CalculatorState.PriceDecreaseTarget = Convert.ToDecimal(txtPriceDecrease_target.Text);
            CalculatorState.PEP_ExitPrice = Convert.ToDecimal(txtPEP_ExitPrice.Text);
            CalculatorState.PEP_Note = txtPEP_Note.Text;
            CalculatorState.LEP_ExitPrice = Convert.ToDecimal(txtLEP_ExitPrice.Text);
            CalculatorState.LEP_Note = txtLEP_Note.Text;
            CalculatorState.Ticker = txtTicker.Text;
            CalculatorState.Strategy = txtStrategy.Text;
            CalculatorState.Note = txtNote.Text;

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
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
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

            if(!Format.isMoney(tb.Text, out msg))
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
            if (AutoValidate != AutoValidate.Disable)
            {
                AutoValidate = AutoValidate.Disable;
                this.Close();

                //DialogResult objDialog = MessageBox.Show("Some data are missing. \n\nAre you sure you want to close this form?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //if (objDialog == DialogResult.Yes)
                //{
                //    AutoValidate = AutoValidate.Disable;
                //    this.Close();
                //}
            }
            
        }
    }
}
