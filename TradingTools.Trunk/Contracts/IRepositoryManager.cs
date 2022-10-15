using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Trunk.Contracts
{
    public interface IRepositoryManager
    {
        ITradeRepository Trade { get; }
        ICalculatorStateRepository CalculatorState { get; }
        ITradeChallengeRepository TradeChallenge { get; }  
        ITradeChallengeProspectRepository TradeChallengeProspect { get; }

        public void Save();
    }
}
