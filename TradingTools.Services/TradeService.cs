using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Services.Extensions;
using TradingTools.Services.Interface;
using TradingTools.Trunk;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Validation;

namespace TradingTools.Services
{
    public class TradeService
    {
        public static IRiskRewardCalc RiskRewardCalcGetInstance(string side)
        {
            if (side == "short")
                return new RiskRewardCalcShort();
            else
                return new RiskRewardCalcLong();
        }

        public static decimal GetTrading_ElaspsedTime_Days(DateTime dateEnter, DateTime? dateExit)
        {
            DateTime exit = dateExit ?? dateEnter;
            var days = (exit - dateEnter).TotalDays;
            return SafeConvert.ToDecimalSafe(days);
        }
        public static bool TradeOpening_Validate(Trade t, out string msg)
        {
            if (!validateTicker(t.Ticker, out msg)) return false;
            if (!validateStatus(t.Status, out msg)) return false;
            if (!validatePositionSide(t.Side, out msg)) return false;
            if (!validateTradeStyle(t.TradingStyle, out msg)) return false;
            if (t.DateEnter == default) { msg = "Date Enter is not set."; return false; }
            if (t.DateEnter > DateTime.Now)
            {
                msg = "'Date Enter'cannot exceed the Date and Time right now.";
                return false;
            }
            if (t.Capital <= 0)
            {
                msg = "Capital must be greater than zero";
                return false;
            }
            if (t.Leverage <= 0)
            {
                msg = "Leverage must be greater than zero";
                return false;
            }
            if (t.EntryPriceAvg <= 0)
            {
                msg = "Entry Price must be greater than zero";
                return false;
            }
            if (t.LotSize <= 0)
            {
                msg = "Lot Size must be greater than zero";
                return false;
            }
            if (t.CalculatorState == default) { msg = "Internal Error: Calculator is not set."; return false; }

            return true;
        }

        public static bool TradeClosing_Validate(Trade t, out string msg)
        {
            msg = string.Empty;
            string pref = "Trade Closing Validation failed: ";
            if (t.ExitPriceAvg <= 0 | t.FinalCapital < 0)   // Liquated will result in zero final capital
            {
                msg =  pref + " invalid data found.";
                return false; 
            }
            if (t.DateEnter > t.DateExit)
            {
                // try to auto fix it if same day
                if (t.DateEnter > DateTime.Today && t.DateExit > DateTime.Today)
                {
                    t.FixDateEnterExit();
                }
                else
                {
                    msg = pref + " 'Date Exit' must come later in time from 'Date Enter'";
                    return false;
                }
            }
            if (t.DateExit > DateTime.Now)  // THis means Date Exit cannot be future dated
            {
                msg = pref + " 'Date Exit'cannot exceed the Date and Time right now.";
                return false;
            }
            if (!t.Status.Equals("closed")) { msg = pref + "invalid Status value"; return false; }

            return true;
        }

        public static bool Trade_Validate(Trade t, out string msg)
        {
            return TradeOpening_Validate(t, out msg) && TradeClosing_Validate(t, out msg);
        }

        private static bool validateTicker(string ticker, out string msg)
        {
            msg = string.Empty;
            if (ticker.Length < 1 || !Format.isTicker(ticker, out msg)) 
            { 
                msg = msg == string.Empty ? "Invalid Ticker format." : msg;
                return false; 
            }

            return true;
        }

        private static bool validatePositionSide(string positionSide, out string msg)
        {
            msg = string.Empty;
            if (positionSide.Length < 1 || !(positionSide.Equals("short") | positionSide.Equals("long")))
            {
                msg = "Invalid position side value. Valid values are: 'long' or 'short'";
                return false;
            }
            return true;
        }

        private static bool validateStatus(string status, out string msg)
        {
            msg = string.Empty;
            if (status.Length < 1 || !(status.Equals("open") | status.Equals("closed")))
            {
                msg = "Invalid Status value. Valid values are: 'open' or 'closed'";
                return false;
            }

            return true;
        }

        private static bool validateTradeStyle(string tradeStyle, out string msg)
        {
            msg = string.Empty;
            if (tradeStyle.Length < 1 || !Enum.IsDefined(typeof(TradingStyle), tradeStyle))
            {
                msg = "Invalid Status value. Valid values are: 'open' or 'closed'";
                return false;
            }

            return true;
        }
    }
}
