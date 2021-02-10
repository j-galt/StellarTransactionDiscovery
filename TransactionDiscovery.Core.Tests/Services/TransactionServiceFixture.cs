using System.Collections.Generic;
using System.Linq;

using NSubstitute;
using NUnit.Framework;
using stellar_dotnet_sdk.responses.operations;

using TransactionDiscovery.Core.Contracts;
using TransactionDiscovery.Core.Services;

namespace TransactionDiscovery.Core.Tests.Services
{
	[TestFixture]
	public class TransactionServiceFixture
	{
		private ITransactionService _transactionService;
		private IUnitOfWork _unitOfWork;
		private ITransactionRepository _transactionRepository;
		private IAccountProcessQueue _accountProcessQueue;

		[SetUp]
		public void SetUp()
		{
			_transactionService = new TransactionService(
				MockStellarRepository(),
				MockTransactionRepository(),
				Substitute.For<IAccountRepository>(),
				MockUnitOfWork(),
				MockAccountProcessQueue());
		}

		[Test]
		public void Should_call_unit_of_work_commit_to_persist_transactions()
		{
			_transactionService.AddNewTransactionsForAccounts(new string[2]);
			_unitOfWork.Received(2).CommitAsync();
		}

		[Test]
		public void Should_call_transaction_repository_to_persist_transactions()
		{
			_transactionService.AddNewTransactionsForAccounts(new string[2]);
			_transactionRepository.Received(2).AddRangeAsync(Arg.Any<IEnumerable<Domain.Transaction>>());
		}

		[Test]
		public void Should_return_new_transactions()
		{
			var transactions = _transactionService.AddNewTransactionsForAccounts(new string[2]);
			Assert.AreEqual(transactions.Result.Count(), 2);
		}

		[Test]
		public void Should_not_process_already_queued_accounts()
		{
			_accountProcessQueue.Enqueue(Arg.Any<string[]>()).Returns(new string[0]);

			var transactions = _transactionService.AddNewTransactionsForAccounts(new string[2]);

			Assert.AreEqual(transactions.Result.Count(), 0);
			_transactionRepository.DidNotReceive().AddRangeAsync(Arg.Any<IEnumerable<Domain.Transaction>>());
			_unitOfWork.DidNotReceive().CommitAsync();
		}

		private IStellarRepository MockStellarRepository()
		{
			var stellarRepository = Substitute.For<IStellarRepository>();

			stellarRepository.GetNativeAssetPayments(Arg.Any<string>(), Arg.Any<string>())
				.Returns(new List<PaymentOperationResponse>
				{
					new PaymentOperationResponse("200", StellarAssetType.Native, "", "", "", ""),
					new PaymentOperationResponse("100", StellarAssetType.Native, "", "", "", "")
				});

			return stellarRepository;
		}

		private ITransactionRepository MockTransactionRepository()
		{
			_transactionRepository = Substitute.For<ITransactionRepository>();
			return _transactionRepository;
		}

		private IUnitOfWork MockUnitOfWork()
		{
			_unitOfWork = Substitute.For<IUnitOfWork>();
			return _unitOfWork;
		}

		private IAccountProcessQueue MockAccountProcessQueue()
		{
			_accountProcessQueue = Substitute.For<IAccountProcessQueue>();
			_accountProcessQueue.Enqueue(Arg.Any<string[]>()).Returns(new string[2]);
			return _accountProcessQueue;
		}
	}
}
