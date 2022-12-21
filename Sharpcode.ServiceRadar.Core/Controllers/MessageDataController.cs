using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Persistence;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class MessageDataController
    {
        private readonly ILogger<MessageDataController> _logger;
        private readonly BrokerDbContext _brokerDbContext;

        public MessageDataController(ILogger<MessageDataController> logger, BrokerDbContext brokerDbContext)
        {
            _logger = logger;
            _brokerDbContext = brokerDbContext;
        }

        public IQueryable<Message> GetMessages()
        {
            return _brokerDbContext.Messages;
        }

        public async Task<List<Message>> GetMessagesByIssueAsync(BusinessIssue businessIssue, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("{caller} - Requesting messages for business issue id: {id}",
                    nameof(GetMessagesByIssueAsync),
                    businessIssue.BusinessIssueId);

                return await GetMessages()
                       .Include(_ => _.BusinessIssue.BusinessIssueId == businessIssue.BusinessIssueId)
                       .OrderBy(_ => _.CreatedAt)
                       .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{caller} - Error while retieving messages for business issue id: {id}",
                    nameof(GetMessagesByIssueAsync),
                    businessIssue.BusinessIssueId);
                throw;
            }
        }
    }
}
