using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Contracts;
using TradingTools.Trunk.Entity;
using Microsoft.EntityFrameworkCore;


namespace TradingTools.Repository
{
    internal sealed class CalculatorStateRepository : RepositoryBase<CalculatorState>, ICalculatorStateRepository
    {
        public CalculatorStateRepository(TradingToolsDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<CalculatorState> GetAllProspects(bool descending = false)
        {
            var c = FindAll(false).Where(x => x.TradeId == null);

            if (descending) c = c.OrderByDescending(c => c.Id);

            return c.ToList();
        }

    }
}
