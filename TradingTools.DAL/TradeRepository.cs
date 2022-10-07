﻿using System;
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
    }
}
