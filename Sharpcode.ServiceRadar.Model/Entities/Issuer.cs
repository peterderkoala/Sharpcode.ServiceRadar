using System.ComponentModel.DataAnnotations;

namespace Sharpcode.ServiceRadar.Model.Entities
{
    public class Issuer
    {
        [Key]
        public int IssuerId { get; set; }
        public string IssuerName { get; set; } = null!;
        public string IssuerMail { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        ICollection<BusinessIssue> Issues { get; set; } = new List<BusinessIssue>();
    }
}
