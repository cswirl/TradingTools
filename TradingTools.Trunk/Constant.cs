using System;

namespace TradingTools.Trunk
{
    public static class Constant
    {
        public const decimal TRADING_FEE = 0.001M;      // Binance
        public const decimal DAILY_INTEREST_RATE = 0.0002M;

        public const string WHOLE_NUMBER = "0";
        
        public const string MONEY_FORMAT = "C";

        public const string PERCENTAGE_FORMAT_NONE = @"0\%";        
        public const string PERCENTAGE_FORMAT_SINGLE = @"0.0\%";
        public const string PERCENTAGE_FORMAT_DOUBLE = @"0.00\%";

        public const string DECIMAL_ONE = "0.0";
        public const string DECIMAL_TWO = "0.00";
        public const string DECIMAL_UPTO_ONE = "0.#";
        public const string DECIMAL_UPTO_TWO = "0.##";
        public const string DECIMAL_UPTO_MAX = "0.########";

        public const string DATE_MMMM_DD_YYYY = "MMMM dd, yyyy";
    }

    public class Position
    {
        public string Ticker { get; set; }
        public decimal Capital { get; set; }
        public decimal Leverage { get; set; }
        public decimal EntryPriceAvg { get; set; }
        public decimal InitialPositionValue { get; set; }
        public decimal LotSize { get; set; }
        public decimal AccountEquity { get; set; }
        public decimal LeveragedCapital
        {
            get
            {
                return Formula.LeveragedCapital(Capital, Leverage);
            }
        }

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

    public enum RiskRewardCalcState
    {
        Empty,
        Loaded,
        TradeOpen,
        TradeClosed,
        Deleted
    }

    public enum TradingStyle
    {
        day,
        swing,
        position,
        investment
    }
}
