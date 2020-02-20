using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedRepositoryCommits
    {
        public int Id { get; set; }
        public int RepositoryId { get; set; }
        public int CommitId { get; set; }

        public virtual Coomits Commit { get; set; }
        public virtual Repository Repository { get; set; }
    }
}
