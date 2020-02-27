using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class Changelogs
    {
        public Changelogs()
        {
            AssociatedProjectChangelogs = new HashSet<AssociatedProjectChangelogs>();
            AssociatedWorkItemChangelogs = new HashSet<AssociatedWorkItemChangelogs>();
        }

        public int Id { get; set; }
        public string LogDetails { get; set; }
        public int? DayOfLog { get; set; }

        public virtual ICollection<AssociatedProjectChangelogs> AssociatedProjectChangelogs { get; set; }
        public virtual ICollection<AssociatedWorkItemChangelogs> AssociatedWorkItemChangelogs { get; set; }
    }
}
