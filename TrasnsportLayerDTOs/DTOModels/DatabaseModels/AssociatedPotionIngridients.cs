using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedPotionIngridients
    {
        public int Id { get; set; }
        public int? IngridientId { get; set; }
        public int? Quantity { get; set; }
        public int? PotionRecepieId { get; set; }

        public virtual PotionIngridients Ingridient { get; set; }
        public virtual PotionRecepie PotionRecepie { get; set; }
    }
}
