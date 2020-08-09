using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class BookReadingQuest
    {
        public BookReadingQuest()
        {
            AssociatedQuestBookReadings = new HashSet<AssociatedQuestBookReadings>();
        }

        public int Id { get; set; }
        public string RandomSentenceOne { get; set; }
        public string RandomSentenceTwo { get; set; }
        public string RandomSentenceTree { get; set; }
        public string RandomSentenceFour { get; set; }
        public string RandomSentenceFive { get; set; }
        public string GeneratedQuiz { get; set; }

        public virtual ICollection<AssociatedQuestBookReadings> AssociatedQuestBookReadings { get; set; }
    }
}
