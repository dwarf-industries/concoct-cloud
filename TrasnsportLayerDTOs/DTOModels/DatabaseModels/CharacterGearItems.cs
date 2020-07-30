using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class CharacterGearItems
    {
        public CharacterGearItems()
        {
            AssociatedEquippedCharacterItems = new HashSet<AssociatedEquippedCharacterItems>();
        }

        public int Id { get; set; }
        public int ItemName { get; set; }
        public int ModelId { get; set; }
        public int? EffectId { get; set; }
        public double Damage { get; set; }
        public int Type { get; set; }
        public int? Slot { get; set; }

        public virtual GearEffects Effect { get; set; }
        public virtual Slots SlotNavigation { get; set; }
        public virtual GearTypes TypeNavigation { get; set; }
        public virtual ICollection<AssociatedEquippedCharacterItems> AssociatedEquippedCharacterItems { get; set; }
    }
}
