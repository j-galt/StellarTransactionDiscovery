using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using stellar_dotnet_sdk.responses.operations;

using TransactionDiscovery.Core.Contracts;
using TransactionDiscovery.Core.Domain;
using TransactionDiscovery.Core.Services.Extensions;

namespace TransactionDiscovery.Core.Services
{
	public class TransactionService
	{
		private readonly ServerContext _serverContext;
		private readonly IAccountProcessQueue _accountProcessQueue;
		private readonly ITransactionRepository _transactionRepository;
		private readonly IAccountRepository _accountRepository;
		private readonly IUnitOfWork _unitOfWork;

		public TransactionService(
			ServerContext serverContext,
			IAccountProcessQueue accountProcessQueue,
			ITransactionRepository transactionRepository,
			IUnitOfWork unitOfWork,
			IAccountRepository accountRepository)
		{
			_serverContext = serverContext;
			_transactionRepository = transactionRepository;
			_unitOfWork = unitOfWork;
			_accountRepository = accountRepository;
			_accountProcessQueue = accountProcessQueue;
		}

		public async Task<IEnumerable<Transaction>> AddNewTransactionsForAccounts(IEnumerable<string> accountIds)
		{
			var newTransactions = new List<Transaction>();
			var queuedAccounts = _accountProcessQueue.Enqueue(accountIds).ToList();

			foreach (var accountId in queuedAccounts)
			{
				await _accountRepository.AddIfNotExistsAsync(accountId);
				var transactions = await GetNewTransactions(accountId);

				await _transactionRepository.AddRangeAsync(transactions);
				await _unitOfWork.CommitAsync();
				newTransactions.AddRange(transactions);
			}

			_accountProcessQueue.Dequeue(queuedAccounts);
			return newTransactions;
		}

		private async Task<Transaction[]> GetNewTransactions(string accountId)
		{
			var lastPaymentId = GetLastPaymentId(accountId);
			var payments = await GetNativeAssetPayments(accountId, lastPaymentId.ToString());
			return payments.ToTransactions(accountId).ToArray();
		}

		private long GetLastPaymentId(string accountId)
		{
			return _transactionRepository.Transactions
				.Where(t => t.AccountId == accountId)
				.SelectMany(t => t.Operations, (operations, operation) => operation.Id)
				.DefaultIfEmpty()
				.Max();
		}

		private async Task<IEnumerable<PaymentOperationResponse>> GetNativeAssetPayments(
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
