using System;
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

        public Trade Trade_Create(CalculatorState c, string positionSide)
        {
            var t = new Trade
            {
                Ticker = c.Ticker,
                PositionSide = positionSide,
                DateEnter = DateTime.Now,
                Capital = c.Capital,
                Leverage = c.Leverage,
                LeveragedCapital = c.Leverage * c.Capital,
                EntryPriceAvg = c.EntryPriceAvg

            };

            return t;
        }

        //not complete or formalized
        public bool Trade_Validate(Trade t, out string msg)
        {
            msg = string.Empty;
            if (t.Ticker.Length < 1 | !Format.isTicker(t.Ticker, out msg)) return false;
            if (t.Status.Length < 1 | !validateStatus(t.Status, out msg)) return false;
            if (t.PositionSide.Length < 1 | !validatePositionSide(t.PositionSide, out msg)) return false;
            if (t.DateEnter == default) return false;
            if (t.Capital <= 10 | t.Leverage < 1 | t.EntryPriceAvg <= 0 | t.LotSize <= 0 | t.OpeningTradingFee <= 0 |
                t.OpeningTradingCost <= 0)
            {
                msg = "Invalid input data";
                return false;
            }
            if (t.CalculatorState == default) return false;

            return true;
        }

        private bool validatePositionSide(string positionSide, out string msg)
        {
            msg = string.Empty;
            if (positionSide.Equals("short") | positionSide.Equals("long")) return true;
            return false;
        }

        private bool validateStatus(string status, out string msg)
        {
            msg = string.Empty;
            if (status.Equals("open") | status.Equals("closed")) return true;
            return false;
        }
    }
}
