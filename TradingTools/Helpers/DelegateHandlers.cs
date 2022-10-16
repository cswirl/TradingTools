using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Services.Interface;
using TradingTools.Trunk.Entity;

namespace TradingTools.Helpers
{
    public class DelegateHandlers
    {
        public master Master { get; set; }
        private IServiceManager _service;
        public DelegateHandlers(master master)
        {
            this.Master = master;
            _service = master.ServiceManager;
        }
        public bool CalculatorState_Officializing_IsCancelled_Handler(CalculatorState c, out string msg)
        {
            msg = "";

            var tradeChallengeId = _service.TradeChallengeProspectService.GetTradeChallengeId(c.Id);
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

            var tradeChallenge = Master.ServiceManager.TradeChallengeService.GetTradeChallenge(t.Id);
            if (tradeChallenge != default)
            {
                msg = $"This Risk/Reward Calculator belongs to Trade Challenge Id: {tradeChallenge.Id}" +
                    $"\n\nYou need to re-open this form in the Trade Challenge window";
                return true;
            }
            return false;
        }

    }
}
