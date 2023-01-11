using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Persistence;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class IssuerDataController
    {
        private readonly ILogger<IssuerDataController> _logger;
        private readonly BrokerDbContext _brokerDbContext;

        public IssuerDataController(
            ILogger<IssuerDataController> logger,
            BrokerDbContext brokerDbContext)
        {
            _logger = logger;
            _brokerDbContext = brokerDbContext;
        }

        public IQueryable<Issuer> GetIssuers() => _brokerDbContext.Issuers;

        public async Task<List<Issuer>> GetIssuersAsync(CancellationToken cancellationToken = default)
            => await _brokerDbContext.Issuers
            .Where(_ => _.DeletedAt == null)
            .ToListAsync(cancellationToken);

        public async Task UpdateIssuerAsync(Issuer input, CancellationToken cancellationToken = default)
        {
            try
            {
                using var _transaction = await _brokerDbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    input.UpdatedAt = DateTime.Now;
                    _brokerDbContext.Issuers.Update(input);
                    await _brokerDbContext.SaveChangesAsync(cancellationToken);
                    await _transaction.CommitAsync(cancellationToken);
                }
                catch (Exception)
                {
                    await _transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "{caller} - Failed Transaction for Issuer: {issue}",
                    nameof(UpdateIssuerAsync),
                    input.IssuerMail);
                throw;
            }
        }

        public async Task CreateIssuerAsync(Issuer input, CancellationToken cancellation = default)
        {
            input.CreatedAt = DateTime.Now;
            await UpdateIssuerAsync(input, cancellation);
        }
    }
}
