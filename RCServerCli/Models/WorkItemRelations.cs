using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class WorkItemRelations
    {
        public WorkItemRelations()
        {
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string RelationName { get; set; }

        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
