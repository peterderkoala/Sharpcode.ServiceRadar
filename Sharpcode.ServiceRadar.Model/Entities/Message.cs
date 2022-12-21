using System.ComponentModel.DataAnnotations;

namespace Sharpcode.ServiceRadar.Model.Entities
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string Title { get; set; } = null!;
        public string MessageBody { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }

        public int BusinessIssueId { get; set; }
        public BusinessIssue BusinessIssue { get; set; } = null!;

        // TODO: Issuer mit aufnehmen in die nachricht???
    }
}
