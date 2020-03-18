using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class Notifications
    {
        public Notifications()
        {
            AssociatedProjectNotifications = new HashSet<AssociatedProjectNotifications>();
            AssociatedUserNotifications = new HashSet<AssociatedUserNotifications>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public int? WorkItemRelationid { get; set; }
        public DateTime? DateOfMessage { get; set; }
        public int? NotificationType { get; set; }

        public virtual NotificationTypes NotificationTypeNavigation { get; set; }
        public virtual ICollection<AssociatedProjectNotifications> AssociatedProjectNotifications { get; set; }
        public virtual ICollection<AssociatedUserNotifications> AssociatedUserNotifications { get; set; }
    }
}
