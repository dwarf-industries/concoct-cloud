using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class NpcMovingPoints
    {
        public NpcMovingPoints()
        {
            AssociatedNpcMovingPoints = new HashSet<AssociatedNpcMovingPoints>();
        }

        public int Id { get; set; }
        public string PointData { get; set; }
        public int? PointPos { get; set; }
        public double? PosX { get; set; }
        public double? PosY { get; set; }
        public double? PosZ { get; set; }

        public virtual ICollection<AssociatedNpcMovingPoints> AssociatedNpcMovingPoints { get; set; }
    }
}
