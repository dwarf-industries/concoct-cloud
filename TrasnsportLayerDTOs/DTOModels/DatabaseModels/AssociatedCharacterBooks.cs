using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedCharacterBooks
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? CharacterId { get; set; }

        public virtual Books Book { get; set; }
        public virtual Characters Character { get; set; }
    }
}
