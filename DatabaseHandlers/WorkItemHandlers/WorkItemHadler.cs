using System;
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
                var relId = int.Parse(x.RelationShipId);
                var getItem = Context.WorkItemRelations.FirstOrDefault(y=>y.Id == relId);
                if(getItem.RelationName == "Parent")
                    relationshipId = getItem.Id;
            });
            if(currentItem.FoundInBuild == null)
                currentItem.FoundInBuild = "0";
            if(currentItem.ResolvedInBuild == null)
                currentItem.ResolvedInBuild = "0";
            
             dbVersion.Title = currentItem.Title;
            if(currentItem.AssignedUser != 0)
                dbVersion.AssignedAccount =currentItem.AssignedUser;
            if(currentItem.WorktItemType != 0)
                dbVersion.WorkItemTypeId = currentItem.WorktItemType;
            if(!string.IsNullOrEmpty(currentItem.State))
                dbVersion.StateId = int.Parse(currentItem.State);
            if(!string.IsNullOrEmpty(currentItem.ItemReason))
                dbVersion.Reason = int.Parse(currentItem.ItemReason);
            if(!string.IsNullOrEmpty(currentItem.ItemIteration))
                dbVersion.Iteration =  int.Parse(currentItem.ItemIteration);
            if(!string.IsNullOrEmpty(currentItem.ItemArea))
            dbVersion.AreaId = int.Parse(currentItem.ItemArea);
               

            if(currentItem.WorktItemType == 1)
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
            else if(currentItem.WorktItemType == 2)
            {
                dbVersion.AcceptanceCriteria = currentItem.AcceptanceCriteria;
                dbVersion.Description = currentItem.Description;
                dbVersion.StartDate = currentItem.StartDate == null ? DateTime.Now : currentItem.StartDate;
                dbVersion.EndDate = currentItem.EndDate == null ? DateTime.Now : currentItem.EndDate;
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                  dbVersion.PriorityId = int.Parse(currentItem.ItemPriority);
                dbVersion.Effort = currentItem.Effort;
                dbVersion.BusinessValue = currentItem.BusinessValue;
                dbVersion.TimeCapacity = currentItem.TimeCriticality;
                dbVersion.ValueAreaId = int.Parse(currentItem.ValueAreId);

            }
            else if(currentItem.WorktItemType == 5)
            {
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                dbVersion.PriorityId = int.Parse(currentItem.ItemPriority);
                if(!string.IsNullOrEmpty(currentItem.RiskId))
                dbVersion.RiskId = int.Parse(currentItem.RiskId);
                dbVersion.Effort = currentItem.Effort;
                dbVersion.BusinessValue = currentItem.BusinessValue;
                dbVersion.TimeCapacity = currentItem.TimeCriticality;
                if(!string.IsNullOrEmpty(currentItem.ValueAreId))
                dbVersion.ValueAreaId = int.Parse(currentItem.ValueAreId);
                dbVersion.StartDate = currentItem.StartDate == null ? DateTime.Now : currentItem.StartDate;
                dbVersion.EndDate = currentItem.EndDate == null ? DateTime.Now : currentItem.EndDate;
                
            }
            else if(currentItem.WorktItemType == 6)
            {
                dbVersion.StackRank = currentItem.Rank;
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                dbVersion.PriorityId = int.Parse(currentItem.ItemPriority);
                dbVersion.DueDate = currentItem.DueDate == null ? DateTime.Now : currentItem.DueDate;
            }
            else if(currentItem.WorktItemType == 3)
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
            else if(currentItem.WorktItemType == 7)
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
            dbVersion.StartDate = currentItem.StartDate.Ticks == 0 ? DateTime.Now : currentItem.StartDate;
            dbVersion.EndDate = currentItem.EndDate.Ticks == 0 ? DateTime.Now : currentItem.EndDate;
            dbVersion.DueDate = currentItem.DueDate.Ticks == 0 ? DateTime.Now : currentItem.DueDate;
            Context.Entry(dbVersion).State = EntityState.Modified;
            Context.SaveChanges();
            return true;
        }


        public static bool AddNewWorkItem (IncomingWorkItem currentItem, RokonoControlContext Context)
        {
                var relationshipId = default(int);
            currentItem.SelectedChildren.ForEach(x=>{
                                var relId = int.Parse(x.RelationShipId);

                var getItem = Context.WorkItemRelations.FirstOrDefault(y=>y.Id == relId);
                if(getItem.RelationName == "Parent")
                    relationshipId = getItem.Id;
            });
            if(currentItem.FoundInBuild == null)
                currentItem.FoundInBuild = "0";
            if(currentItem.ResolvedInBuild == null)
                currentItem.ResolvedInBuild = "0";
            
            var databaseItem = new WorkItem();
            databaseItem.Title = currentItem.Title;
            if(currentItem.AssignedUser != 0)
                databaseItem.AssignedAccount = currentItem.AssignedUser;
            if(currentItem.WorktItemType != 0)
                databaseItem.WorkItemTypeId = currentItem.WorktItemType;
            if(!string.IsNullOrEmpty(currentItem.State))
                databaseItem.StateId = int.Parse(currentItem.State);
            if(!string.IsNullOrEmpty(currentItem.ItemReason))
                databaseItem.Reason = int.Parse(currentItem.ItemReason);
            if(!string.IsNullOrEmpty(currentItem.ItemIteration))
                databaseItem.Iteration =  int.Parse(currentItem.ItemIteration);
            if(!string.IsNullOrEmpty(currentItem.ItemArea))
            databaseItem.AreaId = int.Parse(currentItem.ItemArea);

               

            if(currentItem.WorktItemType == 1)
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
            else if(currentItem.WorktItemType == 2)
            {
                databaseItem.AcceptanceCriteria = currentItem.AcceptanceCriteria;
                databaseItem.Description = currentItem.Description;
                databaseItem.StartDate = currentItem.StartDate == null ? DateTime.Now : currentItem.StartDate;
                databaseItem.EndDate = currentItem.EndDate == null ? DateTime.Now : currentItem.EndDate;
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                  databaseItem.PriorityId = int.Parse(currentItem.ItemPriority);
                databaseItem.Effort = currentItem.Effort;
                databaseItem.BusinessValue = currentItem.BusinessValue;
                databaseItem.TimeCapacity = currentItem.TimeCriticality;
                databaseItem.ValueAreaId = int.Parse(currentItem.ValueAreId);

            }
            else if(currentItem.WorktItemType == 5)
            {
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                databaseItem.PriorityId = int.Parse(currentItem.ItemPriority);
                if(!string.IsNullOrEmpty(currentItem.RiskId))
                databaseItem.RiskId = int.Parse(currentItem.RiskId);
                databaseItem.Effort = currentItem.Effort;
                databaseItem.BusinessValue = currentItem.BusinessValue;
                databaseItem.TimeCapacity = currentItem.TimeCriticality;
                if(!string.IsNullOrEmpty(currentItem.ValueAreId))
                databaseItem.ValueAreaId = int.Parse(currentItem.ValueAreId);
                databaseItem.StartDate = currentItem.StartDate == null ? DateTime.Now : currentItem.StartDate;
                databaseItem.EndDate = currentItem.EndDate == null ? DateTime.Now : currentItem.EndDate;
            }
            else if(currentItem.WorktItemType == 6)
            {
                databaseItem.StackRank = currentItem.Rank;
                if(!string.IsNullOrEmpty(currentItem.ItemPriority))
                databaseItem.PriorityId = int.Parse(currentItem.ItemPriority);
                databaseItem.DueDate = currentItem.DueDate == null ? DateTime.Now : currentItem.DueDate;;
            }
            else if(currentItem.WorktItemType == 3)
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
            else if(currentItem.WorktItemType == 7)
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
            databaseItem.StartDate = currentItem.StartDate.Ticks == 0 ? DateTime.Now : currentItem.StartDate;
            databaseItem.EndDate = currentItem.EndDate.Ticks == 0 ? DateTime.Now : currentItem.EndDate;
            databaseItem.DueDate = currentItem.DueDate.Ticks == 0 ? DateTime.Now : currentItem.DueDate;
            var item = Context.WorkItem.Add(databaseItem);
            Context.SaveChanges();
        
            var pBoard = Context.AssociatedProjectBoards.Include(x=>x.Board).FirstOrDefault(x=> x.ProjectId == currentItem.ProjectId && x.Board.BoardName == "Open");
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
                                                var relId = int.Parse(x.RelationShipId);

                    var getItem = Context.WorkItemRelations.FirstOrDefault(y=>y.Id ==relId);
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
                                WorkItemId = item.Entity.Id,
                                RelationType = relId
                            });
                            Context.SaveChanges();
                        }
                     
                    }
                    else
                    {
                        var dbItem = Context.WorkItem.FirstOrDefault(y=>y.Id == x.WorkItemId);
                        dbItem.ParentId = item.Entity.Id;
                        Context.Attach(dbItem);
                        Context.Update(dbItem);
                        Context.SaveChanges();
                            
                    }
                });
            return true;
        }
    }
}