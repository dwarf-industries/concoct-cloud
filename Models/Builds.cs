using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class Builds
    {
        public Builds()
        {
            AssociatedProjectBuilds = new HashSet<AssociatedProjectBuilds>();
        }

        public int Id { get; set; }
        public string ReasonName { get; set; }
        public int? FrameworkVersion { get; set; }
        public DateTime? DateOfBuild { get; set; }
        public int? CompleationStatus { get; set; }
        public int? AccountId { get; set; }
        public int? PlatformId { get; set; }

        public virtual ICollection<AssociatedProjectBuilds> AssociatedProjectBuilds { get; set; }
    }
}
