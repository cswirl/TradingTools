using System;
using TradingTools.Trunk.Extensions;
using TradingTools.Trunk.Validations;

namespace TradingTools.Trunk
{
    public static class Formula
    {
        public static decimal LeveragedCapital(decimal capital, decimal leverage)
        {
            return  capital * leverage;
        }

        public static decimal LotSize(decimal leveragedCapital, decimal entryPrice)
        {
            if (entryPrice == 0) return 0;
            return leveragedCapital / entryPrice;
        }

        public static decimal LotSize(decimal capital, decimal leverage, decimal entryPrice)
        {
            if (entryPrice == 0) return 0;
            return leverage * capital / entryPrice;
        }

        public static decimal Leverage(decimal leveragedCapital, decimal capital)
        {
            capital.CannotBeZero();
            return leveragedCapital / capital;
        }

        public static decimal Leverage(decimal entryPrice, decimal lotSize, decimal capital)
        {
            capital.CannotBeZero();
            return entryPrice * lotSize / capital;
        }

        public static decimal PCP(decimal entryPrice, decimal? ExitPrice)
        {
            if (entryPrice == 0) return 0;      // divide by zero
            decimal exitPrice = ExitPrice ?? entryPrice;    // if ExitPrice is null, will result to zero
            return (exitPrice - entryPrice) / entryPrice * 100;
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

        public static decimal BorrowedAmount(decimal leverage, decimal capital)
        {
            return LeveragedCapital(capital, leverage) - capital;
        }

        public static decimal InterestCost(decimal borrowedAmount, decimal dailyInterestRate, decimal dayCount)
        {
            return borrowedAmount * dailyInterestRate * dayCount;
        }

        public static decimal PositionValue(decimal lotSize, decimal price)
        {
            return price * lotSize;
        }

        public static decimal FinalPositionValue(decimal lotSize, decimal exitPrice)
        {
            return exitPrice * lotSize;
        }

        public static decimal AccountEquity(decimal positionValue, decimal borrowedAmount) 
            => positionValue - borrowedAmount;

        public static decimal AccountEquity(decimal lotSize, decimal price, decimal borrowedAmount)
        {
            return PositionValue(lotSize, price) - borrowedAmount;
        }

        public static decimal TradingFee(decimal leveragedCapital, decimal exchangeFeeRate)
        {
            return leveragedCapital * exchangeFeeRate;
        }

        public static decimal DayCount(DateTime dateEnter, DateTime dateExit)
        {
            return (dateExit - dateEnter).TotalDays.ToDecimalSafe();
        }

        #region "Speculative Computation"
        public static decimal SpeculativePositionValue(decimal leveragedCapital, decimal profits)
        {
            return leveragedCapital + profits;
        }

        public static decimal SpeculativeAccountEquity(decimal leveragedCapital, decimal profits, decimal borrowAmount)
        {
            return SpeculativePositionValue(leveragedCapital, profits) - borrowAmount;
        }

        #endregion

    }
}
