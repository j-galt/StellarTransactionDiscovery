using System;
using System.Threading.Tasks;

namespace TransactionDiscovery.Core.Contracts
{
	public interface IUnitOfWork : IDisposable
	{
		Task CommitAsync();
	}
}
