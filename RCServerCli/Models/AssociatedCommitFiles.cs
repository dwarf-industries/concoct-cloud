using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class AssociatedCommitFiles
    {
        public int Id { get; set; }
        public int? FileId { get; set; }
        public int? CommitId { get; set; }
        public DateTime? DateOfCommit { get; set; }

        public virtual Commits Commit { get; set; }
        public virtual Files File { get; set; }
    }
}
