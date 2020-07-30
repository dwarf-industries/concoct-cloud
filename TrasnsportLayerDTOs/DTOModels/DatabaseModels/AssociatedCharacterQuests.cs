using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedCharacterQuests
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public int QuestId { get; set; }
        public int? Compleate { get; set; }

        public virtual Characters Character { get; set; }
        public virtual Quests Quest { get; set; }
    }
}
