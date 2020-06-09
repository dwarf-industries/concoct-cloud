using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class AssociatedChatChannelMessages
    {
        public int Id { get; set; }
        public int? PublicMessageId { get; set; }
        public int? ChatChannelId { get; set; }

        public virtual ChatChannels ChatChannel { get; set; }
        public virtual PublicMessages PublicMessage { get; set; }
    }
}
