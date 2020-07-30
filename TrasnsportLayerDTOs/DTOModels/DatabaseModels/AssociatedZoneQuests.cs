using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedZoneQuests
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int QuestId { get; set; }

        public virtual Quests Quest { get; set; }
        public virtual Zones Zone { get; set; }
    }
}
