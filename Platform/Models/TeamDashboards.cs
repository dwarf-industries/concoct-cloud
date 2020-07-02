using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class TeamDashboards
    {
        public TeamDashboards()
        {
            UserDashboards = new HashSet<UserDashboards>();
        }

        public int Id { get; set; }
        public string DashboardName { get; set; }
        public int? TeamId { get; set; }
        public DateTime? DateOfCreation { get; set; }

        public virtual Teams Team { get; set; }
        public virtual ICollection<UserDashboards> UserDashboards { get; set; }
    }
}
