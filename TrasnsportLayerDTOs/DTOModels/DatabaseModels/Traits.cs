using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Traits
    {
        public Traits()
        {
            AssociatedCharacterTraits = new HashSet<AssociatedCharacterTraits>();
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }

        public virtual ICollection<AssociatedCharacterTraits> AssociatedCharacterTraits { get; set; }
    }
}
