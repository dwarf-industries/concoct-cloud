using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedSaleProducts
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int BuildingId { get; set; }

        public virtual OwableBuildings Building { get; set; }
        public virtual BusinessItems Item { get; set; }
    }
}
