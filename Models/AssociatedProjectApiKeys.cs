using System;
using System.Collections.Generic;

#nullable disable

namespace Rokono_Control.Models
{
    public partial class AssociatedProjectApiKeys
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? KeyId { get; set; }
        public string ApiSecret { get; set; }

        public virtual ApiKeys Key { get; set; }
        public virtual Projects Project { get; set; }
    }
}
