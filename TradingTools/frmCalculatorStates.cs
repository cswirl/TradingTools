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
        private RiskRewardCalc_Long _rrc_serv = new();

        private BindingSource _binding = new();

        public frmCalculatorStates()
        {
            InitializeComponent();

            // As of now, the binding source is only useful for adding new state object - i need to access the internal list of DbContext DbSet
            _binding.DataSource = _rrc_serv.RetrieveList();
            dgvCalculatorStates.DataSource = _binding;
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
            var form = new frmRiskRewardCalc_Long();
            form.Owner = this;
            form.CalculatorState = (CalculatorState)dgvCalculatorStates.CurrentRow.DataBoundItem;
            form.State = RiskRewardCalcState.Loaded;
            form.Show();
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
        }

        // temporary fix - an event listener sounds perfect for this - maybe a queueu
        public void AddNewCalcStateObject(CalculatorState calc)
        {
            _binding.Add(calc);
        }
    }
}
