using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Services.Interface;
using TradingTools.Trunk.Contracts;
using TradingTools.Trunk.Entity;

namespace TradingTools.Services
{
    public class TradeChallengeService : ITradeChallengeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public TradeChallengeService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public bool Create(TradeChallenge tradeChallenge)
        {
            try
            {
                tradeChallenge.IsOpen = true;
                _repository.TradeChallenge.Create(tradeChallenge);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Create), ex);
                return false;
            }
        }

        public bool Update(TradeChallenge tradeChallenge)
        {
            try
            {
                _repository.TradeChallenge.Update(tradeChallenge);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Update), ex);
                return false;
            }
        }

        public bool Close(TradeChallenge tradeChallenge)
        {
            try
            {
                tradeChallenge.IsOpen = false;
                _repository.TradeChallenge.Update(tradeChallenge);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Close), ex);
                return false;
            }
        }

        public bool Delete(TradeChallenge tradeChallenge)
        {
            try
            {
                tradeChallenge.IsDeleted = true;
                _repository.TradeChallenge.Update(tradeChallenge);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Delete), ex);
                return false;
            }
        }

        public IList<TradeChallenge> GetStatusOpen(bool descending = false)
        {
            try
            {
                var tc = _repository.TradeChallenge.GetStatusOpen(true);
                return tc.ToList();
            }
            catch (Exception ex)
            {
                logError(nameof(GetStatusOpen), ex);
                return null;
            }
        }

        public IList<TradeChallenge> GetStatusClosed(bool descending = false)
        {
            try
            {
                var tc = _repository.TradeChallenge.GetStatusClosed(true);
                return tc.ToList();
            }
            catch (Exception ex)
            {
                logError(nameof(GetStatusClosed), ex);
                return null;
            }
        }

        private void logError(string method, Exception ex) => _logger.LogError($"Something went wrong in the {method} service method {ex}");
    }
}
