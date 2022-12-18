namespace Sharpcode.ServiceRadar.Model.Entities
{
    public class BusinessIssue2Application
    {
        public int BusinessIssue2ApplicationId { get; set; }
        public int BusinessIssueId { get; set; }
        public BusinessIssue BusinessIssue { get; set; }
        public int BusinessApplicationId { get; set; }
        public BusinessApplication BusinessApplication { get; set; }
    }
}
