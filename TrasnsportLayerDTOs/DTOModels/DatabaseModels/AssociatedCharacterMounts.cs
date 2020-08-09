using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedCharacterMounts
    {
        public int Id { get; set; }
        public int MountId { get; set; }
        public int CharacterId { get; set; }

        public virtual Characters Character { get; set; }
        public virtual Mounts Mount { get; set; }
    }
}
