using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedAbilityCombos
    {
        public int Id { get; set; }
        public int? AbilityId { get; set; }
        public int? ComboId { get; set; }
        public int? Position { get; set; }

        public virtual Abilities Ability { get; set; }
        public virtual AbilityCombos Combo { get; set; }
    }
}
