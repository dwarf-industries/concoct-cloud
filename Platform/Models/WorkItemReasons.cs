using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class WorkItemReasons
    {
        public WorkItemReasons()
        {
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string ReasonName { get; set; }

        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
