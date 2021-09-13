using System;
using System.Collections.Generic;

namespace TradingTools.Services
{
    public class RiskRewardCalc_Long
    {
        public const decimal TRADING_FEE = 0.001M;
        public PriceIncreaseTable _priceIncreaseTable;

        public PriceIncreaseTable PriceIncreaseTable { get { return _priceIncreaseTable; } }
        

        public RiskRewardCalc_Long()
        {
            _priceIncreaseTable = new PriceIncreaseTable();
        }

       
    }

    public class PriceIncreaseTable
    {
        // For Testing
        //readonly decimal[] priceIncreasePercentage_array = { 1m, 2m, 3m, 4m, 5m};

        readonly decimal[] priceIncreasePercentage_array = { 7m, 8m, 10m, 15m, 20m, 25m, 30m };

        private IList<PriceIncreaseRecord> _list = new List<PriceIncreaseRecord>();

        //public IList<PriceIncreaseRecord> GetTable() => _list;

        //public PriceIncreaseTable(decimal entryPriceAverage, decimal PositionValue, decimal TradingCost, decimal TradingCost_Percentage)
        //{
        //    foreach (decimal pip in priceIncreasePercentage_array)
        //    {
        //        var rec = new PriceIncreaseRecord();
        //        rec.PriceIncreasePercentage = pip;
        //        decimal dec_pip = pip / 100;
        //        rec.PriceTarget = entryPriceAverage * (1 + dec_pip + TradingCost_Percentage);
        //        rec.Profit = (PositionValue * dec_pip) - TradingCost;
        //        _list.Add(rec);
        //    }
        //}

        public IList<PriceIncreaseRecord> GenerateTable(decimal entryPriceAverage, decimal PositionValue, decimal TradingCost, decimal TradingCostProfitRatio)
        {
            foreach (decimal pip in priceIncreasePercentage_array)
            {
                decimal dec_pip = pip / 100;        // We simply need the Decimal value of Price Increase Percentage
                
                var rec = new PriceIncreaseRecord
                {
                    PriceIncreasePercentage = pip,
                    PriceTarget = entryPriceAverage * (1 + dec_pip + TradingCostProfitRatio),
                    Profit = (PositionValue * dec_pip) - TradingCost
                };
                
                _list.Add(rec);
            }

            return _list;
        }

    }

    // C#9 book page 177 - more about record
    public record PriceIncreaseRecord
    {
        public decimal PriceIncreasePercentage { get; init; }
        public decimal PriceTarget { get; init; }
        public decimal Profit { get; init; }
    }

    public record PriceDecreaseRecord
    {
        public decimal PriceDecreasePercentage { get; init; }
        public decimal PriceTarget { get; init; }
        public decimal Profit { get; init; }
    }
}
