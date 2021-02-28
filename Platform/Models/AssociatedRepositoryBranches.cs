using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class AssociatedRepositoryBranches
    {
        public int Id { get; set; }
        public int RepositoryId { get; set; }
        public int BranchId { get; set; }

        public virtual Branches Branch { get; set; }
        public virtual Repository Repository { get; set; }
    }
}
