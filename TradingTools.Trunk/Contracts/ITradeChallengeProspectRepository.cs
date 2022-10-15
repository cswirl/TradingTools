using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Trunk.Contracts
{
    public interface ITradeChallengeProspectRepository
    {
        IEnumerable<CalculatorState> GetAllByTradeChallenge(int tradeChallengeId, bool descending = false);
        int GetTradeChallengeId(int calculatorStateId);
        void Create(TradeChallengeProspect tradeChallengeProspect);
        TradeChallengeProspect Delete(CalculatorState calculatorState);
        void DeleteBatch(TradeChallengeProspect[] tcp);
    }
}
