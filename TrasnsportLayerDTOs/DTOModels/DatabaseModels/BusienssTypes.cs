using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class BusienssTypes
    {
        public BusienssTypes()
        {
            AssociatedBuildingFunctionality = new HashSet<AssociatedBuildingFunctionality>();
        }

        public int Id { get; set; }
        public int MaximumEmployeesCount { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

        public virtual ICollection<AssociatedBuildingFunctionality> AssociatedBuildingFunctionality { get; set; }
    }
}
