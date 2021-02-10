using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using TransactionDiscovery.Core.Contracts;
using TransactionDiscovery.Core.Services;
using TransactionDiscovery.Infrastructure.External;
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

			services.AddScoped<IServerContext, ServerContext>();
			services.AddScoped<IStellarRepository, StellarRepository>();
			services.AddScoped<ITransactionService, TransactionService>();
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

			if (env.IsEnvironment("DockerDevelopment"))
			{
				using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
				var context = scope.ServiceProvider.GetService<TdsDbContext>();
				context.Database.Migrate();
			}
		}
	}
}
