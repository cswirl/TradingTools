using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Extensions;
using TradingTools.Trunk.Validation;

namespace TradingTools.Trunk
{
    public class TradeUtility
    {
        public static decimal GetTrading_ElaspsedTime_Days(DateTime dateEnter, DateTime dateExit)
        {
            var days = (dateExit - dateEnter).TotalDays;
            return days.ToDecimalSafe();
        }
    }
}
