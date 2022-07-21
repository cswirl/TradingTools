﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Services;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;

namespace TradingTools
{
    public partial class frmTradeChallengeMasterFile : Form
    {
        //private frmTradeChallenge _frmTradeChallenge;
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
            tc.IsOpen = true;
            if (_master.TradeChallenge_Create(tc)) {
               _currentTradeChallenges.Add(tc);
                TradeChallenge_Spawn(tc);
            }
        }

        private void registerForm(frmTradeChallenge form)
        {
            form.TradeChallenge_Updated += this.TradeChallenge_Updated;
            form.TradeChallenge_Closed += this.TradeChallenge_Closed;

            form.FormClosed += (object sender, FormClosedEventArgs e)
                => { _listOf_frmTradeChallenge.Remove((frmTradeChallenge)sender); };

            _listOf_frmTradeChallenge.Add(form);
        }

        private void TradeChallenge_Updated(TradeChallenge tc)
        {
            if (tc.IsOpen) dgvOpen.Invalidate();
            else dgvClosed.Invalidate();

        }

        private void TradeChallenge_Closed(TradeChallenge tc)
        {
            _currentTradeChallenges.Remove(tc);
            _closedTradeChallenges.Add(tc);
        }

        private void TradeChallenge_Spawn(TradeChallenge tc)
        {
            var form = new frmTradeChallenge
            {
                Master = _master,
                TradeChallenge = tc 
            };
            // register
            registerForm(form);

            form.Show(this);
        }

        private void frmTradeChallengeMasterFile_Load(object sender, EventArgs e)
        {
            _currentTradeChallenges = new BindingList<TradeChallenge>(_master.GetTradeChallenges_Open());
            _closedTradeChallenges = new BindingList<TradeChallenge>(_master.GetTradeChallenges_Closed());

            dgvOpen.DataSource = _currentTradeChallenges;
            dgvClosed.DataSource = _closedTradeChallenges;
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            TradeChallenge_ActivateForm((TradeChallenge)dgv.CurrentRow.DataBoundItem);
        }

        private bool TradeChallenge_ActivateForm(TradeChallenge tradeChallenge)
        {
            var tc = _listOf_frmTradeChallenge.Find(x => x.TradeChallenge.Id == tradeChallenge.Id);
            if (tc != null)
            {
                tc.WindowState = FormWindowState.Normal;
                tc.Focus();
            }
            else
            {
                TradeChallenge_Spawn(tradeChallenge);
            }

            return true;
        }
    }
}