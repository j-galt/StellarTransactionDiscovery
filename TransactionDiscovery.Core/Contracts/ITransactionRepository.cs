using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TransactionDiscovery.Core.Domain;

namespace TransactionDiscovery.Core.Contracts
{
	public interface ITransactionRepository
	{
		IQueryable<Transaction> Transactions { get; }
		IQueryable<T> SelectMany<T>(Expression<Func<Transaction, IEnumerable<T>>> selector);
		Task AddRangeAsync(IEnumerable<Transaction> entities);
	}
}
