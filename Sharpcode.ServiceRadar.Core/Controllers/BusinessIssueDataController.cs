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

        public async Task<BusinessIssue> CreateOrUpdateBusinessIssueAsync(BusinessIssue data, CancellationToken cancellationToken)
        {
            using var transaction = await _brokerDbContext.Database.BeginTransactionAsync();
            try
            {
                var result = _brokerDbContext.BusinessIssues.Update(data);
                await _brokerDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{method} - Error while create/update BusinessIssue: {data}", nameof(CreateOrUpdateBusinessIssueAsync), data);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
