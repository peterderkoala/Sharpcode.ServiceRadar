using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Persistence;

namespace Sharpcode.ServiceRadar.Core.Controllers
{
    public class RemoteClientDataController
    {
        private readonly ILogger<RemoteClientDataController> _logger;
        private readonly BrokerDbContext _brokerDbContext;

        public RemoteClientDataController(
            ILogger<RemoteClientDataController> logger,
            BrokerDbContext brokerDbContext)
        {
            _logger = logger;
            _brokerDbContext = brokerDbContext;
        }

        public async Task<bool> CheckClientExisting(string clientMail, CancellationToken cancellationToken = default)
            => await _brokerDbContext.RemoteClients
                .AnyAsync(_ => _.Mail.ToLower() == clientMail.ToLower(), cancellationToken);

        public async Task CreateClient(string clientMail, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("{caller} - Init remoteclient mail: {mail}",
                             nameof(CreateClient),
                             clientMail);

            try
            {
                using var transaction = await _brokerDbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    RemoteClient rc = new()
                    {
                        Mail = clientMail,
                        RemoteClientKey = Guid.NewGuid().ToString(),
                        OrganisationId = 1 // TODO: Hier sollte die passende organisation ermittelt werden.
                    };
                    _brokerDbContext.Update(rc);
                    await _brokerDbContext.SaveChangesAsync(cancellationToken);

                    _logger.LogDebug("{caller} - created remoteclient mail: {mail} with id: {id}",
                                     nameof(CreateClient),
                                     clientMail,
                                     rc.RemoteClientId);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                                 "{caller} - Error while creating client, rolling back! mail: {mail}",
                                 nameof(CreateClient),
                                 clientMail);
                throw;
            }
        }
    }
}
