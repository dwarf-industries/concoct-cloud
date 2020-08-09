using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class UserAccounts
    {
        public UserAccounts()
        {
            AssociatedAccountCharacters = new HashSet<AssociatedAccountCharacters>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string BillingAddress { get; set; }
        public int PlayModel { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string TempGuid { get; set; }
        public double? PledgeAmount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<AssociatedAccountCharacters> AssociatedAccountCharacters { get; set; }
    }
}
