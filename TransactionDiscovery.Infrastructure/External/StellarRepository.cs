using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using stellar_dotnet_sdk.responses.operations;

using TransactionDiscovery.Core.Contracts;

namespace TransactionDiscovery.Infrastructure.External
{
	public class StellarRepository : IStellarRepository
	{
		private readonly IServerContext _serverContext;

		public StellarRepository(IServerContext serverContext)
		{
			_serverContext = serverContext;
		}

		public async Task<IEnumerable<PaymentOperationResponse>> GetNativeAssetPayments(
			string account,
			string cursor = "")
		{
			var operations = await _serverContext.Server.Payments
				.ForAccount(account)
				.Cursor(cursor)
				.Execute();

			return operations.Records
				.Where(o => o.Type == StellarTransactionType.Payment)
				.OfType<PaymentOperationResponse>()
				.Where(p => p.AssetType == StellarAssetType.Native);
		}
	}
}
