using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class ActiveMovementSpeeds
    {
        public ActiveMovementSpeeds()
        {
            Characters = new HashSet<Characters>();
        }

        public int Id { get; set; }
        public double Value { get; set; }
        public int? MovementType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Characters> Characters { get; set; }
    }
}
