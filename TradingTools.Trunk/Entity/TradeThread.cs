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
        [Required]
        public int TradeChallengeId { get; set; }
        public virtual TradeChallenge TradeChallenge { get; set; }

        [Required, DefaultValue(0)]
        public int TradeId_tail { get; set; }
        public virtual Trade Trade_tail { get; set; }

        [Required]
        public int TradeId_head { get; set; }
        public virtual Trade Trade_head { get; set; }
        
    }
}
