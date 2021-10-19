using System;

namespace TradingTools.Trunk
{
    public static class Constant
    {
        public const decimal TRADING_FEE = 0.001M;      // Binance
        public const decimal DAILY_INTEREST_RATE = 0.0002M;

        public const string MAX_DECIMAL_PLACE_FORMAT = "0.########";
        public const string MONEY_FORMAT = "C";
        public const string PERCENTAGE_FORMAT = "F";
    }

    public class Position
    {
        public decimal Capital { get; set; }
        public decimal Leverage { get; set; }
        public decimal EntryPriceAvg { get; set; }
        public decimal InitialPositionValue { get; set; }
        public decimal LeveragedCapital { get; set; }
        public decimal LotSize { get; set; }
        public decimal AccountEquity { get; set; }
        public decimal StopLoss_limit { get; set; }
        public bool Side { get; set; }             // 1 or True for Long, 0 or False Short - MAYBE USE STRING TO SIMPLIFY

        
    }

    public class Borrow
    {
        public decimal Amount { get; set; }
        public int DayCount { get; set; }
        public decimal DailyInterestRate { get; set; }
        public decimal InterestCost { get { return Amount * DailyInterestRate * DayCount; } }
    }

    public class TradingCost
    {
        public decimal TradingFee_percentage { get; set; }
        public static decimal GetTradingFee_in_dollar(decimal amount)
        {
            return amount * Constant.TRADING_FEE;
        }

        public decimal TotalTradingCost { get; set; }

        public decimal GetTotalTradingCost(decimal amount, decimal borrowCost)
        {
            return GetTradingFee_in_dollar(amount) + borrowCost;
        }
        public decimal GetTotalTradingCost(decimal amount)
        {
            return GetTradingFee_in_dollar(amount);
        }
    }
}
