namespace Platform.Models
{
    public class IncomingPublicMessage
    {
        public int ProjectId { get; set; }
        public string SenderName { get; set; }
        public string MessageContent { get; set; }
    }
}