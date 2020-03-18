using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class AssociatedProjectNotifications
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int ProjectId { get; set; }
        public int? NewNotification { get; set; }
        public int? UserAccountId { get; set; }

        public virtual Notifications Notification { get; set; }
        public virtual Projects Project { get; set; }
        public virtual UserAccounts UserAccount { get; set; }
    }
}
