using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TransactionDiscovery.Core.Contracts;
using TransactionDiscovery.Core.Domain;
using TransactionDiscovery.Core.Services.Extensions;

namespace TransactionDiscovery.Core.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly IStellarRepository _stellarRepository;
		private readonly IAccountProcessQueue _accountProcessQueue;
		private readonly ITransactionRepository _transactionRepository;
		private readonly IAccountRepository _accountRepository;
		private readonly IUnitOfWork _unitOfWork;

		public TransactionService(
			IStellarRepository stellarRepository,
			ITransactionRepository transactionRepository,
			IAccountRepository accountRepository,
			IUnitOfWork unitOfWork,
			IAccountProcessQueue accountProcessQueue)
		{
			_stellarRepository = stellarRepository;
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
			var payments = await _stellarRepository.GetNativeAssetPayments(accountId, lastPaymentId.ToString());
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
	}
}
