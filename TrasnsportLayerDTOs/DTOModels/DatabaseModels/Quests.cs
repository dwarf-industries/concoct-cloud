using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Quests
    {
        public Quests()
        {
            AssociatedCharacterCompletedQuests = new HashSet<AssociatedCharacterCompletedQuests>();
            AssociatedCharacterQuestStates = new HashSet<AssociatedCharacterQuestStates>();
            AssociatedCharacterQuests = new HashSet<AssociatedCharacterQuests>();
            AssociatedMobs = new HashSet<AssociatedMobs>();
            AssociatedQuestBookReadings = new HashSet<AssociatedQuestBookReadings>();
            AssociatedQuestCollectable = new HashSet<AssociatedQuestCollectable>();
            AssociatedQuestQuestions = new HashSet<AssociatedQuestQuestions>();
            AssociatedQuestRewards = new HashSet<AssociatedQuestRewards>();
            AssociatedZoneQuests = new HashSet<AssociatedZoneQuests>();
            DialogOptions = new HashSet<DialogOptions>();
        }

        public int Id { get; set; }
        public int QuestType { get; set; }
        public double Experience { get; set; }
        public double Gold { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int? ZoneId { get; set; }
        public double? PossitionX { get; set; }
        public double? PossitionY { get; set; }
        public double? PossitionZ { get; set; }
        public int? Difficulty { get; set; }
        public int? AssociatedCharacterQuestState { get; set; }

        public virtual QuestTypes QuestTypeNavigation { get; set; }
        public virtual ICollection<AssociatedCharacterCompletedQuests> AssociatedCharacterCompletedQuests { get; set; }
        public virtual ICollection<AssociatedCharacterQuestStates> AssociatedCharacterQuestStates { get; set; }
        public virtual ICollection<AssociatedCharacterQuests> AssociatedCharacterQuests { get; set; }
        public virtual ICollection<AssociatedMobs> AssociatedMobs { get; set; }
        public virtual ICollection<AssociatedQuestBookReadings> AssociatedQuestBookReadings { get; set; }
        public virtual ICollection<AssociatedQuestCollectable> AssociatedQuestCollectable { get; set; }
        public virtual ICollection<AssociatedQuestQuestions> AssociatedQuestQuestions { get; set; }
        public virtual ICollection<AssociatedQuestRewards> AssociatedQuestRewards { get; set; }
        public virtual ICollection<AssociatedZoneQuests> AssociatedZoneQuests { get; set; }
        public virtual ICollection<DialogOptions> DialogOptions { get; set; }
    }
}
