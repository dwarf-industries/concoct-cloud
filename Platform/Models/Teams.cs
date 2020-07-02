using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class Teams
    {
        public Teams()
        {
            TeamDashboards = new HashSet<TeamDashboards>();
        }

        public int Id { get; set; }
        public string TeamName { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? DateOfCreation { get; set; }

        public virtual ICollection<TeamDashboards> TeamDashboards { get; set; }
    }
}
