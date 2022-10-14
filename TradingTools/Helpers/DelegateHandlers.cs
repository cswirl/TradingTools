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
        public bool CalculatorState_Officializing_IsCancelled_Handler(CalculatorState c, out string msg)
        {
            msg = "";

            var tradeChallengeId = Master.TradeChallengeProspect_GetTradeChallengeId(c.Id);
            if (tradeChallengeId > 0)
            {
                msg = $"This Risk/Reward Calculator belongs to Trade Challenge Id: {tradeChallengeId}" +
                    $"\n\nYou need to re-open this form in the Trade Challenge window";
                return true;
            }
            return false;
        }

        public bool Trade_Closing_IsCancelled_Handler(Trade t, out string msg)
        {
            msg = "";

            var tradeChallengeId = Master.TradeThread_GetTradeChallengeId(t.Id);
            if (tradeChallengeId > 0)
            {
                msg = $"This Risk/Reward Calculator belongs to Trade Challenge Id: {tradeChallengeId}" +
                    $"\n\nYou need to re-open this form in the Trade Challenge window";
                return true;
            }
            return false;
        }

    }
}
