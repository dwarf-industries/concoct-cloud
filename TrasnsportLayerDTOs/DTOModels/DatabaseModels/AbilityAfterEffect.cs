using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AbilityAfterEffect
    {
        public AbilityAfterEffect()
        {
            Abilities = new HashSet<Abilities>();
        }

        public int Id { get; set; }
        public string AbilityPath { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Abilities> Abilities { get; set; }
    }
}
