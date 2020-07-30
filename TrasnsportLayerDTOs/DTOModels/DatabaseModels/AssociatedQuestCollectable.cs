using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedQuestCollectable
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public int QuestItemId { get; set; }
        public int? AreaSize { get; set; }

        public virtual QuestAreaSizes AreaSizeNavigation { get; set; }
        public virtual Quests Quest { get; set; }
        public virtual QuestItems QuestItem { get; set; }
    }
}
