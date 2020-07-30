using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Stories
    {
        public Stories()
        {
            AssociatedDialogOptionNcc = new HashSet<AssociatedDialogOptionNcc>();
            AssociatedNpcConversations = new HashSet<AssociatedNpcConversations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ZoneId { get; set; }
        public int RaceId { get; set; }

        public virtual ICollection<AssociatedDialogOptionNcc> AssociatedDialogOptionNcc { get; set; }
        public virtual ICollection<AssociatedNpcConversations> AssociatedNpcConversations { get; set; }
    }
}
