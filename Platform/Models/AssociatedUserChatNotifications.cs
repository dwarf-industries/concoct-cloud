using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedUserChatNotifications
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
        public int? ChatChannelId { get; set; }

        public virtual ChatChannels ChatChannel { get; set; }
        public virtual Projects Project { get; set; }
        public virtual UserAccounts User { get; set; }
    }
}
