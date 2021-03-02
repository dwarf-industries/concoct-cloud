using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class ChatRoomRights
    {
        public ChatRoomRights()
        {
            AssociatedChatRoomRights = new HashSet<AssociatedChatRoomRights>();
            AssociatedUserChatRights = new HashSet<AssociatedUserChatRights>();
        }

        public int Id { get; set; }
        public string RightName { get; set; }
        public int? PojectId { get; set; }
        public string Background { get; set; }
        public string Foreground { get; set; }

        public virtual Projects Poject { get; set; }
        public virtual ICollection<AssociatedChatRoomRights> AssociatedChatRoomRights { get; set; }
        public virtual ICollection<AssociatedUserChatRights> AssociatedUserChatRights { get; set; }
    }
}
