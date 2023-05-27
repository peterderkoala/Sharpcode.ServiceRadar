using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sharpcode.ServiceRadar.HubClient
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHubConnections(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            // TODO: read config
            // TODO: Every Hub Connection with different extension?
            var conn = new HubConnectionBuilder()
                .WithUrl("http://localhost:5554/messageHub")
                .Build();

            services.AddSingleton<HubConnection>(conn);

            return services;
        }
    }
}
