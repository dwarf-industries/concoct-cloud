using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedUserDashboardPremade
    {
        public int Id { get; set; }
        public int? UserDashboard { get; set; }
        public int? PremadeWidgetId { get; set; }
        public int? DataRow { get; set; }
        public int? DataCol { get; set; }
        public int? DataSizeX { get; set; }
        public int? DataSizeY { get; set; }

        public virtual PremadeWidgets PremadeWidget { get; set; }
        public virtual UserDashboards UserDashboardNavigation { get; set; }
    }
}
