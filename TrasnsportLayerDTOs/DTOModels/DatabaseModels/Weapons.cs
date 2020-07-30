using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Weapons
    {
        public Weapons()
        {
            AssociatedEquipmentItemWeapons = new HashSet<AssociatedEquipmentItemWeapons>();
            AssociatedWeaponPart = new HashSet<AssociatedWeaponPart>();
            Characters = new HashSet<Characters>();
        }

        public int Id { get; set; }
        public string ResourceLocation { get; set; }
        public int Hand { get; set; }
        public string HasSecondary { get; set; }
        public int Aimable { get; set; }
        public int WeaponType { get; set; }
        public double? PosX { get; set; }
        public double? PosZ { get; set; }
        public double? RotX { get; set; }
        public double? RotY { get; set; }
        public double? RotZ { get; set; }
        public double? SposX { get; set; }
        public double? SposY { get; set; }
        public double? SposZ { get; set; }
        public double? SrotX { get; set; }
        public double? SrotY { get; set; }
        public double? SrotZ { get; set; }
        public double? PosY { get; set; }
        public double? ScaleX { get; set; }
        public double? ScaleY { get; set; }
        public double? ScaleZ { get; set; }

        public virtual ICollection<AssociatedEquipmentItemWeapons> AssociatedEquipmentItemWeapons { get; set; }
        public virtual ICollection<AssociatedWeaponPart> AssociatedWeaponPart { get; set; }
        public virtual ICollection<Characters> Characters { get; set; }
    }
}
