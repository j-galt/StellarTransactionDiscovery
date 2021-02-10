using System;
using System.Linq;
using System.Threading.Tasks;

using TransactionDiscovery.Core.Contracts;
using TransactionDiscovery.Core.Domain;

namespace TransactionDiscovery.Infrastructure.Persistence
{
	public class AccountRepository : IAccountRepository
	{
		private readonly TdsDbContext _dbContext;

		public IQueryable<Account> Accounts => _dbContext.Accounts.AsQueryable();

		public AccountRepository(TdsDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public bool Exists(string id)
		{
			return Accounts.Any(a => a.Id == id);
		}

		public async Task AddIfNotExistsAsync(string id)
		{
			if (!Exists(id)) await AddAsync(id);
		}

		public async Task AddAsync(string id)
		{
			await _dbContext.Accounts.AddAsync(new Account { Id = id });
		}
	}
}
