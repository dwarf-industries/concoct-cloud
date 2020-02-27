using Rokono_Control.Models;

namespace RokonoControl.Models
{
    public class IncomingProjectAccount
    {
        public string email{ get ;set;} 
        public string Username { get; set; }
        public string password {get; set;}
        public OutgoingUserAccounts accountRights {get; set;}
        public int ProjectId { get; set; }
        public int AccountId { get; set;}
    }
}