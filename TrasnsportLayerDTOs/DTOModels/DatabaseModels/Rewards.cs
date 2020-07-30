using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Rewards
    {
        public Rewards()
        {
            AssociatedQuestRewards = new HashSet<AssociatedQuestRewards>();
        }

        public int Id { get; set; }
        public string RewardName { get; set; }
        public int RewardId { get; set; }
        public double SellAmmount { get; set; }

        public virtual ICollection<AssociatedQuestRewards> AssociatedQuestRewards { get; set; }
    }
}
