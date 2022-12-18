using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Sharpcode.ServiceRadar.Core.Controllers;
using Sharpcode.ServiceRadar.Core.Policies;

namespace Sharpcode.ServiceRadar.Core
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDataControllers(this IServiceCollection services)
        {
            services.AddTransient<ApplicationDataController>();
            services.AddTransient<BusinessIssueDataController>();
            services.AddTransient<IssuerDataController>();
            services.AddTransient<MessageDataController>();
            services.AddTransient<OrganisationDataController>();

            return services;
        }

        public static IServiceCollection AddHubControllers(this IServiceCollection services)
        {
            services.AddSingleton<IHubConnectionBuilder, BusinessHubConnectionBuilder>();
            services.AddTransient<BusinessIssueHubController>();
            return services;
        }

        public static IServiceCollection AddClientRetryPolicies(this IServiceCollection services)
        {
            services.AddSingleton<ClientPolicy>();
            return services;
        }
    }
}
