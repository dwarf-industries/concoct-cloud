using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rokono_Control.Models
{
    public class IncomingIterationRequest
    {
        public int ProjectId { get; set; }
        public bool IsPublic { get; set; }
        public int Iteration { get; set; }
        public string Calling { get; set; }
    }
}
