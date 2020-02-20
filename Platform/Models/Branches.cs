using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class Branches
    {
        public Branches()
        {
            AssociatedBranchCommits = new HashSet<AssociatedBranchCommits>();
            AssociatedRepositoryBranches = new HashSet<AssociatedRepositoryBranches>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public string BranchName { get; set; }

        public virtual Projects Project { get; set; }
        public virtual ICollection<AssociatedBranchCommits> AssociatedBranchCommits { get; set; }
        public virtual ICollection<AssociatedRepositoryBranches> AssociatedRepositoryBranches { get; set; }
    }
}
