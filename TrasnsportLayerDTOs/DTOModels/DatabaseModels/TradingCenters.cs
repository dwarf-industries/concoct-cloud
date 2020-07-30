using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class TradingCenters
    {
        public TradingCenters()
        {
            AssociatedTradedBussinessItems = new HashSet<AssociatedTradedBussinessItems>();
        }

        public int Id { get; set; }
        public double? TaxAmmount { get; set; }

        public virtual ICollection<AssociatedTradedBussinessItems> AssociatedTradedBussinessItems { get; set; }
    }
}
