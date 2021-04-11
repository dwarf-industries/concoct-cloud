using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class NotificationTypes
    {
        public NotificationTypes()
        {
            Notifications = new HashSet<Notifications>();
        }

        public int Id { get; set; }
        public string NotificationType { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<Notifications> Notifications { get; set; }
    }
}
