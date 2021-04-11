namespace Platform.Models
{
    public class IncomingChatMessage
    {
        public int ActiveRoom { get; set; }
        public int ProjectId { get; set; }
        public string Message { get; set; }
        public string SenderName { get; set; }
        public int ReciverId { get; set; }
        public int IsPersonal { get; set; }
    }
}