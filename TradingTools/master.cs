using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.DAL;

namespace TradingTools
{
    public class master : Form
    {
        public TradingToolsDbContext _dbContext { get; set; } = new();
        public frmRiskRewardCalc_Long frmRRC_Long { get; set; }
        public frmCalculatorStates frmCalcStates { get; set; }


        public master()
        {
            this.Visible = false;
            this.Size = new System.Drawing.Size(1, 1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Load += new EventHandler(master_Load);

            frmCalcStates = new();
            frmCalcStates.Owner = this;
            frmCalcStates.StartPosition = FormStartPosition.CenterScreen;
            frmCalcStates.Show();
        }

        private void master_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(0,0);
        }

        public void ShowRRC_Long()
        {

        }
    }
}
