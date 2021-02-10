using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionDiscovery.Core.Domain
{
	public class Operation
	{
		public long Id { get; set; }
		public string Type { get; set; }
		public decimal Amount { get; set; }

		public Guid TransactionId { get; set; }
	}
}
