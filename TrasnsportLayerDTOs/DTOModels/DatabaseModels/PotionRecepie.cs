using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class PotionRecepie
    {
        public PotionRecepie()
        {
            AssociatedPotionIngridients = new HashSet<AssociatedPotionIngridients>();
        }

        public int Id { get; set; }
        public string PotionName { get; set; }
        public int? RewardId { get; set; }

        public virtual Items Reward { get; set; }
        public virtual ICollection<AssociatedPotionIngridients> AssociatedPotionIngridients { get; set; }
    }
}
