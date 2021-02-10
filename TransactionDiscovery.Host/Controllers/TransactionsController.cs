using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TransactionDiscovery.Core.Contracts;

namespace TransactionDiscovery.Host.Controllers
{
	[ApiController]
	[Route("accounts/transactions")]
	public class TransactionsController1 : ControllerBase
	{
		private readonly ITransactionService _transactionService;

		public TransactionsController1(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpPost]
		public async Task<IActionResult> AddNewTransactions([FromBody] string[] accountIds)
		{
			return Ok(await _transactionService.AddNewTransactionsForAccounts(accountIds));
		}
	}
}
