using System.Collections.Generic;
using System.Threading.Tasks;

using TransactionDiscovery.Core.Domain;

namespace TransactionDiscovery.Core.Contracts
{
	public interface ITransactionService
	{
		Task<IEnumerable<Transaction>> AddNewTransactionsForAccounts(IEnumerable<string> accountIds);
	}
}
