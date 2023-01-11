using Sharpcode.ServiceRadar.Model.Entities;

namespace Sharpcode.ServiceRadar.Model.Interfaces
{
    public interface IBusinessIssueHubClient
    {
        Task GetOrganisation(string clientid, string mail);
        Task GetPendingIssues(string clientid, Organization myOrganisation);
        Task ReloadIssueMessages(string clientid, BusinessIssue businessIssue);
        Task SendPingTest(int value);
    }
}
