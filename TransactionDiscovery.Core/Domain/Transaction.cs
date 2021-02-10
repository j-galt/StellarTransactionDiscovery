using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionDiscovery.Core.Domain
{
	public class Transaction
	{
		public Guid Id { get; set; }
		public decimal Fee { get; set; }
		public long SequenceNumber { get; set; }

		public string SourceAccountId { get; set; }
		public ICollection<Operation> Operations { get; set; }
	}
}
