using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Npcs
    {
        public Npcs()
        {
            AssociatedNpcConversations = new HashSet<AssociatedNpcConversations>();
            AssociatedNpcMovingPoints = new HashSet<AssociatedNpcMovingPoints>();
            Mobs = new HashSet<Mobs>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Damage { get; set; }
        public int? Age { get; set; }
        public int? HairColor { get; set; }
        public int? NailsColor { get; set; }
        public int? TeethColor { get; set; }
        public int? EyesColor { get; set; }
        public int? Gender { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public double? Speed { get; set; }
        public double? HungerLevel { get; set; }
        public double? Health { get; set; }
        public double? PainLevel { get; set; }
        public int? FactionId { get; set; }
        public int? ZoneId { get; set; }
        public int? IsQuestGiver { get; set; }
        public int? InGameModelId { get; set; }
        public double? PossitionX { get; set; }
        public double? PossitionY { get; set; }
        public double? PossitionZ { get; set; }
        public double? RotationZ { get; set; }
        public double? RotationX { get; set; }
        public double? RotationY { get; set; }
        public string CharacterImage { get; set; }
        public string RecepieString { get; set; }
        public int? IsSeller { get; set; }
        public int? BaseAbilityId { get; set; }

        public virtual NpcBaseAbilities BaseAbility { get; set; }
        public virtual Zones Zone { get; set; }
        public virtual ICollection<AssociatedNpcConversations> AssociatedNpcConversations { get; set; }
        public virtual ICollection<AssociatedNpcMovingPoints> AssociatedNpcMovingPoints { get; set; }
        public virtual ICollection<Mobs> Mobs { get; set; }
    }
}
