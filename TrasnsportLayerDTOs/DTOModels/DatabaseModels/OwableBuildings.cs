using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class OwableBuildings
    {
        public OwableBuildings()
        {
            AssociatedAreaBuildings = new HashSet<AssociatedAreaBuildings>();
            AssociatedBuildingFunctionality = new HashSet<AssociatedBuildingFunctionality>();
            AssociatedProductsForSale = new HashSet<AssociatedProductsForSale>();
            AssociatedSaleProducts = new HashSet<AssociatedSaleProducts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public double? Price { get; set; }
        public double? PossitionX { get; set; }
        public double? PossitionY { get; set; }
        public double? PossitionZ { get; set; }
        public double? RotationX { get; set; }
        public double? RotationY { get; set; }
        public double? RotationZ { get; set; }
        public double? RotationW { get; set; }

        public virtual BuildingTypes TypeNavigation { get; set; }
        public virtual ICollection<AssociatedAreaBuildings> AssociatedAreaBuildings { get; set; }
        public virtual ICollection<AssociatedBuildingFunctionality> AssociatedBuildingFunctionality { get; set; }
        public virtual ICollection<AssociatedProductsForSale> AssociatedProductsForSale { get; set; }
        public virtual ICollection<AssociatedSaleProducts> AssociatedSaleProducts { get; set; }
    }
}
