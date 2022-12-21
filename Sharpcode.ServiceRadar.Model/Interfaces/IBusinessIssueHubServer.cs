using Sharpcode.ServiceRadar.Model.Entities;

namespace Sharpcode.ServiceRadar.Model.Interfaces
{
    public interface IBusinessIssueHubServer
    {
        Task NewBusinessIssue(BusinessIssue data);
        Task UpdateBusinessIssue(BusinessIssue update);
        Task PingTest(int value);
        Task GetPendingBusinessIssues(string clientid, Organisation myOrganisation);
        Task RespondPendingBusinessIssues(List<BusinessIssue> data);
        Task RespondOrganisation(Organisation organisation);
        Task RespondIssueMessage(List<Message> data);
        Task RespondError(string message);

    }
}
