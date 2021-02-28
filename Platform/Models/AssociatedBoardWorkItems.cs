using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class AssociatedBoardWorkItems
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public int WorkItemId { get; set; }
        public int? ProjectId { get; set; }

        public virtual Boards Board { get; set; }
        public virtual Projects Project { get; set; }
        public virtual WorkItem WorkItem { get; set; }
    }
}
