using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class AssociatedProjectChangelogs
    {
        public int Id { get; set; }
        public int? LogId { get; set; }
        public int? ProjectId { get; set; }
        public int? CurrentMonth { get; set; }
        public int? LogYear { get; set; }

        public virtual Changelogs Log { get; set; }
        public virtual Projects Project { get; set; }
    }
}
