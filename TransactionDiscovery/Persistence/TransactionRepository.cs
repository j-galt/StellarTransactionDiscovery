using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using TransactionDiscovery.Core.Contracts;
using TransactionDiscovery.Core.Domain;

namespace TransactionDiscovery.Infrastructure.Persistence
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly TdsDbContext _dbContext;

		public IQueryable<Transaction> Transactions => _dbContext.Transactions.AsQueryable();

		public TransactionRepository(TdsDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public IQueryable<T> SelectMany<T>(Expression<Func<Transaction, IEnumerable<T>>> selector)
		{
			return _dbContext.Transactions.SelectMany(selector);
		}

		public async Task AddRangeAsync(IEnumerable<Transaction> entities)
		{
			await _dbContext.Transactions.AddRangeAsync(entities);
		}
	}
}
