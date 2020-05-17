using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class ChatChannels
    {
        public ChatChannels()
        {
            AssociatedChatChannelMessages = new HashSet<AssociatedChatChannelMessages>();
        }

        public int Id { get; set; }
        public string ChannelName { get; set; }
        public int? ChannelType { get; set; }
        public int? ChatRoom { get; set; }

        public virtual ChatChannelTypes ChannelTypeNavigation { get; set; }
        public virtual ChatRooms ChatRoomNavigation { get; set; }
        public virtual ICollection<AssociatedChatChannelMessages> AssociatedChatChannelMessages { get; set; }
    }
}
