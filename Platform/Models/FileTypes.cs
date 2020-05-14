using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class FileTypes
    {
        public FileTypes()
        {
            SystemFiles = new HashSet<SystemFiles>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<SystemFiles> SystemFiles { get; set; }
    }
}
