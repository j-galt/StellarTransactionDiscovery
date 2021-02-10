using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using TransactionDiscovery.Core.Contracts;

namespace TransactionDiscovery.Core.Services
{
	public class AccountProcessQueue : IAccountProcessQueue
	{
		private readonly ConcurrentDictionary<string, byte> _accountsInProgress
			= new ConcurrentDictionary<string, byte>();

		public IEnumerable<string> Enqueue(IEnumerable<string> accountIds)
		{
			return accountIds.Where(accountId => _accountsInProgress.TryAdd(accountId, byte.MinValue));
		}

		public void Dequeue(IEnumerable<string> accountIds)
		{
			foreach (var accountId in accountIds)
				_accountsInProgress.TryRemove(accountId, out _);
		}
	}
}
