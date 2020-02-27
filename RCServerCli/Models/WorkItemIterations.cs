using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class WorkItemIterations
    {
        public WorkItemIterations()
        {
            AssociatedProjectIterations = new HashSet<AssociatedProjectIterations>();
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string IterationName { get; set; }

        public virtual ICollection<AssociatedProjectIterations> AssociatedProjectIterations { get; set; }
        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
