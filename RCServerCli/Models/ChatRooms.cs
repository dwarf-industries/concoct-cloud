using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class ChatRooms
    {
        public ChatRooms()
        {
            AssociatedChatRoomRights = new HashSet<AssociatedChatRoomRights>();
            ChatChannels = new HashSet<ChatChannels>();
        }

        public int Id { get; set; }
        public string RoomName { get; set; }
        public int? ProjectId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual ICollection<AssociatedChatRoomRights> AssociatedChatRoomRights { get; set; }
        public virtual ICollection<ChatChannels> ChatChannels { get; set; }
    }
}
