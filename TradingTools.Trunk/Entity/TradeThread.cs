using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Trunk.Entity
{
    public class TradeThread
    {
        public int Id { get; set; }

        public int? TradeChallengeId { get; set; }
        public virtual TradeChallenge TradeChallenge { get; set; }

        public int? TradeId { get; set; }
        public virtual Trade Trade { get; set; }
        
    }
}
