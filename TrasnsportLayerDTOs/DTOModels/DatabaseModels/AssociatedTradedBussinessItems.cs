using System.Collections.Generic;

namespace DbScaffold.Models
{
[System.Serializable]
    public partial class AssociatedTradedBussinessItems
    {
        public int Id { get; set; }
        public int? TradingCenterId { get; set; }
        public int? BusinessItemId { get; set; }
        public int? CategoryId { get; set; }

        public virtual BusinessItems BusinessItem { get; set; }
        public virtual ItemCategories Category { get; set; }
        public virtual TradingCenters TradingCenter { get; set; }
    }
}
