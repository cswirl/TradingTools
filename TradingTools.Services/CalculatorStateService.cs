using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Services.Interface;
using TradingTools.Trunk;
using TradingTools.Trunk.Contracts;
using TradingTools.Trunk.Entity;

namespace TradingTools.Services
{
    public class CalculatorStateService : ICalculatorStateService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public CalculatorStateService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public bool Add(CalculatorState calculatorState)
        {
            try
            {
                _repository.CalculatorState.Create(calculatorState);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Add), ex);
                return false;
            }
        }

        public bool Update(CalculatorState calculatorState)
        {
            try
            {
                _repository.CalculatorState.Update(calculatorState);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Update), ex);
                return false;
            }
        }

        public bool Delete(CalculatorState calculatorState)
        {
            try
            {
                _repository.CalculatorState.Delete(calculatorState);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                logError(nameof(Delete), ex);
                return false;
            }
        }

        public IList<CalculatorState> GetAllProspects(bool descending = false)
        {
            try
            {
                var prospects = _repository.CalculatorState.GetAllProspects(true);
                return prospects.ToList();
            }
            catch (Exception ex)
            {
                logError(nameof(GetAllProspects), ex);
                return null;
            }
            
        }

        private void logError(string method, Exception ex) => _logger.LogError($"Something went wrong in the {method} service method {ex}");

    }
}
