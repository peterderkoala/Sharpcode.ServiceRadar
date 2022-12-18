using System.ComponentModel.DataAnnotations;

namespace Sharpcode.ServiceRadar.Model.Entities
{
    public class BusinessApplication
    {
        [Key]
        public int ApplicationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset DeletedAt { get; set; }

        public IEnumerable<BusinessIssue2Application> BusinessIssues { get; set; } = new List<BusinessIssue2Application>();
    }
}
