using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TransactionDiscovery.Core.Contracts;

namespace TransactionDiscovery.Infrastructure.Persistence
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly TdsDbContext _dbContext;

		public Repository(TdsDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
		{
			return _dbContext.Set<T>().Where(predicate).AsEnumerable();
		}

		public async Task AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
		}

		public async Task AddRangeAsync(IEnumerable<T> entities)
		{
			await _dbContext.Set<T>().AddRangeAsync(entities);
		}
	}
}
