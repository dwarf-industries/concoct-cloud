namespace Platform.Models
{
    public class IncomingNewBugReport
    {
        public int ProjectId { get; set; }
        public string SenderName { get; set; }
        public string BugDescription { get; set; }
        public string ImagePath { get; set; }
        public int WorkItemId { get; set; }
    }
}