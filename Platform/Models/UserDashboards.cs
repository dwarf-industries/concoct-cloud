using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class UserDashboards
    {
        public UserDashboards()
        {
            UserDashboardItem = new HashSet<UserDashboardItem>();
        }

        public int Id { get; set; }
        public string DashboardName { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateOfDashboard { get; set; }
        public int? ProjectId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual ICollection<UserDashboardItem> UserDashboardItem { get; set; }
    }
}
