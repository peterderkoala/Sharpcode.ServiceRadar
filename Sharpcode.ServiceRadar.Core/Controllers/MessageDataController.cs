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

        public IQueryable<Message> GetMessages() => _brokerDbContext.Messages;


        public async Task<List<Message>> GetMessagesByIssueAsync(BusinessIssue businessIssue, CancellationToken cancellationToken = default)
            => await GetMessageByIssueIdAsync(businessIssue.BusinessIssueId, cancellationToken);

        public async Task<List<Message>> GetMessageByIssueIdAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("{caller} - Requesting messages for business issue id: {id}",
                    nameof(GetMessagesByIssueAsync),
                    id);

                return await GetMessages()
                       .Include(_ => _.BusinessIssue)
                       .Where(_ => _.BusinessIssue.BusinessIssueId == id)
                       .OrderBy(_ => _.CreatedAt)
                       .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{caller} - Error while retieving messages for business issue id: {id}",
                    nameof(GetMessagesByIssueAsync),
                    id);
                throw;
            }
        }

        public async Task CreateMessageAsync(Message input, CancellationToken cancellationToken = default)
        {
            try
            {
                using var _transaction = await _brokerDbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    input.CreatedAt = DateTime.Now;
                    _brokerDbContext.Messages.Update(input);
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
                    nameof(CreateMessageAsync),
                    input.Title);
                throw;
            }
        }
    }
}
