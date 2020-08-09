using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Books
    {
        public Books()
        {
            AssociatedBookPages = new HashSet<AssociatedBookPages>();
            AssociatedCharacterBooks = new HashSet<AssociatedCharacterBooks>();
        }

        public int Id { get; set; }
        public string ModelPath { get; set; }
        public string Name { get; set; }
        public int IntellectLevel { get; set; }

        public virtual ICollection<AssociatedBookPages> AssociatedBookPages { get; set; }
        public virtual ICollection<AssociatedCharacterBooks> AssociatedCharacterBooks { get; set; }
    }
}
