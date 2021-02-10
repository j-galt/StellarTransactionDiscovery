using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TransactionDiscovery.Core.Contracts;
using TransactionDiscovery.Core.Services;
using Microsoft.EntityFrameworkCore;
using TransactionDiscovery.Infrastructure.Persistence;

namespace TransactionDiscovery.Host
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.Configure<ServerConfiguration>(Configuration.GetSection("Horizon"));
			services.AddDbContext<TdsDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("TdsDb")));

			services.AddScoped<ServerContext>();
			services.AddScoped<TransactionService>();
			services.AddScoped<ITransactionRepository, TransactionRepository>();
			services.AddScoped<IAccountRepository, AccountRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddSingleton<IAccountProcessQueue, AccountProcessQueue>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
