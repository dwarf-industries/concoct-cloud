using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class AssociatedUserDashboardItemComponent
    {
        public int Id { get; set; }
        public int? Item { get; set; }
        public int? ItemComponent { get; set; }

        public virtual UserDashboardItemComponent ItemComponentNavigation { get; set; }
        public virtual UserDashboardItem ItemNavigation { get; set; }
    }
}
