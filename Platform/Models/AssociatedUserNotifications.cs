using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedUserNotifications
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int? NewNotification { get; set; }
        public int? IsRead { get; set; }

        public virtual Notifications Notification { get; set; }
        public virtual UserAccounts User { get; set; }
    }
}
