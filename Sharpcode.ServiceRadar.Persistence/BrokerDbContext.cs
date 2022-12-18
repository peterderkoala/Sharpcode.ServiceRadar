using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Sharpcode.ServiceRadar.Model.Entities;
using System.Text;

namespace Sharpcode.ServiceRadar.Persistence
{
    public class BrokerDbContext : DbContext
    {
        private readonly string _key = "NcRfUjXn2r5u8x/A%D*G-KaPdSgVkYp3";
        private readonly string _iv = "TjWnZr4u7x!A%D*G";

        private readonly IEncryptionProvider _provider;

        public BrokerDbContext(DbContextOptions<BrokerDbContext> options)
            : base(options)
        {
            var aes = Encoding.ASCII.GetBytes(_key, 0, _key.Length);
            var iv = Encoding.ASCII.GetBytes(_iv, 0, _iv.Length);
            //this._provider = new AesProvider(aes);
            _provider = new AesProvider(aes, iv);
        }

        public DbSet<BusinessApplication> Applications => Set<BusinessApplication>();
        public DbSet<BusinessIssue> BusinessIssues => Set<BusinessIssue>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Issuer> Issuers => Set<Issuer>();
        public DbSet<Organisation> Organizations => Set<Organisation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_provider);
            modelBuilder.Entity<BusinessApplication>()
                .ToTable("Application")
                .HasMany(_ => _.BusinessIssues)
                .WithOne(_ => _.BusinessApplication);

            modelBuilder.Entity<BusinessIssue>()
                .ToTable("BusinessIssue")
                .HasMany(x => x.BusinessApplications)
                .WithOne(x => x.BusinessIssue);

            modelBuilder.Entity<Message>()
                .ToTable("Message")
                .HasOne(x => x.BusinessIssue)
                .WithMany(y => y.BusinessIssueMessages);

            modelBuilder.Entity<Issuer>()
                .ToTable("Issuer");

            modelBuilder.Entity<Organisation>()
                .ToTable("Organisation");
        }
    }
}
