using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.DAL;
using TradingTools.Services;
using TradingTools.Trunk.Entity;

namespace TradingTools
{
    public class master : Form
    {
        private List<frmRiskRewardCalc_Long> _listOf_frmRRC_Long;
        //private BindingList<CalculatorState> _calculatorStates_unofficial_bindingList;      // not-in-use
        private BindingList<Trade> _trades_open_bindingList;
        
        private List<CalculatorState> _calcStates_List;             // in-use

        public TradingToolsDbContext DbContext { get; set; }
        public frmCalculatorStates frmCalcStates { get; set; }


        public master()
        {
            // make master invisible to the user
            this.Visible = false;
            this.Size = new System.Drawing.Size(1, 1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Load += new EventHandler(master_Load);

            // Initialize Components
            DbContext = new();
            _listOf_frmRRC_Long = new();
            //_calc_serv = new();
            //_calculatorStates_unofficial_bindingList = _calc_serv.GetBindingList();
            //_calculatorStates_unsaved = new();


            // gateway
            frmCalcStates = new();
            frmCalcStates.Owner = this;
            frmCalcStates.StartPosition = FormStartPosition.CenterScreen;
            frmCalcStates.Show();
            // delegates
            frmCalcStates.CalculatorState_Loaded_OnRequest += this.FormRRCLong_Loaded_Spawned;
            frmCalcStates.FormRRCLong_Empty_Open += this.FormRRCLong_Empty_Spawned;
        }

        private void master_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(0, 0);

        }

        private bool FormRRCLong_Loaded_Spawned(CalculatorState c)
        {
            // TODO: the EF core list is being renew whenever a Trade is Officialized or CalculatorState is Updated
            // - maybe use id or something, maybe hash
            //var rrc = _listOf_frmRRC_Long.Find(x => x.CalculatorState.GetHashCode().Equals(c.GetHashCode()));
            var rrc = _listOf_frmRRC_Long.Find(x => x.CalculatorState.Equals(c));       // THOUGH THIS IS WORKING FINE
            if (rrc != null)
            {
                rrc.WindowState = FormWindowState.Normal;
                rrc.Focus();
            }
            else
            {
                var form = new frmRiskRewardCalc_Long();
                form.State = RiskRewardCalcState.Loaded;
                form.Text = c.Ticker;
                form.Owner = this;
                form.CalculatorState = c;
                form.Show();
                //TODO: Delegates assignment here
                form.FormClosing += (object sender, FormClosingEventArgs e) => _listOf_frmRRC_Long.Remove((frmRiskRewardCalc_Long)sender);

                _listOf_frmRRC_Long.Add(form);
            }

            return true;
        }

        private void FormRRCLong_Empty_Spawned(object sender, EventArgs e)
        {
            var form = new frmRiskRewardCalc_Long();
            form.Owner = this;
            form.Show();
            //TODO: Delegates assignment here

            //TODO: Add this to a list - should i mix this with the loaded ones?
            //_calculatorStates_unsaved.Add(form.CalculatorState);
        }

        
        //public BindingList<CalculatorState> GetCalculatorStates_Unofficial_BindingList()
        //{
        //    Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load(
        //        DbContext.CalculatorStates
        //        .Include(x => x.Trade).Where(y => y.Trade == default)
        //        );

        //    _calculatorStates_unofficial_bindingList = DbContext.CalculatorStates.Local.ToBindingList();

        //    return _calculatorStates_unofficial_bindingList;

        //}

        
        public List<CalculatorState> GetCalculatorStates_Unofficial_List()
        {
            _calcStates_List = DbContext.CalculatorStates
                .Where(x => x.TradeId == null)
                .Include(x => x.Trade).ToList();

            return _calcStates_List;

        }

        // temporary fix - an event listener sounds perfect for this - maybe a queueu
        // Return True if no error
        public bool CalculatorState_Add(CalculatorState calculatorState)
        {
            _calcStates_List.Add(calculatorState);
            // This one line code must persist the new object in the database
            DbContext.SaveChanges();

            return true;
        }

        public bool CalculatorState_Update()
        {
            DbContext.SaveChanges();
            frmCalcStates.dgvUnofficial_Refresh();       // Refereshes the DataGridView

            return true;
        }

        public bool CalculatorState_Delete(CalculatorState calculatorState)
        {
            _calcStates_List.Remove(calculatorState);
            // This one line code must persist the new object in the database
            DbContext.SaveChanges();

            return true;
        }

        public BindingList<Trade> GetTrades_Open()
        {
            // im not sure if the WHERE filter will work - we'll see later on when there are Status = closed
            Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load(DbContext.Trades
                .Where(t => t.Status.Equals("open"))
                .Include(t => t.CalculatorState));

            _trades_open_bindingList = DbContext.Trades.Local.ToBindingList();

            return _trades_open_bindingList;

            //return DbContext.Trades
            //    .Where(t => t.Status.Equals("open"))
            //    .Include(t => t.CalculatorState)
            //    .ToList();
        }

        internal bool Trade_Add(Trade t)
        {
            _trades_open_bindingList.Add(t);
            DbContext.SaveChanges();
            frmCalcStates.dgvUnofficial_Refresh();       // Refereshes the DataGridView

            return true;
        }

        internal bool Trade_ClosePosition(CalculatorState calculatorState)
        {
            DbContext.SaveChanges();
            frmCalcStates.dgvTrades_Open_Refresh();       // Refereshes the DataGridView

            return true;
        }


        #region UNUSED
        private BindingSource _calculatorStates_unsaved;
        public BindingSource GetCalculatorStates_Unsaved_BindingSource()
        {
            _calculatorStates_unsaved.DataSource = _listOf_frmRRC_Long
                .Where(x => x.State == RiskRewardCalcState.Empty)
                .Select(x => x.CalculatorState)
                .ToList();

            return _calculatorStates_unsaved;


            //// Another implementation for a list of Unsaved RRC Form i.e. their Id == default(int) - maybe create a dedicated List() for this
            //// They will have their own DataGridView - same functionality as the first dgv
            //var rrc_forms = Array.ConvertAll(this.OwnedForms, form => (frmRiskRewardCalc_Long)form).ToList();
            //var rrc = rrc_forms.Find(x => x.CalculatorState.Id == default(int));
            //if (rrc != null)
            //{
            //    rrc.WindowState = FormWindowState.Normal;
            //    rrc.Focus();
            //}
            //else
            //{

            //}
        }
        #endregion
    }
}