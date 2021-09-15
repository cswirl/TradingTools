﻿using System;

namespace TradingTools.Trunk
{
    public static class Constant
    {
        public const decimal TRADING_FEE = 0.001M;      // Binance

    }

    public class Position
    {
        public decimal Capital { get; set; }
        public decimal EntryPriceAvg { get; set; }
        public decimal LotSize { get; set; }
        public decimal Leverage { get; set; }
        public decimal PositionValue { get; set; }
        public decimal AccountEquity { get; set; }
        public decimal StopLoss_limit { get; set; }
        public bool Direction { get; set; }             // 1 or True for Long, 0 or False Short
    }

    public class Borrow
    {
        public decimal Amount { get; set; }
        public int DayCount { get; set; }
        public decimal InterestPerDay { get; set; }
        public decimal InterestCost { get; set; }
    }

    public class TradingCost
    {
        public Borrow Borrow { get; set; }
        public decimal TradingFee_percentage { get; set; }
        public decimal GetTradingFee_in_dollar(decimal positionValue)
        {
            return positionValue * Constant.TRADING_FEE;
        }

        //Can be deleted
        public decimal TotalTradingCost { get; set; }

        
        //Must be Non-Negative number
        //Use decimal form for Trading Cost in Percent i.e. divide by 100
        public decimal TradingCostProfitRatio { get; set; }

        public decimal GetTotalTradingCost(decimal positionValue, decimal borrowCost)
        {
            return GetTradingFee_in_dollar(positionValue) + borrowCost;
        }
    }


}
