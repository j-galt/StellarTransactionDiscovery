using System.Threading.Tasks;

namespace TransactionDiscovery.Core.Contracts
{
	public interface IAccountRepository
	{
		Task AddIfNotExistsAsync(string id);
	}
}
