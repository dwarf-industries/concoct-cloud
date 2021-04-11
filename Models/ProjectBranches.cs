using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rokono_Control.Models
{
    public class ProjectBranches
    {
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
        public string BranchName { get; set; }
        public List<OutgoingCommitTemp> Commits { get; set; }
    }
}
