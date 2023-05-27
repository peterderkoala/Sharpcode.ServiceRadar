using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Model.Interfaces;

namespace Sharpcode.ServiceRadar.HubClient.Receivers
{
    internal sealed partial class MessageReceiver : MessageReceiverEvents, IBusinessIssueHubServer
    {
        private readonly ILogger<MessageReceiver> _logger;

        public MessageReceiver(ILogger<MessageReceiver> logger)
        {
            _logger = logger;
        }
        public Task PingTest(int value)
        {
            throw new NotImplementedException();
        }

        public async Task GetPendingBusinessIssues(string clientid, Organization myOrganisation)
        {
            throw new NotImplementedException();
        }

        public async Task NewBusinessIssue(BusinessIssue data)
        {
            throw new NotImplementedException();
        }


        public async Task RespondError(string message)
        {
            throw new NotImplementedException();
        }

        public async Task RespondIssueMessage(List<Message> data)
        {
            await OneNewIssueMessageEvent(data);
        }

        public async Task RespondOrganisation(Organization organisation)
        {
            await OnOrganisationEvent(organisation);
        }

        public async Task RespondPendingBusinessIssues(List<BusinessIssue> data)
        {
            await OnBusinessIssueEvent(data);
        }

        public async Task UpdateBusinessIssue(BusinessIssue update)
        {
            await OnNewBusinessIssueEvent(update);
        }
    }
}
