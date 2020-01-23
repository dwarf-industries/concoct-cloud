using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class Risks
    {
        public Risks()
        {
            WorkItem = new HashSet<WorkItem>();
        }

        public int Id { get; set; }
        public string RiskName { get; set; }

        public virtual ICollection<WorkItem> WorkItem { get; set; }
    }
}
