using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedBlogPosts
    {
        public int Id { get; set; }
        public int? BlogId { get; set; }
        public int? PostId { get; set; }
        public int? CategoryId { get; set; }
        public int? IsFeatured { get; set; }
        public int? IsPrioritized { get; set; }

        public virtual Blogs Blog { get; set; }
        public virtual BlogCategories Category { get; set; }
        public virtual BlogPost Post { get; set; }
    }
}
