using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class DocumentationArea
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public string AreaContent { get; set; }
        public int? CategoryId { get; set; }

        public virtual DocumentationCategory Category { get; set; }
    }
}
