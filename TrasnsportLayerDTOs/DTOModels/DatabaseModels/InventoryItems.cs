using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class InventoryItems
    {
        public InventoryItems()
        {
            AssociatedInventoryItems = new HashSet<AssociatedInventoryItems>();
        }

        public int Id { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public string InGameModel { get; set; }
        public int? RefferenceId { get; set; }
        public int? EquipmentItem { get; set; }

        public virtual EquipmentItem EquipmentItemNavigation { get; set; }
        public virtual ICollection<AssociatedInventoryItems> AssociatedInventoryItems { get; set; }
    }
}
