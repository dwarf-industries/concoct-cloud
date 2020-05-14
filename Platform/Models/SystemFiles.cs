using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class SystemFiles
    {
        public SystemFiles()
        {
            AssociatedWorkItemFiles = new HashSet<AssociatedWorkItemFiles>();
        }

        public int Id { get; set; }
        public string SenderName { get; set; }
        public string FileLocation { get; set; }
        public DateTime? DateOfMessage { get; set; }
        public int? FileType { get; set; }

        public virtual FileTypes FileTypeNavigation { get; set; }
        public virtual ICollection<AssociatedWorkItemFiles> AssociatedWorkItemFiles { get; set; }
    }
}
