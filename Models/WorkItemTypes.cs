using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class WorkItemTypes
    {
        public WorkItemTypes()
        {
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
