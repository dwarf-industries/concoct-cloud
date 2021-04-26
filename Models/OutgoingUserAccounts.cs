namespace Rokono_Control.Models
{
    public class OutgoingUserAccounts
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public int WorkItemOption { get; set; }
        public int ChatChannels { get; set; }
        public int EditUserRights { get; set; }
        public int IterationOptions { get; set; }
        public int ViewWorkItems { get; set; }
        public int ScheduleManagement { get; set; }
        public bool Online { get; set; }
        public int Documentation { get; set; }
    }
}