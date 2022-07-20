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

namespace TradingTools
{
    public partial class frmTradeChallenge : Form
    {
        private List<Trade> _activeTrade;
        private List<frmRiskRewardCalc> _prospects;
        private master _master;

        public frmTradeChallenge()
        {
            InitializeComponent();

            _prospects = new();
        }

        private void btnOpenCalcLong_Empty_Click(object sender, EventArgs e)
        {
            var rrc = _master.FormRRC_Long_Empty_Spawn(null, null);
            // rrc delegates
            // save
            // officialized
            
        }

        private void RRC_Save(frmRiskRewardCalc rrc)
        {
            _prospects.Add(rrc);
        }

        private void Trade_Officialized(Trade t)
        {
            //if (_activeTrade.Count > 0) return;
            _activeTrade.Add(t);

            // add to TradeThread
        }
    }
}
