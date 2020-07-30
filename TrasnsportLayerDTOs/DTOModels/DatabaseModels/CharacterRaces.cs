using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class CharacterRaces
    {
        public CharacterRaces()
        {
            Characters = new HashSet<Characters>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Characters> Characters { get; set; }
    }
}
