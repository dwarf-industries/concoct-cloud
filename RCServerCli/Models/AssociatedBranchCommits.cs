using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class AssociatedBranchCommits
    {
        public int Id { get; set; }
        public int CommitId { get; set; }
        public int BranchId { get; set; }

        public virtual Branches Branch { get; set; }
        public virtual Commits Commit { get; set; }
    }
}
