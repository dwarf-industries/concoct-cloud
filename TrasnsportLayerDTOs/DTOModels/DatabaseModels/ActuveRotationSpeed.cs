using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class ActuveRotationSpeed
    {
        public ActuveRotationSpeed()
        {
            Characters = new HashSet<Characters>();
        }

        public int Id { get; set; }
        public double Value { get; set; }

        public virtual ICollection<Characters> Characters { get; set; }
    }
}
