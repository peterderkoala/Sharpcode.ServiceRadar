using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Persistence;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class ApplicationDataController
    {
        private readonly ILogger<ApplicationDataController> _logger;
        private readonly BrokerDbContext _brokerDbContext;

        public ApplicationDataController(
           ILogger<ApplicationDataController> logger,
           BrokerDbContext brokerDbContext)
        {
            _logger = logger;
            _brokerDbContext = brokerDbContext;
        }

        public IQueryable<BusinessApplication> GetBusinessApplications() => _brokerDbContext.Applications;

        public async Task<List<BusinessApplication>> GetBusinessApplicationsAsync(CancellationToken cancellationToken = default)
            => await _brokerDbContext.Applications
            .Where(_ => _.DeletedAt == null)
            .ToListAsync(cancellationToken);

        public async Task UpdateBusinessApplicationAsync(BusinessApplication input, CancellationToken cancellationToken = default)
        {
            try
            {
                using var _transaction = await _brokerDbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    input.UpdatedAt = DateTime.Now;
                    _brokerDbContext.Applications.Update(input);
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
                    "{caller} - Failed Transaction for Application: {app}",
                    nameof(UpdateBusinessApplicationAsync),
                    input.Title);
                throw;
            }
        }

        public async Task CreateBusinessApplicationAsync(BusinessApplication input, CancellationToken cancellation = default)
        {
            input.CreatedAt = DateTime.Now;
            await UpdateBusinessApplicationAsync(input, cancellation);
        }
    }
}
