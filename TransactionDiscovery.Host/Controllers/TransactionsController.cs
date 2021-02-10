using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TransactionDiscovery.Core.Services;

namespace TransactionDiscovery.Host.Controllers
{
	[ApiController]
	[Route("accounts/transactions")]
	public class TransactionsController1 : ControllerBase
	{
		private readonly TransactionService _transactionService;

		public TransactionsController1(TransactionService transactionService)
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
