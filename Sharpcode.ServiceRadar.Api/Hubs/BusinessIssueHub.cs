using Microsoft.AspNetCore.SignalR;
using Sharpcode.ServiceRadar.Model.Entities;

namespace Sharpcode.ServiceRadar.Api.Hubs
{
    public class BusinessIssueHub : Hub
    {
        public async Task NewBusinessIssue(
            BusinessIssue businessIssue,
            CancellationToken cancellationToken = default)
        {
            await Clients.All
                .SendAsync("NewBusinessIssue", businessIssue, cancellationToken);
        }

        public async Task UpdateBusinessIssue(
            BusinessIssue businessIssue,
            CancellationToken cancellationToken = default)
        {
            await Clients.All
                .SendAsync("UpdateBusinessIssue", businessIssue, cancellationToken);
        }
    }
}
