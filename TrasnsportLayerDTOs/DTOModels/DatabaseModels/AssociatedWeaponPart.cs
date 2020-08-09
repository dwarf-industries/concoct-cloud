using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedWeaponPart
    {
        public int Id { get; set; }
        public int? WeaponId { get; set; }
        public int? PartId { get; set; }

        public virtual WeaponPart Part { get; set; }
        public virtual Weapons Weapon { get; set; }
    }
}
