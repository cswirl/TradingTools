using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Trunk.Model
{
    class Model
    {
    }

    public class Position
    {
        public decimal Capital { get; set; }
        public decimal LeveragedCapital { get; set; }
        public decimal EntryPriceAvg { get; set; }
        public decimal LotSize { get; set; }
        public decimal Leverage { get; set; }
        public decimal InitialPositionValue { get; set; }
        public decimal AccountEquity { get; set; }
        public decimal StopLoss_limit { get; set; }
        public bool Side { get; set; }             // 1 or True for Long, 0 or False Short
    }

    public class Borrow
    {
        public decimal Amount { get; set; }
        public int DayCount { get; set; }
        public decimal InterestPerDay { get; set; }
        public decimal InterestCost { get; set; }
    }

    public class ClosingCost
    {
        private Borrow _borrow;
        private TradingCost _tradingCost;

        public ClosingCost()
        {
            _borrow = new();
            _tradingCost = new();
        }

        public Borrow Borrow { get { return _borrow; } }
        public TradingCost TradingCost { get { return _tradingCost; } }

    }

    public class TradingCost
    {
        public decimal TradingFee_percentage { get; set; }
        public decimal GetTradingFee_in_dollar(decimal accountSize)
        {
            return accountSize * Constant.TRADING_FEE;
        }

        public decimal TotalTradingCost { get; set; }

        public decimal GetTotalTradingCost(decimal positionValue, decimal borrowCost)
        {
            return GetTradingFee_in_dollar(positionValue) + borrowCost;
        }
        public decimal GetTotalTradingCost(decimal capital)
        {
            return GetTradingFee_in_dollar(capital);
        }
    }
}
