using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class CharacterRecepies
    {
        public CharacterRecepies()
        {
            Characters = new HashSet<Characters>();
        }

        public int Id { get; set; }
        public string Grown { get; set; }
        public string Adult { get; set; }
        public string Teenager { get; set; }

        public virtual ICollection<Characters> Characters { get; set; }
    }
}
