using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Validation;

namespace TradingTools.Trunk
{
    public class Trade_Utility
    {
        public static decimal GetTrading_ElaspsedTime_Days(DateTime dateEnter, DateTime? dateExit)
        {
            DateTime exit = dateExit ?? dateEnter;  // Deliberately result to zero
            var days = (exit - dateEnter).TotalDays;
            return SafeConvert.ToDecimalSafe(days);
        }
    }
}
