using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class WorkItemIterations
    {
        public WorkItemIterations()
        {
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string IterationName { get; set; }

        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
