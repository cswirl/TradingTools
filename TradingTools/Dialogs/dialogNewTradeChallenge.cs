﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Dialogs;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Extensions;

namespace TradingTools
{
    public partial class dialogNewTradeChallenge : Form
    {
        public TradeChallenge TradeChallenge { get; set; }
        private dialogCompoundCalc _dialogCompundCalc;

        public dialogNewTradeChallenge()
        {
            InitializeComponent();
            TradeChallenge = new();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TradeChallenge.TradeCap = txtCap.Text.ToInteger();
            TradeChallenge.TargetPercentage = txtTargetPercentage.Text.ToDecimal();
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

        private void txtTargetPercentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnCompoundCalc_Click(object sender, EventArgs e)
        {
            (string initialCapital, string targetPercent, string tradeCap) arg = ("100", txtTargetPercentage.Text, txtCap.Text);
            if (_dialogCompundCalc == default) _dialogCompundCalc = new dialogCompoundCalc(arg);
            _dialogCompundCalc.UseData = (pos) => {
                txtTargetPercentage.Text = pos.targetPercent;
                txtCap.Text = pos.tradeCap;
            };
            _dialogCompundCalc.ShowDialog();
        }
    }
}
