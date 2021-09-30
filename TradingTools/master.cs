using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingTools
{
    public class master : Form
    {
        public frmRiskRewardCalc_Long frmRRC_Long { get; set; }
        public frmCalculatorStates frmCalcStates { get; set; }


        public master()
        {
            this.Visible = false;
            this.Size = new System.Drawing.Size(1, 1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;

            frmCalcStates = new();
            frmCalcStates.Owner = this;
            frmCalcStates.StartPosition = FormStartPosition.CenterScreen;
            frmCalcStates.Show();
        }

        public void ShowRRC_Long()
        {

        }
    }
}
