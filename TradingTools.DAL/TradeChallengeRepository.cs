﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Contracts;
using TradingTools.Trunk.Entity;

namespace TradingTools.Repository
{
    public class TradeChallengeRepository : RepositoryBase<TradeChallenge>, ITradeChallengeRepository
    {
        public TradeChallengeRepository(TradingToolsDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<TradeChallenge> GetStatusOpen(bool descending = false)
        {
            var tc = FindByCondition(x => x.IsOpen && !x.IsDeleted).AsQueryable();

            if (descending) tc = tc.OrderByDescending(t => t.Id);

            return tc.ToList();
        }

        public IEnumerable<TradeChallenge> GetStatusClosed(bool descending = false)
        {
            var trade = FindByCondition(x => !x.IsOpen && !x.IsDeleted).AsQueryable();

            if (descending) trade = trade.OrderByDescending(t => t.Id);

            return trade.ToList();
        }

        public IEnumerable<Trade> GetAllTrades(int tradeChallengeId)
        {
            return RepositoryContext.Trade
                .Include(x => x.CalculatorState)
                .Include(x => x.TradeThread)
                .Where(x => !x.IsDeleted && x.TradeThread.TradeChallengeId == tradeChallengeId)
                .AsNoTracking();
        }

        public IEnumerable<Trade> GetActiveTrades(int tradeChallengeId)
        {
            return RepositoryContext.Trade
                .Include(x => x.CalculatorState)
                .Include(x => x.TradeThread).ThenInclude(tr => tr.TradeChallenge)
                .Where(x => !x.IsDeleted && x.Status.Equals("open") && x.TradeThread.TradeChallengeId == tradeChallengeId)
                .AsNoTracking();
        }

        public IEnumerable<Trade> GetTradeHistory(int tradeChallengeId, bool descending = false)
        {
            var tradeHist = RepositoryContext.Trade
                .Include(x => x.CalculatorState)
                .Include(x => x.TradeThread).ThenInclude(tr => tr.TradeChallenge)
                .Where(x => !x.IsDeleted && x.Status.Equals("closed") && x.TradeThread.TradeChallengeId == tradeChallengeId)
                .AsNoTracking();

            if (descending) tradeHist = tradeHist.OrderByDescending(x => x.DateExit);

            return tradeHist;
        }

        public TradeChallenge GetTradeChallenge(int tradeId)
        {
            return RepositoryContext.TradeThread
                .Where(tr => tr.TradeId == tradeId)
                .AsNoTracking()
                .Select(x => x.TradeChallenge)
                .FirstOrDefault();
        }
    }
}
