using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.DAL
{
    public class CalculatorStateDAO
    {
        private TradingToolsDbContext _dbContext;

        public CalculatorStateDAO()
        {
            _dbContext = new();
        }

        //public List<CalculatorState> RetrieveList()
        //{

        //    return _dbContext.CalculatorStates.ToList();
        //}

        public BindingList<CalculatorState> RetrieveBindingList()
        {
            Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load(_dbContext.CalculatorStates);
            return _dbContext.CalculatorStates.Local.ToBindingList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
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
