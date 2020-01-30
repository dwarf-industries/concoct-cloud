using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedWrorkItemChildren
    {
        public int Id { get; set; }
        public int? WorkItemId { get; set; }
        public int? WorkItemChildId { get; set; }
        public int? RelationType { get; set; }

        public virtual WorkItemRealtionshipType RelationTypeNavigation { get; set; }
        public virtual WorkItem WorkItem { get; set; }
        public virtual WorkItem WorkItemChild { get; set; }
    }
}
