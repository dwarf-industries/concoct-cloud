using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedCharacterCompletedQuests
    {
        public int Id { get; set; }
        public int? QuestId { get; set; }
        public int? CharacterId { get; set; }

        public virtual Characters Character { get; set; }
        public virtual Quests CharacterNavigation { get; set; }
    }
}
