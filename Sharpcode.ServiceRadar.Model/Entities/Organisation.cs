using System.ComponentModel.DataAnnotations;

namespace Sharpcode.ServiceRadar.Model.Entities
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }
        public string Title { get; set; } = null!;
        public string? Desription { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public IEnumerable<BusinessIssue> BusinessIssues { get; set; } = new List<BusinessIssue>();
        public IEnumerable<RemoteClient> RemoteClients { get; set; } = new List<RemoteClient>();
    }
}
