using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedChatRoomRights
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public int RightId { get; set; }

        public virtual ChatRooms ChatRoom { get; set; }
        public virtual ChatRoomRights Right { get; set; }
    }
}
