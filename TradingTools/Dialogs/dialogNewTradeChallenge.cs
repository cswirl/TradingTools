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
using TradingTools.Trunk.Extensions;

namespace TradingTools
{
    public partial class dialogNewTradeChallenge : Form
    {
        public TradeChallenge TradeChallenge { get; set; }

        public dialogNewTradeChallenge()
        {
            InitializeComponent();
            TradeChallenge = new();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TradeChallenge.TradeCap = txtCap.Text.ToInteger();
            TradeChallenge.Description = txtDesc.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult=DialogResult.Cancel;
            this.Close();
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
