using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class PotionIngridients
    {
        public PotionIngridients()
        {
            AssociatedPotionIngridients = new HashSet<AssociatedPotionIngridients>();
        }

        public int Id { get; set; }
        public string IngridientName { get; set; }
        public int? ItemId { get; set; }

        public virtual Items Item { get; set; }
        public virtual ICollection<AssociatedPotionIngridients> AssociatedPotionIngridients { get; set; }
    }
}
