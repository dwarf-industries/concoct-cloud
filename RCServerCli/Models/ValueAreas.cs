using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class ValueAreas
    {
        public ValueAreas()
        {
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string ValueAreaName { get; set; }

        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
