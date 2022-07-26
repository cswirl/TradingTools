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
            registerFormRRC(rrc);   
        }

        private void btnOpenCalcShort_Empty_Click(object sender, EventArgs e)
        {
            var rrc = Master.FormRRC_Short_Empty_Spawn();
            registerFormRRC(rrc);
        }

        private void dgvTrades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var trade = (Trade)dgv.CurrentRow.DataBoundItem;
            if (trade == null) return;
            var rrc = Master.FormRRC_Trade_Spawn(trade);
            registerFormRRC(rrc);
        }

        private void dgvProspects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var c = (CalculatorState)dgv.CurrentRow.DataBoundItem;
            if (c == null) return;
            var rrc = Master.FormRRC_Loaded_Spawn(c);
            registerFormRRC(rrc);
        }

        // register delegates
        internal void registerFormRRC(frmRiskRewardCalc rrc)
        {
            //delegates
            rrc.CalculatorState_Added += this.CalculatorState_Added;
            rrc.CalculatorState_Updated += this.CalculatorState_Updated;
            rrc.CalculatorState_Deleted += this.CalculatorState_Deleted;
            rrc.Trade_Officialized += this.Trade_Officialized;
            rrc.Trade_Closed += this.Trade_Closed;
            // Here, not using the '+=' assignment to override the assignment in master.DelegateHandlers 
            rrc.Trade_Officializing_Cancelled = this.Trade_Officializing_Cancelled;
        }

        private void CalculatorState_Added(CalculatorState c)
        {
            var tcp = new TradeChallengeProspect { TradeChallenge = this.TradeChallenge, CalculatorState = c};
            // Create record to database
            if (Master.TradeChallengeProspect_Create(tcp)) _prospects.Add(c);
        }

        private void CalculatorState_Updated(CalculatorState c) => dgvProspects.Invalidate();

        private void CalculatorState_Deleted(CalculatorState c)
        {
            // will rely on Foreign Key referential integrity OnDelete->Cascade
            _prospects.Remove(c);
        }

        private bool Trade_Officializing_Cancelled(CalculatorState c, out string msg)
        {
            msg = "";
            if (_activeTrades.Count > 0)
            {
                msg = $"The Trade Challenge only allows one Active Trade at a time." +
                    $"\n\nYou must first closed its Active Trade with Id: {_activeTrades.First().Id}";
                return true;
            }
            return false;
        }

        private void Trade_Officialized(Trade t)
        {
            // add to TradeThread
            var tr = new TradeThread
            {
                TradeChallengeId = this.TradeChallenge.Id,
                TradeId_head = t.Id,
                TradeId_tail = _tradeHistory.Count < 1 ? null : getTail_Id()
        };

            if (Master.TradeThread_Create(tr))
            {
                _activeTrades.Add(t);
                _prospects.Remove(t.CalculatorState);
            }
        }
        private int getTail_Id() => _tradeHistory.Last().Id;

        private void Trade_Closed(Trade t)
        {
            _activeTrades.Remove(t);
            _tradeHistory.Add(t);
        }

        private void frmTradeChallenge_Load(object sender, EventArgs e)
        {
            _activeTrades = new BindingList<Trade>(Master.TradeThread_GetActiveTrade(TradeChallenge.Id));
            _tradeHistory = new BindingList<Trade>(Master.TradeThread_GetTradeHistory(TradeChallenge.Id));
            _prospects = new BindingList<CalculatorState>(Master.TradeChallengeProspect_GetAll(TradeChallenge.Id));

            dgvActiveTrade.DataSource = _activeTrades;
            dgvProspects.DataSource = _prospects;
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
