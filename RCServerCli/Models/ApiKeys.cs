using System;
using System.Collections.Generic;

namespace RCServerCli.Models
{
    public partial class ApiKeys
    {
        public ApiKeys()
        {
            AssociatedProjectApiKeys = new HashSet<AssociatedProjectApiKeys>();
        }

        public int Id { get; set; }
        public string FeatureName { get; set; }
        public string SecretKey { get; set; }

        public virtual ICollection<AssociatedProjectApiKeys> AssociatedProjectApiKeys { get; set; }
    }
}
