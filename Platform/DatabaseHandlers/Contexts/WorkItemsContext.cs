namespace Platform.DatabaseHandlers.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Platform.Models;
    using Rokono_Control.Models;
    using RokonoControl.DatabaseHandlers.WorkItemHandlers;
    using RokonoControl.Models;

    public class WorkItemsContext : IDisposable
    {

        RokonoControlContext Context;
        IConfiguration Configuration;
        private bool disposedValue;
        private bool disposedValue1;

        public WorkItemsContext(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        internal List<WorkItemTypes> GetAllWorkItemTypes() => Context.WorkItemTypes.ToList();

        internal WorkItemIterations GetProjectIteration(int id)
        {
            return Context.WorkItemIterations.FirstOrDefault(x => x.Id == id);
        }

        internal List<AssociatedBoardWorkItems> GetProjectWorkItems(int id, int parentType , int iteration)
        {
            var items = new List<AssociatedBoardWorkItems>();
            if (parentType == 0)
            {
                items = Context.AssociatedBoardWorkItems.Include(x => x.WorkItem)
                                       .ThenInclude(WorkItem => WorkItem.WorkItemType)
                                       .Include(x => x.WorkItem)
                                       .ThenInclude(WorkItem => WorkItem.State)
                                       .Include(x => x.WorkItem)
                                       .ThenInclude(WorkItem => WorkItem.AssignedAccountNavigation)
                                       .Include(x => x.WorkItem)
                                       .ThenInclude(WorkItem => WorkItem.AssociatedWrorkItemChildrenWorkItem)
                                       .ThenInclude(AssociatedWrorkItemChildrenWorkItem => AssociatedWrorkItemChildrenWorkItem.WorkItemChild)
                                       .Include(x => x.WorkItem)
                                       .ThenInclude(WorkItem => WorkItem.AssociatedWrorkItemChildrenWorkItem)
                                       .ThenInclude(AssociatedWrorkItemChildrenWorkItem => AssociatedWrorkItemChildrenWorkItem.RelationTypeNavigation)
                                       .Where(x => x.ProjectId == id && x.WorkItem.Iteration == iteration)
                                       .ToList();
            }
            else
            {
                items = Context.AssociatedBoardWorkItems.Include(x => x.WorkItem)
                                                       .ThenInclude(WorkItem => WorkItem.WorkItemType)
                                                       .Include(x => x.WorkItem)
                                                       .ThenInclude(WorkItem => WorkItem.State)
                                                       .Include(x => x.WorkItem)
                                                       .ThenInclude(WorkItem => WorkItem.AssignedAccountNavigation)
                                                       .Include(x => x.WorkItem)
                                                       .ThenInclude(WorkItem => WorkItem.AssociatedWrorkItemChildrenWorkItem)
                                                       .ThenInclude(AssociatedWrorkItemChildrenWorkItem => AssociatedWrorkItemChildrenWorkItem.WorkItemChild)
                                                       .Include(x => x.WorkItem)
                                                       .ThenInclude(WorkItem => WorkItem.AssociatedWrorkItemChildrenWorkItem)
                                                       .ThenInclude(AssociatedWrorkItemChildrenWorkItem => AssociatedWrorkItemChildrenWorkItem.RelationTypeNavigation)
                                                       .Where(x => x.ProjectId == id && x.WorkItem.WorkItemTypeId == parentType && x.WorkItem.Iteration == iteration)
                                                       .ToList();
            }
             return items;
        }

        internal List<AssociatedBoardWorkItems> GetPublicBugReports(int id)
        {
            var  items = Context.AssociatedBoardWorkItems.Include(x => x.WorkItem)
                                .ThenInclude(WorkItem => WorkItem.WorkItemType)
                                .Include(x => x.WorkItem)
                                .ThenInclude(WorkItem => WorkItem.State)
                                .Include(x => x.WorkItem)
                                .ThenInclude(WorkItem => WorkItem.AssignedAccountNavigation)
                                .Include(x => x.WorkItem)
                                .ThenInclude(WorkItem => WorkItem.AssociatedWrorkItemChildrenWorkItem)
                                .ThenInclude(AssociatedWrorkItemChildrenWorkItem => AssociatedWrorkItemChildrenWorkItem.WorkItemChild)
                                .Include(x => x.WorkItem)
                                .ThenInclude(WorkItem => WorkItem.AssociatedWrorkItemChildrenWorkItem)
                                .ThenInclude(AssociatedWrorkItemChildrenWorkItem => AssociatedWrorkItemChildrenWorkItem.RelationTypeNavigation)
                                .Where(x => x.ProjectId == id && x.WorkItem.IsPublic == 1 && x.WorkItem.WorkItemTypeId == 1)
                                .ToList();

            return items;
        }
        internal WorkItem GetPublicBugReport(int id)
        {
            var  item = Context.WorkItem
                            .Include(WorkItem => WorkItem.AssociatedWorkItemFiles)
                            .ThenInclude(AssociatedWorkItemFiles => AssociatedWorkItemFiles.File)
                            .ThenInclude(File => File.FileTypeNavigation)
                            .FirstOrDefault( x=>x.Id == id);
            if(item != null)
                return item;

            return null;
        }

        internal void CloseIteration(int projectId, int iteration, int newIteration)
        {
            var workItems = Context.AssociatedBoardWorkItems.Include(x => x.WorkItem)
                                            .Where(x => x.WorkItem.Iteration == iteration && x.Board.BoardType != 4)
                                            .Select(x=>x.WorkItem)
                                            .ToList();
            workItems.ForEach(x=>{
                var item = x;
                item.Iteration = newIteration;
                Context.Attach(item);
                Context.Update(item);
                Context.SaveChanges(); 
            });
            var oldIteration = Context.WorkItemIterations.FirstOrDefault(x=>x.Id == iteration);
            oldIteration.IsActive = 0;
            Context.Attach(oldIteration);
            Context.Update(oldIteration);
            Context.SaveChanges();
            var activeIteration = Context.WorkItemIterations.FirstOrDefault(x => x.Id == newIteration);
            activeIteration.IsActive = 1;
            Context.Attach(activeIteration);
            Context.Update(activeIteration);
            Context.SaveChanges(); 
        }

        internal List<SystemFiles> GetWorkItemFiles(int workItem)
        {
            return Context.AssociatedWorkItemFiles.Include(x => x.File)
                                                  .Where(x => x.WorkItemId == workItem)
                                                  .Select(x=>x.File)
                                                  .ToList();
        }

        internal bool IsNotParent(int parentId)
        {
            return Context.WorkItem.Any(x=>x.ParentId == parentId);
        }
        internal void RemoveWorkItems(List<OutgoingWorkItem> items)
        {
            items.ForEach(x=>{
                var item = Context.WorkItem.FirstOrDefault(y=>y.Id == x.Id);
                var associationsWhereParent = Context.WorkItem.Where(y=>y.ParentId == x.Id).ToList();
                var AssociatedWrorkItemChildren =  Context.AssociatedWrorkItemChildren.Where(y=>y.WorkItemChildId == x.Id).ToList();
                associationsWhereParent.ForEach(z=>{
                    var pItem = z;
                    pItem.ParentId = 0;
                    Context.Attach(pItem);
                    Context.Update(pItem);
                    Context.SaveChanges();
                });
                AssociatedWrorkItemChildren.ForEach(z=>{
                    var association = z;
                    Context.AssociatedWrorkItemChildren.Remove(association);
                    Context.SaveChanges();
                });
                var discussions = Context.AssociatedWorkItemMessages.Where(y=>y.WorkItemId == x.Id).ToList();
                Context.RemoveRange(discussions);
                Context.SaveChanges();
                var changelogs = Context.AssociatedWorkItemChangelogs.Where(y=>y.WorkitemId == x.Id).ToList();
                Context.AssociatedWorkItemChangelogs.RemoveRange(changelogs);
                Context.SaveChanges();
                var boards = Context.AssociatedBoardWorkItems.Where(y=>y.WorkItemId == x.Id).ToList();
                Context.AssociatedBoardWorkItems.RemoveRange(boards);
                Context.SaveChanges();
            });
        }

        internal void MakeWorkItemPrivate(int id, int status)
        {
            var workItem = Context.WorkItem.FirstOrDefault(x=>x.Id == id);
            workItem.IsPublic = status;
            Context.Attach(workItem);
            Context.Update(workItem);
            Context.SaveChanges();
        }
        internal List<WorkItem> GetWorkItemChildrenClean(int workItemId)
        {
            var items = new List<WorkItem>();
            items = Context.AssociatedWrorkItemChildren.Include(x => x.WorkItemChild)
                .Where(x => x.WorkItemId == workItemId)
                .Select(x => x.WorkItemChild)
                .ToList();
            items.AddRange(Context.WorkItem.Where(x=>x.ParentId == workItemId));
            return items;
        }

        internal void ChangeCardOwner(IncomingCardOwnerRequest card)
        {
            var getId = card.CardId.Split(" ");
            var parse = int.Parse(getId[1]);
            var getAccount = Context.UserAccounts.FirstOrDefault(x=>x.Email == card.Name);
            var currentCard = Context.WorkItem.FirstOrDefault(x=>x.Id == parse);
            currentCard.AssignedAccount = getAccount.Id;
            Context.Attach(currentCard);
            Context.Update(currentCard);
            Context.SaveChanges();
        }

        internal int GetProjectDefautIteration(int id)
        {
            return Context.AssociatedProjectIterations.FirstOrDefault(x => x.ProjectId == id && x.Iteration.IsActive == 1).IterationId;
        }

        internal void ChangeWorkItemBoard(IncomingCardRequest card)
        {
            var newBoardAssociation = Context.AssociatedProjectBoards.Include(x => x.Board).FirstOrDefault(x => x.ProjectId == card.ProjectId
                                                                                      && x.Board.BoardName == card.Board);
            var currentAssociation = Context.AssociatedBoardWorkItems.FirstOrDefault(x => x.WorkItemId == card.CardId);

            currentAssociation.BoardId = newBoardAssociation.Board.Id;
            Context.Attach(currentAssociation);
            Context.Update(currentAssociation);
            Context.SaveChanges();


        }
        internal WorkItem GetWorkItemById(int parse)
        {
            return Context.WorkItem.Include(x => x.WorkItemType)
                                   .FirstOrDefault(x=>x.Id == parse);
        }
        internal List<BindingCards> GetProjectCards(int projectId, int workItemType)
        {
            var boards = Context.AssociatedProjectBoards.Include(x => x.Board)
                                                .ThenInclude(Board => Board.AssociatedBoardWorkItems)
                                                .ThenInclude(AssociatedBoardWorkItems => AssociatedBoardWorkItems.WorkItem)
                                                .ThenInclude(WorkItem => WorkItem.State)
                                                .Include(x => x.Board)
                                                .ThenInclude(Board => Board.AssociatedBoardWorkItems)
                                                .ThenInclude(AssociatedBoardWorkItems => AssociatedBoardWorkItems.WorkItem)
                                                .ThenInclude(WorkItem => WorkItem.WorkItemType)
                                                .Include(x => x.Board)
                                                .ThenInclude(Board => Board.AssociatedBoardWorkItems)
                                                .ThenInclude(AssociatedBoardWorkItems => AssociatedBoardWorkItems.WorkItem)
                                                .ThenInclude(WorkItem => WorkItem.AssignedAccountNavigation)
                                                .Where(x => x.ProjectId == projectId && x.Board.AssociatedBoardWorkItems.Any(z => z.WorkItem.WorkItemTypeId == workItemType))
                                                .ToList();

            var result = new List<BindingBoard>();
            var Cards = new List<BindingCards>();
            boards.ForEach(x =>
            {
                x.Board.AssociatedBoardWorkItems.Where(y => y.WorkItem.WorkItemTypeId == workItemType).ToList().ForEach(y =>
                {
                    var related = new List<RelatedItems>();
                    y.WorkItem.AssociatedWrorkItemChildrenWorkItemChild.ToList().ForEach(z =>
                    {
                        related.Add(new RelatedItems
                        {
                            Id = z.WorkItem.Id,
                            Name = z.WorkItem.Title
                        });
                    });

                    Cards.Add(new BindingCards
                    {
                        InnerId = y.WorkItem.Id,
                        Id = $"Task {y.WorkItem.Id}",
                        Summary = $"Description: {y.WorkItem.Description} <br/> Acceptence creteria: {y.WorkItem.AcceptanceCriteria} ",
                        Title = y.WorkItem.Title,
                        Tags = $"{y.WorkItem.WorkItemType.TypeName}",
                        Priority = GetCardType(y.WorkItem.WorkItemType.TypeName),
                        Type = $"{x.Board.BoardName}",
                        Status = x.Board.BoardName,
                        AssgignedAccount = y.WorkItem.AssignedAccountNavigation != null ? y.WorkItem.AssignedAccountNavigation.Email : "Unassigned",
                        AssigneeId = y.WorkItem.AssignedAccountNavigation != null ? y.WorkItem.AssignedAccountNavigation.Id : 0,
                        Assignee = y.WorkItem.WorkItemType.TypeName
                        // Children = related
                    });
                });
            });
            return Cards;
        }
        internal List<BindingCards> GetProjectSprints(IncomingSprintRequest dataRequest, bool hasRights, int userId)
        {
            var result = new List<BindingBoard>();
            var Cards = new List<BindingCards>();
            var projectSprints = new List<WorkItem>();
            if (dataRequest.All == 1 && hasRights)
                projectSprints = Context.AssociatedBoardWorkItems.Include(x => x.WorkItem).Where(x => x.WorkItem.WorkItemTypeId == 7
                                                                              && x.ProjectId == dataRequest.ProjectId
                                                                              && x.WorkItem.Iteration == dataRequest.IterationId)
                                                                            .Select(x => x.WorkItem)
                                                                            .ToList();
            else if (dataRequest.PersonId != 0 && hasRights)
            {
                projectSprints = Context.AssociatedBoardWorkItems.Include(x => x.WorkItem).Where(x => x.WorkItem.WorkItemTypeId == 7
                                                                            && x.ProjectId == dataRequest.ProjectId
                                                                            && x.WorkItem.Iteration == dataRequest.IterationId
                                                                            && x.WorkItem.AssignedAccount == dataRequest.PersonId)
                                                                           .Select(x => x.WorkItem)
                                                                           .ToList();
            }
            else
                projectSprints = Context.AssociatedBoardWorkItems.Include(x => x.WorkItem).Where(x => x.WorkItem.WorkItemTypeId == 7
                                                                            && x.ProjectId == dataRequest.ProjectId
                                                                            && x.WorkItem.Iteration == dataRequest.IterationId
                                                                            && x.WorkItem.AssignedAccount == userId)
                                                                           .Select(x => x.WorkItem)
                                                                           .ToList();

            projectSprints.ForEach(x =>
            {
                var sprintTasks = Context.AssociatedWrorkItemChildren.Include(y => y.WorkItemChild)
                                                                     .ThenInclude(WorkItemChild => WorkItemChild.WorkItemType)
                                                                     .Include(y => y.WorkItemChild)
                                                                     .ThenInclude(WorkItemChild => WorkItemChild.AssignedAccountNavigation)
                                                                     .Where(y => y.WorkItemId == x.Id
                                                                                 && y.WorkItemChild.WorkItemTypeId != 7
                                                                                 && y.WorkItemChild.WorkItemTypeId != 2
                                                                                 && y.WorkItemChild.WorkItemTypeId != 5)
                                                                     .ToList();
                var whereParent = Context.WorkItem.Include(b=>b.AssignedAccountNavigation)
                                                  .Include(b=>b.WorkItemType)
                                                  .Where(b=>b.ParentId == x.Id  && b.WorkItemTypeId != 7
                                                                                 && b.WorkItemTypeId != 2
                                                                                 && b.WorkItemTypeId != 5).ToList();
                whereParent.ForEach(sprintTask => {
                    var taskBoard = Context.AssociatedBoardWorkItems.Include(z => z.Board)
                                                                    .FirstOrDefault(z => z.WorkItemId == sprintTask.Id);

                    var activeBoard = string.Empty;
                    if (taskBoard == null)
                        activeBoard = "Open";
                    else
                        activeBoard = taskBoard.Board.BoardName;
                    Cards.Add(new BindingCards
                    {
                        InnerId = sprintTask.Id,
                        Id = $"Task {sprintTask.Id}",
                        Summary = $"Description: {sprintTask.Description} <br/> Acceptence creteria: {sprintTask.AcceptanceCriteria} ",
                        Title = sprintTask.Title,
                        Tags = $"{sprintTask.WorkItemType.TypeName}",
                        Priority = GetCardType(sprintTask.WorkItemType.TypeName),
                        Type = $"{activeBoard}",
                        Status = activeBoard,
                        AssigneeId = sprintTask.AssignedAccountNavigation != null ? sprintTask.AssignedAccountNavigation.Id : 0,
                        Assignee = x.Title,
                        AssgignedAccount = sprintTask.AssignedAccountNavigation != null ? sprintTask.AssignedAccountNavigation.GitUsername : "Unassigned"
                    });
                });
                sprintTasks.ForEach(task =>
                {
                    var taskBoard = Context.AssociatedBoardWorkItems.Include(z => z.Board)
                                                                    .FirstOrDefault(z => z.WorkItemId == task.WorkItemChildId);

                    var activeBoard = string.Empty;
                    if (taskBoard == null)
                        activeBoard = "Open";
                    else
                        activeBoard = taskBoard.Board.BoardName;
                    Cards.Add(new BindingCards
                    {
                        InnerId = task.WorkItemChild.Id,
                        Id = $"Task {task.WorkItemChild.Id}",
                        Summary = $"Description: {task.WorkItemChild.Description} <br/> Acceptence creteria: {task.WorkItemChild.AcceptanceCriteria} ",
                        Title = task.WorkItemChild.Title,
                        Tags = $"{task.WorkItemChild.WorkItemType.TypeName}",
                        Priority = GetCardType(task.WorkItemChild.WorkItemType.TypeName),
                        Type = $"{activeBoard}",
                        Status = activeBoard,
                        Assignee = x.Title,
                        AssgignedAccount = task.WorkItemChild.AssignedAccountNavigation != null ? task.WorkItemChild.AssignedAccountNavigation.GitUsername : "Unassigned"
                    });
                });

            });

            return Cards;
        }

        private string GetCardType(string board)
        {
            var res = string.Empty;
            switch (board)
            {
                case "Epic":
                    res = "Epic";
                    break;
                case "Bug":
                    res = "Bug";
                    break;
                case "Task":
                    res = "Task";
                    break;
                case "User Story":
                    res = "Sprint";
                    break;
                 case "Issue":
                    res = "Issue";
                    break;
                case "Test":
                    res = "Test";
                    break;
                    
                default:
                    res = "Task";
                    break;
            }
            return res;
        }

        internal List<WorkItem> BackgroundWorkItems(List<OutgoingWorkItem> items)
        {
            var result = new List<WorkItem>();
            items.ForEach(x=>{
                result.Add(Context.WorkItem.FirstOrDefault(y=>y.Id ==x.Id));
                x.subtasks.ForEach(y=>{
                    result.Add(Context.WorkItem.FirstOrDefault(z=>z.Id == y.Id));
                });
            });
            return result;
        }
        internal AssociatedWorkItemMessages AddNewWorkItemMessage(IncomingWorkItemMessage request, int id)
        {
            var message = Context.WorkItemMessage.Add(new WorkItemMessage{
                SenderId = id,
                Content = request.Message,
                DateOfMessage = DateTime.Now
            });
            Context.SaveChanges();
            var association = Context.AssociatedWorkItemMessages.Add(new AssociatedWorkItemMessages{
                MessageId = message.Entity.Id,
                WorkItemId = request.WorkItemId
            });
            Context.SaveChanges();
            return association.Entity;
        }
        internal List<AssociatedWorkItemMessages> GetWorkItemDiscussions(int projectId, int workItemId)
        {
            return  Context.AssociatedWorkItemMessages.Include(x => x.Message)
                                                             .ThenInclude(Message => Message.Sender)
                                                             .Include(x => x.Message)
                                                             .Where(x => x.WorkItemId == workItemId)
                                                             .ToList();

        }



         internal string ChangeProjectBoardStatus(IncomingPublicBoardRequest request, string domain)
        {
            var getProject = Context.Projects.FirstOrDefault(x=>x.Id == request.ProjectId);
            getProject.PublicBoard = request.IsChecked;
            Context.Attach(getProject);
            Context.Update(getProject);
            Context.SaveChanges();
            var iteration = GetProjectIterations(request.ProjectId).FirstOrDefault();


            if(request.IsChecked == 1)
                return $"https://{domain}/Boards/PublicBoard?projectId={request.ProjectId}&iteration={iteration.Id}&person=0";
            else
                return string.Empty;
        }
        internal int GetWorkItemCountByType(int id, int boardType) => Context.AssociatedBoardWorkItems.Include(x => x.Board)
                                                                                                      .Where(x => x.ProjectId == id 
                                                                                                      && x.Board.BoardType == boardType)
                                                                                                      .Count();
        internal int GetCreatedWorkItemCount(int id) => Context.AssociatedBoardWorkItems.Where(x => x.ProjectId == id)
                                                                                        .Count();

        internal WorkItem GetWorkItemByTitle(string title)
        {
            return Context.WorkItem.FirstOrDefault(x=>x.Title == title && x.WorkItemTypeId == 7);
        }
        internal bool UpdateWorkItem(IncomingWorkItem currentItem) => WorkItemHadler.UpdateWorkItem(currentItem, Context);
        internal OutgoingJsonData AddNewWorkItem(IncomingWorkItem currentItem,int userId)
        {
            var result = WorkItemHadler.AddNewWorkItem(currentItem, Context, Configuration, userId);
            if(result)
                return new OutgoingJsonData{ Data = "true"};
            else
                return new OutgoingJsonData{ Data = "false"};
        }
        internal List<WorkItemRelations> GetProjectRelationships()
        {
            return Context.WorkItemRelations.ToList();
        }

          internal OutgoingBoundRelations GetAllWorkItemRelations(int workItemId, int projectId)
        {
            var workItem = Context.WorkItem.Include(x => x.AssociatedWrorkItemChildrenWorkItem)
                                           .ThenInclude(AssociatedWrorkItemChildrenWorkItem => AssociatedWrorkItemChildrenWorkItem.WorkItem)
                                           .Include(x => x.AssociatedWrorkItemChildrenWorkItem)
                                           .ThenInclude(AssociatedWrorkItemChildrenWorkItem => AssociatedWrorkItemChildrenWorkItem.RelationTypeNavigation)
                                           .FirstOrDefault(x => x.Id == workItemId);
            var relations = new List<BindingWorkItemRelation>();
            var bindingRelations = new List<BindingWorkItemRelation>();
            bindingRelations.AddRange(workItem.AssociatedWrorkItemChildrenWorkItemChild.Select(x => new BindingWorkItemRelation
            {
                WorkItem = new BindingWorkItemDTO
                {
                    Title = x.WorkItemChild.Title,
                    Id = x.WorkItemChild.Id
                },
                RelationType = x.RelationTypeNavigation.TypeName
            }).ToList());
            var parent = default(WorkItem);
            if (workItem.ParentId != null)
            {
                if (workItem.ParentId != 0)
                {
                    parent = Context.WorkItem.FirstOrDefault(x => x.Id == workItem.ParentId);
                    if(parent != null)
                    {
                        bindingRelations.Add(new BindingWorkItemRelation
                        {
                            WorkItem = new BindingWorkItemDTO
                            {
                                Title = parent.Title,
                                Id = parent.Id
                            },
                            RelationType = "Parent"
                        });
                    }
                }
            }
            var res = new StringBuilder();

            relations.AddRange(bindingRelations);
            res.AppendLine($"class {RemoveWhitespace(workItem.Title)}");
            res.AppendLine("{");
            res.AppendLine("}");
            relations.ForEach(x =>
            {
                res.AppendLine($"class {RemoveWhitespace(x.WorkItem.Title)}");
                res.AppendLine("{");
                res.AppendLine($" is {x.RelationType} of {workItem.Title}");
                res.AppendLine($" Open Work Item [[[https://localhost:5001/Dashboard/EditWorkItem?projectId={projectId}&&workItem={x.WorkItem.Id}]]]");
                res.AppendLine("}");
            });
            //    AssociatedBoardWorkItems "1" *-- "many" Boards
            relations.ForEach(x =>
            {
                res.AppendLine($" {RemoveWhitespace(x.WorkItem.Title)} \"1\" *--  \"{x.RelationType}\" {RemoveWhitespace(workItem.Title)} ");
            });
            return new OutgoingBoundRelations
            {
                WorkItems = relations,
                UmlData = res.ToString()
            };
        }

        internal void AssociatedRelation(IncomingWorkItemRelation incomingRelation)
        {
            var currentWorkItem = Context.WorkItem.FirstOrDefault(x => x.Id == incomingRelation.CurrWorkItemId);
            incomingRelation.LinkedItems.ForEach(x =>
            {
                var relId = int.Parse(x.RelationShipId);
                if (relId == 1)
                {
                    currentWorkItem.ParentId = x.WorkItemId;
                    Context.Attach(currentWorkItem);
                    Context.Update(currentWorkItem);
                    Context.SaveChanges();
                }
                else
                {
                    Context.AssociatedWrorkItemChildren.Add(new AssociatedWrorkItemChildren
                    {
                        WorkItemId = currentWorkItem.Id,
                        WorkItemChildId = x.WorkItemId,
                        RelationType = relId
                    });
                    Context.SaveChanges();
                }
            });

        }

        internal List<OutgoingBindingWorkItem> GetAllWorkItems(int projectId)
        {
    
            return Context.AssociatedBoardWorkItems
                        .Include(x => x.WorkItem)
                        .ThenInclude(WorkItem => WorkItem.WorkItemType)
                        .Include(x => x.WorkItem)
                        .ThenInclude(WorkItem => WorkItem.State)
                        .Where(x => x.ProjectId == projectId)
                        .Select(x => new OutgoingBindingWorkItem
                        {
                            Id = x.WorkItem.Id,
                            Title = x.WorkItem.Title,
                            ItemState = x.WorkItem.State.StateName,
                            ItemStateId = x.WorkItem.State.Id,
                            ItemType = x.WorkItem.WorkItemType.TypeName,
                            ItemTypeId = x.WorkItem.WorkItemType.Id
                        })
                        .ToList();

        }
        public string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }




        internal WorkItem AddChildrenToParent(int workItemId, int currWorkItemId)
        {
            var getWorkItem = Context.WorkItem.Include(x=>x.WorkItemType).FirstOrDefault(x=>x.Id == currWorkItemId);
            getWorkItem.ParentId = workItemId;
            Context.Attach(getWorkItem);
            Context.Update(getWorkItem);
            Context.SaveChanges();
            return getWorkItem;
        }
        internal WorkItem GetWorkItem(int workItem, int projectId)
        {
            return Context.AssociatedBoardWorkItems
                       .Include(x => x.WorkItem)
                       .ThenInclude(WorkItem => WorkItem.WorkItemType)
                       .Include(x => x.WorkItem)
                       .ThenInclude(WorkItem => WorkItem.AssociatedWrorkItemChildrenWorkItem)
                       .ThenInclude(AssociatedWrorkItemChildrenWorkItem => AssociatedWrorkItemChildrenWorkItem.WorkItemChild)
                       .Include(x => x.WorkItem)
                       .ThenInclude(WorkItem => WorkItem.AssignedAccountNavigation)
                       .FirstOrDefault(x => x.ProjectId == projectId && x.WorkItemId == workItem).WorkItem;
        }
       
        internal WorkItem GetWorkItemClean(int workItemId, int projectId)
        {
            var getWorkItem = Context.AssociatedBoardWorkItems.Include(x => x.WorkItem).FirstOrDefault(x => x.WorkItemId == workItemId && x.ProjectId == projectId);
            if (getWorkItem != null)
            {
                getWorkItem.WorkItem.AssociatedWrorkItemChildrenWorkItem = null;
                getWorkItem.WorkItem.AssociatedWrorkItemChildrenWorkItemChild = null;
                getWorkItem.WorkItem.AssociatedBoardWorkItems = null;
                getWorkItem.WorkItem.AssignedAccountNavigation = null;

                return getWorkItem.WorkItem;
            }


            return null;
        }

        internal List<WorkItemIterations> GetProjectIterations(int projectId)
        {
            return Context.AssociatedProjectIterations.Include(x => x.Iteration).Where(x => x.ProjectId == projectId)
            .Select(x => x.Iteration).ToList();
        }

        internal OutboundBackupModel BackUpSpecificProject(int projectId)
        {
            var result = string.Empty;
            var project = Context.Projects.FirstOrDefault(x=>x.Id == projectId);
            var Iterations = new List<WorkItemIterations>();
            var members = new List<UserAccounts>();
            var boards = new List<Boards>();
            var workItems = new List<WorkItem>();
            project.CreationDate = project.CreationDate == null ? DateTime.Now : project.CreationDate;
            result += $"{project.ProjectName};";
            result += $"{project.ProjectTitle};";
            result += $"{project.RepositoryId};";
            result += $"{project.ProjectDescription};";
            result += $"{project.CreationDate.Value.Year}|{project.CreationDate.Value.Month}|{project.CreationDate.Value.Day}";
            result += ",";
            var getProjectIterations = Context.AssociatedProjectIterations.Where(x=>x.ProjectId == projectId).ToList();
            getProjectIterations.ForEach(x=>{
                var iteration = Context.WorkItemIterations.FirstOrDefault(y=>y.Id == x.IterationId);
                result += $"{iteration.IterationName};";
                Iterations.Add(iteration);
            });
            result += ",";
            var projectMembers = Context.AssociatedProjectMembers.Where(x=>x.ProjectId == projectId).ToList();
            projectMembers.ForEach(x=>{
                var member = Context.UserAccounts.FirstOrDefault(y=>y.Id == x.UserAccountId);
                result += $"{member.Email}|{member.FirstName}|{member.LastName}|{member.Password}|{member.Salt}|{member.Id};";
                members.Add(member);
            });
            result += ",";
            Context.AssociatedProjectMemberRights.Include(x=>x.Rights).Where(x => x.ProjectId == projectId)
            .ToList().ForEach(x=>{
                var userMail = Context.UserAccounts.FirstOrDefault(y=>y.Id == x.UserAccountId);
                result += $"{x.ProjectId}|{x.RightsId}|{userMail.Email}|{x.Rights.ManageIterations}|{x.Rights.ManageUserdays}|{x.Rights.UpdateUserRights}|{x.Rights.ViewOtherPeoplesWork}|{x.Rights.WorkItemRule}|{x.Rights.ChatChannelsRule};";
            });
            result += ",";
            var associatedProjectBoards = Context.AssociatedProjectBoards.Where(x=>x.ProjectId == projectId).ToList();
            associatedProjectBoards.ForEach(x=>{
                var board =Context.Boards.FirstOrDefault(y=>y.Id == x.BoardId);
                result += $"{board.RepositoryId}|{board.BoardName}|{board.BoardType}|{board.Id};";
                boards.Add(board);
            });

            result += ",";
            var assocaitedProjectWorkItems = Context.AssociatedBoardWorkItems.Where(x=>x.ProjectId == projectId).ToList();
            assocaitedProjectWorkItems.ForEach(x=>{
                var workItem = Context.WorkItem.FirstOrDefault(y=>y.Id == x.WorkItemId);
                if(workItem.ParentId == 0)
                    result += $"{x.Board.BoardType}|{workItem.IntegratedInBuild}|{workItem.Iteration}|{workItem.ItemPriority}|{workItem.OriginEstitame}|{workItem.ParentId}|{workItem.PriorityId}|{workItem.ReasonId}|{workItem.RelationId}|{workItem.RepoSteps}|{workItem.ResolvedReason}|{workItem.RiskId}|{workItem.Severity}|{workItem.StackRank}|{workItem.StartDate.ToString()}|{workItem.StateId}|{workItem.StoryPoints}|{workItem.SystemInfo}|{workItem.TimeCapacity}|{workItem.Title}|{workItem.ValueAreaId}|{workItem.WorkItemTypeId}|{workItem.AreaId};";
                else
                {
                    var parent = Context.WorkItem.FirstOrDefault(y=>y.Id == workItem.ParentId);
                    result += $"{x.Board.BoardType}|{workItem.IntegratedInBuild}|{workItem.Iteration}|{workItem.ItemPriority}|{workItem.OriginEstitame}|{parent.Title}|{workItem.PriorityId}|{workItem.ReasonId}|{workItem.RelationId}|{workItem.RepoSteps}|{workItem.ResolvedReason}|{workItem.RiskId}|{workItem.Severity}|{workItem.StackRank}|{workItem.StartDate.ToString()}|{workItem.StateId}|{workItem.StoryPoints}|{workItem.SystemInfo}|{workItem.TimeCapacity}|{workItem.Title}|{workItem.ValueAreaId}|{workItem.WorkItemTypeId}|{workItem.AreaId};";
                }
                workItems.Add(workItem);
            });
            result += ",";
            Context.AssociatedWrorkItemChildren.Include(x=>x.WorkItemChild)
                                                .Include(x=>x.WorkItem)
                                                .Include(x=>x.RelationTypeNavigation)
                .Where(x=>x.WorkItem.AssociatedBoardWorkItems.Any(y=>y.ProjectId == projectId))
                .ToList()
                .ForEach(x=>{
                    result += $"{x.WorkItem.Title}|{x.WorkItemChild.Title}|{x.RelationType};";
                });

            var outgoingResult = new OutboundBackupModel{
                Iterations = Iterations,
                Boards  = boards,
                CurrentProject = project,
                UserAccounts = members,
                WorkItems = workItems,
                Serialized = result
            };
            return outgoingResult;
        }
        internal string GetWorkItemName(int workItemType)
        {
            return Context.WorkItemTypes.FirstOrDefault(x => x.Id == workItemType).TypeName;
        }


        internal bool ImportExistingProject(string data)
        {
            var projectDta = data.Split(',');
            var projectDataHolder = projectDta[0].Split(';');
            var dataOfProjectData =  projectDataHolder[4].Split('|');
            var project = Context.Projects.FirstOrDefault(x=>x.ProjectName == projectDataHolder[0]);
            if(project == null)
            {
                 project = Context.Projects.Add(new Projects{
                    ProjectName = projectDataHolder[0],
                    ProjectTitle = projectDataHolder[1],
                    ProjectDescription = projectDataHolder[2],
                    CreationDate = new DateTime(int.Parse(dataOfProjectData[0]),int.Parse(dataOfProjectData[1]),int.Parse(dataOfProjectData[2])),
                    RepositoryId = 5013
                }).Entity;
                Context.SaveChanges();
            }
            
            var iterationsDataHolder = projectDta[1];
            var iterationsData = iterationsDataHolder.Split(';');
            iterationsData.ToList().ForEach(x=> {
                var iteration = Context.WorkItemIterations.Add(new WorkItemIterations{
                    IterationName = x,
                });
                Context.SaveChanges();
                Context.AssociatedProjectIterations.Add(new AssociatedProjectIterations{
                    IterationId = iteration.Entity.Id,
                    ProjectId = project.Id
                });
                Context.SaveChanges();
            });
            var projectMemberData = projectDta[2].Split(';');
            var memebrs = new List<UserAccounts>();
            projectMemberData.ToList().ForEach(x=>{
                if(x != "")
                {
                    var accData = x.Split('|');

                    var checkExistingAccount = Context.UserAccounts.FirstOrDefault(y=>y.Email == accData[0]);
                    if(checkExistingAccount == null)
                    {
                        checkExistingAccount = Context.UserAccounts.Add(new UserAccounts{
                            Email = accData[0],
                            FirstName = accData[1],
                            LastName = accData[2],
                            Password = accData[3],
                            Salt = accData[4]
                        }).Entity;

                        Context.SaveChanges();
                    }
                    memebrs.Add(checkExistingAccount);
                    Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers{
                        ProjectId = project.Id,
                        UserAccountId = checkExistingAccount.Id,
                        RepositoryId = project.RepositoryId
                    });
                    Context.SaveChanges();
                }
            });
            var memberRightsData =  projectDta[3].Split(';');
            memberRightsData.ToList().ForEach(x=>
            {
                if(x != "")
                {
                    var currentRigh = x.Split('|');
                    var checkExistingMemberRight = Context.UserRights.FirstOrDefault(y=>y.ManageIterations == short.Parse(currentRigh[3]) 
                    && y.ManageUserdays == short.Parse(currentRigh[4]) 
                    && y.UpdateUserRights == short.Parse(currentRigh[5]) 
                    && y.ViewOtherPeoplesWork == short.Parse(currentRigh[6]) 
                    && y.WorkItemRule == short.Parse(currentRigh[7]) 
                    && y.ChatChannelsRule ==short.Parse(currentRigh[8]));
                    if(checkExistingMemberRight == null)
                    {
                        checkExistingMemberRight = Context.UserRights.Add(new UserRights{
                            ManageIterations = short.Parse(currentRigh[3]), 
                            ManageUserdays = short.Parse(currentRigh[4]),
                            UpdateUserRights = short.Parse(currentRigh[5]), 
                            ViewOtherPeoplesWork = short.Parse(currentRigh[6]), 
                            WorkItemRule = short.Parse(currentRigh[7]),
                            ChatChannelsRule = short.Parse(currentRigh[8])
                        }).Entity;
                        Context.SaveChanges();
                    }
                    var member = Context.UserAccounts.FirstOrDefault(x=>x.Email == currentRigh[2]);
                    Context.AssociatedProjectMemberRights.Add(new AssociatedProjectMemberRights{
                            ProjectId = project.Id,
                            RightsId = checkExistingMemberRight.Id,
                            UserAccountId = member.Id
                    });
                    Context.SaveChanges();
                }
            });
            var boardsDataHolder = projectDta[4].Split(';');
            var boards = new List<Boards>();
            boardsDataHolder.ToList().ForEach(x=>{
                if(x != "")
                {
                    var boardData = x.Split('|');
                    var cBoard = Context.Boards.Add(new Boards{
                        RepositoryId = project.RepositoryId,
                        BoardName = boardData[1],
                        BoardType = int.Parse(boardData[2]),
                    });
                    Context.SaveChanges();
                    Context.AssociatedProjectBoards.Add(new AssociatedProjectBoards{
                        BoardId = cBoard.Entity.Id,
                        Position = cBoard.Entity.BoardType,
                        ProjectId = project.Id,
                    });
                    Context.SaveChanges();
                    boards.Add(cBoard.Entity);
                }
            });
            var workItemData = projectDta[5].Split(';');
            workItemData.ToList().ForEach(x=>{
                if(x != "")
                {
                    var itemData = x.Split('|');
                    var currentWItem = new WorkItem();
                    if(itemData[5] == "0")
                    {
                        var parent = Context.AssociatedBoardWorkItems.Include(y=>y.WorkItem).FirstOrDefault(y=>y.WorkItem.Title == itemData[5]);
                        if(!string.IsNullOrEmpty(itemData[1]) )
                            currentWItem.IntegratedInBuild = int.Parse(itemData[1]);
                        if(!string.IsNullOrEmpty(itemData[2]) )
                            currentWItem.Iteration =  int.Parse(itemData[2]);
                        if(!string.IsNullOrEmpty(itemData[3]))
                        currentWItem.ItemPriority = int.Parse(itemData[3]);
                        currentWItem.OriginEstitame = itemData[4];
                        currentWItem.ParentId = parent.Id;
                        if(!string.IsNullOrEmpty(itemData[6]))
                            currentWItem.PriorityId =  int.Parse(itemData[6]);
                        if(!string.IsNullOrEmpty(itemData[7]) )
                        currentWItem.ReasonId =  int.Parse(itemData[7]);
                        if(!string.IsNullOrEmpty(itemData[8]))
                            currentWItem.RelationId =  int.Parse(itemData[8]);
                        currentWItem.RepoSteps = itemData[9];
                        currentWItem.ResolvedReason = itemData[10];
                        if(!string.IsNullOrEmpty(itemData[11]))
                            currentWItem.RiskId =   int.Parse(itemData[11]);
                        if(!string.IsNullOrEmpty(itemData[12]))
                            currentWItem.Severity = int.Parse(itemData[12]);
                        currentWItem.StackRank = itemData[13];
                        currentWItem.StartDate = DateTime.Parse(itemData[14]);
                        if(!string.IsNullOrEmpty(itemData[15]))
                            currentWItem.StateId = int.Parse(itemData[15]);
                        currentWItem.StoryPoints = itemData[16];
                        currentWItem.SystemInfo = itemData[17];
                        currentWItem.TimeCapacity = itemData[18];
                        currentWItem.Title = itemData[19];
                        if(!string.IsNullOrEmpty(itemData[20]))
                            currentWItem.ValueAreaId = int.Parse(itemData[20]);
                        if(!string.IsNullOrEmpty(itemData[21]))
                            currentWItem.WorkItemTypeId =  int.Parse(itemData[21]);
                        if(!string.IsNullOrEmpty(itemData[22]))
                            currentWItem.AreaId = int.Parse(itemData[22]);
                        
                        currentWItem = Context.WorkItem.Add(currentWItem).Entity;
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(itemData[1]) )
                            currentWItem.IntegratedInBuild = int.Parse(itemData[1]);
                        if(!string.IsNullOrEmpty(itemData[2]) )
                            currentWItem.Iteration =  int.Parse(itemData[2]);
                        if(!string.IsNullOrEmpty(itemData[3]))
                        currentWItem.ItemPriority = int.Parse(itemData[3]);
                        currentWItem.OriginEstitame = itemData[4];
                        if(!string.IsNullOrEmpty(itemData[6]))
                            currentWItem.PriorityId =  int.Parse(itemData[6]);
                        if(!string.IsNullOrEmpty(itemData[7]) )
                        currentWItem.ReasonId =  int.Parse(itemData[7]);
                        if(!string.IsNullOrEmpty(itemData[8]))
                            currentWItem.RelationId =  int.Parse(itemData[8]);
                        currentWItem.RepoSteps = itemData[9];
                        currentWItem.ResolvedReason = itemData[10];
                        if(!string.IsNullOrEmpty(itemData[11]))
                            currentWItem.RiskId =   int.Parse(itemData[11]);
                        if(!string.IsNullOrEmpty(itemData[12]))
                            currentWItem.Severity = int.Parse(itemData[12]);
                        currentWItem.StackRank = itemData[13];
                        currentWItem.StartDate = DateTime.Parse(itemData[14]);
                        if(!string.IsNullOrEmpty(itemData[15]))
                            currentWItem.StateId = int.Parse(itemData[15]);
                        currentWItem.StoryPoints = itemData[16];
                        currentWItem.SystemInfo = itemData[17];
                        currentWItem.TimeCapacity = itemData[18];
                        currentWItem.Title = itemData[19];
                        if(!string.IsNullOrEmpty(itemData[20]))
                            currentWItem.ValueAreaId = int.Parse(itemData[20]);
                        if(!string.IsNullOrEmpty(itemData[21]))
                            currentWItem.WorkItemTypeId =  int.Parse(itemData[21]);
                        if(!string.IsNullOrEmpty(itemData[22]))
                            currentWItem.AreaId = int.Parse(itemData[22]);
                        
                        currentWItem = Context.WorkItem.Add(currentWItem).Entity;

                    }
                    Context.SaveChanges();
                    Context.AssociatedBoardWorkItems.Add(new AssociatedBoardWorkItems{
                        ProjectId = project.Id,
                        WorkItemId = currentWItem.Id,
                        BoardId = boards.FirstOrDefault(x=>x.BoardType == int.Parse(itemData[0])).Id ,
                        
                    });
                    Context.SaveChanges();
                }
            });
            var workitemAssociations = projectDta[6].Split(';');
            workitemAssociations.ToList().ForEach(x=>{
                if(x  != "")
                {
                    var currentAssociation = x.Split('|');
                    var WId = Context.AssociatedBoardWorkItems.Include(y=>y.WorkItem).FirstOrDefault(y=> y.WorkItem.Title == currentAssociation[0]);
                    var child = Context.AssociatedBoardWorkItems.Include(y=>y.WorkItem).FirstOrDefault(y=> y.WorkItem.Title == currentAssociation[1]);
                    var cAssociation =  new AssociatedWrorkItemChildren();
                    cAssociation.WorkItemId = WId.Id;
                    cAssociation.WorkItemChildId = child.Id;

                    var checkassociation = Context.AssociatedWrorkItemChildren.FirstOrDefault(y=>y.WorkItemId == WId.Id && y.WorkItemChildId == child.Id);
                    if(checkassociation == null)
                    {
                        if(!string.IsNullOrEmpty(currentAssociation[2]))
                            cAssociation.RelationType = int.Parse(currentAssociation[2]);
                        Context.AssociatedWrorkItemChildren.Add(cAssociation);
                        Context.SaveChanges();
                    }
                }
            });
            return true;
        }


        internal List<WorkItemPriorities> GetProjectPriorities(int projectId)
        {
            return Context.WorkItemPriorities
            .ToList();
        }

        internal bool ValidateWorkItemConnection(IncomingWorkItemRelation incomingRequest)
        {
            var result = Context.WorkItem.FirstOrDefault(x => x.PriorityId == incomingRequest.ProjectId && x.Id == incomingRequest.CurrWorkItemId);
            return result == null ? true : false;
        }

        internal List<WorkItemActivity> GetProjectActivities(int projectId)
        {
            return Context.WorkItemActivity
            .ToList();
        }

        internal List<Builds> GetProjectBuilds(int projectId)
        {
            return Context.AssociatedProjectBuilds
            .Where(x => x.ProjectId == projectId)
            .Select(x => x.Build)
            .ToList();
        }
        
        internal List<Risks> GetProjectRisks(int projectId)
        {
            return Context.Risks.ToList();
        }

        internal List<ValueAreas> GetProjectValueAreas(int projectId)
        {
            return Context.ValueAreas.ToList();
        }
        internal List<WorkItemSeverities> GetProjectSeverities(int projectId)
        {
            return Context.WorkItemSeverities
            .ToList();
        }
        internal List<WorkItemAreas> GetProjectAreas(int projectId)
        {
            return Context.WorkItemAreas
            .ToList();
        }
        internal List<WorkItemReasons> GetProjectReasons(int projectId)
        {
            return Context.WorkItemReasons
            .ToList();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue1)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue1 = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~WorkItemsContext()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}