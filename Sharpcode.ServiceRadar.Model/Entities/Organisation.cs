using System.ComponentModel.DataAnnotations;

namespace Sharpcode.ServiceRadar.Model.Entities
{
    public class Organisation
    {
        [Key]
        public int OrganisationId { get; set; }
        public string Title { get; set; } = null!;
        public string? Desription { get; set; }
    }
}
