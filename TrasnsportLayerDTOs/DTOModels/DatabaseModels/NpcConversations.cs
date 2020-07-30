using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class NpcConversations
    {
        public NpcConversations()
        {
            AssociatedCcpermutations = new HashSet<AssociatedCcpermutations>();
            AssociatedDialogOptionNcc = new HashSet<AssociatedDialogOptionNcc>();
            AssociatedNpcConversations = new HashSet<AssociatedNpcConversations>();
        }

        public int Id { get; set; }
        public int DialogOptions1 { get; set; }
        public int? DialogOption2 { get; set; }
        public int? DialogOption3 { get; set; }
        public string NpcText { get; set; }

        public virtual DialogOptions DialogOption2Navigation { get; set; }
        public virtual DialogOptions DialogOption3Navigation { get; set; }
        public virtual DialogOptions DialogOptions1Navigation { get; set; }
        public virtual ICollection<AssociatedCcpermutations> AssociatedCcpermutations { get; set; }
        public virtual ICollection<AssociatedDialogOptionNcc> AssociatedDialogOptionNcc { get; set; }
        public virtual ICollection<AssociatedNpcConversations> AssociatedNpcConversations { get; set; }
    }
}
