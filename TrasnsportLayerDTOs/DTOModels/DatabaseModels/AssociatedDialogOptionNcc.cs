using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedDialogOptionNcc
    {
        public int Id { get; set; }
        public int DiloagOptionId { get; set; }
        public int NpcConversationId { get; set; }
        public int? StoryLineId { get; set; }

        public virtual DialogOptions DiloagOption { get; set; }
        public virtual NpcConversations NpcConversation { get; set; }
        public virtual Stories StoryLine { get; set; }
    }
}
