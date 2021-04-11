using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class PremadeWidgets
    {
        public PremadeWidgets()
        {
            AssociatedUserDashboardPremade = new HashSet<AssociatedUserDashboardPremade>();
        }

        public int Id { get; set; }
        public string ControlName { get; set; }
        public string ViewComponentName { get; set; }
        public string ControlDescription { get; set; }
        public string Settings { get; set; }
        public int? CustomSettings { get; set; }

        public virtual ICollection<AssociatedUserDashboardPremade> AssociatedUserDashboardPremade { get; set; }
    }
}
