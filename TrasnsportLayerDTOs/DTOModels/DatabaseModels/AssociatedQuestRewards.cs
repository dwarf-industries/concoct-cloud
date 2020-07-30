using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedQuestRewards
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public int RewardId { get; set; }

        public virtual Quests Quest { get; set; }
        public virtual Rewards Reward { get; set; }
    }
}
