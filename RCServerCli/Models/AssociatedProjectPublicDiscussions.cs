using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class AssociatedProjectPublicDiscussions
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? PublicMessageId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual PublicMessages PublicMessage { get; set; }
    }
}
