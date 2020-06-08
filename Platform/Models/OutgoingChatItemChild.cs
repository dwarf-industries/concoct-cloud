namespace Platform.Models
{
    public class OutgoingChatItemChild
    {
        
        public int InternalId {get; set;}
        // public int NodeId { get; set; }
        public string NodeText { get; set; }
        public string IconCss { get; set; }
        public string Link { get; set; }
        public int ChannelType { get; set; }
        public int ParentId {get; set;}
        public bool IsPersonal { get; set; }
    }
}