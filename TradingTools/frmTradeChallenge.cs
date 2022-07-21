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
        // delegates
        public delegate void Save(TradeChallenge tc);
        public Save TradeChallenge_Updated;
        public delegate void Completed(TradeChallenge tc);
        public Completed TradeChallenge_Closed;

        private BindingList<Trade> _activeTrades;
        private BindingList<Trade> _tradeHistory;
        private BindingList<CalculatorState> _prospects;
        
        public TradeChallenge TradeChallenge { get; set; }
        public master Master { get; set; }

        public Status State { get; set; } = Status.Open;

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
            // add to TradeThread


            _activeTrades.Add(t);
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
            txtTitle.Text = TradeChallenge.Title;

            changeState(this.TradeChallenge.IsOpen ? Status.Open : Status.Closed);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // validation

            // copy back to original
            captureTradeChallenge().CopyProperties(this.TradeChallenge);
            if (Master.TradeChallenge_Update(this.TradeChallenge))
            {
                MyMessageBox.Inform("Changes were saved");
                TradeChallenge_Updated?.Invoke(this.TradeChallenge);
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
            var premature = (_tradeHistory.Count < TradeChallenge.TradeCap) ? "Pre-Maturely" : "";
            DialogResult objDialog = MyMessageBox.Question_YesNo(
                     $"Are you sure to terminate Trade Challenge {premature} ?",
                     "Terminating Trade Challenge");
            if (objDialog == DialogResult.Yes)
            {
                TradeChallenge.IsOpen = false;
                if (Master.TradeChallenge_Update(this.TradeChallenge))
                {
                    MyMessageBox.Inform($"Trade Challenge: {TradeChallenge.Id} was closed");
                    TradeChallenge_Closed?.Invoke(this.TradeChallenge);
                    changeState(Status.Closed);
                }
                else
                {
                    MyMessageBox.Error($"Fail terminating the Trade Challenge: {TradeChallenge.Id}");
                }
            }
        }

        private void changeState(Status s)
        {
            switch (s)
            {
                case Status.Open:
                    radioOpen.Checked = true;
                    break;

                case Status.Closed:
                    radioClosed.Checked = true;
                    btnCompleted.Visible = false;
                    tableLayoutPanel_LongShortButtons.Visible = false;
                    txtCap.ReadOnly = true;
                    break;
            }  
        }

        public enum Status
        {
            Open,
            Closed
        }
    }

    
}
