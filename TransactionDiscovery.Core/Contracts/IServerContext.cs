using stellar_dotnet_sdk;

namespace TransactionDiscovery.Core.Contracts
{
	public interface IServerContext
	{
		Server Server { get; }
	}
}
