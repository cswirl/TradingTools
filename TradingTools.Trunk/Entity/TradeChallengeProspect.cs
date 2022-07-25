using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Trunk.Entity
{
    public class TradeChallengeProspect
    {
        public int Id { get; set; }
        public int TradeChallengeId { get; set; }
        public TradeChallenge TradeChallenge { get; set; }
        public int CalculatorStateId { get; set; }
        public virtual CalculatorState CalculatorState { get; set; }
    }
}
