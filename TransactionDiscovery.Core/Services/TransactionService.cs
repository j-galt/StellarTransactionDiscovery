using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using stellar_dotnet_sdk.responses.page;
using stellar_dotnet_sdk.responses.results;

namespace TransactionDiscovery.Core.Services
{
	public class TransactionService
	{
		private readonly ServerContext _serverContext;

		public TransactionService(ServerContext serverContext)
		{
			_serverContext = serverContext;
		}

		public async Task<IEnumerable<OperationResponse>> GetPaymentTransactions(string account)
		{
			var operations = await _serverContext.Server.Payments
				.ForAccount(account)
				.Execute();

			return operations.Records.Where(o =>
				o.Type == "payment" && ((PaymentOperationResponse)o).AssetType == "native");
		}
	}
}
