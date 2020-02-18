
namespace Rokono_Control.Models
{
    using System.Collections.Generic;
    public class IncomingWorkItemRelation
    {
        public int WorkItemId { get; set; }
        public int CurrWorkItemId {get;set;}
        public int ProjectId { get; set; }
        public string RelationType { get; set; }
        public List<LinkedItems> LinkedItems { get; set; }
    }
}