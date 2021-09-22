﻿using System;
using System.Collections.Generic;
using TradingTools.Trunk;

namespace TradingTools.Services
{
    public class RiskRewardCalc_Long
    {
        private PriceIncreaseTable _priceIncreaseTable;
        private PriceDecreaseTable _priceDecreaseTable;

        public PriceIncreaseTable PriceIncreaseTable { get { return _priceIncreaseTable; } }
        public PriceDecreaseTable PriceDecreaseTable { get { return _priceDecreaseTable; } }

        public RiskRewardCalc_Long()
        {
            _priceIncreaseTable = new();
            _priceDecreaseTable = new();
        }
    }

    public class PriceChangeTable
    {

        decimal[] _princeChangeIncrements = { 1m, 2m, 3m, 4m, 5m };

        public IList<PriceChangeRecord> GenerateTable(decimal EntPA, decimal lotSize, decimal borrowCost, decimal[] increments = null)
        {
            _princeChangeIncrements = increments != null ? increments : _princeChangeIncrements;
            var list = new List<PriceChangeRecord>();
            foreach (decimal pcp in _princeChangeIncrements)
            {
                decimal dec_pcp = pcp / 100;        // We simply need the Decimal value of Price Increase Percentage
                decimal ExitPrice = EntPA * (1 + dec_pcp);
                decimal tradingCost = TradingCost(ExitPrice, lotSize, borrowCost);
                var rec = new PriceChangeRecord
                {
                    PriceChangePercentage = pcp,
                    ExitPrice = ExitPrice,
                    PnL = (EntPA * lotSize * dec_pcp) - tradingCost,
                    TradingCost = tradingCost
                };

                list.Add(rec);
            }

            return list;
        }

        public PriceChangeRecord GeneratePriceChangeRecord(decimal ExitPrice, decimal EntryPrice, decimal lotSize, decimal borrowCost)
        {
            decimal pip = (ExitPrice - EntryPrice) / EntryPrice * 100;
            decimal tradingCost = TradingCost(ExitPrice, lotSize, borrowCost);

            return new PriceChangeRecord
            {
                PriceChangePercentage = pip,
                ExitPrice = ExitPrice,
                PnL = (EntryPrice * lotSize * pip / 100) - tradingCost,
                TradingCost = tradingCost
            };
        }

        private decimal TradingCost(decimal ExitPrice, decimal lotSize, decimal borrowCost) => SpeculativeTradingFee(ExitPrice, lotSize) + borrowCost;

        private decimal SpeculativeTradingFee(decimal ExitPrice, decimal lotSize) => ExitPrice * lotSize * Constant.TRADING_FEE;

        
    }

    // C#9 book page 177 - more about record
    public record PriceChangeRecord
    {
        public decimal PriceChangePercentage { get; init; }
        public decimal ExitPrice { get; init; }
        public decimal PnL { get; init; }
        public decimal TradingCost { get; set; }
    }



    public class PriceIncreaseTable
    {
        // For Testing
        //readonly decimal[] priceIncreasePercentage_array = { 1m, 2m, 3m, 4m, 5m};

        readonly decimal[] priceIncreasePercentage_array = { 7m, 8m, 10m, 15m, 20m, 25m, 30m };

        private PriceChangeTable _priceChangeTable;

        public PriceIncreaseTable()
        {
            _priceChangeTable = new();
        }


        public IList<PriceChangeRecord> GenerateTable(decimal EntPA, decimal lotSize, decimal borrowCost)
        {
            return _priceChangeTable.GenerateTable(EntPA, lotSize, borrowCost, priceIncreasePercentage_array);
        }

        public PriceChangeRecord GeneratePriceIncreaseRecord(decimal exitPrice, decimal entryPrice, decimal lotSize, decimal borrowCost)
        {
            return _priceChangeTable.GeneratePriceChangeRecord(exitPrice, entryPrice, lotSize, borrowCost);
        }

    }


    public class PriceDecreaseTable
    {
        // For Testing
        //readonly decimal[] priceDecreasePercentage_array = { -0.3m, -0.5m, -0.7m, -0.8m, -0.9m, -1m, -1.1m};

        readonly decimal[] priceDecreasePercentage_array = { -1m, -2m, -3m, -4m, -5m, -7m, -10m };

        private PriceChangeTable _priceChangeTable;

        public PriceDecreaseTable()
        {
            _priceChangeTable = new();
        }

        public IList<PriceChangeRecord> GenerateTable(decimal EntPA, decimal lotSize, decimal borrowCost)
        {
            return _priceChangeTable.GenerateTable(EntPA, lotSize, borrowCost, priceDecreasePercentage_array);
        }

        public PriceChangeRecord GeneratePriceDecreaseRecord(decimal exitPrice, decimal entryPrice, decimal lotSize, decimal borrowCost)
        {
            return _priceChangeTable.GeneratePriceChangeRecord(exitPrice, entryPrice, lotSize, borrowCost);
        }
    }


    //public record PriceIncreaseRecord
    //{
    //    public decimal PriceIncreasePercentage { get; init; }
    //    public decimal PriceTarget { get; init; }
    //    public decimal Profit { get; init; }
    //    public decimal TradingCost { get; set; }
    //}

    //public record PriceDecreaseRecord
    //{
    //    public decimal PriceDecreasePercentage { get; init; }
    //    public decimal PriceTarget { get; init; }
    //    public decimal Loss { get; init; }
    //    public decimal TradingCost { get; set; }
    //}
}
