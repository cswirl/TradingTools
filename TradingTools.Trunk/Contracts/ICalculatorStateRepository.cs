using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Trunk.Contracts
{
    public interface ICalculatorStateRepository
    {
        IEnumerable<CalculatorState> GetAllProspects(bool descending);
        void Create(CalculatorState calculatorState);
        void Update(CalculatorState calculatorState);
        void Delete(CalculatorState calculatorState);
    }
}
