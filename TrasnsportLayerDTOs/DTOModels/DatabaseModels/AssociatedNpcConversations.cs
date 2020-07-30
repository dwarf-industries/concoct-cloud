using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedNpcConversations
    {
        public AssociatedNpcConversations()
        {
            AssociatedCcpermutations = new HashSet<AssociatedCcpermutations>();
        }

        public int Id { get; set; }
        public int NpcConversationId { get; set; }
        public int CharacterId { get; set; }
        public int StoryLineId { get; set; }
        public int? NpcId { get; set; }

        public virtual Npcs Npc { get; set; }
        public virtual NpcConversations NpcConversation { get; set; }
        public virtual Stories StoryLine { get; set; }
        public virtual ICollection<AssociatedCcpermutations> AssociatedCcpermutations { get; set; }
    }
}
