using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedEquippedCharacterItems
    {
        public int Id { get; set; }
        public int? CharacterId { get; set; }
        public int? GearItemId { get; set; }
        public int? Slot { get; set; }

        public virtual Characters Character { get; set; }
        public virtual CharacterGearItems GearItem { get; set; }
    }
}
