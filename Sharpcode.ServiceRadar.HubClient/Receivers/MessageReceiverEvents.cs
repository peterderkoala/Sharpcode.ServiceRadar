using Sharpcode.ServiceRadar.Model.Entities;

namespace Sharpcode.ServiceRadar.HubClient.Receivers
{
    public class MessageReceiverEvents
    {
        public delegate void OrganisationEventHandler(object sender, Organization e);
        public event EventHandler<Organization> OrganisationEvent = null!;

        public delegate void BusinessIssueEventHandler(object sender, List<BusinessIssue> e);
        public event EventHandler<List<BusinessIssue>> BusinessIssueEvent = null!;

        public delegate void NewBusinessIssueEventHandler(object sender, BusinessIssue e);
        public event EventHandler<BusinessIssue> NewBusinessIssueEvent = null!;

        public delegate void IssueMessageMessageEventHandler(object sender, List<Message> e);
        public event EventHandler<List<Message>> IssueMessageMessageEvent = null!;

        protected virtual async Task OnOrganisationEvent(Organization organisation)
        {
            OrganisationEvent.Invoke(this, organisation);
        }

        protected virtual Task OnBusinessIssueEvent(List<BusinessIssue> issues)
        {
            BusinessIssueEvent.Invoke(this, issues);
            return Task.CompletedTask;
        }

        protected virtual async Task OnNewBusinessIssueEvent(BusinessIssue issue)
        {
            NewBusinessIssueEvent.Invoke(this, issue);
        }

        protected virtual async Task OneNewIssueMessageEvent(List<Message> messages)
        {
            IssueMessageMessageEvent.Invoke(this, messages);
        }
    }
}
