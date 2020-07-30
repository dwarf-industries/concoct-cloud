using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Pages
    {
        public Pages()
        {
            AssociatedBookPages = new HashSet<AssociatedBookPages>();
        }

        public int Id { get; set; }
        public string Page1 { get; set; }
        public string Page2 { get; set; }
        public int PagerId { get; set; }

        public virtual ICollection<AssociatedBookPages> AssociatedBookPages { get; set; }
    }
}
