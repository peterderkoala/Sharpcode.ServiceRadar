using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Persistence;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class BusinessIssueDataController
    {
        private readonly ILogger<BusinessIssueDataController> _logger;
        private readonly BrokerDbContext _brokerDbContext;

        public BusinessIssueDataController(
            ILogger<BusinessIssueDataController> logger,
            BrokerDbContext brokerDbContext)
        {
            _logger = logger;
            _brokerDbContext = brokerDbContext;
        }

        public IQueryable<BusinessIssue> GetBusinessIssues() => _brokerDbContext.BusinessIssues;

        public async Task<List<BusinessIssue>> GetBusinessIssuesAsync(CancellationToken cancellationToken = default)
            => await _brokerDbContext.BusinessIssues
            .Where(_ => _.ClosedAt == null)
            .ToListAsync(cancellationToken);

        public async Task<BusinessIssue> UpdateBusinessIssuesAsync(BusinessIssue input, CancellationToken cancellationToken = default)
        {
            try
            {
                using var _transaction = await _brokerDbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    input.UpdatedAt = DateTime.Now;
                    _brokerDbContext.BusinessIssues.Update(input);
                    await _brokerDbContext.SaveChangesAsync(cancellationToken);
                    await _transaction.CommitAsync(cancellationToken);
                    return input;
                }
                catch (Exception ex)
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
                    nameof(UpdateBusinessIssuesAsync),
                    input.Title);
                throw;
            }
        }

        public async Task<BusinessIssue> CreateBusinessIssueAsync(BusinessIssue input, CancellationToken cancellation = default)
        {
            input.IssuedAt = DateTime.Now;
            return await UpdateBusinessIssuesAsync(input, cancellation);
        }
    }
}
