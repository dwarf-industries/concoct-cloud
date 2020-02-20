using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class Files
    {
        public Files()
        {
            AssociatedCommitFiles = new HashSet<AssociatedCommitFiles>();
        }

        public int Id { get; set; }
        public string FilePath { get; set; }
        public string CurrentName { get; set; }
        public string FileData { get; set; }
        public DateTime? DateOfFile { get; set; }

        public virtual ICollection<AssociatedCommitFiles> AssociatedCommitFiles { get; set; }
    }
}
