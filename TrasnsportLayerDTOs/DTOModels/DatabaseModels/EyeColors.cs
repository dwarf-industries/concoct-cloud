using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class EyeColors
    {
        public EyeColors()
        {
            Characters = new HashSet<Characters>();
        }

        public int Id { get; set; }
        public double? R { get; set; }
        public double? G { get; set; }
        public double? B { get; set; }
        public double? Transperency { get; set; }

        public virtual ICollection<Characters> Characters { get; set; }
    }
}
