using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class QuestTypes
    {
        public QuestTypes()
        {
            Quests = new HashSet<Quests>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Quests> Quests { get; set; }
    }
}
