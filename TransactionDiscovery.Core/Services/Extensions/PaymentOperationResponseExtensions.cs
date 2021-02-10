using System;
using System.Collections.Generic;
using System.Linq;

using stellar_dotnet_sdk.responses.operations;

using TransactionDiscovery.Core.Domain;

namespace TransactionDiscovery.Core.Services.Extensions
{
	public static class PaymentOperationResponseExtensions
	{
		public static IEnumerable<Transaction> ToTransactions(this IEnumerable<PaymentOperationResponse> payments, string accountId)
		{
			return payments
				.GroupBy(p => p.TransactionHash)
				.Select(tr => new Transaction
				{
					Id = Guid.NewGuid(),
					Hash = tr.Key,
					AccountId = accountId,
					Operations = tr.Select(o => new Operation
					{
						Id = o.Id,
						Amount = decimal.Parse(o.Amount),
						Type = o.AssetType
					}).ToArray()
				});
		}
	}
}
