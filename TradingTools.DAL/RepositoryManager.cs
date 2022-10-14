﻿using System;
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

		public RepositoryManager(TradingToolsDbContext repositoryContext)
		{
			_repositoryContext = repositoryContext;
			_tradeRepository = new(() => new TradeRepository(repositoryContext));
			_calculatorStateRepository = new(() => new CalculatorStateRepository(repositoryContext));
			_tradeChallengeRepository = new(() => new TradeChallengeRepository(repositoryContext));	
		}

		public ITradeRepository Trade => _tradeRepository.Value;
		public ICalculatorStateRepository CalculatorState => _calculatorStateRepository.Value;
		public ITradeChallengeRepository TradeChallenge => _tradeChallengeRepository.Value;

		public void Save() => _repositoryContext.SaveChanges();
	}
}
