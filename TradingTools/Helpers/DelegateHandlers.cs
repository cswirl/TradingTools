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
            if (tradeChallengeId == 0) return false;

            var _activeTrades = Master.TradeThread_GetActiveTrade(tradeChallengeId);
            if (_activeTrades == default) return false;
            
            if (_activeTrades.Count > 0)
            {
                msg = $"This Risk/Reward Calculator belongs to Trade Challenge: {tradeChallengeId}" +
                    $"\n\nYou must first closed its Active Trade with Id: {_activeTrades.First().Id}";
                return true;
            }
            return false;
        }

    }
}
