using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class WeaponPart
    {
        public WeaponPart()
        {
            AssociatedWeaponPart = new HashSet<AssociatedWeaponPart>();
            BusinessItems = new HashSet<BusinessItems>();
        }

        public int Id { get; set; }
        public string ModelLocation { get; set; }
        public int? PartType { get; set; }
        public int? WeaponType { get; set; }
        public string HeaderName { get; set; }
        public double? CpossitionX { get; set; }
        public double? CpositionY { get; set; }
        public double? CpositionZ { get; set; }
        public double? PushNextX { get; set; }
        public double? PushNextY { get; set; }
        public double? PushNextZ { get; set; }

        public virtual ICollection<AssociatedWeaponPart> AssociatedWeaponPart { get; set; }
        public virtual ICollection<BusinessItems> BusinessItems { get; set; }
    }
}
