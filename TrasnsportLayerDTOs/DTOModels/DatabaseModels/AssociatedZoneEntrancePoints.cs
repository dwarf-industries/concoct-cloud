using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedZoneEntrancePoints
    {
        public int Id { get; set; }
        public int? ZoneId { get; set; }
        public int? PointId { get; set; }

        public virtual ZoneEntrances Point { get; set; }
        public virtual Zones Zone { get; set; }
    }
}
