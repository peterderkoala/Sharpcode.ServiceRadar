using Microsoft.AspNetCore.SignalR;
using Sharpcode.ServiceRadar.Model.Entities;

namespace Sharpcode.ServiceRadar.Api.Hubs
{
    public class BusinessIssueHub : Hub<IBusinessHubClient>
    {
        public async Task NewBusinessIssue(
            BusinessIssue businessIssue,
            CancellationToken cancellationToken = default)
        {
            await Clients.All
                .NewBusinessIssue(data: businessIssue, cancellationToken);
        }

        public async Task UpdateBusinessIssue(
            BusinessIssue businessIssue,
            CancellationToken cancellationToken = default)
        {
            await Clients.All
                .UpdateBusinessIssue(update: businessIssue, cancellationToken);
        }

        public async Task SendPingTest(int value, CancellationToken cancellation)
        {
            await Clients.All
                .PingTest(value, cancellation);
        }
    }
}
