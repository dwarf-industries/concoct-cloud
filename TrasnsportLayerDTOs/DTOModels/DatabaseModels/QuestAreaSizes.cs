using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class QuestAreaSizes
    {
        public QuestAreaSizes()
        {
            AssociatedQuestCollectable = new HashSet<AssociatedQuestCollectable>();
        }

        public int Id { get; set; }
        public int Size { get; set; }

        public virtual ICollection<AssociatedQuestCollectable> AssociatedQuestCollectable { get; set; }
    }
}
