using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class WorkItemTypes
    {
        public WorkItemTypes()
        {
            AssociatedWrorkItemChildren = new HashSet<AssociatedWrorkItemChildren>();
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<AssociatedWrorkItemChildren> AssociatedWrorkItemChildren { get; set; }
        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
