using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Model.Interfaces;

namespace Sharpcode.ServiceRadar.Ui.Console
{
    internal class Receiver : IBusinessIssueHubServer
    {
        public delegate void OrganisationEventHandler(object sender, Organization e);
        public event EventHandler<Organization> OrganisationEvent;

        public delegate void BusinessIssueEventHandler(object sender, List<BusinessIssue> e);
        public event EventHandler<List<BusinessIssue>> BusinessIssueEvent;

        public delegate void NewBusinessIssueEventHandler(object sender, BusinessIssue e);
        public event EventHandler<BusinessIssue> NewBusinessIssueEvent;

        private readonly ILogger<Receiver> _logger;

        public Receiver(ILogger<Receiver> logger)
        {
            _logger = logger;
        }

        public Task GetPendingBusinessIssues(string clientid, Organization myOrganisation)
        {
            throw new NotImplementedException();
        }

        public async Task NewBusinessIssue(BusinessIssue data)
        {
            _logger.LogInformation(
                "Issue: {issue} \r\n{obj}",
                data.Title,
                data);
        }

        public async Task PingTest(int value)
        {
            _logger.LogInformation("PingTest: {Value}", value);
        }

        public async Task RespondError(string message)
        {
            _logger.LogError(message);
        }

        public Task RespondIssueMessage(List<Message> data)
        {
            throw new NotImplementedException();
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

        protected virtual async Task OnOrganisationEvent(Organization organisation)
        {
            OrganisationEvent.Invoke(this, organisation);
        }

        protected virtual async Task OnBusinessIssueEvent(List<BusinessIssue> issues)
        {
            BusinessIssueEvent.Invoke(this, issues);
        }

        protected virtual async Task OnNewBusinessIssueEvent(BusinessIssue issue)
        {
            NewBusinessIssueEvent.Invoke(this, issue);
        }
    }
}
