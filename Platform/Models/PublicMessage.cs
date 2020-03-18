using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class PublicMessage
    {
        public PublicMessage()
        {
            AssociatedProjectPublicMessages = new HashSet<AssociatedProjectPublicMessages>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public int? WorkItemRelationid { get; set; }
        public int? Rating { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfMessage { get; set; }

        public virtual ICollection<AssociatedProjectPublicMessages> AssociatedProjectPublicMessages { get; set; }
    }
}
