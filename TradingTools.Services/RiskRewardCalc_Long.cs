using System;
using System.Collections.Generic;

namespace TradingTools.Services
{
    public class RiskRewardCalc_Long
    {
        

        
    }

    public class PriceIncreaseTable
    {
        readonly decimal[] priceIncreasePercentage_array = {7m, 8m, 10m, 15m, 20m, 25m, 30m };
        private IList<PriceIncreaseRecord> _list = new List<PriceIncreaseRecord>();

        public IList<PriceIncreaseRecord> GetTable() => _list;

        public PriceIncreaseTable(decimal entryPriceAverage, decimal PositionValue, decimal TradingCost, decimal TradingCost_Percentage)
        {
            foreach (decimal pip in priceIncreasePercentage_array)
            {
                var rec = new PriceIncreaseRecord();
                rec.PriceIncreasePercentage = pip;
                decimal dec_pip = pip / 100;
                rec.PriceTarget = entryPriceAverage * (1 + dec_pip + TradingCost_Percentage);
                rec.Profit = (PositionValue * dec_pip) - TradingCost;
                _list.Add(rec);
            }
        }

        public IList<PriceIncreaseRecord> GenerateTable(decimal entryPriceAverage, decimal PositionValue, decimal TradingCost, decimal TradingCost_Percentage)
        {
            foreach (decimal pip in priceIncreasePercentage_array)
            {
                var rec = new PriceIncreaseRecord();
                rec.PriceIncreasePercentage = pip;
                decimal dec_pip = pip / 100;
                rec.PriceTarget = entryPriceAverage * (1 + dec_pip + TradingCost_Percentage);
                rec.Profit = (PositionValue * dec_pip) - TradingCost;
                _list.Add(rec);
            }

            return _list;
        }

    }

    public class PriceIncreaseRecord
    {
        

        public decimal PriceIncreasePercentage { get; set; }
        public decimal PriceTarget { get; set; }
        public decimal Profit { get; set; }
    }

    public class PriceDecreaseRecord
    {
        public decimal PriceDecreasePercentage { get; set; }
        public decimal PriceTarget { get; set; }
        public decimal Profit { get; set; }
    }
}
