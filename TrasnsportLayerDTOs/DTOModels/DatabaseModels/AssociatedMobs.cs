using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedMobs
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public int MobId { get; set; }

        public virtual Mobs Mob { get; set; }
        public virtual Quests Quest { get; set; }
    }
}
