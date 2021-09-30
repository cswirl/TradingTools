using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;
using TradingTools.DAL;
using System.ComponentModel;

namespace TradingTools.Services
{
    public class CalculatorState_Serv
    {
        private TradingToolsDbContext _dbContext;       // not used - but worthy to consider using it

        public CalculatorState_Serv()
        {
            _dbContext = new();
        }
        public BindingList<CalculatorState> GetBindingList()
        {
            Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load(_dbContext.CalculatorStates);
            return _dbContext.CalculatorStates.Local.ToBindingList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        //// Should the DbContext internal collection of objects is captured
        //// - the functions below is no longer needed
        //// - And we now have a connected Context
        //public void Add(CalculatorState calculatorState)
        //{
        //    _rrc_DAO.Add(calculatorState);
        //}

        //public void Update(CalculatorState calculatorState)
        //{
        //    _rrc_DAO.Update(calculatorState);
        //}

        //public void Delete(CalculatorState calculatorState)
        //{
        //    _rrc_DAO.Delete(calculatorState);
        //}
    }
}
