using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.DAL
{
    public class RiskRewardCalc_Long_DAL
    {
        public void SaveState(CalculatorState calcState)
        {
            // Use ApplicationDbContext here

        }

        public CalculatorState RetrieveState()
        {

            return new CalculatorState();
        }
    }
}
