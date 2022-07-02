using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Services.Dto;
using TradingTools.Services.Interface;
using TradingTools.Services.Models;
using TradingTools.Trunk;

namespace TradingTools.Services
{

    public class RiskRewardCalcLong : IRiskRewardCalc
    {
        private Position _position;

        public RiskRewardCalcLong(Position position)
        {
            _position = position;
        }


        public IList<PnLRecord> GenerateProfitsTable(Position position)
        {
            decimal[] pcp = { 5m, 7m, 8m, 10m, 12m, 15m, 20m, 25m, 30m };

            return PnLTable.GenerateTable(position.EntryPriceAvg, position.LotSize,
                position.Capital, pcp).OrderByDescending(o => o.PCP).ToList();
        }

        public IList<PnLRecord> GenerateLossesTable(Position position)
        {
            decimal[] pcp = { -1m, -2m, -3m, -4m, -5m, -6, -7m, -8, -10m };

            return PnLTable.GenerateTable(position.EntryPriceAvg, position.LotSize,
                position.Capital, pcp).OrderByDescending(o => o.PCP).ToList();
        }

        public PnLRecord ComputePnL(decimal exitPrice, Position pos)
        {
            if (pos == null) throw new NullReferenceException("Position object not set");
            return PnLRecord.Create(exitPrice, pos.EntryPriceAvg, pos.LotSize, pos.Capital);
        }

        // Rename to TradeExitPnl ?
        public Tuple<PnLRecord, decimal, decimal> PnlExitPlan(decimal exitPrice, Position position)
        {
            var rec = ComputePnL(exitPrice, position);
            var sPV = Formula.PositionValue(position.LotSize, exitPrice);
            var equity = Formula.AccountEquity(position.LotSize,
                exitPrice, Formula.BorrowedAmount(position.Leverage, position.Capital));

            return new (rec, sPV, equity);
        }

        public TradeExitDto ComputeTradeExit(decimal exitPrice, Position position)
        {

            return new TradeExitDto();
        }


    }

    
    public class PnLTable
    {
        private static decimal[] _princeChangeIncrements = { 1m, 2m, 3m, 4m, 5m };

        public static IList<PnLRecord> GenerateTable(decimal EntPA, decimal lotSize, decimal capital, decimal[] increments = null)
        {
            _princeChangeIncrements = increments != null ? increments : _princeChangeIncrements;
            var list = new List<PnLRecord>();

            try
            {
                foreach (decimal pcp in _princeChangeIncrements)
                {
                    decimal dec_pcp = pcp / 100;        // We simply need the MoneyToDecimal value of Price Increase Percentage
                    decimal ExitPrice = EntPA * (1 + dec_pcp);
                    decimal pnl = EntPA * lotSize * dec_pcp;
                    var rec = new PnLRecord
                    {
                        PCP = pcp,
                        ExitPrice = ExitPrice,
                        PnL = pnl,
                        PnL_Percentage = pnl / capital * 100
                    };

                    list.Add(rec);
                }

                return list;
            }
            catch (DivideByZeroException dex)
            {

            }

            return null;
        }

        public static PnLRecord GeneratePriceChangeRecord(decimal ExitPrice, decimal EntryPrice, decimal lotSize, decimal capital)
        {
            try
            {
                decimal pip = (ExitPrice - EntryPrice) / EntryPrice * 100;
                decimal pnl = EntryPrice * lotSize * pip / 100;
                return new PnLRecord
                {
                    PCP = pip,
                    ExitPrice = ExitPrice,
                    PnL = pnl,
                    PnL_Percentage = pnl / capital * 100,
                };
            }
            catch (DivideByZeroException dex)
            {

            }
            return null;
        }



        private static decimal TradingCost(decimal ExitPrice, decimal lotSize, decimal borrowCost) => SpeculativeTradingFee(ExitPrice, lotSize) + borrowCost;

        public static decimal SpeculativeTradingFee(decimal ExitPrice, decimal lotSize) => ExitPrice * lotSize * Constant.TRADING_FEE;


    }
}
