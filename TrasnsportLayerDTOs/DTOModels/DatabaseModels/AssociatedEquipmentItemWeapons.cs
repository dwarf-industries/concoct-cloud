using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedEquipmentItemWeapons
    {
        public int Id { get; set; }
        public int? WeaponId { get; set; }
        public int? EquipmentItemId { get; set; }

        public virtual EquipmentItem EquipmentItem { get; set; }
        public virtual Weapons Weapon { get; set; }
    }
}
