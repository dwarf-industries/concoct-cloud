using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedCcpermutations
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public int AssociatedNpcConversationId { get; set; }
        public int? LastConversationId { get; set; }

        public virtual AssociatedNpcConversations AssociatedNpcConversation { get; set; }
        public virtual Characters Character { get; set; }
        public virtual NpcConversations LastConversation { get; set; }
    }
}
