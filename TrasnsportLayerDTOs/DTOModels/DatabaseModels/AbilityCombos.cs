using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AbilityCombos
    {
        public AbilityCombos()
        {
            AssociatedAbilityCombos = new HashSet<AssociatedAbilityCombos>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public double Damage { get; set; }
        public int? EffectId { get; set; }
        public int? IsStatic { get; set; }

        public virtual CombotEffectTypes TypeNavigation { get; set; }
        public virtual ICollection<AssociatedAbilityCombos> AssociatedAbilityCombos { get; set; }
    }
}
