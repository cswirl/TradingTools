using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;

namespace TradingTools.Services.Extensions
{
    public static class TradeExtensions
    {
        public static decimal DaysElapsed(this Trade t)
        {
            return Formula.DayCount(t.DateEnter, t.DateExit ?? DateTime.Now);
        }
    }
}
