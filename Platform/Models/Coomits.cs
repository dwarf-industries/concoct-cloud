using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class Coomits
    {
        public Coomits()
        {
            AssociatedRepositoryCommits = new HashSet<AssociatedRepositoryCommits>();
        }

        public int Id { get; set; }
        public string CommitId { get; set; }
        public DateTime CommitDate { get; set; }
        public string Author { get; set; }
        public int RepositoryId { get; set; }

        public virtual ICollection<AssociatedRepositoryCommits> AssociatedRepositoryCommits { get; set; }
    }
}
