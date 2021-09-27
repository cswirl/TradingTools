using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Trunk.Entity
{
    class CalculatorState
    {
        public decimal Capital { get; set; }
        public decimal Leverage { get; set; }
        public decimal EntryPriceAvg { get; set; }
        public int DayCount { get; set; }
        public decimal DailyInterestRate { get; set; }
        public decimal PriceIncreaseTarget { get; set; }
        public decimal PriceDecreaseTarget { get; set; }
        public decimal PEP_ExitPrice { get; set; }
        public string PEP_Note { get; set; }
        public decimal LEP_ExitPrice { get; set; }
        public string LEP_Note { get; set; }
        public string Ticker { get; set; }
        public string Strategy { get; set; }
        public string Note { get; set; }
    }
}
