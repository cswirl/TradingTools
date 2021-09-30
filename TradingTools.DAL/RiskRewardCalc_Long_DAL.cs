using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public List<CalculatorState> RetrieveList()
        {

            return _dbContext.CalculatorStates.ToList();
        }

        public BindingList<CalculatorState> RetrieveBindingList()
        {

            return _dbContext.CalculatorStates.Local.ToBindingList();
        }

        public void Add(CalculatorState calculatorState)
        {
            // Use ApplicationDbContext here
            _dbContext.CalculatorStates.Add(calculatorState);
            _dbContext.SaveChanges();
        }

        public void Update(CalculatorState calculatorState)
        {
            _dbContext.CalculatorStates.Update(calculatorState);
            _dbContext.SaveChanges();
        }

        public void Delete(CalculatorState calculatorState)
        {
            _dbContext.CalculatorStates.Remove(calculatorState);
            _dbContext.SaveChanges();
        }
    }
}
