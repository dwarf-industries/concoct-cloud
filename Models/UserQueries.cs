using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class UserQueries
    {
        public int Id { get; set; }
        public string QueryName { get; set; }
        public int? UserId { get; set; }
        public string QueryData { get; set; }
        public DateTime? DateOfQuery { get; set; }
        public int? IsShared { get; set; }
        public int? ProjectId { get; set; }

        public virtual UserAccounts User { get; set; }
    }
}
