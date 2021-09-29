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
        private TradingToolsDbContext _dbContext;

        public RiskRewardCalc_Long_DAL()
        {
            _dbContext = new();
        }

        public void SaveState(CalculatorState calcState)
        {
            // Use ApplicationDbContext here
            _dbContext.CalculatorStates.Add(calcState);
            _dbContext.SaveChanges();

        }

        public CalculatorState RetrieveState()
        {

            return new CalculatorState();
        }
    }
}
