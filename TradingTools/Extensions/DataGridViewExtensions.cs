using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingTools.Trunk.Extensions;

namespace TradingTools.Extensions
{
    public static class DataGridViewExtensions
    {
        public static void Pnl_ApplyBackColor(this DataGridView dgv, int columnIndex)
        {
            var profitStyle = new DataGridViewCellStyle();
            profitStyle.BackColor = Color.Green;
            profitStyle.ForeColor = Color.White;

            var lossStyle = new DataGridViewCellStyle();
            lossStyle.BackColor = Color.Red;
            lossStyle.ForeColor = Color.White;

            decimal pnl;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                pnl = dgv.Rows[i].Cells[columnIndex].Value.ToString().ToDecimal();
                if (pnl > 0)
                    dgv.Rows[i].Cells[columnIndex].Style = profitStyle;
                else if (pnl < 0)
                    dgv.Rows[i].Cells[columnIndex].Style = lossStyle;
            }
        }
    }
}
