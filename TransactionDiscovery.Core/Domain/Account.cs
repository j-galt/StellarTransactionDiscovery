﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace TransactionDiscovery.Core.Domain
{
	public class Account
	{
		public string Id { get; set; }
		public ulong SequenceNumber { get; set; }

		public ICollection<Transaction> Transactions { get; set; }
	}
}
