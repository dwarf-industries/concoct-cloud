using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class WorkItemAreas
    {
        public WorkItemAreas()
        {
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string AreaName { get; set; }

        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
