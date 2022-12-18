using Sharpcode.ServiceRadar.Model.Entities;

namespace Sharpcode.ServiceRadar.Api.Hubs
{
    public interface IBusinessHubClient
    {
        Task NewBusinessIssue(BusinessIssue data, CancellationToken cancellationToken = default);
        Task UpdateBusinessIssue(BusinessIssue update, CancellationToken cancellationToken = default);
        Task PingTest(int value, CancellationToken cancellation = default);
    }
}
