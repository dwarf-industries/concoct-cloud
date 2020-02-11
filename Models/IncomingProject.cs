namespace Rokono_Control.Models
{
    using System.Collections.Generic;
    public class IncomingProject
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public List<OutgoingUserAccounts> Users { get; set; }
        public List<WorkItemIterations> Iterations { get; set; }
    }
}