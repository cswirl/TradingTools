using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Services.Interface;
using TradingTools.Trunk;
using TradingTools.Trunk.Contracts;
using TradingTools.Trunk.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TradingTools.Services
{
    internal sealed class TradeChallengeProspectService : ITradeChallengeProspectService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public TradeChallengeProspectService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public bool Create(TradeChallengeProspect tradeChallengeProspect)
        {
            try
            {
                var calcState = tradeChallengeProspect.CalculatorState;
                var tradeChallenge = tradeChallengeProspect.TradeChallenge;
                if (calcState == default)
                {
                    _logger.LogError("TradeChallengeProspect requires an instance of CalculatorState");
                    return false;
                }
                if (tradeChallenge == default)
                {
                    _logger.LogError("TradeChallengeProspect requires an instance of TradeChallenge");
                    return false;
                }
                // configure relationship values
                calcState.TradeChallengeProspect = tradeChallengeProspect;
               
                // EF Core will create a new TradeChallengeProspect record into the Database
                _repository.CalculatorState.Update(calcState);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Create), ex);
                return false;
            }
        }

        public TradeChallengeProspect Delete(CalculatorState calculatorState)
        {
            try
            {
                var tcp =_repository.TradeChallengeProspect.Delete(calculatorState);

                return tcp;
            }
            catch (Exception ex)
            {
                logError(nameof(Delete), ex);
                return default;
            }
        }

        public bool DeleteBatch(TradeChallengeProspect[] tcp)
        {
            try
            {
                _repository.TradeChallengeProspect.DeleteBatch(tcp);

                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(DeleteBatch), ex);
                return false;
            }
        }

        public IList<CalculatorState> GetAllByTradeChallenge(int tradeChallengeId, bool descending = false)
        {
            try
            {
                var c = _repository.TradeChallengeProspect.GetAllByTradeChallenge(tradeChallengeId, descending);
                return c.ToList();
            }
            catch (Exception ex)
            {
                logError(nameof(GetAllByTradeChallenge), ex);
                return default;
            }
        }

        public int GetTradeChallengeId(int calculatorStateId)
        {
            try
            {
                var id = _repository.TradeChallengeProspect.GetTradeChallengeId(calculatorStateId);
                return id;
            }
            catch (Exception ex)
            {
                logError(nameof(GetTradeChallengeId), ex);
                return 0;
            }
        }

        private void logError(string method, Exception ex) => _logger.LogError($"Something went wrong in the {method} service method {ex}");
    }
}
