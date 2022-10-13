using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Services.Extensions;
using TradingTools.Services.Interface;
using TradingTools.Trunk;
using TradingTools.Trunk.Contracts;
using TradingTools.Trunk.Entity;
using TradingTools.Trunk.Extensions;
using TradingTools.Trunk.Validation;

namespace TradingTools.Services
{
    public class TradeService : ITradeService
    { 
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public TradeService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public bool Create(Trade trade)
        {
            try
            {
                var calcState = trade.CalculatorState;
                if (calcState == default)
                {
                    _logger.LogError("Fatal Error: Trade object must have a CalculatorState instance.");
                    return false;
                }

                // EF Core will create a new Trade record into the Database Trade table
                if (calcState.Trade == default) calcState.Trade = trade;
                _repository.CalculatorState.Update(calcState);  
                _repository.Save();

                if (trade.Id > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                logError(nameof(Create), ex);
                return false;
            }
        }

        public bool Update(Trade trade)
        {
            try
            {
                _repository.Trade.Update(trade);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Update), ex);
                return false;
            }
        }

        public bool Close(Trade trade)
        {
            try
            {
                trade.Status = "closed";
                _repository.Trade.Update(trade);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Close), ex);
                return false;
            }
        }

        public bool Delete(Trade trade)
        {
            try
            {
                trade.IsDeleted = true;
                _repository.Trade.Update(trade);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Delete), ex);
                return false;
            }
        }

        public IList<Trade> GetAll(bool descending = false)
        {
            try
            {
                var trade = _repository.Trade.GetAll(true);
                return trade.ToList();
            }
            catch (Exception ex)
            {
                logError(nameof(GetAll), ex);
                return null;
            }

        }

        public IList<Trade> GetStatusOpen(bool descending = false)
        {
            try
            {
                var trade = _repository.Trade.GetStatusOpen(true);
                return trade.ToList();
            }
            catch (Exception ex)
            {
                logError(nameof(GetStatusOpen), ex);
                return null;
            }
        }

        public IList<Trade> GetStatusClosed(bool descending = false)
        {
            try
            {
                var trade = _repository.Trade.GetStatusClosed(true);
                return trade.ToList();
            }
            catch (Exception ex)
            {
                logError(nameof(GetStatusOpen), ex);
                return null;
            }
        }

        public IList<Trade> GetDeleted(bool descending = false)
        {
            try
            {
                var trade = _repository.Trade.GetDeleted(true);
                return trade.ToList();
            }
            catch (Exception ex)
            {
                logError(nameof(GetStatusOpen), ex);
                return null;
            }
        }


        public IEnumerable<string> GetTickers()
        {
            try
            {
                var tickers = _repository.Trade.GetTickers();
                return tickers.ToList();
            }
            catch (Exception ex)
            {
                logError(nameof(GetAll), ex);
                return null;
            }

        }


        private void logError(string method, Exception ex) => _logger.LogError($"Something went wrong in the {method} service method {ex}");

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
            if (t.ExitPriceAvg <= 0)   
            {
                msg =  pref + "Exit Price must be more than zero.";
                return false; 
            }

            if (t.FinalCapital < 0) // Liquated will result in zero final capital
            {
                msg = pref + "Final Capital cannot be less than zero.";
                return false;
            }

            // Trade Dates validation
            t.FixSameDayDateEnterAndExit();
            if (t.DateEnter > t.DateExit)
            {
                msg = pref + $"'Date Exit' must come later in time from 'Date Enter: {t.DateEnter.ToFull()}'";
                return false;
            }

            if (t.DateExit > DateTime.Today.Midnight())  // Date Exit cannot be future dated
            {
                msg = pref + "'Date Exit'cannot exceed the Date and Time right now.";
                return false;
            }
            
            if (!t.Status.Equals("closed")) t.Status = "closed";    // just fix it and set to 'closed'

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
