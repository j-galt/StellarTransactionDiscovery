using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionDiscovery.Core.Contracts
{
	public interface IAccountProcessQueue
	{
		IEnumerable<string> Enqueue(IEnumerable<string> accountIds);
		void Dequeue(IEnumerable<string> accountIds);
	}
}
