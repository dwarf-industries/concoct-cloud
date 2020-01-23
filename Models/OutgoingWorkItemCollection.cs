using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public class OutgoingWorkItemCollection
    {
        public List<WorkItem> NoParents { get; set; }
        public List<WorkItem> ContainingParent { get; set; }
    }
}