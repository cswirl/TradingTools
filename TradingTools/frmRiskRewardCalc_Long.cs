using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Services;
using TradingTools.Trunk;

namespace TradingTools
{
    public partial class frmRRC_Long : Form
    {
        private RiskRewardCalc_Long _RR_Calc = new();
        private Position _position = new();
        private TradingCost _openingCost = new();
        private Borrow _borrow = new();

        public frmRRC_Long()
        {
            InitializeComponent();
        }

        private void btnReCalculate_Click(object sender, EventArgs e)
        {
            
            // step 2: Collect data from Receptors
            _position.Capital = Convert.ToDecimal(txtCapital.Text);
            _position.EntryPriceAvg = Convert.ToDecimal(txtEntryPrice.Text);
            _position.LotSize = Convert.ToDecimal(txtLotSize.Text);


            // step 3: Process Data collected including others supporting data
            _position.InitialPositionValue = _position.EntryPriceAvg * _position.LotSize;
            _borrow.Amount = _position.InitialPositionValue - _position.Capital;
            _borrow.InterestCost = _borrow.Amount * nudDailyInterestRate.Value * nudDayCount.Value;
            _openingCost.TotalTradingCost = _openingCost.GetTotalTradingCost(_position.Capital);

            // step 4: Represent data back to UI
            txtInitalPositionValue.Text = _position.InitialPositionValue.ToString();
            txtTradingFee_dollar.Text = _openingCost.GetTradingFee_in_dollar(_position.Capital).ToString();
            txtTotalTradingCost_dollar.Text = _openingCost.TotalTradingCost.ToString();

            txtLeverage.Text = (_position.InitialPositionValue / _position.Capital).ToString();
            txtBorrowAmount.Text = _borrow.Amount.ToString();
            txtInterestCost.Text = _borrow.InterestCost.ToString();
            txtAccountEquity.Text = (_position.InitialPositionValue - _borrow.Amount).ToString();


            //Closing Position
            dgvPriceIncreaseTable.DataSource = _RR_Calc.PriceIncreaseTable.GenerateTable(
                _position.EntryPriceAvg, 
                _position.LotSize,
                _borrow.InterestCost
                ).OrderByDescending(o => o.PriceChangePercentage).ToList();

            dgvPriceDecreaseTable.DataSource = _RR_Calc.PriceDecreaseTable.GenerateTable(
                _position.EntryPriceAvg,
                _position.LotSize,
                _borrow.InterestCost
                ).OrderByDescending(o => o.PriceChangePercentage).ToList();


        }

        private void frmRRC_Long_Load(object sender, EventArgs e)
        {
            // Initalize UI controls
            txtTradingFee_percent.Text = Constant.TRADING_FEE.ToString();
            txtBorrowAmount.Text = "0";
            nudDayCount.Value = 1;
            nudDailyInterestRate.Value = Constant.DAILY_INTEREST_RATE;
        }

        private void btnPriceIncrease_custom_Click(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = Convert.ToDecimal(txtPriceIncrease_target.Text);
            //_position.EntryPriceAvg = Convert.ToDecimal(txtEntryPrice.Text);

            // 3
            var rec = _RR_Calc.PriceIncreaseTable.GeneratePriceIncreaseRecord(
                priceTarget, _position.EntryPriceAvg, 
                _position.LotSize, _borrow.InterestCost);

            // 4
            if (rec == null) return;
            txtPriceIncreasePercentage.Text = rec.PriceChangePercentage.ToString();
            txtPriceIncrease_profit.Text = rec.PnL.ToString();
    
        }

        private void btnPriceDecrease_custom_Click(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = Convert.ToDecimal(txtPriceDecrease_target.Text);
            //_position.EntryPriceAvg = Convert.ToDecimal(txtEntryPrice.Text);

            // 3
            var rec = _RR_Calc.PriceDecreaseTable.GeneratePriceDecreaseRecord(
                priceTarget, _position.EntryPriceAvg, 
                _position.LotSize, _borrow.InterestCost);

            // 4
            if (rec == null) return;
            txtPriceDecreasePercentage.Text = rec.PriceChangePercentage.ToString();
            txtPriceDecrease_loss.Text = rec.PnL.ToString();
        }
    }
}
