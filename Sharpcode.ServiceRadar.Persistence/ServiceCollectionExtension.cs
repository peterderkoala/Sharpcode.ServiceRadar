using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sharpcode.ServiceRadar.Persistence
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBrokerDbContext(this IServiceCollection services)
        {
            services.AddDbContext<BrokerDbContext>(
                (sp, options) =>
                {
                    options.UseSqlServer(
                        sp.GetRequiredService<IConfiguration>()
                            .GetConnectionString("Default"));                    
                },
                ServiceLifetime.Transient);

            return services;
        }
    }
}
