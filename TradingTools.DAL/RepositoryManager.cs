using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Contracts;

namespace TradingTools.Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
		private readonly TradingToolsDbContext _repositoryContext;
		private readonly Lazy<ITradeRepository> _tradeRepository;
		private readonly Lazy<ICalculatorStateRepository> _calculatorStateRepository;
		private readonly Lazy<ITradeChallengeRepository> _tradeChallengeRepository;
		private readonly Lazy<ITradeChallengeProspectRepository> _tradeChallengeProspectRepository;	

		public RepositoryManager(TradingToolsDbContext repositoryContext)
		{
			_repositoryContext = repositoryContext;
			_tradeRepository = new(() => new TradeRepository(repositoryContext));
			_calculatorStateRepository = new(() => new CalculatorStateRepository(repositoryContext));
			_tradeChallengeRepository = new(() => new TradeChallengeRepository(repositoryContext));	
			_tradeChallengeProspectRepository = new(() => new TradeChallengeProspectRepository(repositoryContext));
		}

		public ITradeRepository Trade => _tradeRepository.Value;
		public ICalculatorStateRepository CalculatorState => _calculatorStateRepository.Value;
		public ITradeChallengeRepository TradeChallenge => _tradeChallengeRepository.Value;
		public ITradeChallengeProspectRepository TradeChallengeProspect => _tradeChallengeProspectRepository.Value;

		public void Save() => _repositoryContext.SaveChanges();
	}
}
