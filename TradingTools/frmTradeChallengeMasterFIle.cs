using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingTools
{
    public partial class frmTradeChallengeMasterFile : Form
    {
        private frmTradeChallenge _frmTradeChallenge;


        public frmTradeChallengeMasterFile()
        {
            InitializeComponent();
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmTradeChallenge = new();
            _frmTradeChallenge.Show();
        }
    }
}
