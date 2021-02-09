using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TransactionDiscovery.Core.Domain;

namespace TransactionDiscovery.Infrastructure.Persistence
{
	public class TdsDbContext : DbContext
	{
		public DbSet<Transaction> Transactions { get; set; }

		public TdsDbContext(DbContextOptions<TdsDbContext> options) : base(options)
		{
		}
	}
}
