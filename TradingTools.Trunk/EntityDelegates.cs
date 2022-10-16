using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.Trunk
{
    public class EntityDelegates
    {
    }

    public delegate void CalculatorStateCreated(CalculatorState c);
    public delegate void CalculatorStateUpdated(CalculatorState c);
    public delegate void CalculatorStateDeleted(CalculatorState c);

    public delegate bool CalculatorStateUpdating(CalculatorState c, out string msg);
    //
    public delegate void TradeCreated(Trade t);
    public delegate void TradeUpdated(Trade t);
    public delegate void TradeDeleted(Trade t);

    public delegate bool TradeUpdating(Trade t, out string msg);
    public delegate void TradeOfficializing(Trade t);
    //
    public delegate void TradeChallengeCreated(TradeChallenge tc);
    public delegate void TradeChallengeUpdated(TradeChallenge tc);
    public delegate void TradeChallengeDeleted(TradeChallenge tc);
    //
    public delegate void TradeThreadCreated(TradeThread tr);
    public delegate void TradeThreadUpdated(TradeThread tr);
    public delegate void TradeThreadDeleted(TradeThread tr);
    //
    public delegate void TradeChallengeProspectCreated(TradeChallengeProspect tcp);
    public delegate void TradeChallengeProspectUpdated(TradeChallengeProspect tcp);
    public delegate void TradeChallengeProspectDeleted(TradeChallengeProspect tcp);
    public delegate void TradeChallengeProspectDeletedRange(TradeChallengeProspect[] tcp);

}
