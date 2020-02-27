using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class WorkItemSeverities
    {
        public WorkItemSeverities()
        {
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string SeverityName { get; set; }

        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
