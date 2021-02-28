using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
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
