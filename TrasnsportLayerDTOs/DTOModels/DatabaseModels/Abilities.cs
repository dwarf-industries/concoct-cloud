using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Abilities
    {
        public Abilities()
        {
            AssociatedAbilityCombos = new HashSet<AssociatedAbilityCombos>();
            AssociatedCharacterAbilities = new HashSet<AssociatedCharacterAbilities>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Damage { get; set; }
        public int EffectId { get; set; }
        public double Effort { get; set; }
        public string FilePath { get; set; }
        public int? IsStatic { get; set; }
        public double? PosX { get; set; }
        public double? PosY { get; set; }
        public double? PosZ { get; set; }
        public int? HitTime { get; set; }
        public string AbilityIconPath { get; set; }
        public int? AbilityType { get; set; }
        public string AbilityDescription { get; set; }
        public string VideoSource { get; set; }

        public virtual CombotEffectTypes AbilityTypeNavigation { get; set; }
        public virtual AbilityAfterEffect Effect { get; set; }
        public virtual ICollection<AssociatedAbilityCombos> AssociatedAbilityCombos { get; set; }
        public virtual ICollection<AssociatedCharacterAbilities> AssociatedCharacterAbilities { get; set; }
    }
}
