using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Persistence;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class OrganizationDataController
    {
        private readonly ILogger<OrganizationDataController> _logger;
        private readonly BrokerDbContext _brokerDbContext;

        public OrganizationDataController(
            ILogger<OrganizationDataController> logger,
            BrokerDbContext brokerDbContext)
        {
            _logger = logger;
            _brokerDbContext = brokerDbContext;
        }

        public IQueryable<Organization> GetOrganizations() => _brokerDbContext.Organizations;

        public async Task<List<Organization>> GetOrganizationsAsync(CancellationToken cancellationToken = default)
            => await _brokerDbContext.Organizations
            .Where(_ => _.DeletedAt == null)
            .ToListAsync(cancellationToken);

        public async Task UpdateOrganizationAsync(
            Organization input,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var _transaction = await _brokerDbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    input.UpdatedAt = DateTime.Now;
                    _brokerDbContext.Organizations.Update(input);
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
                    "{caller} - Failed Transaction for Issue: {issue}",
                    nameof(UpdateOrganizationAsync),
                    input.Title);
                throw;
            }
        }

        public async Task CreateOrganizationAsync(
            Organization input,
            CancellationToken cancellation = default)
        {
            input.CreatedAt = DateTime.Now;
            await UpdateOrganizationAsync(input, cancellation);
        }
    }
}
