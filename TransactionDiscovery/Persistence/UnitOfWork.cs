using System.Threading.Tasks;

using TransactionDiscovery.Core.Contracts;

namespace TransactionDiscovery.Infrastructure.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly TdsDbContext _dbContext;

		public UnitOfWork(TdsDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task CommitAsync()
		{
			await _dbContext.SaveChangesAsync();
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}
