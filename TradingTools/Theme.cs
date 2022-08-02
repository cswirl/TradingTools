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
    public interface ISideTheme
    {
        // Properties
        string Title { get; }
        Color Default { get; }
        Color Empty { get; }
        Color Loaded { get; }
        Color TradeOpen { get; }
        Color TradeClosed { get; }
        

        // Methods
        void Format_PriceDecreaseTable(DataGridView d);
        void Format_PriceIncreaseTable(DataGridView d);
    }

    public class Theme
    {
        public static ISideTheme SideTheme_GetInstance(string side)
        {
            if (side == "long")
                return new LongTheme();
            else
                return new ShortTheme();
        }

        public void Format_PCPTable(DataGridView d)
        {
            //d.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            d.DefaultCellStyle.SelectionForeColor = Color.White;
            d.AutoGenerateColumns = false;
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.AllowUserToOrderColumns = true;
            d.AllowUserToDeleteRows = false;
            d.AllowUserToAddRows = false;
            d.AllowUserToResizeColumns = false;
            d.AllowUserToResizeRows = false;
            d.ReadOnly = true;
            d.MultiSelect = false;
            d.Font = new Font(new FontFamily("tahoma"), 8.5f, FontStyle.Regular);
            d.Columns.Clear();

            // PCP
            var pcp = new DataGridViewTextBoxColumn();
            pcp.DataPropertyName = "PCP";
            pcp.Name = "PCP";
            pcp.HeaderText = "PCP";
            pcp.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            pcp.DefaultCellStyle.Format = Constant.PERCENTAGE_FORMAT_NONE;
            pcp.Width = 80;
            pcp.ReadOnly = true;
            pcp.SortMode = DataGridViewColumnSortMode.NotSortable;
            pcp.Visible = true;
            d.Columns.Add(pcp);
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
            // PnL
            var PnL = new DataGridViewTextBoxColumn();
            PnL.DataPropertyName = "PnL";
            PnL.Name = "PnL";
            PnL.HeaderText = "PnL";
            PnL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            PnL.DefaultCellStyle.Format = Constant.MONEY_FORMAT;
            PnL.Width = 100;
            PnL.ReadOnly = true;
            PnL.SortMode = DataGridViewColumnSortMode.NotSortable;
            PnL.Visible = true;
            d.Columns.Add(PnL);
            // ExitPrice
            var exitPrice = new DataGridViewTextBoxColumn();
            exitPrice.DataPropertyName = "ExitPrice";
            exitPrice.Name = "ExitPrice";
            exitPrice.HeaderText = "Exit Price";
            exitPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            exitPrice.DefaultCellStyle.Format = Constant.DECIMAL_UPTO_MAX;
            exitPrice.DefaultCellStyle.ForeColor = Color.White;
            exitPrice.DefaultCellStyle.Font = new Font(new FontFamily("tahoma"), 8f, FontStyle.Bold);
            exitPrice.DefaultCellStyle.BackColor = Color.SteelBlue;
            //exitPrice.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            //exitPrice.DefaultCellStyle.SelectionForeColor = Color.White;
            exitPrice.Width = 100;
            exitPrice.ReadOnly = true;
            exitPrice.SortMode = DataGridViewColumnSortMode.NotSortable;
            exitPrice.Visible = true;
            d.Columns.Add(exitPrice);
        }
    }

    public class LongTheme : Theme, ISideTheme
    {
        string ISideTheme.Title => "Long";
        Color ISideTheme.Default => Color.MediumSeaGreen;
        Color ISideTheme.Empty => Color.Orange;
        Color ISideTheme.Loaded => Color.Blue;
        Color ISideTheme.TradeOpen => Color.MediumSeaGreen;
        Color ISideTheme.TradeClosed => Color.MediumSeaGreen;

        //Color ISideTheme.StateBandColor => throw new NotImplementedException();

        public void Format_PriceIncreaseTable(DataGridView d)
        {
            d.DefaultCellStyle.BackColor = Color.Green;
            d.DefaultCellStyle.ForeColor = Color.White;

            Format_PCPTable(d);
        }

        public void Format_PriceDecreaseTable(DataGridView d)
        {
            d.DefaultCellStyle.BackColor = Color.Red;
            d.DefaultCellStyle.ForeColor = Color.White;
            Format_PCPTable(d);
        }
    }

    public class ShortTheme : Theme, ISideTheme
    {
        Color ISideTheme.Default => Color.Red;
        Color ISideTheme.Empty => Color.Yellow;
        Color ISideTheme.Loaded => Color.Blue;
        Color ISideTheme.TradeOpen => Color.Red;
        Color ISideTheme.TradeClosed => Color.Red;

        string ISideTheme.Title => "Short";

        public void Format_PriceIncreaseTable(DataGridView d)
        {
            d.DefaultCellStyle.BackColor = Color.Red;
            d.DefaultCellStyle.ForeColor = Color.White;

            Format_PCPTable(d);

            // change color of pcp
            var pcp = d.Columns["PCP"];
            pcp.DefaultCellStyle.BackColor = Color.Green;
        }

        public void Format_PriceDecreaseTable(DataGridView d)
        {
            d.DefaultCellStyle.BackColor = Color.Green;
            d.DefaultCellStyle.ForeColor = Color.White;

            Format_PCPTable(d);

            // change color of pcp
            var pcp = d.Columns["PCP"];
            pcp.DefaultCellStyle.BackColor = Color.Red;
        }
    }
}
