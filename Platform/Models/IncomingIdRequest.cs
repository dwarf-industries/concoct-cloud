namespace Rokono_Control.Models
{
    public class IncomingIdRequest
    {
        public int Id { get; set; }
        public int WorkItemType { get; set; }
        public string Phase { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string __RequestVerificationToken { get; set; }
    }
}