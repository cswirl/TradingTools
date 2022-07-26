using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Helpers
{
    public class DelegateHandlers
    {
        public master Master { get; set; }
        public DelegateHandlers(master m)
        {
            this.Master = m;
        }
        public bool Trade_Officializing_Cancelled(CalculatorState c, out string msg)
        {
            msg = "";

            var tradeChallengeId = Master.TradeChallengeProspect_GetTradeChallengeId(c.Id);
            if (tradeChallengeId > 0)
            {
                msg = $"This Risk/Reward Calculator belongs to Trade Challenge: {tradeChallengeId}" +
                    $"\n\nYou need to re-open this form in the Trade Challenge window";
                return true;
            }
            return false;
        }

    }
}
