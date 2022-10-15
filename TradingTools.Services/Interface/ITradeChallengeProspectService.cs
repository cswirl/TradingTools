using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Services.Interface
{
    public interface ITradeChallengeProspectService
    {
        IList<CalculatorState> GetAllByTradeChallenge(int tradeChallengeId, bool descending = false);
        int GetTradeChallengeId(int calculatorStateId);
        bool Create(TradeChallengeProspect tradeChallengeProspect);
        TradeChallengeProspect Delete(CalculatorState calculatorState);
        bool DeleteBatch(TradeChallengeProspect[] tcp);
    }
}
