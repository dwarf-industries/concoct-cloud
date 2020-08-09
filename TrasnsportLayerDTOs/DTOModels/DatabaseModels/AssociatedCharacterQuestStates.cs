using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedCharacterQuestStates
    {
        public int Id { get; set; }
        public int? CharacterId { get; set; }
        public int? QuestStateId { get; set; }
        public int? QuestId { get; set; }

        public virtual Characters Character { get; set; }
        public virtual Quests Quest { get; set; }
        public virtual QuestState QuestState { get; set; }
    }
}
