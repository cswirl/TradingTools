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
        public delegate void TradeChallenge_Create(TradeChallenge tc);
        public TradeChallenge_Create TradeChallenge_Save;

        public dialogNewTradeChallenge()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var tc = new TradeChallenge
            {
                TradeCap = txtCap.Text.ToInteger(),
                Description = txtDesc.Text
            };
            this.Close();
            // invoke delegate for Trade Challenge Master File
            TradeChallenge_Save?.Invoke(tc);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
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
