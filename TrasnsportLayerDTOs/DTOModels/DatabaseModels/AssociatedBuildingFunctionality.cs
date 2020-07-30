using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedBuildingFunctionality
    {
        public int Id { get; set; }
        public int? BuildingFunctionality { get; set; }
        public int? BuildingId { get; set; }
        public int? BusinessType { get; set; }

        public virtual OwableBuildings Building { get; set; }
        public virtual BuildingTypes BuildingFunctionalityNavigation { get; set; }
        public virtual BusienssTypes BusinessTypeNavigation { get; set; }
    }
}
