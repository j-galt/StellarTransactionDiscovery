using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;

namespace TransactionDiscovery.Core.Services
{
	public class TransactionService
	{
		private readonly ServerContext _serverContext;

		public TransactionService(ServerContext serverContext)
		{
			_serverContext = serverContext;
		}

		public async Task<Page<TransactionResponse>> GetTransactions(string account)
		{
			return await _serverContext.Server.Transactions
				.ForAccount(account)
				.Execute();
		}
	}
}
