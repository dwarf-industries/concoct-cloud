using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class QuestQuestistions
    {
        public QuestQuestistions()
        {
            AssociatedQuestQuestions = new HashSet<AssociatedQuestQuestions>();
        }

        public int Id { get; set; }
        public string Questiong { get; set; }
        public string QuestionAnswer { get; set; }
        public string MemorizedText { get; set; }
        public string DudAnswer1 { get; set; }
        public string DudAnswer2 { get; set; }

        public virtual ICollection<AssociatedQuestQuestions> AssociatedQuestQuestions { get; set; }
    }
}
