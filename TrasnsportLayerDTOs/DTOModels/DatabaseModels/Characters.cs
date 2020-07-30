using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class Characters
    {
        public Characters()
        {
            AssociatedAccountCharacters = new HashSet<AssociatedAccountCharacters>();
            AssociatedAreaBuildings = new HashSet<AssociatedAreaBuildings>();
            AssociatedCcpermutations = new HashSet<AssociatedCcpermutations>();
            AssociatedCharacterAbilities = new HashSet<AssociatedCharacterAbilities>();
            AssociatedCharacterBooks = new HashSet<AssociatedCharacterBooks>();
            AssociatedCharacterCompletedQuests = new HashSet<AssociatedCharacterCompletedQuests>();
            AssociatedCharacterMounts = new HashSet<AssociatedCharacterMounts>();
            AssociatedCharacterQuestStates = new HashSet<AssociatedCharacterQuestStates>();
            AssociatedCharacterQuests = new HashSet<AssociatedCharacterQuests>();
            AssociatedCharacterTraits = new HashSet<AssociatedCharacterTraits>();
            AssociatedEquippedCharacterItems = new HashSet<AssociatedEquippedCharacterItems>();
            AssociatedInventoryItems = new HashSet<AssociatedInventoryItems>();
        }

        public int Id { get; set; }
        public string CharacterName { get; set; }
        public int CharacterRace { get; set; }
        public int MainStackAbilities { get; set; }
        public int SecondaryStackAbilities { get; set; }
        public int CharacterLevel { get; set; }
        public int CharacterAge { get; set; }
        public double Hidratation { get; set; }
        public double Hunger { get; set; }
        public double Sleep { get; set; }
        public double Stamina { get; set; }
        public int? WeaponType { get; set; }
        public int? InZone { get; set; }
        public int? CharacterGender { get; set; }
        public int? HairColor { get; set; }
        public double? PossitionX { get; set; }
        public double? PossitionY { get; set; }
        public double? PossitionZ { get; set; }
        public double? RotationX { get; set; }
        public double? RotationY { get; set; }
        public double? RotationZ { get; set; }
        public double? RotationW { get; set; }
        public int? CharacterId { get; set; }
        public double? CharacterWeight { get; set; }
        public double? CharacterHeight { get; set; }
        public double? Height { get; set; }
        public int? EyeColor { get; set; }
        public string CharacterRecepie { get; set; }
        public int? ActiveMovementSpeed { get; set; }
        public int? ActiveRotationSpeed { get; set; }
        public int? EquipedWeaponId { get; set; }
        public int? ActiveRecepie { get; set; }
        public double? Gold { get; set; }
        public double? Experience { get; set; }

        public virtual ActiveMovementSpeeds ActiveMovementSpeedNavigation { get; set; }
        public virtual CharacterRecepies ActiveRecepieNavigation { get; set; }
        public virtual ActuveRotationSpeed ActiveRotationSpeedNavigation { get; set; }
        public virtual CharacterRaces CharacterRaceNavigation { get; set; }
        public virtual Weapons EquipedWeapon { get; set; }
        public virtual EyeColors EyeColorNavigation { get; set; }
        public virtual ICollection<AssociatedAccountCharacters> AssociatedAccountCharacters { get; set; }
        public virtual ICollection<AssociatedAreaBuildings> AssociatedAreaBuildings { get; set; }
        public virtual ICollection<AssociatedCcpermutations> AssociatedCcpermutations { get; set; }
        public virtual ICollection<AssociatedCharacterAbilities> AssociatedCharacterAbilities { get; set; }
        public virtual ICollection<AssociatedCharacterBooks> AssociatedCharacterBooks { get; set; }
        public virtual ICollection<AssociatedCharacterCompletedQuests> AssociatedCharacterCompletedQuests { get; set; }
        public virtual ICollection<AssociatedCharacterMounts> AssociatedCharacterMounts { get; set; }
        public virtual ICollection<AssociatedCharacterQuestStates> AssociatedCharacterQuestStates { get; set; }
        public virtual ICollection<AssociatedCharacterQuests> AssociatedCharacterQuests { get; set; }
        public virtual ICollection<AssociatedCharacterTraits> AssociatedCharacterTraits { get; set; }
        public virtual ICollection<AssociatedEquippedCharacterItems> AssociatedEquippedCharacterItems { get; set; }
        public virtual ICollection<AssociatedInventoryItems> AssociatedInventoryItems { get; set; }
    }
}
