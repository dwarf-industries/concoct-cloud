using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedProductsForSale
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int? BusinessId { get; set; }

        public virtual OwableBuildings Business { get; set; }
        public virtual BusinessItems Item { get; set; }
    }
}
