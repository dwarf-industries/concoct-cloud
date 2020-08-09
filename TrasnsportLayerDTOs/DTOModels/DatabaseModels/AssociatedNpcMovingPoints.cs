using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedNpcMovingPoints
    {
        public int Id { get; set; }
        public int? PointId { get; set; }
        public int? NpcId { get; set; }
        public string RefferencePath { get; set; }

        public virtual Npcs Npc { get; set; }
        public virtual NpcMovingPoints Point { get; set; }
    }
}
