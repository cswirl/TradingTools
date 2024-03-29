﻿using System;
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
        TradeChallenge GetTradeChallenge(Trade trade, bool deleted = false);

        // Prospects
        TradeChallenge GetTradeChallenge(CalculatorState calcState, bool deleted = false);

        //
        IList<TradeChallenge> GetStatusOpen(bool descending);
        IList<TradeChallenge> GetStatusClosed(bool descending);
        bool Create(TradeChallenge tradeChallenge);
        bool Update(TradeChallenge tradeChallenge);

        bool Close(TradeChallenge tradeChallenge);
        bool Delete(TradeChallenge tradeChallenge);
    }
}
