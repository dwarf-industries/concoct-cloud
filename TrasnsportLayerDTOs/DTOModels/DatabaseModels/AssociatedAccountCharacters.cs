using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedAccountCharacters
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? CharacterId { get; set; }

        public virtual UserAccounts Account { get; set; }
        public virtual Characters Character { get; set; }
    }
}
