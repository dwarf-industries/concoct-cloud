using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedItemSubCategories
    {
        public int Id { get; set; }
        public int? MainId { get; set; }
        public int? SubCategoryId { get; set; }

        public virtual ItemCategories Main { get; set; }
        public virtual ItemCategories SubCategory { get; set; }
    }
}
