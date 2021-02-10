using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using stellar_dotnet_sdk.responses.operations;

using TransactionDiscovery.Core.Contracts;
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

		public async Task AddNewTransactionsForAccounts(IEnumerable<string> accountIds)
		{
			var queuedAccounts = _accountProcessQueue.Enqueue(accountIds).ToList();

			foreach (var accountId in queuedAccounts)
			{
				await _accountRepository.AddIfNotExistsAsync(accountId);

				var lastPaymentId = GetLastPaymentId(accountId);
				var payments = await GetNativeAssetPayments(accountId, lastPaymentId.ToString());

				await _transactionRepository.AddRangeAsync(payments.ToTransactions(accountId));
				await _unitOfWork.CommitAsync();
			}

			_accountProcessQueue.Dequeue(queuedAccounts);
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

		private long GetLastPaymentId(string accountId)
		{
			return _transactionRepository.Transactions
				.Where(t => t.AccountId == accountId)
				.SelectMany(t => t.Operations, (operations, operation) => operation.Id)
				.DefaultIfEmpty()
				.Max();
		}
	}
}
