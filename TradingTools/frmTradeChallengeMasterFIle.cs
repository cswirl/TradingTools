using System;
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
        private List<frmTradeChallenge> _listOf_frmTradeChallenge;
        private master _master { get { return (master)this.Owner; } }

        private BindingList<TradeChallenge> _currentTradeChallenges;
        private BindingList<TradeChallenge> _closedTradeChallenges;

        public frmTradeChallengeMasterFile()
        {
            InitializeComponent();

            _listOf_frmTradeChallenge = new();
            DataGridViewFormat_Common(dgvOpen);
            DataGridViewFormat_Common(dgvClosed);
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
               _currentTradeChallenges.Insert(0,tc);
                TradeChallenge_Spawn(tc);
            }
        }

        private void registerForm(frmTradeChallenge form)
        {
            _master.TradeChallenge_Updated += this.TradeChallenge_Updated;
            _master.TradeChallenge_Closed += this.TradeChallenge_Closed;
            _master.TradeChallenge_Deleted += this.TradeChallenge_Deleted;

            form.FormClosed += (object sender, FormClosedEventArgs e)
                => { _listOf_frmTradeChallenge.Remove((frmTradeChallenge)sender); };

            _listOf_frmTradeChallenge.Add(form);
        }

        private void TradeChallenge_Updated(TradeChallenge tc)
        {
            if (_currentTradeChallenges.Contains(tc)) dgvOpen.Invalidate();
            else if (_closedTradeChallenges.Contains(tc)) dgvClosed.Invalidate();
        }

        private void TradeChallenge_Closed(TradeChallenge tc)
        {
            if (_currentTradeChallenges.Contains(tc))
            {
                _currentTradeChallenges.Remove(tc);
                _closedTradeChallenges.Insert(0,tc);
            }
        }

        private void TradeChallenge_Deleted(TradeChallenge tc)
        {
            if (_currentTradeChallenges.Contains(tc)) 
                _currentTradeChallenges.Remove(tc);

            else if (_closedTradeChallenges.Contains(tc))
                _closedTradeChallenges.Remove(tc);
        }

        private void TradeChallenge_Spawn(TradeChallenge tc)
        {
            var form = new frmTradeChallenge(this._master)
            {
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

        private void frmTradeChallengeMasterFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                // let the form close gracefully
            }
            else
            {
                DialogResult objDialog = MessageBox.Show("Confirm to close this form ?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (objDialog == DialogResult.Cancel)
                {
                    // keep the form open
                    e.Cancel = true;
                }
                else if (objDialog == DialogResult.OK)
                {
                    // let the form close gracefully
                }
            }
            
        }
    }
}
