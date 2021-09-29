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

namespace TradingTools
{
    public partial class frmRRC_Long : Form
    {
        private RiskRewardCalc_Long _RR_Calc = new();
        private CalculationDetails _calc = new();

        public frmRRC_Long()
        {
            InitializeComponent();

            // Initalize UI controls
            txtOpeningTradingFee_percent.Text = Constant.TRADING_FEE.ToString();
            txtBorrowAmount.Text = "0";
            nudDayCount.Value = 1;
            nudDailyInterestRate.Value = Constant.DAILY_INTEREST_RATE;
        }

        private void btnReCalculate_Click(object sender, EventArgs e)
        {
            
            // step 2: Collect data from Receptors
            _calc.Position.Capital = Convert.ToDecimal(txtCapital.Text);
            _calc.Position.Leverage = Convert.ToInt32(txtLeverage.Text);
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
            dgvPriceIncreaseTable.DataSource = _RR_Calc.PriceIncreaseTable.GenerateTable(
                _calc.Position.EntryPriceAvg, 
                _calc.Position.LotSize,
                _calc.Borrow.InterestCost,
                _calc.Position.Capital
                ).OrderByDescending(o => o.PriceChangePercentage).ToList();

            dgvPriceDecreaseTable.DataSource = _RR_Calc.PriceDecreaseTable.GenerateTable(
                _calc.Position.EntryPriceAvg,
                _calc.Position.LotSize,
                _calc.Borrow.InterestCost,
                _calc.Position.Capital
                ).OrderByDescending(o => o.PriceChangePercentage).ToList();


        }

        private void frmRRC_Long_Load(object sender, EventArgs e)
        {
            
        }

        private void btnPriceIncrease_custom_Click(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = Convert.ToDecimal(txtPriceIncrease_target.Text);
            //_position.EntryPriceAvg = Convert.ToDecimal(txtEntryPrice.Text);

            // 3
            var rec = _RR_Calc.PriceIncreaseTable.GeneratePriceIncreaseRecord(
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
            var rec = _RR_Calc.PriceDecreaseTable.GeneratePriceDecreaseRecord(
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
            var rec = _RR_Calc.PriceIncreaseTable.GeneratePriceIncreaseRecord(
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
            var rec = _RR_Calc.PriceDecreaseTable.GeneratePriceDecreaseRecord(
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
            // This object must maintain one instance of CalculatorState
            var calc = new CalculatorState()
            {
                Capital = _calc.Position.Capital,
                Leverage = _calc.Position.Leverage,
                EntryPriceAvg = _calc.Position.EntryPriceAvg,
                DayCount = _calc.Borrow.DayCount,
                DailyInterestRate = _calc.Borrow.DailyInterestRate,
                PriceIncreaseTarget = Convert.ToDecimal(txtPriceIncrease_target.Text),
                PriceDecreaseTarget = Convert.ToDecimal(txtPriceDecrease_target.Text),
                PEP_ExitPrice = Convert.ToDecimal(txtPEP_ExitPrice.Text),
                PEP_Note = txtPEP_Note.Text,
                LEP_ExitPrice = Convert.ToDecimal(txtLEP_ExitPrice.Text),
                LEP_Note = txtLEP_Note.Text,
                Ticker = txtTicker.Text,
                Strategy = txtStrategy.Text,
                Note = txtNote.Text
            };

            _RR_Calc.SaveState(calc);
        }
    }
}
