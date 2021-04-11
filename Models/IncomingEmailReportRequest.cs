namespace Platform.Models
{
    using System.Collections.Generic;
    using Rokono_Control.Models;
    public class IncomingEmailReportRequest
    {
        public int ProjectId { get; set; }
        public List<OutgoingWorkItem> Items {get; set;}
    }
}