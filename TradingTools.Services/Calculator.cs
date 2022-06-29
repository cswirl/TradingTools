using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;
using TradingTools.DAL;
using System.ComponentModel;
using TradingTools.Trunk.Validations;
//using TradingTools.Trunk.Validations.NumericValidationExtensions;

namespace TradingTools.Services
{
    public class Calculator
    {
        
    }

    public static class Formula
    {
        public static decimal LeveragedCapital(decimal capital, decimal leverage)
        {
            capital.MustBeEqualOrAbove(10, nameof(capital));
            leverage.MustBeAbove(1);
            return capital * leverage;
        }

        public static decimal LotSize(decimal leveragedCapital, decimal entryPrice)
        {
            leveragedCapital.MustBeEqualOrAbove(10, nameof(leveragedCapital));
            entryPrice.MustBeAbove(0, nameof(entryPrice));
            return leveragedCapital / entryPrice;
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

        

        public static decimal PositionValue(decimal lotSize, decimal price)
        {
            return price * lotSize;
        }

        public static decimal FinalPositionValue(decimal lotSize, decimal? exitPrice)
        {
            decimal _exitPrice = exitPrice ?? 0;
            return _exitPrice * lotSize;
        }
        public static decimal AccountEquity(decimal positionValue, decimal borrowedAmount) => positionValue - borrowedAmount;
    }
}
