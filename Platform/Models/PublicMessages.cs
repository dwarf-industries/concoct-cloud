using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class PublicMessages
    {
        public PublicMessages()
        {
            AssociatedChatChannelMessages = new HashSet<AssociatedChatChannelMessages>();
            AssociatedChatPersonalMessages = new HashSet<AssociatedChatPersonalMessages>();
            AssociatedProjectFeedback = new HashSet<AssociatedProjectFeedback>();
            AssociatedProjectPublicDiscussions = new HashSet<AssociatedProjectPublicDiscussions>();
        }

        public int Id { get; set; }
        public string SenderName { get; set; }
        public string MessageContent { get; set; }
        public DateTime? DateOfMessage { get; set; }
        public int? IsNew { get; set; }
        public int? SenderId { get; set; }

        public virtual ICollection<AssociatedChatChannelMessages> AssociatedChatChannelMessages { get; set; }
        public virtual ICollection<AssociatedChatPersonalMessages> AssociatedChatPersonalMessages { get; set; }
        public virtual ICollection<AssociatedProjectFeedback> AssociatedProjectFeedback { get; set; }
        public virtual ICollection<AssociatedProjectPublicDiscussions> AssociatedProjectPublicDiscussions { get; set; }
    }
}
