using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Contracts;
using TradingTools.Trunk.Entity;

namespace TradingTools.Repository
{
    public class TradeRepository : RepositoryBase<Trade>, ITradeRepository
    {
        public TradeRepository(TradingToolsDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Trade> GetAll(bool descending = false)
        {
            var trade = FindByCondition(t => !t.IsDeleted)
                .Include(t => t.CalculatorState)
                .AsQueryable();

            if (descending) trade = trade.OrderByDescending(t => t.Id);

            return trade.ToList();
        }

        public IEnumerable<Trade> GetStatusOpen(bool descending)
        {
            var trade = FindByCondition(x => x.Status.Equals("open") && !x.IsDeleted)
                .Include(t => t.CalculatorState)
                .AsQueryable();

            if (descending) trade = trade.OrderByDescending(t => t.Id);

            return trade.ToList();
        }

        public IEnumerable<Trade> GetStatusClosed(bool descending)
        {
            var trade = FindByCondition(x => x.Status.Equals("closed") && !x.IsDeleted)
                .Include(t => t.CalculatorState)
                .AsQueryable();

            if (descending) trade = trade.OrderByDescending(t => t.Id);

            return trade.ToList();
        }

        public IEnumerable<Trade> GetDeleted(bool descending)
        {
            var trade = FindByCondition(x => x.IsDeleted)
                .Include(t => t.CalculatorState)
                .AsQueryable();

            if (descending) trade = trade.OrderByDescending(t => t.Id);

            return trade.ToList();
        }

        public IEnumerable<string> GetTickers() => FindAll().Select(t => t.Ticker.ToUpper());
    }
}
