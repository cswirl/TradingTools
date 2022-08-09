using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TradingTools.Dialogs;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Extensions;

namespace TradingTools
{
    public partial class frmTradeChallenge : Form
    {
        private BindingList<Trade> _activeTrades;
        private BindingList<Trade> _tradeHistory;
        private BindingList<CalculatorState> _prospects;

        public TradeChallenge TradeChallenge { get; set; }
        private master _master;
        private string _lastSavedStateHash;

        public Status State { get; set; } = Status.Open;

        public frmTradeChallenge(master master)
        {
            InitializeComponent();

            this._master = master;
            appInitialize();
        }

        private void appInitialize()
        {
            // delegates
            _master.RefreshTimer.Tick += this.timer_Tick;
            /// Use delegate from the master - these are invoked right after DbContext CRUD statements
            _master.CalculatorState_Updated += this.CalculatorState_Updated;
            _master.CalculatorState_Deleted += this.CalculatorState_Deleted;
            _master.Trade_Updated += this.Trade_Updated;
            _master.Trade_Closed += this.Trade_Closed;
            _master.Trade_Deleted += this.Trade_Deleted;
            //

            dgvProspects.Format_Common();
            dgvActiveTrade.Format_Trade_Active();
            dgvTradeHistory.Format_Trade_Closed();
        }

        #region Delegate Handlers
        private void CalculatorState_Added(CalculatorState c)
        {
            var tcp = new TradeChallengeProspect { TradeChallenge = this.TradeChallenge, CalculatorState = c };
            // Create record to database
            if (_master.TradeChallengeProspect_Create(tcp)) _prospects.Insert(0, c);
            messageBus($"New prospect ticker: {c.Ticker} added successfully");
        }

        private void CalculatorState_Updated(CalculatorState c)
        {
            if (_prospects.Contains(c))
            {
                dgvProspects.Invalidate();
                messageBus($"Prospect with Id: {c.Id} was updated successfully");
            }

        }

        private void CalculatorState_Deleted(CalculatorState c)
        {
            // will rely on Foreign Key referential integrity OnDelete->Cascade
            if (_prospects.Remove(c))
                messageBus($"Prospect with Id: {c.Id} was removed successfully");
        }

        private void Trade_Officialized(Trade t)
        {
            // add to TradeThread
            var tr = new TradeThread
            {
                TradeChallenge = this.TradeChallenge,
                Trade = t,
            };

            if (_master.TradeThread_Create(tr))
            {
                _prospects.Remove(t.CalculatorState);
                _activeTrades.Insert(0, t);
                monthCalendarDateEnter.Visible = true;
                if (_tradeHistory.Count < 1) refreshCalendarBoldDates();
                else monthCalendarDateEnter.BoldedDates = monthCalendarDateEnter.BoldedDates.Append(t.DateEnter).ToArray();
                messageBus($"New Trade with ticker: {t.Ticker} was officialized");
                // Delete the Prospect from the TradeChallengeProspect table
                if (!_master.TradeChallengeProspect_Delete(t.CalculatorState))
                    messageBus($"An error occur while removing Prospect: {t.CalculatorState.Id} from the database");
            }
        }
        private void Trade_Updated(Trade t)
        {
            bool flag = false;
            if (_activeTrades.Contains(t))
            {
                dgvActiveTrade.Invalidate();
                flag = true;
            }
            else if (_tradeHistory.Contains(t))
            {
                dgvTradeHistory.Invalidate();
                flag = true;
            }

            if (flag)
            {
                messageBus($"Trade with Id: {t.Id} was updated successfully");
                refreshCalendarBoldDates();
            }
        }

        private void Trade_Closed(Trade t)
        {
            if (_activeTrades.Contains(t))
            {
                messageBus($"Trade {t.Id} was closed successfully");
                _activeTrades.Remove(t);
                _tradeHistory.Insert(0, t);
                tradeHistoryStats();
            }
        }

        private void Trade_Deleted(Trade t)
        {
            bool flag = false;
            if (_activeTrades.Remove(t)) flag = true;
            else if (_tradeHistory.Remove(t))
            {
                flag = true;
                tradeHistoryStats();
            }

            if (flag)
            {
                messageBus($"Trade with Id: {t.Id} was removed successfully");
                refreshCalendarBoldDates();
            }
        }
        #endregion


        private void btnOpenCalcLong_Empty_Click(object sender, EventArgs e)
        {
            var rrc = _master.FormRRC_Long_Empty_Spawn();
            registerFormRRC(rrc);   
        }

        private void btnOpenCalcShort_Empty_Click(object sender, EventArgs e)
        {
            var rrc = _master.FormRRC_Short_Empty_Spawn();
            registerFormRRC(rrc);
        }

        private void dgvTrades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var trade = (Trade)dgv.CurrentRow.DataBoundItem;
            if (trade == null) return;
            var rrc = _master.FormRRC_Trade_Spawn(trade);
            registerFormRRC(rrc);
        }

        private void dgvProspects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var c = (CalculatorState)dgv.CurrentRow.DataBoundItem;
            if (c == null) return;
            var rrc = _master.FormRRC_Loaded_Spawn(c);
            registerFormRRC(rrc);
        }

        // register delegates
        internal void registerFormRRC(frmRiskRewardCalc rrc)
        {
            /// this block of code prevents duplicate delegate registration for frmRRC objects
            /// - only one of the delegates below is enough for checking
            if (rrc.CalculatorState_Added != default)
                foreach (var x in rrc.CalculatorState_Added.GetInvocationList())
                    if (x.Target.Equals(this)) return;
            ///

            /// delegates
            // Empty frmRRC - Trade Challenge object has exclusive access to empty frmRRC spawned by it
            rrc.CalculatorState_Added += this.CalculatorState_Added;

            /// Once a Trade Challenge object has a hook to rrc.Trade_Officialized delegate thru here, 
            /// it doesn't matter if a frmRRC object is re-activated anywhere in the program
            rrc.Trade_Officialized = this.Trade_Officialized;

            /// Bypass delegate handler from master.DelegateHandlers
            // Here, not using the '+=' assignment to override the handler in master.DelegateHandlers
            rrc.CalculatorState_Officializing_IsCancelled = this.CalculatorState_Officializing_IsCancelled_Handler;
            rrc.Trade_Closing_IsCancelled = null;   
        }

        private void messageBus(string msg) => statusMessage.Text = msg;

        private void refreshCalendarBoldDates()
        {
            monthCalendarDateEnter.BoldedDates = null;
            var allTrades = getAllTrades();
            if (allTrades.Count > 0)
            {
                monthCalendarDateEnter.MinDate = allTrades.First().DateEnter;
                monthCalendarDateEnter.BoldedDates = allTrades.Select(x => x.DateEnter).ToArray();
                monthCalendarDateEnter.MaxDate = DateTime.Now;
            }
            else
                monthCalendarDateEnter.Visible = false;
        }

        private void tradeHistoryStats() => lblTradeHist.Text = $"Trade History   ( {_tradeHistory.Count()} / {TradeChallenge.TradeCap} )";

        private bool CalculatorState_Officializing_IsCancelled_Handler(CalculatorState c, out string msg)
        {
            msg = "";
            if (_activeTrades.Count > 0)
            {
                msg = $"The Trade Challenge only allows one Active Trade at a time" +
                    $"\n\nTwo options:" +
                    $"\n\t- Close the Active Trade with Id: {_activeTrades.First().Id}" +
                    $"\n\tOR" +
                    $"\n\t- Save this Risk/Reward Calculator for future use";
                return true;
            }
            return false;
        }

        private void frmTradeChallenge_Load(object sender, EventArgs e)
        {
            if (TradeChallenge == default) return;
            // data bindings
            _prospects = new(_master.TradeChallengeProspect_GetAll(TradeChallenge.Id, true));
            _activeTrades = new(_master.TradeThread_GetActiveTrade(TradeChallenge.Id));
            _tradeHistory = new(_master.TradeThread_GetTradeHistory(TradeChallenge.Id, true));

            dgvProspects.DataSource = _prospects;
            dgvActiveTrade.DataSource = _activeTrades;
            dgvTradeHistory.DataSource = _tradeHistory;

            // Load Trade Challenge Object
            txtId.Text = TradeChallenge.Id.ToString();
            txtCap.Text = TradeChallenge.TradeCap.ToString();
            txtDesc.Text = TradeChallenge.Description;
            txtTitle.Text = TradeChallenge.Title;
            // calendar
            refreshCalendarBoldDates();
            tradeHistoryStats();

            changeState(this.TradeChallenge.IsOpen ? Status.Open : Status.Closed);
            messageBus("Form loaded successful");

            SetLastSavedCalculatorHash();

            // form properties
            this.Owner = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            if (TradeChallenge == default) return false;
            // copy back to original
            captureTradeChallenge().CopyProperties(this.TradeChallenge);
            if (_master.TradeChallenge_Update(this.TradeChallenge))
            {
                statusMessage.Text = "Changes were saved successfully";
                txtTitle.Text = TradeChallenge.Title;
                this.Text = TradeChallenge.Title;
                tradeHistoryStats();
                SetLastSavedCalculatorHash();
                return true;
            }

            return false;
        }

        private TradeChallenge captureTradeChallenge()
        {
            if (TradeChallenge == default) return default;
            // make a clone
            var clone = new TradeChallenge();
            TradeChallenge.CopyProperties(clone);
            // get changes
            clone.TradeCap = txtCap.Text.ToInteger();
            clone.Description = txtDesc.Text;
            return clone;
        }

        private void deleteTradeChallenge(TradeChallenge tc)
        {
            string msg;
            var result = AppMessageBox.Question_YesNo(
                $"Trade Challenge: {tc.Id} does not contain any Trade and will be deleted", 
                "Ending Trade Challenge");
            if (result == DialogResult.Yes)
                if (_master.TradeChallenge_Delete(tc))
                {
                    msg = $"Trade Challenge: {tc.Id} is now deleted\n\nThis form will now close";
                    messageBus(msg);
                    AppMessageBox.Inform(msg);
                    _prospects.Clear(); // We will rely on foreign key's Cascade on delete
                    this.Close();
                }
                else
                {
                    msg = $"An unexpected error occur while deleting Trade Challenge: {tc.Id}";
                    messageBus(msg);
                    AppMessageBox.Error(msg);
                }
        }

        private void btnCompleted_Click(object sender, EventArgs e)
        {
            string msg;

            // Active Trade Exist
            if (_activeTrades.Count > 0)
            {
                msg = "To proceed ending this Trade Challenge, the Active Trade need to be closed first";
                messageBus(msg);
                AppMessageBox.Error(msg, "Ending Trade Challenge");
                return;
            }
            // Empty Trade Challenge
            else if (_master.TradeThread_GetAllTrades(TradeChallenge.Id).Count < 1)
            {
                deleteTradeChallenge(this.TradeChallenge);
            }
            else
            {
                // Dialog confirmation
                var premature = (_tradeHistory.Count < TradeChallenge.TradeCap) ? "Pre-Maturely" : "";
                DialogResult objDialog = AppMessageBox.Question_YesNo(
                         $"Are you sure to terminate Trade Challenge: {this.TradeChallenge.Id} {premature} ?",
                         "Terminating Trade Challenge");
                if (objDialog == DialogResult.Yes)
                {
                    if (_master.TradeChallenge_Close(this.TradeChallenge))
                    {
                        msg = $"Trade Challenge: {TradeChallenge.Id} was closed";
                        messageBus(msg);
                        AppMessageBox.Inform(msg);
                        changeState(Status.Closed);
                        // delete the CalculatorStates in the TradeProspects linked to this Trade Challenge
                        var tcp = _prospects.Select(p => p.TradeChallengeProspect).ToArray();
                        if (_master.TradeChallengeProspect_Delete(tcp))
                        {
                            _prospects.Clear();
                        }
                        else
                        {
                            msg = $"Fail deleting the Prospects in the database";
                            messageBus(msg);
                            AppMessageBox.Error(msg);
                        }
                        SetLastSavedCalculatorHash();
                    }
                    else
                    {
                        msg = $"Fail ending the Trade Challenge: {TradeChallenge.Id}";
                        messageBus(msg);
                        AppMessageBox.Error(msg);
                    }
                }
            }
        }

        private List<Trade> getAllTrades() => _master.TradeThread_GetAllTrades(TradeChallenge.Id);

        private void changeState(Status s)
        {
            this.Text = TradeChallenge.Title;

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

                    // calendar
                    var allTrades = getAllTrades();
                    if (allTrades.Count > 0)
                        monthCalendarDateEnter.MaxDate = allTrades.Last().DateExit
                            ?? allTrades.Last().DateEnter.AddMonths(2);

                    break;
            }  
        }

        private void CalendarMaxDateRefresh()
        {
            if (this.State == Status.Open) monthCalendarDateEnter.MaxDate = DateTime.Now;   
        }

        public enum Status
        {
            Open,
            Closed
        }

        private void txtCap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            CalendarMaxDateRefresh();
            statusMessage.Text = "status . . .";
        }

        private void frmTradeChallenge_FormClosing(object sender, FormClosingEventArgs e)
        {
            // statements to just ignore and close the form
            if (TradeChallenge == default) return;
            if (DoNotAskSave()) return;

            // show the form in case was minimized and closing was came from external such as from a parent form
            this.WindowState = FormWindowState.Normal;
            this.Focus();

            var msgBoxButtons = (e.CloseReason == CloseReason.ApplicationExitCall) ? MessageBoxButtons.YesNo : MessageBoxButtons.YesNoCancel;
            DialogResult objDialog = MessageBox.Show("Do you want to save before closing ?", this.Text, msgBoxButtons, MessageBoxIcon.Question);

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

        private bool DoNotAskSave() => _lastSavedStateHash == captureTradeChallenge().CreateHash();

        private void SetLastSavedCalculatorHash() => _lastSavedStateHash = captureTradeChallenge().CreateHash();

    }

}
