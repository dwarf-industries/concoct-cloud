namespace Rokono_Control.Models
{
    public class OutgoingProjectRules
    {
        public bool CanCommit { get; set; }
        public bool CanView { get; set; }
        public bool CanCreateWork { get; set; }
        public bool CanDeleteWork { get; set; }
        public bool CanClone { get; set; }
    }
}