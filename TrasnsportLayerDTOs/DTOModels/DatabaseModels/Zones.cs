using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Zones
    {
        public Zones()
        {
            AssociatedAreaBuildings = new HashSet<AssociatedAreaBuildings>();
            AssociatedZoneEntrancePoints = new HashSet<AssociatedZoneEntrancePoints>();
            AssociatedZoneQuests = new HashSet<AssociatedZoneQuests>();
            Npcs = new HashSet<Npcs>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AssociatedAreaBuildings> AssociatedAreaBuildings { get; set; }
        public virtual ICollection<AssociatedZoneEntrancePoints> AssociatedZoneEntrancePoints { get; set; }
        public virtual ICollection<AssociatedZoneQuests> AssociatedZoneQuests { get; set; }
        public virtual ICollection<Npcs> Npcs { get; set; }
    }
}
