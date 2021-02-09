using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionDiscovery.Core.Services;

namespace TransactionDiscovery.Host.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly TransactionService _transactionService;


		public WeatherForecastController(TransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var transactions = await _transactionService.GetTransactions("GAVMAUBRY2IM2ZJ5YGWRCJHV2HZ2LRQUGHGMULGU53ERK5XF2A2GWKCN");
			return Ok(transactions);
		}
	}
}
