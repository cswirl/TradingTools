using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Services.Models;

namespace TradingTools.Services.Extensions
{
    public static class PnLRecordExtensions
    {
        public static PnLRecord Short(this PnLRecord pnl)
        {
            // Invert the signs
            //pnl.PCP = -pnl.PCP;
            pnl.PnL_Percentage = -pnl.PnL_Percentage;
            pnl.PnL = -pnl.PnL;

            return pnl;
        }
    }
}
