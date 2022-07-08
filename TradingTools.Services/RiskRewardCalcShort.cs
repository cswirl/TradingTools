using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Services.Dto;
using TradingTools.Services.Extensions;
using TradingTools.Services.Interface;
using TradingTools.Services.Models;
using TradingTools.Trunk;

namespace TradingTools.Services
{
    public class RiskRewardCalcShort : IRiskRewardCalc
    {
        private readonly Position _position;
        public string Side { get; } = "short";

        public RiskRewardCalcShort()
        {

        }

        PnLRecord IRiskRewardCalc.ComputePnL(decimal exitPrice, Position position)
        {
            if (position == null) throw new NullReferenceException("Position object not set");
            return PnLRecord.Create(exitPrice, position.EntryPriceAvg, position.LotSize, position.Capital).Short();
        }

        Tuple<PnLRecord, decimal, decimal> IRiskRewardCalc.PnlExitPlan(decimal exitPrice, Position position)
        {
            PnLRecord rec = ((IRiskRewardCalc)this).ComputePnL(exitPrice, position);
            //var sPV = Formula.SpeculativePositionValue(position.LeveragedCapital, rec.PnL);
            var sPV = Formula.PositionValue(position.LotSize, exitPrice);
            var equity = Formula.SpeculativeAccountEquity(position.LeveragedCapital, rec.PnL,
                Formula.BorrowedAmount(position.Leverage, position.Capital));

            return new(rec, sPV, equity);
        }

        IList<PnLRecord> IRiskRewardCalc.GeneratePriceIncreaseTable(Position position)
        {
            decimal[] pcp = { 1m, 2m, 3m, 4m, 5m, 6, 7m, 8, 10m };

            var table = PnLTable.GenerateTable(position.EntryPriceAvg, position.LotSize,
                position.Capital, pcp).OrderByDescending(o => o.PCP).ToList();

            // Invert the signs of PnL for visual convenience
            foreach (var x in table)
                x.Short();

            return table;
        }

        IList<PnLRecord> IRiskRewardCalc.GeneratePriceDecreaseTable(Position position)
        {
            decimal[] pcp = { -5m, -7m, -8m, -10m, -12m, -15m, -20m, -25m, -30m };
            

            var table = PnLTable.GenerateTable(position.EntryPriceAvg, position.LotSize,
                position.Capital, pcp).OrderByDescending(o => o.PCP).ToList();

            // Invert the signs of PnL for visual convenience
            foreach (var x in table)
                x.Short();

            return table;
        }
    }
}
