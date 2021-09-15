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
        private TradingCost _tradingCost = new();
        private Borrow _borrow = new();

        public frmRRC_Long()
        {
            InitializeComponent();
        }

        private void btnRefreshTables_Click(object sender, EventArgs e)
        {
            
            // step 2: Collect data from Receptors
            _position.Capital = Convert.ToDecimal(txtCapital.Text);
            _position.EntryPriceAvg = Convert.ToDecimal(txtEntryPrice.Text);
            _position.LotSize = Convert.ToDecimal(txtLotSize.Text);


            // step 3: Process Data collected including others supporting data
            _position.PositionValue = _position.EntryPriceAvg * _position.LotSize;
            _tradingCost.TotalTradingCost = _tradingCost.GetTotalTradingCost(_position.PositionValue, 0); // 0.16 is for testing only
            //_tradingCost.TCPVR = _tradingCost.TotalTradingCost / _position.PositionValue;                 This is removed in the program


            // step 4: Represent data back to UI
            txtPositionValue.Text = _position.PositionValue.ToString();
            txtTradingFee_dollar.Text = _tradingCost.GetTradingFee_in_dollar(_position.PositionValue).ToString();
            txtTotalTradingCost_dollar.Text = _tradingCost.TotalTradingCost.ToString();

            
            dgvPriceIncreaseTable.DataSource = _RR_Calc.PriceIncreaseTable.GenerateTable(
                _position.EntryPriceAvg, 
                _position.PositionValue, 
                _tradingCost.TotalTradingCost
                ).OrderByDescending(o => o.PriceIncreasePercentage).ToList();

            dgvPriceDecreaseTable.DataSource = _RR_Calc.PriceDecreaseTable.GenerateTable(
                _position.EntryPriceAvg,
                _position.PositionValue,
                _tradingCost.TotalTradingCost
                );


        }

        private void frmRRC_Long_Load(object sender, EventArgs e)
        {
            //
            txtTradingFee_percent.Text = Constant.TRADING_FEE.ToString();
        }

        private void btnPriceIncrease_custom_Click(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = Convert.ToDecimal(txtPriceIncrease_target.Text);
            //_position.EntryPriceAvg = Convert.ToDecimal(txtEntryPrice.Text);

            // 3
            var rec = _RR_Calc.PriceIncreaseTable.GeneratePriceIncreaseRecord(priceTarget, _position.EntryPriceAvg, _position.PositionValue, _tradingCost.TotalTradingCost);

            // 4
            if (rec == null) return;
            txtPriceIncreasePercentage.Text = rec.PriceIncreasePercentage.ToString();
            txtPriceIncrease_profit.Text = rec.Profit.ToString();
    
        }

        private void btnPriceDecrease_custom_Click(object sender, EventArgs e)
        {
            // 2
            decimal priceTarget = Convert.ToDecimal(txtPriceDecrease_target.Text);
            //_position.EntryPriceAvg = Convert.ToDecimal(txtEntryPrice.Text);

            // 3
            var rec = _RR_Calc.PriceDecreaseTable.GeneratePriceDecreaseRecord(priceTarget, _position.EntryPriceAvg, _position.PositionValue, _tradingCost.TotalTradingCost);

            // 4
            if (rec == null) return;
            txtPriceDecreasePercentage.Text = rec.PriceDecreasePercentage.ToString();
            txtPriceDecrease_loss.Text = rec.Loss.ToString();
        }
    }
}
