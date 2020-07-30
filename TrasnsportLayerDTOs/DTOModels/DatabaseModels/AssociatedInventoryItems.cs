using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedInventoryItems
    {
        public int Id { get; set; }
        public int? CharacterId { get; set; }
        public int? ItemId { get; set; }

        public virtual Characters Character { get; set; }
        public virtual InventoryItems Item { get; set; }
    }
}
