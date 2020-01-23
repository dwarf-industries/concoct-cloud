using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public class OutgoingWorkItem
    {

        public int Id { get; set; }
        public string WorkItemIcon { get; set; }
        public string TypeName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public List<OutgoingWorkItem> subtasks { get; set; }
    }
}