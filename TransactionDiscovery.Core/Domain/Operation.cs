using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionDiscovery.Core.Domain
{
	public class Operation
	{
		public Guid Id { get; set; }
		public string Type { get; set; }

		public Guid TransactionId { get; set; }
	}
}
