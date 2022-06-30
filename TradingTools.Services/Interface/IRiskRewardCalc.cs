using System.Collections.Generic;
using TradingTools.Services.Dto;
using TradingTools.Services.Models;
using TradingTools.Trunk;

namespace TradingTools.Services.Interface
{
    public interface IRiskRewardCalc
    {
        void ComputeLoss(decimal exitPrice);
        PnLRecord ComputeProfit(decimal exitPrice);
        TradeExitDto ComputeTradeExit(decimal exitPrice, Position position);
        IList<PnLRecord> GenerateLossesTable(Position position);
        IList<PnLRecord> GenerateProfitsTable(Position position);
    }
}