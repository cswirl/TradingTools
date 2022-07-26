using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Trunk;
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

            DataGridViewFormat_Common(dgvProspects);
            DataGridViewFormat_Trade_Active(dgvActiveTrade);
            DataGridViewFormat_Trade_Closed(dgvTradeHistory);
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
                txtTitle.Text = TradeChallenge.Title;
                this.Text = TradeChallenge.Title;

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
            if (_activeTrades.Count > 0)
            {
                MyMessageBox.Error("To proceed ending this Trade Challenge, the Active Trade need to be closed first", "Ending Trade Challenge");
                return;
            }

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
            this.Text = TradeChallenge.Title;
            switch (s)
            {
                case Status.Open:
                    radioOpen.Checked = true;

                    // calendar
                    if (_tradeHistory.Count > 0)
                    {
                        monthCalendarDateEnter.MinDate = _tradeHistory.First().DateEnter;
                        monthCalendarDateEnter.MaxDate = _tradeHistory.First().DateEnter.AddYears(2);
                        var boldDates = _tradeHistory.Select(x => x.DateEnter).ToList();
                        boldDates.Add(_activeTrades.FirstOrDefault().DateEnter);
                        monthCalendarDateEnter.BoldedDates = boldDates.ToArray();
                    }
                    else
                    {
                        var today = DateTime.Today;
                        monthCalendarDateEnter.MinDate = today;
                        monthCalendarDateEnter.MaxDate = today.AddYears(2);
                    }
                    break;

                case Status.Closed:
                    radioClosed.Checked = true;
                    btnCompleted.Visible = false;
                    tableLayoutPanel_LongShortButtons.Visible = false;
                    txtCap.ReadOnly = true;

                    // calendar
                    if (_tradeHistory.Count > 0)
                    {
                        monthCalendarDateEnter.MinDate = _tradeHistory.First().DateEnter;
                        monthCalendarDateEnter.MaxDate = _tradeHistory.Last().DateExit 
                            ?? _tradeHistory.First().DateEnter.AddYears(2);
                        var boldDates = _tradeHistory.Select(x => x.DateEnter).ToList();
                        boldDates.Add(_activeTrades.FirstOrDefault().DateEnter);
                        monthCalendarDateEnter.BoldedDates = boldDates.ToArray();
                    }
                    else
                    {
                        var today = DateTime.Today;
                        monthCalendarDateEnter.MinDate = today;
                        monthCalendarDateEnter.MaxDate = today.AddYears(2);
                    }
                    break;
            }  
        }



        #region DataGridView Formatting
        private void DataGridViewFormat_Common(DataGridView d)
        {
            d.DefaultCellStyle.SelectionForeColor = Color.White;
            d.AutoGenerateColumns = false;
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.AllowUserToOrderColumns = true;
            d.AllowUserToDeleteRows = false;
            d.AllowUserToAddRows = false;
            d.AllowUserToResizeColumns = true;
            d.AllowUserToResizeRows = false;
            d.ReadOnly = true;
            d.MultiSelect = false;
            d.Font = new Font(new FontFamily("tahoma"), 8.5f, FontStyle.Regular);
            d.Columns.Clear();

            // Id
            var id = new DataGridViewTextBoxColumn();
            id.DataPropertyName = "Id";
            id.Name = "Id";
            id.HeaderText = "Id";
            id.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            id.DefaultCellStyle.Format = Constant.WHOLE_NUMBER;
            id.Width = 65;
            id.ReadOnly = true;
            id.SortMode = DataGridViewColumnSortMode.NotSortable;
            id.Visible = true;
            d.Columns.Add(id);

            // Ticker
            var tick = new DataGridViewTextBoxColumn();
            tick.DataPropertyName = "Ticker";
            tick.Name = "Ticker";
            tick.HeaderText = "Ticker";
            tick.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tick.Width = 80;
            tick.ReadOnly = true;
            tick.SortMode = DataGridViewColumnSortMode.NotSortable;
            tick.Visible = true;
            d.Columns.Add(tick);

            // Side
            var Side = new DataGridViewTextBoxColumn();
            Side.DataPropertyName = "Side";
            Side.Name = "Side";
            Side.HeaderText = "Side";
            Side.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Side.Width = 50;
            Side.ReadOnly = true;
            Side.SortMode = DataGridViewColumnSortMode.NotSortable;
            Side.Visible = true;
            d.Columns.Add(Side);
        }

        private void DataGridViewFormat_Trade_Common(DataGridView d)
        {
            // Capital
            var cap = new DataGridViewTextBoxColumn();
            cap.DataPropertyName = "Capital";
            cap.Name = "Capital";
            cap.HeaderText = "Capital";
            cap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            cap.DefaultCellStyle.Format = Constant.MONEY_FORMAT;
            cap.Width = 80;
            cap.ReadOnly = true;
            cap.SortMode = DataGridViewColumnSortMode.NotSortable;
            cap.Visible = true;
            d.Columns.Add(cap);
        }

        private void DataGridViewFormat_Trade_Active(DataGridView d)
        {
            DataGridViewFormat_Common(d);
            DataGridViewFormat_Trade_Common(d);

            // Date Enter
            var dateEnter = new DataGridViewTextBoxColumn();
            dateEnter.DataPropertyName = "DateEnter";
            dateEnter.Name = "DateEnter";
            dateEnter.HeaderText = "Date Enter";
            dateEnter.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dateEnter.DefaultCellStyle.Format = Constant.DATE_MMMM_DD_YYYY;
            dateEnter.Width = 120;
            dateEnter.ReadOnly = true;
            dateEnter.SortMode = DataGridViewColumnSortMode.NotSortable;
            dateEnter.Visible = true;
            d.Columns.Add(dateEnter);

            // Day Count
            var days = new DataGridViewTextBoxColumn();
            days.DataPropertyName = "DayCount";
            days.Name = "DayCount";
            days.HeaderText = "Days";
            days.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            days.DefaultCellStyle.Format = Constant.WHOLE_NUMBER;
            days.Width = 40;
            days.ReadOnly = true;
            days.SortMode = DataGridViewColumnSortMode.NotSortable;
            days.Visible = true;
            d.Columns.Add(days);
        }

        private void DataGridViewFormat_Trade_Closed(DataGridView d)
        {
            DataGridViewFormat_Common(d);
            DataGridViewFormat_Trade_Common(d);

            // Final Capital
            var fcap = new DataGridViewTextBoxColumn();
            fcap.DataPropertyName = "FinalCapital";
            fcap.Name = "FinalCapital";
            fcap.HeaderText = "F. Capital";
            fcap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            fcap.DefaultCellStyle.Format = Constant.MONEY_FORMAT;
            fcap.Width = 80;
            fcap.ReadOnly = true;
            fcap.SortMode = DataGridViewColumnSortMode.NotSortable;
            fcap.Visible = true;
            d.Columns.Add(fcap);

            // PnL
            var PnL = new DataGridViewTextBoxColumn();
            PnL.DataPropertyName = "PnL";
            PnL.Name = "PnL";
            PnL.HeaderText = "PnL";
            PnL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            PnL.DefaultCellStyle.Format = Constant.MONEY_FORMAT;
            PnL.Width = 60;
            PnL.ReadOnly = true;
            PnL.SortMode = DataGridViewColumnSortMode.NotSortable;
            PnL.Visible = true;
            d.Columns.Add(PnL);

            // PnL %
            var PnL_percentage = new DataGridViewTextBoxColumn();
            PnL_percentage.DataPropertyName = "PnL_Percentage";
            PnL_percentage.Name = "PnL_Percentage";
            PnL_percentage.HeaderText = "PnL %";
            PnL_percentage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PnL_percentage.DefaultCellStyle.Format = Constant.PERCENTAGE_FORMAT_SINGLE;
            PnL_percentage.Width = 80;
            PnL_percentage.ReadOnly = true;
            PnL_percentage.SortMode = DataGridViewColumnSortMode.NotSortable;
            PnL_percentage.Visible = true;
            d.Columns.Add(PnL_percentage);

            // Date Enter
            var dateEnter = new DataGridViewTextBoxColumn();
            dateEnter.DataPropertyName = "DateEnter";
            dateEnter.Name = "DateEnter";
            dateEnter.HeaderText = "Date Enter";
            dateEnter.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dateEnter.DefaultCellStyle.Format = Constant.DATE_MMMM_DD_YYYY;
            dateEnter.Width = 120;
            dateEnter.ReadOnly = true;
            dateEnter.SortMode = DataGridViewColumnSortMode.NotSortable;
            dateEnter.Visible = true;
            d.Columns.Add(dateEnter);

            // Date Enter
            var dateExit = new DataGridViewTextBoxColumn();
            dateExit.DataPropertyName = "DateExit";
            dateExit.Name = "DateExit";
            dateExit.HeaderText = "Date Exit";
            dateExit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dateExit.DefaultCellStyle.Format = Constant.DATE_MMMM_DD_YYYY;
            dateExit.Width = 120;
            dateExit.ReadOnly = true;
            dateExit.SortMode = DataGridViewColumnSortMode.NotSortable;
            dateExit.Visible = true;
            d.Columns.Add(dateExit);

            // Day Count
            var days = new DataGridViewTextBoxColumn();
            days.DataPropertyName = "DayCount";
            days.Name = "DayCount";
            days.HeaderText = "Days";
            days.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            days.DefaultCellStyle.Format = Constant.WHOLE_NUMBER;
            days.Width = 40;
            days.ReadOnly = true;
            days.SortMode = DataGridViewColumnSortMode.NotSortable;
            days.Visible = true;
            d.Columns.Add(days);
        }
        #endregion


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
    }

}
