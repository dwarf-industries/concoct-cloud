using System;
using System.Collections.Generic;

namespace Rokono_Control.Models
{
    public partial class WorkItem
    {
        public WorkItem()
        {
            AssociatedBoardWorkItems = new HashSet<AssociatedBoardWorkItems>();
            AssociatedWorkItemDuplicatesWorkItem = new HashSet<AssociatedWorkItemDuplicates>();
            AssociatedWorkItemDuplicatesWorkItemChild = new HashSet<AssociatedWorkItemDuplicates>();
            AssociatedWorkItemPredecessorsWorkItem = new HashSet<AssociatedWorkItemPredecessors>();
            AssociatedWorkItemPredecessorsWorkItemChild = new HashSet<AssociatedWorkItemPredecessors>();
            AssociatedWorkItemRelatedWorkItem = new HashSet<AssociatedWorkItemRelated>();
            AssociatedWorkItemRelatedWorkItemChild = new HashSet<AssociatedWorkItemRelated>();
            AssociatedWorkItemSuccessorsWorkItem = new HashSet<AssociatedWorkItemSuccessors>();
            AssociatedWorkItemSuccessorsWorkItemChild = new HashSet<AssociatedWorkItemSuccessors>();
            AssociatedWorkItemTestsWorkItem = new HashSet<AssociatedWorkItemTests>();
            AssociatedWorkItemTestsWorkItemChild = new HashSet<AssociatedWorkItemTests>();
            AssociatedWrorkItemChildrenWorkItem = new HashSet<AssociatedWrorkItemChildren>();
            AssociatedWrorkItemChildrenWorkItemChild = new HashSet<AssociatedWrorkItemChildren>();
            AssociatedWrorkItemParentsWorkItem = new HashSet<AssociatedWrorkItemParents>();
            AssociatedWrorkItemParentsWorkItemChild = new HashSet<AssociatedWrorkItemParents>();
        }

        public int Id { get; set; }
        public int? WorkItemTypeId { get; set; }
        public string Title { get; set; }
        public int? AssignedAccount { get; set; }
        public int? StateId { get; set; }
        public int? AreaId { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PriorityId { get; set; }
        public int? ClassificationId { get; set; }
        public int? DevelopmentId { get; set; }
        public int? ParentId { get; set; }
        public int? Reason { get; set; }
        public int? Iteration { get; set; }
        public string RepoSteps { get; set; }
        public string SystemInfo { get; set; }
        public string ResolvedReason { get; set; }
        public int? ItemPriority { get; set; }
        public int? Severity { get; set; }
        public int? Activity { get; set; }
        public string OriginEstitame { get; set; }
        public string Remaining { get; set; }
        public string Compleated { get; set; }
        public int? BranchId { get; set; }
        public int? FoundInBuild { get; set; }
        public int? IntegratedInBuild { get; set; }
        public int? ReasonId { get; set; }
        public int? RelationId { get; set; }
        public int? RiskId { get; set; }
        public string BusinessValue { get; set; }
        public string TimeCapacity { get; set; }
        public int? ValueAreaId { get; set; }
        public string Effort { get; set; }
        public string StackRank { get; set; }
        public DateTime? DueDate { get; set; }
        public string StoryPoints { get; set; }
        public string AcceptanceCriteria { get; set; }

        public virtual WorkItemActivity ActivityNavigation { get; set; }
        public virtual WorkItemAreas Area { get; set; }
        public virtual UserAccounts AssignedAccountNavigation { get; set; }
        public virtual WorkItemIterations IterationNavigation { get; set; }
        public virtual WorkItemPriorities Priority { get; set; }
        public virtual WorkItemReasons ReasonNavigation { get; set; }
        public virtual WorkItemRelations Relation { get; set; }
        public virtual Risks Risk { get; set; }
        public virtual WorkItemSeverities SeverityNavigation { get; set; }
        public virtual WorkItemStates State { get; set; }
        public virtual ValueAreas ValueArea { get; set; }
        public virtual WorkItemTypes WorkItemType { get; set; }
        public virtual ICollection<AssociatedBoardWorkItems> AssociatedBoardWorkItems { get; set; }
        public virtual ICollection<AssociatedWorkItemDuplicates> AssociatedWorkItemDuplicatesWorkItem { get; set; }
        public virtual ICollection<AssociatedWorkItemDuplicates> AssociatedWorkItemDuplicatesWorkItemChild { get; set; }
        public virtual ICollection<AssociatedWorkItemPredecessors> AssociatedWorkItemPredecessorsWorkItem { get; set; }
        public virtual ICollection<AssociatedWorkItemPredecessors> AssociatedWorkItemPredecessorsWorkItemChild { get; set; }
        public virtual ICollection<AssociatedWorkItemRelated> AssociatedWorkItemRelatedWorkItem { get; set; }
        public virtual ICollection<AssociatedWorkItemRelated> AssociatedWorkItemRelatedWorkItemChild { get; set; }
        public virtual ICollection<AssociatedWorkItemSuccessors> AssociatedWorkItemSuccessorsWorkItem { get; set; }
        public virtual ICollection<AssociatedWorkItemSuccessors> AssociatedWorkItemSuccessorsWorkItemChild { get; set; }
        public virtual ICollection<AssociatedWorkItemTests> AssociatedWorkItemTestsWorkItem { get; set; }
        public virtual ICollection<AssociatedWorkItemTests> AssociatedWorkItemTestsWorkItemChild { get; set; }
        public virtual ICollection<AssociatedWrorkItemChildren> AssociatedWrorkItemChildrenWorkItem { get; set; }
        public virtual ICollection<AssociatedWrorkItemChildren> AssociatedWrorkItemChildrenWorkItemChild { get; set; }
        public virtual ICollection<AssociatedWrorkItemParents> AssociatedWrorkItemParentsWorkItem { get; set; }
        public virtual ICollection<AssociatedWrorkItemParents> AssociatedWrorkItemParentsWorkItemChild { get; set; }
    }
}
