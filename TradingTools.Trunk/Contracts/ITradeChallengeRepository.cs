using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Trunk.Contracts
{
    public interface ITradeChallengeRepository
    {
        IEnumerable<TradeChallenge> GetStatusOpen(bool descending);
        IEnumerable<TradeChallenge> GetStatusClosed(bool descending);
        void Create(TradeChallenge tradeChallenge);
        void Update(TradeChallenge tradeChallenge);
        //void Delete(TradeChallenge tradeChallenge);
    }
}
