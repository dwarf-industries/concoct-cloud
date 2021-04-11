using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class AssociatedChatPersonalMessages
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? PublicMessageId { get; set; }
        public int? SenderId { get; set; }
        public int? ReciverId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual PublicMessages PublicMessage { get; set; }
        public virtual UserAccounts Reciver { get; set; }
        public virtual UserAccounts Sender { get; set; }
    }
}
