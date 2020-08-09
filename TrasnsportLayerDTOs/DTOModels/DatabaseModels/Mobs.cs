using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Mobs
    {
        public Mobs()
        {
            AssociatedMobs = new HashSet<AssociatedMobs>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double HealthPoints { get; set; }
        public double AntyMagicaResistanceLevel { get; set; }
        public double SkinThickness { get; set; }
        public double DamageDealt { get; set; }
        public int Active { get; set; }
        public int? NpcId { get; set; }

        public virtual Npcs Npc { get; set; }
        public virtual ICollection<AssociatedMobs> AssociatedMobs { get; set; }
    }
}
