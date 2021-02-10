using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TransactionDiscovery.Core.Contracts
{
	public interface IUnitOfWork : IDisposable
	{
		Task CommitAsync();
	}
}
