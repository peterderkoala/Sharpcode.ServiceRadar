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
        public DbSet<RemoteClient> RemoteClients => Set<RemoteClient>();

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
                .WithMany(y => y.Messages);

            modelBuilder.Entity<Issuer>()
                .ToTable("Issuer");

            modelBuilder.Entity<Organisation>()
                .ToTable("Organisation");

            modelBuilder.Entity<RemoteClient>()
                .ToTable("RemoteClient");


            #region demo data
            modelBuilder.Entity<Organisation>()
                .HasData(
                new Organisation
                {
                    OrganisationId = 1,
                    Title = "Default",
                    Desription = "Default organisation after initial migration"
                });

            modelBuilder.Entity<BusinessApplication>()
                .HasData(
                new BusinessApplication
                {
                    ApplicationId = 1,
                    Title = "Demo Application",
                    Description = "Demo Application after initial migration",
                    CreatedAt = new DateTimeOffset(new DateTime(2000, 1, 1)),
                    Version = "0.0.1"
                });

            modelBuilder.Entity<Issuer>()
                .HasData(
                new Issuer
                {
                    IssuerId = 1,
                    IssuerName = "Demo Issuer",
                    IssuerMail = "demo@serviceradar.net",
                    CreatedAt = new DateTimeOffset(new DateTime(2000, 1, 1))
                });

            modelBuilder.Entity<BusinessIssue>()
                .HasData(
                new BusinessIssue
                {
                    BusinessIssueId = 1,
                    Title = "Demo issue",
                    Body = "This is a demo issue after initial migration",
                    BusinessIssuePriority = BusinessIssue.IssuePriorities.High,
                    ImpactDuration = new TimeSpan(1, 1, 1),
                    IssuedAt = new DateTimeOffset(new DateTime(2000, 1, 1)),
                    OrganisationId = 1,
                    IssuerId = 1,
                    IssueType = BusinessIssue.IssueTypes.Maintenance
                });

            modelBuilder.Entity<BusinessIssue2Application>()
                .HasData(
                new BusinessIssue2Application
                {
                    BusinessIssue2ApplicationId = 1,
                    BusinessApplicationId = 1,
                    BusinessIssueId = 1
                });

            modelBuilder.Entity<Message>()
                .HasData(
                new Message
                {
                    MessageId = 1,
                    BusinessIssueId = 1,
                    Title = "Demo Message 1",
                    MessageBody = "First demo message.",
                    CreatedAt = new DateTimeOffset(new DateTime(2000, 1, 1))
                });
            #endregion
        }
    }
}
