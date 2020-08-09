using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedAreaBuildings
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public int AreaId { get; set; }
        public int? OwnerId { get; set; }
        public int? ForSale { get; set; }

        public virtual Zones Area { get; set; }
        public virtual OwableBuildings Building { get; set; }
        public virtual Characters Owner { get; set; }
    }
}
