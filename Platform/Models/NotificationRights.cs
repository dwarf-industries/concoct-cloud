using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class NotificationRights
    {
        public NotificationRights()
        {
            AssociatedAccountProjectNotificationRights = new HashSet<AssociatedAccountProjectNotificationRights>();
        }

        public int Id { get; set; }
        public int? PersonalMessageNenabled { get; set; }
        public int? ChatChannelNenabled { get; set; }
        public int? UpdateWorkItemNenabled { get; set; }
        public int? CreateWorkItemNenabled { get; set; }
        public int? PublicDiscussionMnenabled { get; set; }
        public int? FeedbackNenabled { get; set; }
        public int? BugReportNenabled { get; set; }
        public int? ChanegelogNenabled { get; set; }

        public virtual ICollection<AssociatedAccountProjectNotificationRights> AssociatedAccountProjectNotificationRights { get; set; }
    }
}
