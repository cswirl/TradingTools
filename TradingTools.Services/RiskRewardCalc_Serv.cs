﻿using System;
using System.Collections.Generic;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;

namespace TradingTools.Services
{
    public class RiskRewardCalc_Serv
    {   
        private PriceIncreaseTable _priceIncreaseTable;
        private PriceDecreaseTable _priceDecreaseTable;

        public PriceIncreaseTable PriceIncreaseTable { get { return _priceIncreaseTable; } }
        public PriceDecreaseTable PriceDecreaseTable { get { return _priceDecreaseTable; } }

        public RiskRewardCalc_Serv()
        {
            _priceIncreaseTable = new();
            _priceDecreaseTable = new();
        }

        public static decimal PCP(decimal EntryPrice, decimal? ExitPrice)
        {
            if (EntryPrice == 0) return 0;      // divide by zero
            decimal exitPrice = ExitPrice ?? EntryPrice;
            return (exitPrice - EntryPrice) / EntryPrice * 100;
        }

        public static decimal PnL_percentage(decimal initialCapital, decimal? finalCapital)
        {
            // for some reason i cannot use the statement: finalCapital ?? initialCapital directly in the return statement
            decimal f = finalCapital ?? initialCapital;
            return (f - initialCapital) / initialCapital * 100;
        }

        public static decimal PnL(decimal initialCapital, decimal? finalCapital)
        {
            decimal f = finalCapital ?? initialCapital;
            return f - initialCapital;
        }

        public static decimal PositionValue(decimal lotSize, decimal price)
        {
            return price * lotSize;
        }

        public static decimal FinalPositionValue(decimal lotSize, decimal? exitPrice)
        {
            decimal _exitPrice = exitPrice ?? 0;
            return _exitPrice * lotSize;
        }
        public static decimal AccountEquity(decimal positionValue, decimal borrowedAmount) => positionValue - borrowedAmount;
    }

    public class PriceChangeTable
    {

        decimal[] _princeChangeIncrements = { 1m, 2m, 3m, 4m, 5m };

        public IList<PriceChangeRecord> GenerateTable(decimal EntPA, decimal lotSize, decimal borrowCost, decimal capital, decimal[] increments = null)
        {
            _princeChangeIncrements = increments != null ? increments : _princeChangeIncrements;
            var list = new List<PriceChangeRecord>();

            try
            {
                foreach (decimal pcp in _princeChangeIncrements)
                {
                    decimal dec_pcp = pcp / 100;        // We simply need the MoneyToDecimal value of Price Increase Percentage
                    decimal ExitPrice = EntPA * (1 + dec_pcp);
                    decimal tradingCost = TradingCost(ExitPrice, lotSize, borrowCost);
                    decimal pnl = (EntPA * lotSize * dec_pcp) - tradingCost;
                    var rec = new PriceChangeRecord
                    {
                        PCP = pcp,
                        ExitPrice = ExitPrice,
                        PnL = pnl,
                        PnL_Percentage = pnl / capital * 100,
                        TradingCost = tradingCost
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

        public static PriceChangeRecord GeneratePriceChangeRecord(decimal ExitPrice, decimal EntryPrice, decimal lotSize, decimal borrowCost, decimal capital)
        {
            try
            {
                decimal pip = (ExitPrice - EntryPrice) / EntryPrice * 100;
                decimal tradingCost = TradingCost(ExitPrice, lotSize, borrowCost);
                decimal pnl = (EntryPrice * lotSize * pip / 100) - tradingCost;
                return new PriceChangeRecord
                {
                    PCP = pip,
                    ExitPrice = ExitPrice,
                    PnL = pnl,
                    PnL_Percentage = pnl / capital * 100,
                    TradingCost = tradingCost
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

    // C#9 book page 177 - more about record
    public record PriceChangeRecord
    {
        public decimal PCP { get; init; }   // Price Change Percentage
        public decimal ExitPrice { get; init; }
        public decimal PnL { get; init; }
        public decimal PnL_Percentage { get; set; }
        public decimal TradingCost { get; set; }

    }



    public class PriceIncreaseTable
    {
        // For Testing
        //readonly decimal[] priceIncreasePercentage_array = { 1m, 2m, 3m, 4m, 5m};

        readonly decimal[] priceIncreasePercentage_array = { 5m, 7m, 8m, 10m, 12m, 15m, 20m, 25m, 30m };

        private PriceChangeTable _priceChangeTable;

        public PriceIncreaseTable()
        {
            _priceChangeTable = new();
        }


        public IList<PriceChangeRecord> GenerateTable(decimal EntPA, decimal lotSize, decimal borrowCost, decimal capital)
        {
            return _priceChangeTable.GenerateTable(EntPA, lotSize, borrowCost, capital, priceIncreasePercentage_array);
        }

        public PriceChangeRecord GeneratePriceIncreaseRecord(decimal exitPrice, decimal entryPrice, decimal lotSize, decimal borrowCost, decimal capital)
        {
            return PriceChangeTable.GeneratePriceChangeRecord(exitPrice, entryPrice, lotSize, borrowCost, capital);
        }

    }


    public class PriceDecreaseTable
    {
        // For Testing
        //readonly decimal[] priceDecreasePercentage_array = { -0.3m, -0.5m, -0.7m, -0.8m, -0.9m, -1m, -1.1m};

        readonly decimal[] priceDecreasePercentage_array = { -1m, -2m, -3m, -4m, -5m, -6, -7m, -8, -10m };

        private PriceChangeTable _priceChangeTable;

        public PriceDecreaseTable()
        {
            _priceChangeTable = new();
        }

        public IList<PriceChangeRecord> GenerateTable(decimal EntPA, decimal lotSize, decimal borrowCost, decimal capital)
        {
            return _priceChangeTable.GenerateTable(EntPA, lotSize, borrowCost, capital, priceDecreasePercentage_array);
        }

        public PriceChangeRecord GeneratePriceDecreaseRecord(decimal exitPrice, decimal entryPrice, decimal lotSize, decimal borrowCost, decimal capital)
        {
            return PriceChangeTable.GeneratePriceChangeRecord(exitPrice, entryPrice, lotSize, borrowCost, capital);
        }
    }


}
