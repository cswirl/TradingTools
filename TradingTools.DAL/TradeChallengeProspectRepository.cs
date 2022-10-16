using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk;
using TradingTools.Trunk.Contracts;
using TradingTools.Trunk.Entity;

namespace TradingTools.Repository
{
    internal sealed class TradeChallengeProspectRepository : RepositoryBase<TradeChallengeProspect>, ITradeChallengeProspectRepository
    {
        public TradeChallengeProspectRepository(TradingToolsDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public TradeChallengeProspect Delete(CalculatorState calculatorState)
        {
            var tcp = RepositoryContext.TradeChallengeProspect
                .Where(tcp => tcp.CalculatorStateId == calculatorState.Id)
                .AsNoTracking()
                .FirstOrDefault();
            // when there's nothing to remove - just return success/true
            if (tcp == default) return default;

            RepositoryContext.TradeChallengeProspect.Remove(tcp);
            RepositoryContext.SaveChanges();

            return tcp;
        }

        public void DeleteBatch(TradeChallengeProspect[] tcp)
        {
            RepositoryContext.TradeChallengeProspect.RemoveRange(tcp);
            RepositoryContext.SaveChanges();
        }

        public IEnumerable<CalculatorState> GetAllByTradeChallenge(int tradeChallengeId, bool descending = false)
        {
            var c = RepositoryContext.CalculatorState
                .Include(c => c.TradeChallengeProspect)
                .Where(c => c.TradeChallengeProspect.TradeChallengeId == tradeChallengeId
                    && c.TradeId == default)
                .AsQueryable();

            if (descending) c = c.OrderByDescending(c => c.TradeId);

            return c.ToList();
        }

        public int GetTradeChallengeId(int calculatorStateId)
        {
            var p = RepositoryContext.TradeChallengeProspect
                .Include(tcp => tcp.CalculatorState)
                .Where(tcp => tcp.CalculatorStateId == calculatorStateId)
                .FirstOrDefault();

            if (p != default)
                return p.TradeChallengeId;

            return 0;
        }


    }
}
