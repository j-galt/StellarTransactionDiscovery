using System.Collections.Generic;

namespace TransactionDiscovery.Core.Contracts
{
	public interface IAccountProcessQueue
	{
		IEnumerable<string> Enqueue(IEnumerable<string> accountIds);
		void Dequeue(IEnumerable<string> accountIds);
	}
}
