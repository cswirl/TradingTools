using System;
using System.Collections.Generic;

namespace TradingTools.Services
{
    public class RiskRewardCalc_Long
    {
        private PriceIncreaseTable _priceIncreaseTable;
        private PriceDecreaseTable _priceDecreaseTable;

        //public static PriceIncreaseRecord GetPriceIncreaseRecord()

        public PriceIncreaseTable PriceIncreaseTable { get { return _priceIncreaseTable; } }
        public PriceDecreaseTable PriceDecreaseTable { get { return _priceDecreaseTable; } }

        public RiskRewardCalc_Long()
        {
            _priceIncreaseTable = new();
            _priceDecreaseTable = new();
        }

        public PriceIncreaseRecord MakePriceIncreaseRecord(decimal priceTarget)
        {


            return null;
        }
    }

    public class PriceIncreaseTable
    {
        // For Testing
        readonly decimal[] priceIncreasePercentage_array = { 1m, 2m, 3m, 4m, 5m};

        //readonly decimal[] priceIncreasePercentage_array = { 7m, 8m, 10m, 15m, 20m, 25m, 30m };

        private IList<PriceIncreaseRecord> _list = new List<PriceIncreaseRecord>();

        public IList<PriceIncreaseRecord> GenerateTable(decimal entryPriceAverage, decimal PositionValue, decimal TradingCost)
        {
            _list.Clear();
            foreach (decimal pip in priceIncreasePercentage_array)
            {
                decimal dec_pip = pip / 100;        // We simply need the Decimal value of Price Increase Percentage
                
                var rec = new PriceIncreaseRecord
                {
                    PriceIncreasePercentage = pip,
                    PriceTarget = entryPriceAverage * (1 + dec_pip),
                    Profit = (PositionValue * dec_pip) - TradingCost
                };
                
                _list.Add(rec);
            }

            return _list;
        }

        public PriceIncreaseRecord GeneratePriceIncreaseRecord(decimal priceTarget, decimal entryPriceAverage, decimal positionValue, decimal tradingCost)
        {
            decimal pip = ((priceTarget - entryPriceAverage) / entryPriceAverage) * 100;
            return new PriceIncreaseRecord
            {
                PriceTarget = priceTarget,
                PriceIncreasePercentage = pip,
                Profit = (positionValue * pip/100) - tradingCost
            };
        }

    }

    // C#9 book page 177 - more about record
    public record PriceIncreaseRecord
    {
        public decimal PriceIncreasePercentage { get; init; }
        public decimal PriceTarget { get; init; }
        public decimal Profit { get; init; }
    }

    public class PriceDecreaseTable
    {
        // For Testing
        readonly decimal[] priceDecreasePercentage_array = { 0.3m, 0.5m, 0.7m, 0.8m, 0.9m, 1m, 1.1m};

        //readonly decimal[] priceDecreasePercentage_array = { 1m, 2m, 3m, 4m, 5m, 7m, 10m };

        private IList<PriceDecreaseRecord> _list = new List<PriceDecreaseRecord>();

        public IList<PriceDecreaseRecord> GenerateTable(decimal entryPriceAverage, decimal PositionValue, decimal TradingCost)
        {
            _list.Clear();
            foreach (decimal pdp in priceDecreasePercentage_array)
            {
                decimal dec_pdp = pdp / 100;        // We simply need the Decimal value of Price Decrease Percentage

                var rec = new PriceDecreaseRecord
                {
                    PriceDecreasePercentage = pdp,
                    PriceTarget = entryPriceAverage - (dec_pdp * entryPriceAverage),
                    Loss = (PositionValue * dec_pdp) + TradingCost
                };

                _list.Add(rec);
            }

            return _list;
        }

        public PriceDecreaseRecord GeneratePriceDecreaseRecord(decimal priceTarget, decimal entryPriceAverage, decimal positionValue, decimal tradingCost)
        {
            decimal pdp = (entryPriceAverage - priceTarget) / entryPriceAverage * 100;
            return new PriceDecreaseRecord
            {
                PriceTarget = priceTarget,
                PriceDecreasePercentage = pdp,
                Loss = (positionValue * pdp / 100) + tradingCost
            };
        }
    }

    public record PriceDecreaseRecord
    {
        public decimal PriceDecreasePercentage { get; init; }
        public decimal PriceTarget { get; init; }
        public decimal Loss { get; init; }
    }
}
