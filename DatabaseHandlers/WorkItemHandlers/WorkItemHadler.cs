using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rokono_Control.Models;

namespace RokonoControl.DatabaseHandlers.WorkItemHandlers
{
    public class WorkItemHadler
    {
        
        internal static bool UpdateWorkItem(IncomingWorkItem currentItem,  RokonoControlContext Context)
        {
            var dbVersion = Context.WorkItem.FirstOrDefault(x=>x.Id == currentItem.WorkItemId);
            Context.Attach(dbVersion);
            var relationshipId = default(int);
            currentItem.SelectedChildren.ForEach(x=>{
                var getItem = Context.WorkItemRelations.FirstOrDefault(y=>y.Id == x.RelationShipId);
                if(getItem.RelationName == "Parent")
                    relationshipId = getItem.Id;
            });
            if(currentItem.FoundInBuild == null)
                currentItem.FoundInBuild = "0";
            if(currentItem.ResolvedInBuild == null)
                currentItem.ResolvedInBuild = "0";
            
             dbVersion.Title = currentItem.Title;
            if(!string.IsNullOrEmpty(currentItem.AssignedUser))
                dbVersion.AssignedAccount =int.Parse(currentItem.AssignedUser);
            if(!string.IsNullOrEmpty(currentItem.WorktItemType))
                dbVersion.WorkItemTypeId = int.Parse(currentItem.WorktItemType);
            if(!string.IsNullOrEmpty(currentItem.State))
                dbVersion.StateId = int.Parse(currentItem.State);
            if(!string.IsNullOrEmpty(currentItem.ItemReason))
                dbVersion.Reason = int.Parse(currentItem.ItemReason);
            if(!string.IsNullOrEmpty(currentItem.ItemIteration))
                dbVersion.Iteration =  int.Parse(currentItem.ItemIteration);
            if(!string.IsNullOrEmpty(currentItem.ItemArea))
            dbVersion.AreaId = int.Parse(currentItem.ItemArea);
               

            if(int.Parse(currentItem.WorktItemType) == 1)
            {
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                dbVersion.PriorityId = int.Parse(currentItem.ItemPriority);
                if(!string.IsNullOrEmpty(currentItem.ItemActivity))
                dbVersion.Activity = int.Parse(currentItem.ItemActivity);
                dbVersion.RepoSteps = currentItem.RepoSteps;
                dbVersion.SystemInfo = currentItem.SystemInfo;
                if(!string.IsNullOrEmpty(currentItem.FoundInBuild))
                dbVersion.FoundInBuild = int.Parse(currentItem.FoundInBuild);
                if(!string.IsNullOrEmpty(currentItem.ResolvedInBuild))
                dbVersion.IntegratedInBuild = int.Parse(currentItem.ResolvedInBuild);
                dbVersion.Compleated = currentItem.ItemCompleated;
                dbVersion.Remaining = currentItem.ItemRemaining;
                dbVersion.OriginEstitame = currentItem.OriginalEstimate;
                if(!string.IsNullOrEmpty(currentItem.ItemSeverity))
                dbVersion.Severity=int.Parse(currentItem.ItemSeverity);
                dbVersion.ParentId = relationshipId;
                
            }
            else if(int.Parse(currentItem.WorktItemType) == 2)
            {
                dbVersion.AcceptanceCriteria = currentItem.AcceptanceCriteria;
                dbVersion.Description = currentItem.Description;
                dbVersion.StartDate = currentItem.StartDate;
                dbVersion.EndDate = currentItem.EndDate;
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                  dbVersion.PriorityId = int.Parse(currentItem.ItemPriority);
                dbVersion.Effort = currentItem.Effort;
                dbVersion.BusinessValue = currentItem.BValue;
                dbVersion.TimeCapacity = currentItem.TimeCriticality;
                dbVersion.ValueAreaId = int.Parse(currentItem.ValueAreId);

            }
            else if(int.Parse(currentItem.WorktItemType) == 5)
            {
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                dbVersion.PriorityId = int.Parse(currentItem.ItemPriority);
                if(!string.IsNullOrEmpty(currentItem.RiskId))
                dbVersion.RiskId = int.Parse(currentItem.RiskId);
                dbVersion.Effort = currentItem.Effort;
                dbVersion.BusinessValue = currentItem.BValue;
                dbVersion.TimeCapacity = currentItem.TimeCriticality;
                if(!string.IsNullOrEmpty(currentItem.ValueAreId))
                dbVersion.ValueAreaId = int.Parse(currentItem.ValueAreId);
                dbVersion.StartDate = currentItem.StartDate;
                dbVersion.EndDate = currentItem.EndDate;
                
            }
            else if(int.Parse(currentItem.WorktItemType) == 6)
            {
                dbVersion.StackRank = currentItem.Rank;
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                dbVersion.PriorityId = int.Parse(currentItem.ItemPriority);
                dbVersion.DueDate = currentItem.DueDate;
            }
            else if(int.Parse(currentItem.WorktItemType) == 3)
            {
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                dbVersion.PriorityId = int.Parse(currentItem.ItemPriority);
                if(!string.IsNullOrEmpty(currentItem.ItemActivity))
                dbVersion.Activity = int.Parse(currentItem.ItemActivity);
                dbVersion.OriginEstitame = currentItem.OriginalEstimate;
                dbVersion.Remaining = currentItem.ItemRemaining;
                dbVersion.Compleated = currentItem.ItemCompleated;
                if(!string.IsNullOrEmpty(currentItem.ResolvedInBuild))
                dbVersion.IntegratedInBuild = int.Parse(currentItem.ResolvedInBuild);
                dbVersion.Description = currentItem.Description;

            }
            else if(int.Parse(currentItem.WorktItemType) == 7)
            {
                dbVersion.StoryPoints = currentItem.StoryPoints;
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                dbVersion.PriorityId = int.Parse(currentItem.ItemPriority);
                if(!string.IsNullOrEmpty(currentItem.RiskId))
                dbVersion.RiskId = int.Parse(currentItem.RiskId);
                if(!string.IsNullOrEmpty(currentItem.ValueAreId))
                dbVersion.ValueAreaId = int.Parse(currentItem.ValueAreId);
                dbVersion.Description = currentItem.Description;
                dbVersion.AcceptanceCriteria = currentItem.AcceptanceCriteria;

            }

            Context.Entry(dbVersion).State = EntityState.Modified;
            Context.SaveChanges();
            return true;
        }


        public static bool AddNewWorkItem (IncomingWorkItem currentItem, RokonoControlContext Context)
        {
                var relationshipId = default(int);
            currentItem.SelectedChildren.ForEach(x=>{
                var getItem = Context.WorkItemRelations.FirstOrDefault(y=>y.Id == x.RelationShipId);
                if(getItem.RelationName == "Parent")
                    relationshipId = getItem.Id;
            });
            if(currentItem.FoundInBuild == null)
                currentItem.FoundInBuild = "0";
            if(currentItem.ResolvedInBuild == null)
                currentItem.ResolvedInBuild = "0";
            
            var databaseItem = new WorkItem();
            databaseItem.Title = currentItem.Title;
            if(!string.IsNullOrEmpty(currentItem.AssignedUser))
                databaseItem.AssignedAccount =int.Parse(currentItem.AssignedUser);
            if(!string.IsNullOrEmpty(currentItem.WorktItemType))
                databaseItem.WorkItemTypeId = int.Parse(currentItem.WorktItemType);
            if(!string.IsNullOrEmpty(currentItem.State))
                databaseItem.StateId = int.Parse(currentItem.State);
            if(!string.IsNullOrEmpty(currentItem.ItemReason))
                databaseItem.Reason = int.Parse(currentItem.ItemReason);
            if(!string.IsNullOrEmpty(currentItem.ItemIteration))
                databaseItem.Iteration =  int.Parse(currentItem.ItemIteration);
            if(!string.IsNullOrEmpty(currentItem.ItemArea))
            databaseItem.AreaId = int.Parse(currentItem.ItemArea);

               

            if(int.Parse(currentItem.WorktItemType) == 1)
            {
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                databaseItem.PriorityId = int.Parse(currentItem.ItemPriority);
                if(!string.IsNullOrEmpty(currentItem.ItemActivity))
                databaseItem.Activity = int.Parse(currentItem.ItemActivity);
                databaseItem.RepoSteps = currentItem.RepoSteps;
                databaseItem.SystemInfo = currentItem.SystemInfo;
                if(!string.IsNullOrEmpty(currentItem.FoundInBuild))
                databaseItem.FoundInBuild = int.Parse(currentItem.FoundInBuild);
                if(!string.IsNullOrEmpty(currentItem.ResolvedInBuild))
                databaseItem.IntegratedInBuild = int.Parse(currentItem.ResolvedInBuild);
                databaseItem.Compleated = currentItem.ItemCompleated;
                databaseItem.Remaining = currentItem.ItemRemaining;
                databaseItem.OriginEstitame = currentItem.OriginalEstimate;
                if(!string.IsNullOrEmpty(currentItem.ItemSeverity))
                databaseItem.Severity=int.Parse(currentItem.ItemSeverity);
                databaseItem.ParentId = relationshipId;
                
            }
            else if(int.Parse(currentItem.WorktItemType) == 2)
            {
                databaseItem.AcceptanceCriteria = currentItem.AcceptanceCriteria;
                databaseItem.Description = currentItem.Description;
                databaseItem.StartDate = currentItem.StartDate;
                databaseItem.EndDate = currentItem.EndDate;
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                  databaseItem.PriorityId = int.Parse(currentItem.ItemPriority);
                databaseItem.Effort = currentItem.Effort;
                databaseItem.BusinessValue = currentItem.BValue;
                databaseItem.TimeCapacity = currentItem.TimeCriticality;
                databaseItem.ValueAreaId = int.Parse(currentItem.ValueAreId);

            }
            else if(int.Parse(currentItem.WorktItemType) == 5)
            {
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                databaseItem.PriorityId = int.Parse(currentItem.ItemPriority);
                if(!string.IsNullOrEmpty(currentItem.RiskId))
                databaseItem.RiskId = int.Parse(currentItem.RiskId);
                databaseItem.Effort = currentItem.Effort;
                databaseItem.BusinessValue = currentItem.BValue;
                databaseItem.TimeCapacity = currentItem.TimeCriticality;
                if(!string.IsNullOrEmpty(currentItem.ValueAreId))
                databaseItem.ValueAreaId = int.Parse(currentItem.ValueAreId);
                databaseItem.StartDate = currentItem.StartDate;
                databaseItem.EndDate = currentItem.EndDate;
            }
            else if(int.Parse(currentItem.WorktItemType) == 6)
            {
                databaseItem.StackRank = currentItem.Rank;
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                databaseItem.PriorityId = int.Parse(currentItem.ItemPriority);
                databaseItem.DueDate = currentItem.DueDate;
            }
            else if(int.Parse(currentItem.WorktItemType) == 3)
            {
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                databaseItem.PriorityId = int.Parse(currentItem.ItemPriority);
                if(!string.IsNullOrEmpty(currentItem.ItemActivity))
                databaseItem.Activity = int.Parse(currentItem.ItemActivity);
                databaseItem.OriginEstitame = currentItem.OriginalEstimate;
                databaseItem.Remaining = currentItem.ItemRemaining;
                databaseItem.Compleated = currentItem.ItemCompleated;
                if(!string.IsNullOrEmpty(currentItem.ResolvedInBuild))
                databaseItem.IntegratedInBuild = int.Parse(currentItem.ResolvedInBuild);
                databaseItem.Description = currentItem.Description;

            }
            else if(int.Parse(currentItem.WorktItemType) == 7)
            {
                databaseItem.StoryPoints = currentItem.StoryPoints;
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                databaseItem.PriorityId = int.Parse(currentItem.ItemPriority);
                if(!string.IsNullOrEmpty(currentItem.RiskId))
                databaseItem.RiskId = int.Parse(currentItem.RiskId);
                if(!string.IsNullOrEmpty(currentItem.ValueAreId))
                databaseItem.ValueAreaId = int.Parse(currentItem.ValueAreId);
                databaseItem.Description = currentItem.Description;
                databaseItem.AcceptanceCriteria = currentItem.AcceptanceCriteria;


            }
            var item = Context.WorkItem.Add(databaseItem);
            Context.SaveChanges();
        
            var pBoard = Context.AssociatedProjectBoards.FirstOrDefault(x=> x.ProjectId == currentItem.ProjectId);
            if(pBoard != null)
            {
                Context.AssociatedBoardWorkItems.Add(new AssociatedBoardWorkItems{
                    BoardId = pBoard.BoardId.Value,
                    WorkItemId = item.Entity.Id,
                    ProjectId = currentItem.ProjectId
                });
                Context.SaveChanges();
            }
            if(relationshipId == default(int))
            {
                databaseItem.ParentId = item.Entity.Id;
                Context.Attach(item.Entity);
                Context.Entry(item.Entity).Property("ParentId").IsModified = true;
                Context.SaveChanges();           
            }
            currentItem.SelectedChildren.ForEach(x=>{
                    var getItem = Context.WorkItemRelations.FirstOrDefault(y=>y.Id == x.RelationShipId);
                    if(getItem.RelationName != "Parent")
                    {
                        if(getItem.RelationName == "Child")
                        {
                            var getWorkItem = Context.WorkItem.FirstOrDefault(y=> y.Id == x.WorkItemId);
                            getWorkItem.ParentId =item.Entity.Id;
                            
                            Context.Attach(getWorkItem);
                            Context.Entry(getWorkItem).Property("ParentId").IsModified = true;
                            Context.SaveChanges();
                            Context.AssociatedWrorkItemChildren.Add(new AssociatedWrorkItemChildren{
                                WorkItemChildId = x.WorkItemId,
                                WorkItemId = item.Entity.Id
                            });
                            Context.SaveChanges();
                            
                        }
                        if(getItem.RelationName == "Duplicate")
                        {
                            Context.AssociatedWorkItemDuplicates.Add(new AssociatedWorkItemDuplicates{
                                WorkItemChildId = x.WorkItemId,
                                WorkItemId = item.Entity.Id
                            });
                            Context.SaveChanges();
                        }
                        if(getItem.RelationName == "Predeccessor")
                        {
                            Context.AssociatedWorkItemPredecessors.Add(new AssociatedWorkItemPredecessors{
                                WorkItemChildId = x.WorkItemId,
                                WorkItemId = item.Entity.Id
                            });
                            Context.SaveChanges();
                        }
                        if(getItem.RelationName == "Successor")
                        {
                            Context.AssociatedWorkItemSuccessors.Add(new AssociatedWorkItemSuccessors{
                                WorkItemChildId = x.WorkItemId,
                                WorkItemId = item.Entity.Id
                            });
                            Context.SaveChanges();
                        }
                        if(getItem.RelationName == "Related")
                        {
                            Context.AssociatedWorkItemRelated.Add(new AssociatedWorkItemRelated{
                                WorkItemChildId = x.WorkItemId,
                                WorkItemId =item.Entity.Id
                            });
                            Context.SaveChanges();
                        }
                        if(getItem.RelationName == "Test")
                        {
                            Context.AssociatedWorkItemTests.Add(new AssociatedWorkItemTests{
                                WorkItemChildId = x.WorkItemId,
                                WorkItemId = item.Entity.Id
                            });
                            Context.SaveChanges();
                        }
                    }
                    else
                    {
                        Context.AssociatedWrorkItemParents.Add(new AssociatedWrorkItemParents{
                            WorkItemChildId = item.Entity.Id,
                            WorkItemId = x.WorkItemId
                        });
                        Context.SaveChanges();
                            
                    }
                });
            return true;
        }
    }
}