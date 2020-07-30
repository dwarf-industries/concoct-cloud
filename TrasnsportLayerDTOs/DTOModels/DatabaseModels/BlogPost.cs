using System;
using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class BlogPost
    {
        public BlogPost()
        {
            AssociatedBlogPosts = new HashSet<AssociatedBlogPosts>();
        }

        public int Id { get; set; }
        public string Heading { get; set; }
        public string FirstParagraph { get; set; }
        public string SecondParagrahp { get; set; }
        public string ThirdParagraph { get; set; }
        public string FourthParagraph { get; set; }
        public string FifthParagraph { get; set; }
        public string SixthParagraph { get; set; }
        public string InnerImage { get; set; }
        public string BannerLocation { get; set; }
        public DateTime? DateOfPost { get; set; }
        public string InnerHeaing { get; set; }

        public virtual ICollection<AssociatedBlogPosts> AssociatedBlogPosts { get; set; }
    }
}
