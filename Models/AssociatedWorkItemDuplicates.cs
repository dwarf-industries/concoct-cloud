using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedWorkItemDuplicates
    {
        public int Id { get; set; }
        public int? WorkItemId { get; set; }
        public int? WorkItemChildId { get; set; }

        public virtual WorkItem WorkItem { get; set; }
        public virtual WorkItem WorkItemChild { get; set; }
    }
}
