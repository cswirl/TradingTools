using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Repository
{
    public class CalculatorStateDAO
    {
        private TradingToolsDbContext _dbContext;

        public CalculatorStateDAO()
        {
            _dbContext = new();
        }

        public BindingList<CalculatorState> RetrieveBindingList()
        {
            Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load(_dbContext.CalculatorState);
            return _dbContext.CalculatorState.Local.ToBindingList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Add(CalculatorState calculatorState)
        {
            // Use ApplicationDbContext here
            _dbContext.CalculatorState.Add(calculatorState);
            _dbContext.SaveChanges();
        }

        public void Update(CalculatorState calculatorState)
        {
            _dbContext.CalculatorState.Update(calculatorState);
            _dbContext.SaveChanges();
        }

        public void Delete(CalculatorState calculatorState)
        {
            _dbContext.CalculatorState.Remove(calculatorState);
            _dbContext.SaveChanges();
        }
    }
}
