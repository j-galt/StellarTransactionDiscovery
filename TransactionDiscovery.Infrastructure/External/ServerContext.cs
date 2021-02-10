using System;

using Microsoft.Extensions.Options;
using stellar_dotnet_sdk;

using TransactionDiscovery.Core.Contracts;

namespace TransactionDiscovery.Infrastructure.External
{
	public class ServerContext : IServerContext, IDisposable
	{
		public Server Server { get; }

		public ServerContext(IOptions<ServerConfiguration> config)
		{
			Network.UseTestNetwork();
			Server = new Server(config.Value.Uri);
		}

		public void Dispose()
		{
			Server?.Dispose();
		}
	}
}
