
namespace Rokono_Control.Models
{
    using System;
    using System.Collections.Generic;

    public class IncomingWorkItem
    {   
        //Bug Related
        public int WorkItemId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public int AssignedUser { get; set; }
        public string State { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int WorktItemType { get; set; }
        public string ItemReason { get; set; }
        public string ItemArea { get; set; }
        public string Description { get; set; }
        public string ItemIteration { get; set; }
        public string ItemPriority { get; set; }
        public string ItemSeverity { get; set; }
        public string ItemActivity { get; set; }
        public string FoundInBuild { get; set; }
        public string ResolvedInBuild { get; set; }
        public string BusinessValue { get; set; }
        public string RepoSteps { get; set; }
        public string SystemInfo { get; set; }
        public string OriginalEstimate { get; set; }
        public string ItemRemaining { get; set; }
        public string ItemCompleated { get; set; }
        //Epic Related
        public string RiskId { get; set; }
        public string Effort { get; set; }
        public string BValue { get; set; }
        public string TimeCriticality { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ValueAreId { get; set; }

        //Issue
        public DateTime DueDate { get; set; }
        public string Rank { get; set; }
        //User Story
        public string StoryPoints { get; set; }
        //Shared
        public List<LinkedItems> SelectedChildren  { get; set; }
        public int ParentId { get; set; }
  

    }
}