using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class AssociatedProjectMembers
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public int ProjectId { get; set; }
        public int RepositoryId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual Repository Repository { get; set; }
        public virtual UserAccounts UserAccount { get; set; }
    }
}
