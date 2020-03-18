using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedProjectPublicMessages
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public int ProjectId { get; set; }

        public virtual PublicMessage Message { get; set; }
        public virtual Projects Project { get; set; }
    }
}
