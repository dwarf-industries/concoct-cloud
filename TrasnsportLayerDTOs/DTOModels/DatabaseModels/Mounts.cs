using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Mounts
    {
        public Mounts()
        {
            AssociatedCharacterMounts = new HashSet<AssociatedCharacterMounts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ModelPath { get; set; }
        public double MountingHeight { get; set; }
        public double? CharacterHeaight { get; set; }
        public double? MountSpeed { get; set; }

        public virtual ICollection<AssociatedCharacterMounts> AssociatedCharacterMounts { get; set; }
    }
}
