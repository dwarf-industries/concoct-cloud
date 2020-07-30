using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class NpcBaseAbilities
    {
        public NpcBaseAbilities()
        {
            Npcs = new HashSet<Npcs>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string AbilityPath { get; set; }
        public int? AbilityType { get; set; }
        public string AnimationName { get; set; }
        public double? AnimationTime { get; set; }
        public double? AbilityDamage { get; set; }

        public virtual ICollection<Npcs> Npcs { get; set; }
    }
}
