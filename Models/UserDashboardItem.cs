using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class UserDashboardItem
    {
        public UserDashboardItem()
        {
            AssociatedUserDashboardItemComponent = new HashSet<AssociatedUserDashboardItemComponent>();
        }

        public int Id { get; set; }
        public string ItemName { get; set; }
        public int? DataRow { get; set; }
        public int? DataCol { get; set; }
        public int? DataSizeY { get; set; }
        public int? DataSizeX { get; set; }
        public int? UserDashboard { get; set; }

        public virtual UserDashboards UserDashboardNavigation { get; set; }
        public virtual ICollection<AssociatedUserDashboardItemComponent> AssociatedUserDashboardItemComponent { get; set; }
    }
}
