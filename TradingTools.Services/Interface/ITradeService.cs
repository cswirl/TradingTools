using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Services.Interface
{
    public interface ITradeService
    {
        IList<Trade> GetAll(bool descending);
        IList<Trade> GetStatusOpen(bool descending);
        IList<Trade> GetStatusClosed(bool descending);
        IList<Trade> GetDeleted(bool descending);
        IEnumerable<string> GetTickers();
        bool Create(Trade trade);
        bool Update(Trade trade);
        bool Close(Trade trade);
        bool Delete(Trade trade);
    }
}
