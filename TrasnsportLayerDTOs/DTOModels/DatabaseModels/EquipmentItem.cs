using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class EquipmentItem
    {
        public EquipmentItem()
        {
            AssociatedEquipmentItemWeapons = new HashSet<AssociatedEquipmentItemWeapons>();
            InventoryItems = new HashSet<InventoryItems>();
        }

        public int Id { get; set; }
        public string SlotName { get; set; }
        public string ItemModelLocation { get; set; }
        public int? WeaponType { get; set; }
        public int? IsWeapon { get; set; }
        public int? IsConsumablex { get; set; }

        public virtual ICollection<AssociatedEquipmentItemWeapons> AssociatedEquipmentItemWeapons { get; set; }
        public virtual ICollection<InventoryItems> InventoryItems { get; set; }
    }
}
