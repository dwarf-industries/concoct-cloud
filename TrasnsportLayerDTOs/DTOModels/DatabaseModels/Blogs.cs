using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Blogs
    {
        public Blogs()
        {
            AssociatedBlogPosts = new HashSet<AssociatedBlogPosts>();
        }

        public int Id { get; set; }
        public string Heading { get; set; }
        public string BannerHeading { get; set; }

        public virtual ICollection<AssociatedBlogPosts> AssociatedBlogPosts { get; set; }
    }
}
