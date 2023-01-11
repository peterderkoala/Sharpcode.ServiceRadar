using System.ComponentModel.DataAnnotations;

namespace Sharpcode.ServiceRadar.Model.Entities
{
    public class BusinessIssue
    {
        [Key]
        public int BusinessIssueId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;

        public IssueTypes IssueType { get; set; }
        public IssuePriorities BusinessIssuePriority { get; set; }
        public DateTimeOffset IssuedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset? ClosedAt { get; set; }
        public TimeSpan ImpactDuration { get; set; }

        public int OrganisationId { get; set; }
        public Organization Organisation { get; set; }
        public IEnumerable<Message> Messages { get; set; } = new List<Message>();
        public IEnumerable<BusinessIssue2Application> BusinessApplications { get; set; } = new List<BusinessIssue2Application>();
        public int IssuerId { get; set; }
        public Issuer Issuer { get; set; } = null!;

        public enum IssueTypes
        {
            Maintenance,
            Failure,
            Update,
            Attack
        }

        public enum IssuePriorities
        {
            Low,
            Medium,
            High,
            Critical
        }
    }
}
