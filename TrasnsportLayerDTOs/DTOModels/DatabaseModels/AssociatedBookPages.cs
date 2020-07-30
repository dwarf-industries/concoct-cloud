using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedBookPages
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? PageId { get; set; }
        public int? HasReadTrough { get; set; }

        public virtual Books Book { get; set; }
        public virtual Pages Page { get; set; }
    }
}
