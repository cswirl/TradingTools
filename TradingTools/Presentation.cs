using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Trunk;

namespace TradingTools
{
    class Presentation
    {
        private const int SECOND = 1000;
        public const int INTERNAL_TIMER_REFRESH_VALUE = SECOND * 60;

        public static void DateTimePicker_MaxDate_SafeAssign(DateTimePicker dtp, DateTime d)
        {
            dtp.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
            dtp.Value = d;
        }
    }

    public static class BandColor
    {
        public static readonly Color Default = Color.MediumSeaGreen;
        public static readonly Color Empty = Color.Orange;
        public static readonly Color Loaded = Color.Blue;
        public static readonly Color TradeOpen = Default;
        public static readonly Color TradeClosed = Default;
    }

    public static class DataGridViewFormatExtensions
    {
        public static void Format_Common(this DataGridView d)
        {
            d.DefaultCellStyle.SelectionForeColor = Color.White;
            d.AutoGenerateColumns = false;
            d.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
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
            id.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            id.DefaultCellStyle.Format = Constant.WHOLE_NUMBER;
            id.Width = 65;
            id.ReadOnly = true;
            id.SortMode = DataGridViewColumnSortMode.NotSortable;
            id.Visible = true;
            d.Columns.Add(id);

            // Ticker
            var tick = new DataGridViewTextBoxColumn();
            tick.DataPropertyName = "Ticker";
            tick.Name = "Ticker";
            tick.HeaderText = "Ticker";
            tick.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tick.Width = 80;
            tick.ReadOnly = true;
            tick.SortMode = DataGridViewColumnSortMode.NotSortable;
            tick.Visible = true;
            d.Columns.Add(tick);

            // Side
            var Side = new DataGridViewTextBoxColumn();
            Side.DataPropertyName = "Side";
            Side.Name = "Side";
            Side.HeaderText = "Side";
            Side.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Side.Width = 50;
            Side.ReadOnly = true;
            Side.SortMode = DataGridViewColumnSortMode.NotSortable;
            Side.Visible = true;
            d.Columns.Add(Side);
        }

        public static void Format_Trade_Common(this DataGridView d)
        {
            // Capital
            var cap = new DataGridViewTextBoxColumn();
            cap.DataPropertyName = "Capital";
            cap.Name = "Capital";
            cap.HeaderText = "Capital";
            cap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            cap.DefaultCellStyle.Format = Constant.MONEY_FORMAT;
            cap.Width = 80;
            cap.ReadOnly = true;
            cap.SortMode = DataGridViewColumnSortMode.NotSortable;
            cap.Visible = true;
            d.Columns.Add(cap);
        }

        public static void Format_Trade_Active(this DataGridView d)
        {
            d.Format_Common();
            d.Format_Trade_Common();

            // Date Enter
            var dateEnter = new DataGridViewTextBoxColumn();
            dateEnter.DataPropertyName = "DateEnter";
            dateEnter.Name = "DateEnter";
            dateEnter.HeaderText = "Date Enter";
            dateEnter.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dateEnter.DefaultCellStyle.Format = Constant.DATE_MMMM_DD_YYYY;
            dateEnter.Width = 120;
            dateEnter.ReadOnly = true;
            dateEnter.SortMode = DataGridViewColumnSortMode.NotSortable;
            dateEnter.Visible = true;
            d.Columns.Add(dateEnter);

            // Day Count
            var days = new DataGridViewTextBoxColumn();
            days.DataPropertyName = "DayCount";
            days.Name = "DayCount";
            days.HeaderText = "Days";
            days.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            days.DefaultCellStyle.Format = Constant.WHOLE_NUMBER;
            days.Width = 40;
            days.ReadOnly = true;
            days.SortMode = DataGridViewColumnSortMode.NotSortable;
            days.Visible = true;
            d.Columns.Add(days);
        }

        public static void Format_Trade_Closed(this DataGridView d)
        {
            d.Format_Common();
            d.Format_Trade_Common();

            // Final Capital
            var fcap = new DataGridViewTextBoxColumn();
            fcap.DataPropertyName = "FinalCapital";
            fcap.Name = "FinalCapital";
            fcap.HeaderText = "F. Capital";
            fcap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            fcap.DefaultCellStyle.Format = Constant.MONEY_FORMAT;
            fcap.Width = 80;
            fcap.ReadOnly = true;
            fcap.SortMode = DataGridViewColumnSortMode.NotSortable;
            fcap.Visible = true;
            d.Columns.Add(fcap);

            // PnL
            var PnL = new DataGridViewTextBoxColumn();
            PnL.DataPropertyName = "PnL";
            PnL.Name = "PnL";
            PnL.HeaderText = "PnL";
            PnL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            PnL.DefaultCellStyle.Format = Constant.MONEY_FORMAT;
            PnL.Width = 80;
            PnL.ReadOnly = true;
            PnL.SortMode = DataGridViewColumnSortMode.NotSortable;
            PnL.Visible = true;
            d.Columns.Add(PnL);

            // PnL %
            var PnL_percentage = new DataGridViewTextBoxColumn();
            PnL_percentage.DataPropertyName = "PnL_Percentage";
            PnL_percentage.Name = "PnL_Percentage";
            PnL_percentage.HeaderText = "PnL %";
            PnL_percentage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PnL_percentage.DefaultCellStyle.Format = Constant.PERCENTAGE_FORMAT_SINGLE;
            PnL_percentage.Width = 80;
            PnL_percentage.ReadOnly = true;
            PnL_percentage.SortMode = DataGridViewColumnSortMode.NotSortable;
            PnL_percentage.Visible = true;
            d.Columns.Add(PnL_percentage);

            // Date Enter
            var dateEnter = new DataGridViewTextBoxColumn();
            dateEnter.DataPropertyName = "DateEnter";
            dateEnter.Name = "DateEnter";
            dateEnter.HeaderText = "Date Enter";
            dateEnter.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dateEnter.DefaultCellStyle.Format = Constant.DATE_MMMM_DD_YYYY;
            dateEnter.Width = 120;
            dateEnter.ReadOnly = true;
            dateEnter.SortMode = DataGridViewColumnSortMode.NotSortable;
            dateEnter.Visible = true;
            d.Columns.Add(dateEnter);

            // Date Enter
            var dateExit = new DataGridViewTextBoxColumn();
            dateExit.DataPropertyName = "DateExit";
            dateExit.Name = "DateExit";
            dateExit.HeaderText = "Date Exit";
            dateExit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dateExit.DefaultCellStyle.Format = Constant.DATE_MMMM_DD_YYYY;
            dateExit.Width = 120;
            dateExit.ReadOnly = true;
            dateExit.SortMode = DataGridViewColumnSortMode.NotSortable;
            dateExit.Visible = true;
            d.Columns.Add(dateExit);

            // Day Count
            var days = new DataGridViewTextBoxColumn();
            days.DataPropertyName = "DayCount";
            days.Name = "DayCount";
            days.HeaderText = "Days";
            days.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            days.DefaultCellStyle.Format = Constant.WHOLE_NUMBER;
            days.Width = 40;
            days.ReadOnly = true;
            days.SortMode = DataGridViewColumnSortMode.NotSortable;
            days.Visible = true;
            d.Columns.Add(days);
        }
    }


}