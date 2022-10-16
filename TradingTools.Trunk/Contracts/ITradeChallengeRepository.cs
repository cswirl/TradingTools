using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Trunk.Contracts
{
    public interface ITradeChallengeRepository
    {
        // Trade Thread
        IEnumerable<Trade> GetAllTrades(int tradeChallengeId);
        IEnumerable<Trade> GetActiveTrades(int tradeChallengeId);
        IEnumerable<Trade> GetTradeHistory(int tradeChallengeId, bool descending = false);
        TradeChallenge GetTradeChallenge(int tradeId);

        //
        IEnumerable<TradeChallenge> GetStatusOpen(bool descending);
        IEnumerable<TradeChallenge> GetStatusClosed(bool descending);
        void Create(TradeChallenge tradeChallenge);
        void Update(TradeChallenge tradeChallenge);
        //void Delete(TradeChallenge tradeChallenge);
    }
}
