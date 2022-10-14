using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Repository;
using TradingTools.Services.Interface;
using TradingTools.Trunk.Contracts;

namespace TradingTools.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICalculatorStateService> _calculatorStateService;
        private readonly Lazy<ITradeService> _tradeService;
        private readonly Lazy<ITradeChallengeService> _tradeChallengeService;

        public ServiceManager(IRepositoryManager repository, ILoggerManager logger)
        {
            _calculatorStateService = new(() => new CalculatorStateService(repository, logger));
            _tradeService = new(() => new TradeService(repository, logger));
            _tradeChallengeService = new(() => new TradeChallengeService(repository, logger));
        }

        
        public ICalculatorStateService CalculatorStateService => _calculatorStateService.Value;
        public ITradeService TradeService => _tradeService.Value;
        public ITradeChallengeService TradeChallengeService => _tradeChallengeService.Value;
    }
}
