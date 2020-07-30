using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class GearTypes
    {
        public GearTypes()
        {
            CharacterGearItems = new HashSet<CharacterGearItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CharacterGearItems> CharacterGearItems { get; set; }
    }
}
