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
using TradingTools.Services;
using TradingTools.Services.Interface;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;

namespace TradingTools
{
    public partial class frmTradeChallengeMasterFile : Form
    {
        private master _master;
        private IServiceManager _service;

        private BindingList<TradeChallenge> _currentTradeChallenges;
        private BindingList<TradeChallenge> _closedTradeChallenges;

        public frmTradeChallengeMasterFile(master master)
        {
            InitializeComponent();

            _master = master;
            _service = master.ServiceManager;

            // delegates
            _master.TradeChallenge_Updated += this.TradeChallenge_Updated;
            _master.TradeChallenge_Closed += this.TradeChallenge_Closed;
            _master.TradeChallenge_Deleted += this.TradeChallenge_Deleted;

            DataGridViewFormat_Common(dgvOpen);
            DataGridViewFormat_Common(dgvClosed);
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new dialogNewTradeChallenge();
            dialog.ShowDialog();
            if (dialog.DialogResult == DialogResult.OK)
                Create(dialog.TradeChallenge);
        }

        private void Create(TradeChallenge tc)
        {
            if (_service.TradeChallengeService.Create(tc)) {
               _currentTradeChallenges.Insert(0,tc);
               _master.TradeChallenge_Spawn(tc);
               _master.TradeChallenge_Created?.Invoke(tc);
            }
            else
            {
                AppMessageBox.Error("Creating a Trade Challenge failed", "", this);
            }
        }

        private void TradeChallenge_Updated(TradeChallenge tc)
        {
            if (_currentTradeChallenges.Contains(tc)) dgvOpen.Invalidate();
            else if (_closedTradeChallenges.Contains(tc)) dgvClosed.Invalidate();
        }

        private void TradeChallenge_Closed(TradeChallenge tc)
        {
            // ignore if it does not contain
            if (_currentTradeChallenges.Contains(tc))
            {
                _currentTradeChallenges.Remove(tc);
                _closedTradeChallenges.Insert(0,tc);
            }
        }

        private void TradeChallenge_Deleted(TradeChallenge tc)
        {
                _currentTradeChallenges.Remove(tc);
                _closedTradeChallenges.Remove(tc);
        }

        private void frmTradeChallengeMasterFile_Load(object sender, EventArgs e)
        {
            _currentTradeChallenges = new(_service.TradeChallengeService.GetStatusOpen(true));
            _closedTradeChallenges = new(_service.TradeChallengeService.GetStatusClosed(true));

            dgvOpen.DataSource = _currentTradeChallenges;
            dgvClosed.DataSource = _closedTradeChallenges;
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            _master.TradeChallenge_Spawn((TradeChallenge)dgv.CurrentRow.DataBoundItem);
        }

        #region DataGridView Formatting
        private void DataGridViewFormat_Common(DataGridView d)
        {
            d.DefaultCellStyle.SelectionForeColor = Color.White;
            d.AutoGenerateColumns = false;
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.AllowUserToOrderColumns = true;
            d.AllowUserToDeleteRows = false;
            d.AllowUserToAddRows = false;
            d.AllowUserToResizeColumns = true;
            d.AllowUserToResizeRows = false;
            d.ReadOnly = true;
            d.MultiSelect = false;
            d.Font = new Font(new FontFamily("tahoma"), 8.5f, FontStyle.Regular);
            d.Columns.Clear();

            // Id
            var id = new DataGridViewTextBoxColumn();
            id.DataPropertyName = "Id";
            id.Name = "Id";
            id.HeaderText = "Id";
            id.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            id.DefaultCellStyle.Format = Constant.WHOLE_NUMBER;
            id.Width = 65;
            id.ReadOnly = true;
            id.SortMode = DataGridViewColumnSortMode.NotSortable;
            id.Visible = true;
            d.Columns.Add(id);

            // Title
            var title = new DataGridViewTextBoxColumn();
            title.DataPropertyName = "Title";
            title.Name = "Title";
            title.HeaderText = "Title";
            title.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            title.Width = 200;
            title.ReadOnly = true;
            title.SortMode = DataGridViewColumnSortMode.NotSortable;
            title.Visible = true;
            d.Columns.Add(title);

            // Description
            var tick = new DataGridViewTextBoxColumn();
            tick.DataPropertyName = "Description";
            tick.Name = "Description";
            tick.HeaderText = "Description";
            tick.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tick.Width = 250;
            tick.ReadOnly = true;
            tick.SortMode = DataGridViewColumnSortMode.NotSortable;
            tick.Visible = true;
            d.Columns.Add(tick);

            // TradeCap
            var cap = new DataGridViewTextBoxColumn();
            cap.DataPropertyName = "TradeCap";
            cap.Name = "TradeCap";
            cap.HeaderText = "Trade Cap";
            cap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cap.DefaultCellStyle.Format = Constant.WHOLE_NUMBER;
            cap.Width = 80;
            cap.ReadOnly = true;
            cap.SortMode = DataGridViewColumnSortMode.NotSortable;
            cap.Visible = true;
            d.Columns.Add(cap);

            // IsOpen
            var open = new DataGridViewCheckBoxColumn();
            open.DataPropertyName = "IsOpen";
            open.Name = "IsOpen";
            open.HeaderText = "IsOpen";
            open.Width = 65;
            open.ReadOnly = true;
            open.SortMode = DataGridViewColumnSortMode.NotSortable;
            open.Visible = true;
            d.Columns.Add(open);

        }
        #endregion

    }
}
