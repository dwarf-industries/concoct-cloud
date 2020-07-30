using System;
using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class UnassignedPledges
    {
        public int Id { get; set; }
        public string TempGuid { get; set; }
        public DateTime? DateOfAssigment { get; set; }
    }
}
