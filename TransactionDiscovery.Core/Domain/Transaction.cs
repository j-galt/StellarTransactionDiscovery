using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionDiscovery.Core.Domain
{
	public class Transaction
	{
		public Guid Id { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}
