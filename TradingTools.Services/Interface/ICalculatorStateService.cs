using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;

namespace TradingTools.Services.Interface
{
    public interface ICalculatorStateService
    {
        IList<CalculatorState> GetAllProspects(bool descending);
        bool Add(CalculatorState calculatorState);
        bool Update(CalculatorState calculatorState);
        bool Delete(CalculatorState calculatorState);
    }
}
