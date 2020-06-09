using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class DocumentationCategoryField
    {
        public DocumentationCategoryField()
        {
            AssociatedDocumentationCategoryPage = new HashSet<AssociatedDocumentationCategoryPage>();
        }

        public int Id { get; set; }
        public string PageName { get; set; }
        public int? CategoryId { get; set; }

        public virtual DocumentationCategory Category { get; set; }
        public virtual ICollection<AssociatedDocumentationCategoryPage> AssociatedDocumentationCategoryPage { get; set; }
    }
}
