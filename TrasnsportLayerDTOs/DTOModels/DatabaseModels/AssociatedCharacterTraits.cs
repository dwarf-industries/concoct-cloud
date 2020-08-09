using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedCharacterTraits
    {
        public int Id { get; set; }
        public int TraidId { get; set; }
        public int CharacterId { get; set; }
        public int AnswerType { get; set; }

        public virtual Characters Character { get; set; }
        public virtual Traits Traid { get; set; }
    }
}
