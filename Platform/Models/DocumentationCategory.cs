using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class DocumentationCategory
    {
        public DocumentationCategory()
        {
            DocumentationArea = new HashSet<DocumentationArea>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? ProjectId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual ICollection<DocumentationArea> DocumentationArea { get; set; }
    }
}
