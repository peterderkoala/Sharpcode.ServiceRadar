using Microsoft.Extensions.Logging;
using Sharpcode.ServiceRadar.Model.Entities;
using Sharpcode.ServiceRadar.Model.Interfaces;

namespace Sharpcode.ServiceRadar.Ui.Console
{
    internal class Receiver : IBusinessIssueHubServer
    {
        private readonly ILogger<Receiver> _logger;

        public Receiver(ILogger<Receiver> logger)
        {
            _logger = logger;
        }

        public Task GetPendingBusinessIssues(string clientid, Organisation myOrganisation)
        {
            throw new NotImplementedException();
        }

        public Task NewBusinessIssue(BusinessIssue data)
        {
            throw new NotImplementedException();
        }

        public async Task PingTest(int value)
        {
            _logger.LogInformation("PingTest: {Value}", value);
        }

        public Task RespondError(string message)
        {
            throw new NotImplementedException();
        }

        public Task RespondIssueMessage(List<Message> data)
        {
            throw new NotImplementedException();
        }

        public Task RespondOrganisation(Organisation organisation)
        {
            throw new NotImplementedException();
        }

        public Task RespondPendingBusinessIssues(List<BusinessIssue> data)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBusinessIssue(BusinessIssue update)
        {
            throw new NotImplementedException();
        }
    }
}
