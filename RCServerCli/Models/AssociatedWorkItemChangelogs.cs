using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class AssociatedWorkItemChangelogs
    {
        public int Id { get; set; }
        public int? LogId { get; set; }
        public int? WorkitemId { get; set; }
        public int? ProjectId { get; set; }

        public virtual Changelogs Log { get; set; }
        public virtual Projects Project { get; set; }
        public virtual WorkItem Workitem { get; set; }
    }
}
