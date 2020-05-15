using System.Collections.Generic;
using Rokono_Control.Models;

namespace RokonoControl.Models
{
    public class OutgoingValidWorkItem
    {
        public List<WorkItem> WorkItem { get; set; }
        public bool Valid { get; set; }
        public int WorkItemTypeId { get; set; }
        public int WorkItemId { get; set; }
        public string  RelationshipId { get; set; }
        public WorkItem Last { get; set; }
    }
}