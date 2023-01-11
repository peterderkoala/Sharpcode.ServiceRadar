using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Sharpcode.ServiceRadar.Core.Controllers;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Model.Interfaces;

namespace Sharpcode.ServiceRadar.Api.Hubs
{
    public class MessageHub : Hub<IBusinessIssueHubServer>, IBusinessIssueHubClient
    {
        private readonly ILogger<MessageHub> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MessageHub(ILogger<MessageHub> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public async Task GetOrganisation(string clientid, string mail)
        {
            try
            {
                using var scope = _serviceProvider.CreateAsyncScope();
                var organisationController = scope.ServiceProvider.GetRequiredService<OrganizationDataController>();
                var remoteClientController = scope.ServiceProvider.GetRequiredService<RemoteClientDataController>();

                if (!await remoteClientController.CheckClientExisting(mail))
                    await remoteClientController.CreateClient(mail);

                var organisation = await organisationController.GetOrganizations()
                    .Include(x => x.RemoteClients.Where(x => x.Mail.ToLower() == mail))
                    .FirstOrDefaultAsync();

                if (organisation is null)
                {
                    await Clients.Client(clientid)
                    .RespondError("Es konnte keine Organisation gefunden werden");
                }
                else
                {
                    _logger.LogInformation("{caller} answering Org: [{org}]", nameof(GetOrganisation), organisation.Title);
                    await Clients.Client(clientid).RespondOrganisation(organisation);
                }

            }
            catch (Exception ex)
            {
                // TODO Full qualified error to RemoteClient
                await Clients.Client(clientid)
                    .RespondError("Es ist ein Fehler aufgetreten");

                _logger.LogError(ex,
                                 "{caller} - Error while retrieving organisation information for client mail: {mail}",
                                 nameof(GetOrganisation),
                                 mail);
            }
        }

        public async Task GetPendingIssues(string clientid, Organization myOrganisation)
        {
            using var scope = _serviceProvider.CreateAsyncScope();
            var issueController = scope.ServiceProvider.GetRequiredService<BusinessIssueDataController>();

            var pendingIssues = await issueController.GetBusinessIssues()
                .Include(x => x.Organisation)
                .Where(x => x.ClosedAt == null && x.Organisation.OrganizationId == myOrganisation.OrganizationId)
                .ToListAsync();

            await Clients.Client(clientid)
                .RespondPendingBusinessIssues(pendingIssues);
        }

        public async Task ReloadIssueMessages(string clientid, BusinessIssue businessIssue)
        {
            using var scope = _serviceProvider.CreateAsyncScope();
            var messageController = scope.ServiceProvider.GetRequiredService<MessageDataController>();

            var messages = await messageController.GetMessagesByIssueAsync(businessIssue);
            await Clients.Client(clientid).RespondIssueMessage(messages);
        }

        public Task SendPingTest(int value)
        {
            throw new NotImplementedException();
        }
    }
}
