using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedProjectMemberRights
    {
        public int Id { get; set; }
        public int? RightsId { get; set; }
        public int? UserAccountId { get; set; }
        public int? ProjectId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual UserRights Rights { get; set; }
        public virtual UserAccounts UserAccount { get; set; }
    }
}
