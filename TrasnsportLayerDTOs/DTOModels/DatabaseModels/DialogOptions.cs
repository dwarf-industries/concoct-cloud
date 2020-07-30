using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class DialogOptions
    {
        public DialogOptions()
        {
            AssociatedDialogOptionNcc = new HashSet<AssociatedDialogOptionNcc>();
            NpcConversationsDialogOption2Navigation = new HashSet<NpcConversations>();
            NpcConversationsDialogOption3Navigation = new HashSet<NpcConversations>();
            NpcConversationsDialogOptions1Navigation = new HashSet<NpcConversations>();
        }

        public int Id { get; set; }
        public string ChoiceText { get; set; }
        public int IsYelling { get; set; }
        public int? IsOffensive { get; set; }
        public int? IsTaunting { get; set; }
        public int? AcceptQuest { get; set; }
        public int? IsLeaving { get; set; }
        public int? QuestId { get; set; }
        public string DialogIcon { get; set; }

        public virtual Quests Quest { get; set; }
        public virtual ICollection<AssociatedDialogOptionNcc> AssociatedDialogOptionNcc { get; set; }
        public virtual ICollection<NpcConversations> NpcConversationsDialogOption2Navigation { get; set; }
        public virtual ICollection<NpcConversations> NpcConversationsDialogOption3Navigation { get; set; }
        public virtual ICollection<NpcConversations> NpcConversationsDialogOptions1Navigation { get; set; }
    }
}
