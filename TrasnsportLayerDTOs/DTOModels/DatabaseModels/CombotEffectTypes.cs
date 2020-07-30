using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class CombotEffectTypes
    {
        public CombotEffectTypes()
        {
            Abilities = new HashSet<Abilities>();
            AbilityCombos = new HashSet<AbilityCombos>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Abilities> Abilities { get; set; }
        public virtual ICollection<AbilityCombos> AbilityCombos { get; set; }
    }
}
