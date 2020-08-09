using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class QuestState
    {
        public QuestState()
        {
            AssociatedCharacterQuestStates = new HashSet<AssociatedCharacterQuestStates>();
        }

        public int Id { get; set; }
        public int? IsCollectable { get; set; }
        public int? CollectableCount { get; set; }
        public int? IsMob { get; set; }
        public int? MobCount { get; set; }
        public int? IsClass { get; set; }
        public int? BookReadingPage { get; set; }
        public int? RewardMultiplier { get; set; }
        public string Remembered { get; set; }
        public int? IsInQuiz { get; set; }

        public virtual ICollection<AssociatedCharacterQuestStates> AssociatedCharacterQuestStates { get; set; }
    }
}
