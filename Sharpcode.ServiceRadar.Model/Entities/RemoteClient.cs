using System.ComponentModel.DataAnnotations;

namespace Sharpcode.ServiceRadar.Model.Entities
{
    public class RemoteClient
    {
        [Key]
        public int RemoteClientId { get; set; }
        public string RemoteClientKey { get; set; }
        public string Mail { get; set; }

        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; }
    }
}
