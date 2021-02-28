using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class ChatChannelTypes
    {
        public ChatChannelTypes()
        {
            ChatChannels = new HashSet<ChatChannels>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<ChatChannels> ChatChannels { get; set; }
    }
}
