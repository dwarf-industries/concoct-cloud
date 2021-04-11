using System.Collections.Generic;
using Rokono_Control.Models;

namespace RokonoControl.Models
{
    public class IncomingExistingProjectMembers
    {
        public int ProjectId { get; set; }
        public List<OutgoingUserAccounts> Accounts { get; set; }
    }
}