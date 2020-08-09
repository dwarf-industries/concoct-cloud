using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class ItemCategories
    {
        public ItemCategories()
        {
            AssociatedItemSubCategoriesMain = new HashSet<AssociatedItemSubCategories>();
            AssociatedItemSubCategoriesSubCategory = new HashSet<AssociatedItemSubCategories>();
            AssociatedTradedBussinessItems = new HashSet<AssociatedTradedBussinessItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AssociatedItemSubCategories> AssociatedItemSubCategoriesMain { get; set; }
        public virtual ICollection<AssociatedItemSubCategories> AssociatedItemSubCategoriesSubCategory { get; set; }
        public virtual ICollection<AssociatedTradedBussinessItems> AssociatedTradedBussinessItems { get; set; }
    }
}
