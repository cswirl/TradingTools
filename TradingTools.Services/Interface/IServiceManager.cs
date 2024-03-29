﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Services.Interface
{
    public interface IServiceManager
    {
        ICalculatorStateService CalculatorStateService { get; }
        ITradeService TradeService { get; }
        ITradeChallengeService TradeChallengeService { get; }
        ITradeChallengeProspectService TradeChallengeProspectService { get; }
    }
}
