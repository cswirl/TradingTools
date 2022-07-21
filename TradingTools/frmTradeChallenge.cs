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
using TradingTools.Trunk.Extensions;

namespace TradingTools
{
    public partial class frmTradeChallenge : Form
    {
        private BindingList<Trade> _activeTrades;
        private BindingList<Trade> _tradeHistory;
        private BindingList<CalculatorState> _prospects;
        //private master Master { get { return (master)this.Owner; } }
        public TradeChallenge TradeChallenge { get; set; }
        public master Master { get; set; }

        public frmTradeChallenge()
        {
            InitializeComponent();
        }

        private void btnOpenCalcLong_Empty_Click(object sender, EventArgs e)
        {
            var rrc = Master.FormRRC_Long_Empty_Spawn();
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
            _activeTrades = new BindingList<Trade>(Master.GetTradeChallenges_ActiveTrade(TradeChallenge.Id));
            _tradeHistory = new BindingList<Trade>(Master.GetTradeChallenges_TradeHistory(TradeChallenge.Id));

            dgvActiveTrade.DataSource = _activeTrades;
            //dgvProspects.DataSource = _prospects;
            dgvTradeHistory.DataSource = _tradeHistory;

            // Load Trade Challenge Object
            txtId.Text = TradeChallenge.Id.ToString();
            txtCap.Text = TradeChallenge.TradeCap.ToString();
            txtDesc.Text = TradeChallenge.Description;
            if (TradeChallenge.IsOpen) radioOpen.Checked = true; 
            else radioClosed.Checked = true;

            txtTitle.Text = TradeChallenge.Title;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // validation

            // copy back to original
            captureTradeChallenge().CopyProperties(this.TradeChallenge);
            if (Master.TradeChallenge_Update(this.TradeChallenge))
            {
                MyMessageBox.Inform("Changes were saved");
            }
        }

        private TradeChallenge captureTradeChallenge()
        {
            // make a clone
            var clone = new TradeChallenge();
            TradeChallenge.CopyProperties(clone);
            // get changes
            clone.TradeCap = txtCap.Text.ToInteger();
            clone.Description = txtDesc.Text;
            return clone;
        }

        private void btnCompleted_Click(object sender, EventArgs e)
        {
            if (_tradeHistory.Count < TradeChallenge.TradeCap)
            {
                DialogResult objDialog = MyMessageBox.Question_YesNo(
                    "Are you sure to terminate Trade Challenge pre-maturely ?",
                    "Terminating Trade Challenge");
                if (objDialog == DialogResult.Yes)
                {
                    TradeChallenge.IsOpen = false;
                    if (Master.TradeChallenge_Update(this.TradeChallenge))
                    {
                        MyMessageBox.Inform($"Trade Challenge: {TradeChallenge.Id} was closed");
                        radioClosed.Checked = true;
                    }
                    else
                    {
                        MyMessageBox.Error($"Fail terminating the Trade Challenge: {TradeChallenge.Id}");
                    }
                }
            }
        }
    }
}
