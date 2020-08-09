using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Items
    {
        public Items()
        {
            PotionIngridients = new HashSet<PotionIngridients>();
            PotionRecepie = new HashSet<PotionRecepie>();
            QuestItems = new HashSet<QuestItems>();
        }

        public int Id { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public string ItemImage { get; set; }

        public virtual ICollection<PotionIngridients> PotionIngridients { get; set; }
        public virtual ICollection<PotionRecepie> PotionRecepie { get; set; }
        public virtual ICollection<QuestItems> QuestItems { get; set; }
    }
}
