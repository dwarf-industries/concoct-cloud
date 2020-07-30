using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class BusinessItems
    {
        public BusinessItems()
        {
            AssociatedProductsForSale = new HashSet<AssociatedProductsForSale>();
            AssociatedSaleProducts = new HashSet<AssociatedSaleProducts>();
            AssociatedTradedBussinessItems = new HashSet<AssociatedTradedBussinessItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double SellPrice { get; set; }
        public double BuyPrice { get; set; }
        public int? Quantity { get; set; }
        public int? SellerId { get; set; }
        public int? Type { get; set; }
        public int? WeaponPartId { get; set; }

        public virtual WeaponPart WeaponPart { get; set; }
        public virtual ICollection<AssociatedProductsForSale> AssociatedProductsForSale { get; set; }
        public virtual ICollection<AssociatedSaleProducts> AssociatedSaleProducts { get; set; }
        public virtual ICollection<AssociatedTradedBussinessItems> AssociatedTradedBussinessItems { get; set; }
    }
}
