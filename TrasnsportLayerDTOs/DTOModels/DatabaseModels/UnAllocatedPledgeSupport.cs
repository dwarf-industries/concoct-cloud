using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class UnAllocatedPledgeSupport
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public double DonationAmmount { get; set; }
    }
}
