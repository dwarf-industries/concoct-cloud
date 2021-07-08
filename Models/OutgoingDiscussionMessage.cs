using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rokono_Control.Models
{
    public class OutgoingDiscussionMessage
    {
        public int AccountId { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
    }
}
