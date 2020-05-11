using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedProjectFeedback
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? MessageId { get; set; }
        public int? Rating { get; set; }

        public virtual PublicMessages Message { get; set; }
        public virtual Projects Project { get; set; }
    }
}
