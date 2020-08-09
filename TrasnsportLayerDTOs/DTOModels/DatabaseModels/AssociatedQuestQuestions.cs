using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedQuestQuestions
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public int? QuestId { get; set; }

        public virtual Quests Quest { get; set; }
        public virtual QuestQuestistions Question { get; set; }
    }
}
