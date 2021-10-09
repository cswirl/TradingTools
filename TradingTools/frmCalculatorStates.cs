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
        //public event EventHandler<CalculatorState> FormRRCLong_Loaded_Open;
        //public event EventHandler FormRRCLong_Empty_Open;

        public delegate bool CalculatorState_OnRequest(CalculatorState c);
        public CalculatorState_OnRequest CalculatorState_Loaded_OnRequest;

        public event EventHandler FormRRCLong_Empty_Open;

        private master _master { get { return (master)this.Owner; } }

        public frmCalculatorStates()
        {
            InitializeComponent();
        }

        private void frmCalculatorStates_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnOpenCalc_Click(object sender, EventArgs e)
        {
            CalculatorState_Loaded_OnRequest((CalculatorState)dgvUnofficial.CurrentRow.DataBoundItem);
        }

        public void dgvUnofficial_Refresh()
        {
            // As of now, the binding list is able to track changes
            dgvUnofficial.DataSource = _master.GetCalculatorStates_Unofficial_List();
            //dgvUnofficial.Invalidate();
        }

        public void dgvUnofficial_Invalidate()
        {
            dgvUnofficial.Invalidate();
        }

        public void dgvTrades_Open_Refresh()
        {
            dgvTrades_Open.DataSource = _master.GetTrades_Open();
            //dgvTrades_Open.Invalidate();
        }

        public void dgvTrades_Open_Invalidate()
        {
            dgvTrades_Open.Invalidate();
        }

        private void btnOpenCalc_Empty_Click(object sender, EventArgs e)
        {
            FormRRCLong_Empty_Open?.Invoke(this, EventArgs.Empty);
        }

        private void frmCalculatorStates_Load(object sender, EventArgs e)
        {
            dgvUnofficial.DataSource = _master.GetCalculatorStates_Unofficial_List();
            dgvTrades_Open.DataSource = _master.GetTrades_Open();

            // Adds- complexity - not used
            //dgvUnsaved.DataSource = _master.GetCalculatorStates_Unsaved_BindingSource();
        }
    }
}
