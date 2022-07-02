using System;
using System.Collections.Generic;
using TradingTools.Services.Dto;
using TradingTools.Services.Models;
using TradingTools.Trunk;

namespace TradingTools.Services.Interface
{
    public interface IRiskRewardCalc
    {
        PnLRecord ComputePnL(decimal exitPrice, Position position);
        TradeExitDto ComputeTradeExit(decimal exitPrice, Position position);
        IList<PnLRecord> GenerateLossesTable(Position position);
        IList<PnLRecord> GenerateProfitsTable(Position position);
        Tuple<PnLRecord, decimal, decimal> PnlExitPlan(decimal exitPrice, Position position);
    }
}