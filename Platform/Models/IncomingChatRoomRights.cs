namespace Platform.Models
{
    public class IncomingChatRoomRights
    {
        
        public int Id { get; set; }
        public string RightName { get; set; }
        public int ProjectId { get; set; }
        public string Background { get; set; }
        public string Foreground { get; set; }
        public int ChatRoomId { get; set; }
    }
}