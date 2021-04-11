namespace Platform.Models
{
    public class IncomingWorkItemMessage
    {
        public int ProjectId { get; set; }
        public int WorkItemId { get; set; }
        public string Message { get; set; }
    }
}