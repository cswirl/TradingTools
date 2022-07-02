using System;
using TradingTools.Trunk.Validations;

namespace TradingTools.Trunk
{
    public static class Formula
    {
        public static decimal LeveragedCapital(decimal capital, decimal leverage)
        {
            capital.MustBeEqualOrAbove(0, nameof(capital));
            leverage.MustBeEqualOrAbove(0, nameof(leverage));
            return capital * leverage;
        }

        public static decimal LotSize(decimal leveragedCapital, decimal entryPrice)
        {
            entryPrice.MustBeAbove(0, nameof(entryPrice));
            return leveragedCapital / entryPrice;
        }

        public static decimal LotSize(decimal capital, decimal leverage, decimal entryPrice)
        {
            entryPrice.MustBeAbove(0, nameof(entryPrice));
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

        public static decimal BorrowedAmount(decimal leverage, decimal capital)
        {
            return LeveragedCapital(capital, leverage) - capital;
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

        public static decimal SpeculativePositionValue(decimal lotSize, decimal ExitPrice)
        {
            return lotSize * ExitPrice;
        }
    }
}
