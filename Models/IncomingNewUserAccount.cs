namespace Rokono_Control.Models
{
    public class IncomingNewUserAccount
    {
        public string Email { get; set; }       
        public string Password { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public bool ProjectRights { get; set; }
    }
}