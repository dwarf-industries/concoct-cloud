using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class BlogCategories
    {
        public BlogCategories()
        {
            AssociatedBlogPosts = new HashSet<AssociatedBlogPosts>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<AssociatedBlogPosts> AssociatedBlogPosts { get; set; }
    }
}
