using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Services.Interface
{
    public interface ITradeChallengeService
    {
        // Trade Thread
        IList<Trade> GetAllTrades(int tradeChallengeId);
        IList<Trade> GetActiveTrades(int tradeChallengeId);
        IList<Trade> GetTradeHistory(int tradeChallengeId, bool descending = false);
        TradeChallenge GetTradeChallenge(int tradeId);
        bool CreateThread(TradeThread tr);
        //
        IList<TradeChallenge> GetStatusOpen(bool descending);
        IList<TradeChallenge> GetStatusClosed(bool descending);
        bool Create(TradeChallenge tradeChallenge);
        bool Update(TradeChallenge tradeChallenge);

        bool Close(TradeChallenge tradeChallenge);
        bool Delete(TradeChallenge tradeChallenge);
    }
}
