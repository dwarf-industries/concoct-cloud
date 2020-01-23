using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public class OutgoingAccountManagment
    {
        public int AccountId { get; set; }
        public  string Projects {get; set;} 
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool ProjectRights { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        
    }
}