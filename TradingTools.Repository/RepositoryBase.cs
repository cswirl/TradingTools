﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Repository;
using TradingTools.Trunk.Contracts;
using TradingTools.Trunk.Entity;

namespace TradingTools.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
		protected TradingToolsDbContext RepositoryContext;

		public RepositoryBase(TradingToolsDbContext repositoryContext)
			=> RepositoryContext = repositoryContext;

		public IQueryable<T> FindAll() 
			=> RepositoryContext.Set<T>();

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) 
			=> RepositoryContext.Set<T>()
				.Where(expression);

		public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

		public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

		public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
	}
}
