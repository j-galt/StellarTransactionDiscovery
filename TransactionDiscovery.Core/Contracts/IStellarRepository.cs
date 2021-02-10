using System.Collections.Generic;
using System.Threading.Tasks;

using stellar_dotnet_sdk.responses.operations;

namespace TransactionDiscovery.Core.Contracts
{
	public interface IStellarRepository
	{
		Task<IEnumerable<PaymentOperationResponse>> GetNativeAssetPayments(string account, string cursor = "");
	}
}
