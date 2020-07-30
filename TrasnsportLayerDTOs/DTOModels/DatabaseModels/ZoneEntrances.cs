using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class ZoneEntrances
    {
        public ZoneEntrances()
        {
            AssociatedZoneEntrancePoints = new HashSet<AssociatedZoneEntrancePoints>();
            InverseExitPointNavigation = new HashSet<ZoneEntrances>();
        }

        public int Id { get; set; }
        public double? ScaleX { get; set; }
        public double? ScaleY { get; set; }
        public double? ScaleZ { get; set; }
        public double? CenterX { get; set; }
        public double? CenterY { get; set; }
        public double? CenterZ { get; set; }
        public double? PositionX { get; set; }
        public double? PositionY { get; set; }
        public double? PositionZ { get; set; }
        public double? RotationX { get; set; }
        public double? RotationY { get; set; }
        public double? RotationZ { get; set; }
        public double? RotationW { get; set; }
        public int? ExitPoint { get; set; }
        public int? IsZoneEntrance { get; set; }
        public int? NextZoneId { get; set; }
        public string EntranceName { get; set; }

        public virtual ZoneEntrances ExitPointNavigation { get; set; }
        public virtual ICollection<AssociatedZoneEntrancePoints> AssociatedZoneEntrancePoints { get; set; }
        public virtual ICollection<ZoneEntrances> InverseExitPointNavigation { get; set; }
    }
}
