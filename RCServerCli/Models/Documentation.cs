using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class Documentation
    {
        public Documentation()
        {
            DocumentationCategory = new HashSet<DocumentationCategory>();
        }

        public int Id { get; set; }
        public int? ProjectId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual ICollection<DocumentationCategory> DocumentationCategory { get; set; }
    }
}
