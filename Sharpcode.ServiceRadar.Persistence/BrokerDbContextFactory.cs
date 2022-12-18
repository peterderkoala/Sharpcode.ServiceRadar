using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sharpcode.ServiceRadar.Persistence
{
    public class BrokerDbContextFactory : IDesignTimeDbContextFactory<BrokerDbContext>
    {
        public BrokerDbContext CreateDbContext(string[] args)
        {

            var dbContextBuilder = new DbContextOptionsBuilder<BrokerDbContext>()
                .UseSqlServer(connectionString: "Server=(localdb)\\mssqllocaldb;Database=Sharpcode.ServiceRadar;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False");

            return new BrokerDbContext(dbContextBuilder.Options);
        }
    }
}
