using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TransactionDiscovery.Core.Contracts
{
	public interface IRepository<T> where T : class
	{
		Task<T> GetByIdAsync(int id);
		IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
		Task AddAsync(T entity);
		Task AddRangeAsync(IEnumerable<T> entities);
	}
}
