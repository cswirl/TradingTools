﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Validation;

namespace TradingTools.Services
{
    public class Trade_Serv
    {
        public static bool TradeOpening_Validate(Trade t, out string msg)
        {
            if (!validateTicker(t.Ticker, out msg)) return false;
            if (!validateStatus(t.Status, out msg)) return false;
            if (!validatePositionSide(t.PositionSide, out msg)) return false;
            if (t.DateEnter == default) { msg = "Invalid data. Date is not set."; return false; }
            if (t.Capital <= 10 | t.Leverage < 1 | t.EntryPriceAvg <= 0 | t.LotSize <= 0 | t.OpeningTradingFee <= 0 |
                t.OpeningTradingCost <= 0)
            {
                msg = "Invalid input data";
                return false;
            }
            if (t.CalculatorState == default) { msg = "Internal Error: Calculator is not set."; return false; }

            return true;
        }

        public static bool TradeClosing_Validate(Trade t, out string msg)
        {
            msg = string.Empty;
            string pref = "Trade Closing Validation failed: ";
            if (t.ExitPriceAvg <= 0 | t.DayCount < 0 | t.DailyInterestRate < 0 | t.InterestCost < 0 ) 
            {
                msg =  pref + " invalid data found.";
                return false; 
            }
            if (t.DateEnter >= t.DateExit) { msg = pref + " 'Date Exit' must come later in time from 'Date Enter'"; return false; }
            if (t.ClosingTradingFee < 0 | t.ClosingTradingCost < 0 | t.ClosingTradingFee > t.ClosingTradingCost)
            {
                msg = pref + " invalid data found.";
                return false;
            }
            if (!t.Status.Equals("closed")) { msg = pref + "invalid Status value"; return false; }

            return true;
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
    }
}