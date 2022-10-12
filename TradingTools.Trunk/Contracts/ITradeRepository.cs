using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Trunk.Contracts
{
    public interface ITradeRepository
    {
        IEnumerable<Trade> GetAll(bool descending);
        IEnumerable<Trade> GetStatusOpen(bool descending);
        IEnumerable<Trade> GetStatusClosed(bool descending);
        IEnumerable<Trade> GetDeleted(bool descending);
        IEnumerable<string> GetTickers();
        void Create(Trade trade);
        void Update(Trade trade);
        void Delete(Trade trade);
    }
}
