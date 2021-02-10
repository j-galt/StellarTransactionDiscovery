using System;
using System.Collections.Generic;

namespace TransactionDiscovery.Core.Domain
{
	public class Transaction
	{
		public Guid Id { get; set; }
		public string Hash { get; set; }

		public string AccountId { get; set; }
		public ICollection<Operation> Operations { get; set; }
	}
}
