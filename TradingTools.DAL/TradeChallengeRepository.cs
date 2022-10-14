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
    }
}
