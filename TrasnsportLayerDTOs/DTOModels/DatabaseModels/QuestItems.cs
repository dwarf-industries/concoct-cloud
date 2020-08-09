using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class QuestItems
    {
        public QuestItems()
        {
            AssociatedQuestCollectable = new HashSet<AssociatedQuestCollectable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public int? ObjectId { get; set; }
        public double? PossitionX { get; set; }
        public double? PossitionY { get; set; }
        public double? PossitionZ { get; set; }
        public double? ScaleX { get; set; }
        public double? ScaleY { get; set; }
        public double? ScaleZ { get; set; }

        public virtual Items Object { get; set; }
        public virtual ICollection<AssociatedQuestCollectable> AssociatedQuestCollectable { get; set; }
    }
}
