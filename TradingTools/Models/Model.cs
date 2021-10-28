using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk;
using TradingTools.Services;

namespace TradingTools.Model
{
    class Model
    {
    }

    public class CalculationDetails
    {
        public OpeningCost OpeningCost = new();
        public Position Position = new();
        public Borrow Borrow = new();

        private decimal _sPV = 0m;

        public decimal GetSpeculativePositionValue(decimal ExitPrice)
        {
            _sPV = Position.LotSize * ExitPrice;
            return _sPV;
        }

        public decimal GetSpeculativeAccountEquity(decimal ExitPrice)
        {
            return GetSpeculativePositionValue(ExitPrice) - Borrow.Amount;
        }

        public bool PreCalculate_Validate(out string msg)
        {
            // Minimum Value
            msg = string.Empty;
            if (Position.Capital <= 10 | Position.Leverage < 1 | Position.EntryPriceAvg <= 0)
            {
                msg = "Invalid input data";
                return false;
            }
            return true;
        }

        public bool Trade_Unofficial_Calculate(decimal capital, decimal leverage, decimal entryPrice, int dayCount, decimal dailyInterestRate, out string msg)
        {
            // step 2: Collect data
            // Receptors
            Position.Capital = capital;
            Position.Leverage = leverage;
            Position.EntryPriceAvg = entryPrice;
            Borrow.DayCount = dayCount;
            Borrow.DailyInterestRate = dailyInterestRate;

            // step 2-B: Validation - The collection process will assign default values - no null object is expected
            // Hence, Minimum value 
            if (!PreCalculate_Validate(out msg)) return false;

            // step 3: ORDER IS IMPORTANT - Process Data collected including others supporting data
            Position.LeveragedCapital = Position.Capital * Position.Leverage;
            OpeningCost.TradingFee = TradingCost.GetTradingFee_in_dollar(Position.LeveragedCapital);
            Position.InitialPositionValue = Position.LeveragedCapital - OpeningCost.TradingFee;
            Position.LotSize = Position.InitialPositionValue / Position.EntryPriceAvg;
            Borrow.Amount = Position.LeveragedCapital - Position.Capital;
            Position.AccountEquity = Position.InitialPositionValue - Borrow.Amount;

            return true;
        }

        public bool Trade_Official_Calculate(decimal capital, decimal leverage, decimal lotSize, decimal entryPrice, int dayCount, decimal dailyInterestRate, out string msg)
        {
            msg = string.Empty;
            // step 2: Collect data
            // Receptors
            Position.Capital = capital;
            Position.Leverage = leverage == 0 ? entryPrice * lotSize / capital : leverage;
            Position.LotSize = lotSize;
            Position.EntryPriceAvg = entryPrice;
            Borrow.DayCount = dayCount;
            Borrow.DailyInterestRate = dailyInterestRate;

            // step 2-B: Validation - The collection process will assign default values - no null object is expected
            // Hence, Minimum value 
            if (Position.Capital <= 10 | Position.LotSize <= 0 | Position.EntryPriceAvg <= 0 | dailyInterestRate < 0)
            {
                msg = "Invalid input data";
                return false;
            }

            // step 3: ORDER IS IMPORTANT - Process Data collected including others supporting data
            Position.LeveragedCapital = Position.Capital * Position.Leverage;
            OpeningCost.TradingFee = TradingCost.GetTradingFee_in_dollar(Position.LeveragedCapital);
            Position.InitialPositionValue = Position.LeveragedCapital - OpeningCost.TradingFee;
            Borrow.Amount = Position.LeveragedCapital - Position.Capital;
            Position.AccountEquity = Position.InitialPositionValue - Borrow.Amount;

            return true;
        }

        public bool Calculate(decimal capital, decimal leverage, decimal lotSize, decimal entryPrice, int dayCount, decimal dailyInterestRate, out string msg)
        {
            // divide by zero screener
            if (capital == 0 || entryPrice == 0)
            {
                msg = "Capital and Entry Price needed.";
                return false;
            }
            msg = string.Empty;
            // step 2: Collect data
            // Receptors
            Position.Capital = capital;
            Position.Leverage = leverage == 0 ? entryPrice * lotSize / capital : leverage;
            Position.LotSize = lotSize == 0 ? leverage * capital / entryPrice : lotSize;
            Position.EntryPriceAvg = entryPrice;
            Borrow.DayCount = dayCount;
            Borrow.DailyInterestRate = dailyInterestRate;

            // step 2-B: Validation - The collection process will assign default values - no null object is expected
            // Hence, Minimum value 
            if (Position.Capital <= 10 | Position.LotSize <= 0 | Position.EntryPriceAvg <= 0 | dailyInterestRate < 0)
            {
                msg = "Invalid input data";
                return false;
            }

            // step 3: ORDER IS IMPORTANT - Process Data collected including others supporting data
            Position.LeveragedCapital = Position.Capital * Position.Leverage;
            OpeningCost.TradingFee = TradingCost.GetTradingFee_in_dollar(Position.LeveragedCapital);
            Position.InitialPositionValue = Position.LeveragedCapital - OpeningCost.TradingFee;
            Borrow.Amount = Position.LeveragedCapital - Position.Capital;
            Position.AccountEquity = Position.InitialPositionValue - Borrow.Amount;

            return true;
        }

        public static CalculationDetails GetCalculationDetails(
            decimal capital,
            decimal leverage,
            decimal entryPrice,
            decimal lotSize,
            int dayCount,
            decimal dailyInterestRate,
            out string msg)
        {
            bool r = false;
            CalculationDetails cd = new();
            if (lotSize <= 0)
            {
                r = cd.Trade_Unofficial_Calculate(
                    capital,
                    leverage,
                    entryPrice,
                    dayCount,
                    dailyInterestRate,
                    out msg
                    );
            }
            else
            {
                r = cd.Trade_Official_Calculate(
                    capital,
                    leverage,
                    lotSize,
                    entryPrice,
                    dayCount,
                    dailyInterestRate,
                    out msg
                    );
            }

            if (!r) return default;

            return cd;
        }
    }

    public class OpeningCost
    {
        public decimal TradingFee { get; set; }
    }

}
