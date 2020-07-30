using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class BuildingTypes
    {
        public BuildingTypes()
        {
            AssociatedBuildingFunctionality = new HashSet<AssociatedBuildingFunctionality>();
            OwableBuildings = new HashSet<OwableBuildings>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AssociatedBuildingFunctionality> AssociatedBuildingFunctionality { get; set; }
        public virtual ICollection<OwableBuildings> OwableBuildings { get; set; }
    }
}
