using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class WorkItemRealtionshipType
    {
        public WorkItemRealtionshipType()
        {
            AssociatedWrorkItemChildren = new HashSet<AssociatedWrorkItemChildren>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AssociatedWrorkItemChildren> AssociatedWrorkItemChildren { get; set; }
    }
}
