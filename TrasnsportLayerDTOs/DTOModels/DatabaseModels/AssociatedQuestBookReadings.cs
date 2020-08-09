using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedQuestBookReadings
    {
        public int Id { get; set; }
        public int? BookReadingId { get; set; }
        public int? QuestId { get; set; }
        public int? HasReadTrough { get; set; }
        public int? CharacterId { get; set; }
        public string Remembred { get; set; }

        public virtual BookReadingQuest BookReading { get; set; }
        public virtual Quests Quest { get; set; }
    }
}
