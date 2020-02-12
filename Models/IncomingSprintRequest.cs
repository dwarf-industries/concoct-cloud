using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rokono_Control.Models
{
    public class IncomingSprintRequest
    {        
        public int ProjectId { get; set; }
        public int WorkItemTypeId { get; set; }
        public int IterationId { get; set; }
        public int PersonId { get; set; }
        public int All { get; set; }
    }
}
