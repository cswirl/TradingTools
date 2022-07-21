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
    public partial class frmTradeChallengeMasterFile : Form
    {
        private frmTradeChallenge _frmTradeChallenge;
        private List<frmTradeChallenge> _listOf_frmTradeChallenge;
        private master _master { get { return (master)this.Owner; } }

        private BindingList<TradeChallenge> _currentTradeChallenges;
        private BindingList<TradeChallenge> _closedTradeChallenges;

        public frmTradeChallengeMasterFile()
        {
            InitializeComponent();

            _listOf_frmTradeChallenge = new();
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new dialogNewTradeChallenge();
            dialog.TradeChallenge_Save += TradeChallenge_Save;
            dialog.ShowDialog();
        }

        private void TradeChallenge_Save(TradeChallenge tc)
        {
            if (_master.TradeChallenge_Create(tc)) {
               _currentTradeChallenges.Add(tc);
                openTradeChallenge(tc);
            }
        }

        private void openTradeChallenge(TradeChallenge tc)
        {
            _frmTradeChallenge = new()
            {
                Owner = _master,
                TradeChallenge = tc 
            };
            _frmTradeChallenge.FormClosed += (object sender, FormClosedEventArgs e)
                => { _listOf_frmTradeChallenge.Remove((frmTradeChallenge)sender);  };

            _frmTradeChallenge.Show();
        }

        private void frmTradeChallengeMasterFile_Load(object sender, EventArgs e)
        {
            _currentTradeChallenges = new BindingList<TradeChallenge>(_master.GetTradeChallenges_Open());
            _closedTradeChallenges = new BindingList<TradeChallenge>(_master.GetTradeChallenges_Closed());

            dgvCurrent.DataSource = _currentTradeChallenges;
            dgvClosed.DataSource = _closedTradeChallenges;
        }
    }
}
