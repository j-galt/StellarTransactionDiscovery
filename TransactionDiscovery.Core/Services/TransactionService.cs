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

		public async Task AddNewTransactionsForAccounts(IEnumerable<string> accountIds)
		{
			foreach (var accountId in accountIds)
			{
				var payments = await GetNativeAssetPayments(accountId);

				var transactions = payments
					.GroupBy(p => p.TransactionHash)
					.Select(tr => new Domain.Transaction
					{
						Id = Guid.NewGuid(),
						Hash = tr.Key,
						SourceAccountId = tr.First().SourceAccount,
						Operations = tr.Select(o => new Domain.Operation
						{
							Id = o.Id,
							Amount = decimal.Parse(o.Amount),
							Type = o.AssetType
						}).ToArray()
					});

				// persist trs
			}
		}

		public async Task<IEnumerable<PaymentOperationResponse>> GetNativeAssetPayments(
			string account,
			string cursor = "")
		{
			var operations = await _serverContext.Server.Payments
				.ForAccount(account)
				.Cursor(cursor)
				.Execute();

			//Create constants
			return operations.Records
				.Where(o => o.Type == "payment")
				.OfType<PaymentOperationResponse>()
				.Where(p => p.AssetType == "native");
		}
	}
}
