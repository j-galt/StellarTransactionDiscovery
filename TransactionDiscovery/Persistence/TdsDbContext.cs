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
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Operation> Operations { get; set; }

		public TdsDbContext(DbContextOptions<TdsDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Operation>().HasKey(o => new { o.Id, o.TransactionId });
			modelBuilder.Entity<Operation>()
				.HasOne<Transaction>()
				.WithMany(t => t.Operations);
			modelBuilder.Entity<Transaction>()
				.HasOne<Account>()
				.WithMany(a => a.Transactions)
				.HasForeignKey(t => t.AccountId);
		}
	}
}
