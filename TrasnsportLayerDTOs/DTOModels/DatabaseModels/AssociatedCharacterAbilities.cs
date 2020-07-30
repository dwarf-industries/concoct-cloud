using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedCharacterAbilities
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public int AbilityId { get; set; }
        public int RequiredWeaponType { get; set; }
        public int? HasPos { get; set; }
        public int? SlotPosition { get; set; }
        public string BoundButton { get; set; }

        public virtual Abilities Ability { get; set; }
        public virtual Characters Character { get; set; }
    }
}
