using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Trunk.Entity;

namespace TradingTools
{
    public partial class frmTradeChallenge : Form
    {
        private BindingList<Trade> _activeTrades;
        private BindingList<Trade> _tradeHistory;
        private BindingList<CalculatorState> _prospects;
        private master _master { get { return (master)this.Owner; } }
        public TradeChallenge TradeChallenge { get; set; }

        public frmTradeChallenge()
        {
            InitializeComponent();
        }

        private void btnOpenCalcLong_Empty_Click(object sender, EventArgs e)
        {
            var rrc = _master.FormRRC_Long_Empty_Spawn();
            //delegates
            rrc.CalculatorState_Added += this.CalculatorState_Added;
            rrc.CalculatorState_Deleted += this.CalculatorState_Deleted;
            rrc.Trade_Officialized += this.Trade_Officialized;
            rrc.Trade_Closed += this.Trade_Closed; 
        }

        private void CalculatorState_Added(CalculatorState c)
        {
            _prospects.Add(c);
        }

        private void CalculatorState_Deleted(CalculatorState c)
        {
            _prospects.Remove(c);
        }

        private void Trade_Officialized(Trade t)
        {
            //if (_activeTrade.Count > 0) return;
            _activeTrades.Add(t);

            // add to TradeThread
        }

        private void Trade_Closed(Trade t)
        {
            _activeTrades.Remove(t);
            _tradeHistory.Add(t);
        }

        private void frmTradeChallenge_Load(object sender, EventArgs e)
        {
            //todo: set binding lists
            _activeTrades = new BindingList<Trade>(_master.GetTradeChallenges_ActiveTrade(TradeChallenge.Id));
            _tradeHistory = new BindingList<Trade>(_master.GetTradeChallenges_TradeHistory(TradeChallenge.Id));


            dgvActiveTrade.DataSource = _activeTrades;
            //dgvProspects.DataSource = _prospects;
            dgvTradeHistory.DataSource = _tradeHistory;
        }
    }
}
