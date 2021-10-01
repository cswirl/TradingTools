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
using TradingTools.Trunk.Entity;

namespace TradingTools
{
    public partial class frmCalculatorStates : Form
    {
        private CalculatorState_Serv _calc_serv = new();

        private BindingList<CalculatorState> _bindingList;

        public frmCalculatorStates()
        {
            InitializeComponent();

            // As of now, the binding source is only useful for adding new state object - i need to access the internal list of DbContext DbSet
            _bindingList = _calc_serv.GetBindingList();
            dgvCalculatorStates.DataSource = _bindingList;
        }

        private void frmCalculatorStates_Load(object sender, EventArgs e)
        {
            
           
        }

        private void frmCalculatorStates_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnOpenCalc_Click(object sender, EventArgs e)
        {
            var selected = (CalculatorState)dgvCalculatorStates.CurrentRow.DataBoundItem;
            var rrc_forms = Array.ConvertAll(this.OwnedForms, form => (frmRiskRewardCalc_Long)form).ToList();
            var rrc = rrc_forms.Find(x => x.CalculatorState.Equals(selected));
            if (rrc != null)
            {
                rrc.WindowState = FormWindowState.Normal;
                rrc.Focus();
            }
            else
            {
                var form = new frmRiskRewardCalc_Long();
                form.Owner = this;
                form.CalculatorState = selected;
                form.State = RiskRewardCalcState.Loaded;
                form.Show();
            }
        }

        // not used - ideally an event listener
        public void DataGridView_Refresh()
        {
            dgvCalculatorStates.Invalidate();
        }

        private void btnOpenCalc_Empty_Click(object sender, EventArgs e)
        {
            var form = new frmRiskRewardCalc_Long();
            form.Owner = this;
            form.Show();

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

        // temporary fix - an event listener sounds perfect for this - maybe a queueu
        // Return True if no error
        public bool CalculatorState_Add(CalculatorState calculatorState)
        {
            _bindingList.Add(calculatorState);
            // This one line code must persist the new object in the database
            _calc_serv.SaveChanges();

            return true;
        }

        public bool CalculatorState_Update()
        {
            _calc_serv.SaveChanges();
            dgvCalculatorStates.Invalidate();       // Refereshes the DataGridView

            return true;
        }

        public bool CalculatorState_Delete(CalculatorState calculatorState)
        {
            _bindingList.Remove(calculatorState);
            // This one line code must persist the new object in the database
            _calc_serv.SaveChanges();

            return true;
        }
    }
}
