using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class UserRights
    {
        public UserRights()
        {
            AssociatedProjectMemberRights = new HashSet<AssociatedProjectMemberRights>();
        }

        public int Id { get; set; }
        public short WorkItemRule { get; set; }
        public short ChatChannelsRule { get; set; }
        public short UpdateUserRights { get; set; }
        public short ManageIterations { get; set; }
        public short ManageUserdays { get; set; }
        public short ViewOtherPeoplesWork { get; set; }

        public virtual ICollection<AssociatedProjectMemberRights> AssociatedProjectMemberRights { get; set; }
    }
}
