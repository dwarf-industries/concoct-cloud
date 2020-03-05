using System.Collections.Generic;
using Rokono_Control.Models;

namespace Platform.Models
{
    public class OutboundBackupModel
    {
        public List<WorkItemIterations> Iterations {get; set;}
        public List<UserAccounts> UserAccounts {get; set;}
        public  List<Boards> Boards {get; set;}
        public   List<WorkItem> WorkItems {get; set;}
        public List<AssociatedProjectMemberRights> MemberRights {get; set;}
        public List<AssociatedWrorkItemChildren> WrorkItemChildrens {get; set;}
        public Projects CurrentProject {get; set;}
        public string Serialized { get; set; }
    }
}