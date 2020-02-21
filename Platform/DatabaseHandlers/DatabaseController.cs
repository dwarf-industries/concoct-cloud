namespace Rokono_Control.DatabaseHandlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Platform.Models;
    using Rokono_Control.DataHandlers;
    using Rokono_Control.Models;
    using RokonoControl.DatabaseHandlers.WorkItemHandlers;
    using RokonoControl.Models;

    public class DatabaseController : IDisposable
    {
        RokonoControlContext Context;

        public DatabaseController(RokonoControlContext context)
        {
            Context = context;
        }
 

        private int I { get; set; }
        private int InternalId { get; set; }
        internal List<Repository> GetAllRepositories()
        {
            return Context.Repository.Include(x => x.Projects).ToList();
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
                                                .Where(x => x.ProjectId == projectId && x.Board.AssociatedBoardWorkItems.Any(z => z.WorkItem.WorkItemTypeId == workItemType)).ToList();

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
                        Summary = $"asd {y.WorkItem.Description}",
                        Title = y.WorkItem.Title,
                        Tags = $"{y.WorkItem.WorkItemType.TypeName}",
                        Priority = "High",
                        Type = $"{x.Board.BoardName}",
                        Status = x.Board.BoardName,
                        Assignee = y.WorkItem.WorkItemType.TypeName
                        // Children = related
                    });
                });
            });
            return Cards;
        }

        internal List<OutgoingUserAccounts> GetProjectUsers(int projectId)
        {
            return Context.AssociatedProjectMembers.Include(x=>x.UserAccount).Where(x=>x.ProjectId == projectId).Select(x=> new OutgoingUserAccounts{
                Name = x.UserAccount.Email,
                AccountId = x.UserAccount.Id
            }).ToList();
        }

        internal bool IsNotParent(int parentId)
        {
            return Context.WorkItem.Any(x=>x.ParentId == parentId);
        }

        internal void DeleteAccountFromProject(IncomingProjectAccount accounts)
        {
            var association = Context.AssociatedProjectMembers.FirstOrDefault(x=>x.ProjectId == accounts.ProjectId && x.UserAccountId == accounts.AccountId);
            Context.AssociatedProjectMembers.Remove(association);
            Context.SaveChanges();
            var memberRights = Context.AssociatedProjectMemberRights.Where(x=>x.ProjectId == accounts.ProjectId && x.UserAccountId == accounts.AccountId).ToList();
            Context.AssociatedProjectMemberRights.RemoveRange(memberRights);
            Context.SaveChanges();
            var WorkItemsAssigned = Context.WorkItem.Where(x=>x.AssignedAccount == accounts.AccountId).ToList();
            WorkItemsAssigned.ForEach(x=>{
                var workItem = x;
                workItem.AssignedAccount =null;
                Context.Update(workItem);
                Context.SaveChanges();
            });
        }

        internal void AssociatedProjectExistingMembers(IncomingExistingProjectMembers accounts)
        {
            var projectRepository = Context.Projects.FirstOrDefault(x=> x.Id == accounts.ProjectId).RepositoryId;
            accounts.Accounts.ForEach(y=>{

            
                Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers{
                    ProjectId = accounts.ProjectId,
                    RepositoryId = projectRepository,
                    UserAccountId = y.AccountId,
                });
                Context.SaveChanges();
                var commonRightsId = default(int);
                var getCommonRights  = Context.UserRights.FirstOrDefault(x=>x.ManageIterations == y.IterationOptions && 
                                                                            x.ChatChannelsRule == y.ChatChannels &&
                                                                            x.ManageUserdays == y.ScheduleManagement &&
                                                                            x.UpdateUserRights == y.EditUserRights &&
                                                                            x.ViewOtherPeoplesWork == y.ViewWorkItems &&
                                                                            x.WorkItemRule == y.WorkItemOption);
                if(getCommonRights != null)
                    commonRightsId = getCommonRights.Id;
                else
                {

                    commonRightsId = Context.UserRights.Add(new UserRights{
                        ChatChannelsRule = Convert.ToInt16(y.ChatChannels),
                        ManageIterations = Convert.ToInt16(y.IterationOptions),
                        ViewOtherPeoplesWork = Convert.ToInt16(y.ViewWorkItems),
                        ManageUserdays = Convert.ToInt16(y.ScheduleManagement),
                        UpdateUserRights = Convert.ToInt16(y.EditUserRights),
                        WorkItemRule = Convert.ToInt16(y.WorkItemOption)
                    }).Entity.Id;
                    Context.SaveChanges();
                }
                Context.AssociatedProjectMemberRights.Add(new AssociatedProjectMemberRights{
                    ProjectId = accounts.ProjectId,
                    UserAccountId = y.AccountId,
                    RightsId = commonRightsId
                });
                Context.SaveChanges();
            });
        }

        internal void AddProjectInvitation(IncomingProjectAccount projectAccount)
        {
            var accountId = AddUserAccount(new IncomingNewUserAccount{
                Email = projectAccount.email,
                Password = projectAccount.password,
                ProjectRights = false,
                FirstName = "Unassigned",
                LastName = "Unassigned"
            });
            var projectRepository = Context.Projects.FirstOrDefault(x=> x.Id == projectAccount.ProjectId).RepositoryId;
            Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers{
                ProjectId = projectAccount.ProjectId,
                RepositoryId = projectRepository,
                UserAccountId = accountId,
            });
            Context.SaveChanges();
            var commonRightsId = default(int);
            var getCommonRights  = Context.UserRights.FirstOrDefault(x=>x.ManageIterations == projectAccount.accountRights.IterationOptions && 
                                                                        x.ChatChannelsRule == projectAccount.accountRights.ChatChannels &&
                                                                        x.ManageUserdays == projectAccount.accountRights.ScheduleManagement &&
                                                                        x.UpdateUserRights == projectAccount.accountRights.EditUserRights &&
                                                                        x.ViewOtherPeoplesWork == projectAccount.accountRights.ViewWorkItems &&
                                                                        x.WorkItemRule == projectAccount.accountRights.WorkItemOption);
            if(getCommonRights != null)
                commonRightsId = getCommonRights.Id;
            else
            {

                commonRightsId = Context.UserRights.Add(new UserRights{
                     ChatChannelsRule = Convert.ToInt16(projectAccount.accountRights.ChatChannels),
                     ManageIterations = Convert.ToInt16(projectAccount.accountRights.IterationOptions),
                     ViewOtherPeoplesWork = Convert.ToInt16(projectAccount.accountRights.ViewWorkItems),
                     ManageUserdays = Convert.ToInt16(projectAccount.accountRights.ScheduleManagement),
                     UpdateUserRights = Convert.ToInt16(projectAccount.accountRights.EditUserRights),
                     WorkItemRule = Convert.ToInt16(projectAccount.accountRights.WorkItemOption)
                 }).Entity.Id;
                Context.SaveChanges();
            }
            Context.AssociatedProjectMemberRights.Add(new AssociatedProjectMemberRights{
                ProjectId = projectAccount.ProjectId,
                UserAccountId = accountId,
                RightsId = commonRightsId
            });
            Context.SaveChanges();
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

        internal UserRights GetUserRights(int id, int projectId)
        {
            return Context.AssociatedProjectMemberRights
                          .Include(x=>x.Rights)
                          .Where(x=>x.ProjectId == projectId && x.UserAccountId == id)
                          .ToList()
                          .LastOrDefault()
                          .Rights;
        }

        internal string GetWorkItemName(int workItemType)
        {
            return Context.WorkItemTypes.FirstOrDefault(x => x.Id == workItemType).TypeName;
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
                                                                     .Where(y => y.WorkItemId == x.Id && y.WorkItemChild.WorkItemTypeId == 3)
                                                                     .ToList();
                var whereParent = Context.WorkItem.Include(b=>b.AssignedAccountNavigation)
                                                  .Include(b=>b.WorkItemType)
                                                  .Where(b=>b.ParentId == x.Id).ToList();
                whereParent.ForEach(sprintTasks => {
                    var taskBoard = Context.AssociatedBoardWorkItems.Include(z => z.Board)
                                                                    .FirstOrDefault(z => z.WorkItemId == sprintTasks.Id);

                    var activeBoard = string.Empty;
                    if (taskBoard == null)
                        activeBoard = "Open";
                    else
                        activeBoard = taskBoard.Board.BoardName;
                    Cards.Add(new BindingCards
                    {
                        InnerId = sprintTasks.Id,
                        Id = $"Task {sprintTasks.Id}",
                        Summary = $"asd {sprintTasks.Description}",
                        Title = sprintTasks.Title,
                        Tags = $"{sprintTasks.WorkItemType.TypeName}",
                        Priority = GetCardType(sprintTasks.WorkItemType.TypeName),
                        Type = $"{activeBoard}",
                        Status = activeBoard,
                        AssigneeId = sprintTasks.AssignedAccountNavigation != null ? sprintTasks.AssignedAccountNavigation.Id : 0,
                        Assignee = x.Title,
                        AssgignedAccount = sprintTasks.AssignedAccountNavigation != null ? sprintTasks.AssignedAccountNavigation.GitUsername : "Unassigned"
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
                        Summary = $"asd {task.WorkItemChild.Description}",
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

        internal int CheckUserViewWorkitemRights(int userId, int projectId)
        {
            return Context.AssociatedProjectMemberRights.Include(x => x.Rights)
                                                        .FirstOrDefault(x => x.UserAccountId == userId
                                                        && x.ProjectId == projectId)
                                                        .Rights.ViewOtherPeoplesWork == 1 ? 1 : 0;
        }

        internal UserAccounts GetUserAccounts(int userId)
        {
            var result = Context.AssociatedProjectMembers.Include(x => x.UserAccount).FirstOrDefault(x => x.UserAccount.Id == userId);
            return result != null ? result.UserAccount : null;
        }

        internal List<UserAccounts> GetProjectPerons(int projectId)
        {
            return Context.AssociatedProjectMembers.Include(x => x.UserAccount).Where(x => x.ProjectId
            == projectId).Select(x => x.UserAccount).ToList();
        }

        private string GetCardType(string board)
        {
            var res = string.Empty;
            switch (board)
            {
                case "Epic":
                    res = "Normal";
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
                default:
                    res = "Task";
                    break;
            }
            return res;
        }

        internal int GetCreatedWorkItemCount(int id) => Context.AssociatedBoardWorkItems.Where(x => x.ProjectId == id).Count();
        internal int GetWorkItemCountByType(int id, int boardType) => Context.AssociatedBoardWorkItems.Include(x => x.Board).Where(x => x.ProjectId == id && x.Board.BoardType == boardType).Count();


        internal int GetProjectDefautIteration(int id)
        {
            return Context.AssociatedProjectIterations.FirstOrDefault(x => x.ProjectId == id).IterationId;
        }

        internal object GetProjectName(int projectId)
        {
            return Context.Projects.FirstOrDefault(x => x.Id == projectId).ProjectName;
        }

        internal UserAccounts GetDefaultAccount()
        {
            return Context.UserAccounts.FirstOrDefault(x => x.Id == 1);
        }

        internal object GetAllWorkItemTypes() => Context.WorkItemTypes.ToList();

        internal object GetNewNotifications(object value)
        {
            throw new NotImplementedException();
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

        internal List<Branches> GetProjectBranches(int projectId)
        {
            var branches = Context.Projects.Include(x => x.Repository)
                                   .ThenInclude(Repository => Repository.AssociatedRepositoryBranches)
                                   .ThenInclude(AssociatedRepositoryBranches => AssociatedRepositoryBranches.Branch)
                                   .FirstOrDefault(x => x.Id == projectId);
            if (branches != null)
                return branches.Repository.AssociatedRepositoryBranches.Select(y => y.Branch).ToList();
            else
                return null;

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

        public string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        internal OutgoingSourceFile GetSelectedFileByName(string fileName, int projectId, int branchId)
        {
            var project = Context.Projects.Include(x => x.Repository).FirstOrDefault(x => x.Id == projectId);
            var branch = Context.Branches.FirstOrDefault(x => x.Id == branchId);
            var fileLanauge = GitRepositoryManager.GetFileLanaguage(fileName);
            var branchCommits = Execute($"{Program.Configuration.ShellScripts.FirstOrDefault(x => x.Name == "GetGitList.sh").Path}", $"{project.Repository.FolderPath} {branch.BranchName}");
            var fileData = ExecuteSingle($"{Program.Configuration.ShellScripts.FirstOrDefault(x => x.Name == "GetCommitFile.sh").Path}", $"{project.Repository.FolderPath} {branchCommits.FirstOrDefault()}:{fileName}");
            return new OutgoingSourceFile { Data = fileData, LanguageType = fileLanauge };
        }

        internal OutgoingUserAccounts GetOutgoingUserAccount(int v)
        {
            var account = Context.UserAccounts.FirstOrDefault(x => x.Id == v);
            return new OutgoingUserAccounts
            {
                AccountId = account.Id,
                ChatChannels = 1,
                EditUserRights = 1,
                IterationOptions = 1,
                ScheduleManagement = 1,
                ViewWorkItems = 1,
                WorkItemOption = 1
            };
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

        internal List<string> GetBranchFiles(int projectId, int branchId)
        {
            var result = new List<string>();
            var project = Context.Projects.FirstOrDefault(x => x.Id == projectId);
            if (project == null)
                return null;
            var getBranch = Context.AssociatedRepositoryBranches.Include(x => x.Branch)
                                                                .Include(x => x.Repository)
                                                                .FirstOrDefault(x => x.RepositoryId == project.RepositoryId && x.Branch.Id == branchId);
            if (getBranch != null)
            {
                var name = getBranch.Repository.FolderPath;
                // var fileName = file.FilePath.Split('/').LastOrDefault();
                //var fileLanauge = GitRepositoryManager.GetFileLanaguage(fileName);
                result = Execute($"{Program.Configuration.ShellScripts.FirstOrDefault(x => x.Name == "LsFiles.sh").Path}", $"{name}");

            }
            return result;
        }



        internal List<CommitFileHirarhicalData> GetFileHirarchy(List<string> files)
        {
            var folders = new List<CommitFileHirarhicalData>();
            var data = new List<CommitFileHirarhicalData>();
            var count = 1;
            InternalId = 1;
            files.ForEach(x =>
            {

                // folders.Add(GenerateDirectory(x,folders.Count));
                var item = GenerateDirectory(x, $"{count++}");
                if (item != null)
                    folders.Add(item);
                count++;
            });
            data.AddRange(folders);
            return data;
        }
        public CommitFileHirarhicalData GenerateDirectory(string path, string count)
        {
            if (Directory.Exists(path))
            {
                var dFiles = Directory.EnumerateFiles(path);
                var item = new CommitFileHirarhicalData
                {
                    Name = path.Split("/").LastOrDefault(),
                    FullPathName = path,
                    InternalId = InternalId,
                    Id = $"{count + 1}",
                    SubChild = new List<CommitFileHirarhicalData>()
                };
                item.SubChild = GenerateSubDirectory(item, dFiles.ToList(), $"{count}-{I++}", path);
                InternalId++;

                var directories = Directory.GetDirectories(path);
                directories.ToList().ForEach(e =>
                {
                    item.SubChild.Add(GenerateDirectory(e, $"{count}-{item.SubChild.Count + 1}"));
                });

                return item;
            }
            else
            {
                var item = new CommitFileHirarhicalData
                {
                    Name = path,
                    InternalId = InternalId,

                    FullPathName = path,
                    Id = $"{count + 1}"
                };
                InternalId++;

                return item;
            }

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

        public List<CommitFileHirarhicalData> GenerateSubDirectory(CommitFileHirarhicalData item, List<string> files, string parent, string directory)
        {
            InternalId++;

            files.ToList().ForEach(z =>
            {
                item.SubChild.Add(new CommitFileHirarhicalData
                {
                    Name = z.Split("/").LastOrDefault(),
                    FullPathName = z,
                    Id = parent,
                    InternalId = InternalId,

                    SubChild = new List<CommitFileHirarhicalData>()
                });
            });

            return item.SubChild;
        }



        internal List<Branches> GetBranchesForProject(int projectId)
        {
            return Context.AssociatedRepositoryBranches
                        .Include(x => x.Branch)
                        .Include(x => x.Repository)
                        .ThenInclude(Repository => Repository.Projects)
                        .Where(x => x.Repository.Projects.Any(y => y.Id == projectId))
                        .Select(x => new Branches
                        {
                            BranchName = x.Branch.BranchName,
                            Id = x.Branch.Id,
                        }).ToList();

        }

        internal UserAccounts LoginUser(IncomingLoginRequest request)
        {
            var account = Context.UserAccounts
                                 .FirstOrDefault(x => x.Email == request.Email);
            if (account == null)
                return null;
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: request.Password,
              salt: Convert.FromBase64String(account.Salt),
              prf: KeyDerivationPrf.HMACSHA1,
              iterationCount: 10000,
              numBytesRequested: 256 / 8));



            if (hashed == account.Password)
                return account;
            else
                return null;
        }

        internal List<CommitFileHirarhicalData> GetCommitFilesHirarchy(string commitId)
        {
            var folders = new List<CommitFileHirarhicalData>();
            var data = new List<CommitFileHirarhicalData>();
            var files = Context.AssociatedCommitFiles
                    .Include(x => x.File)
                    .Include(x => x.Commit)
                    .Where(x => x.Commit.CommitKey == commitId)
                    .ToList();

            files.ForEach(x =>
            {
                var ifSingle = x.File.FilePath.Split('/').ToList();
                if (ifSingle.Count() <= 1)
                    data.Add(new CommitFileHirarhicalData
                    {
                        Id = $"{data.Count + 1}",
                        Name = x.File.FilePath,
                        ItemId = x.File.Id

                    });

            });
            var count = data.Count + 1;
            files.ForEach(x =>
            {
                var ifSingle = x.File.FilePath.Split('/').ToList();
                var file = ifSingle.LastOrDefault();

                if (ifSingle.Count() > 1)
                {
                    ifSingle.Remove(file);
                    var folder = string.Empty;
                    ifSingle.ForEach(cFolder =>
                    {
                        folder += $"{cFolder}/";
                    });
                    if (folders.FirstOrDefault(cFolders => cFolders.Name == folder) == null)
                        folders.Add(new CommitFileHirarhicalData
                        {
                            Id = $"{count++}",
                            Name = folder,
                            SubChild = new List<CommitFileHirarhicalData>(),
                            ItemId = x.File.Id

                        });
                    else
                    {
                        if (folders.FirstOrDefault(z => z.Name == folder).SubChild == null)
                            folders.FirstOrDefault(z => z.Name == folder).SubChild = new List<CommitFileHirarhicalData>();
                        folders.FirstOrDefault(z => z.Name == folder).SubChild.Add(new CommitFileHirarhicalData
                        {
                            Id = $"{count}-{folders.FirstOrDefault(z => z.Name == folder).SubChild.Count + 1}",
                            Name = file,
                            ItemId = x.File.Id
                        });
                    }
                }
            });
            data.AddRange(folders);
            return data;
        }

        internal OutgoingSourceFile GetSelectedFileById(int fileId, int branch)
        {
            var file = Context.Files
                            .Include(x => x.AssociatedCommitFiles)
                            .ThenInclude(AssociatedCommitFiles => AssociatedCommitFiles.Commit)
                            .FirstOrDefault(x => x.Id == fileId);
            var commitKey = file.AssociatedCommitFiles.FirstOrDefault().Commit.CommitKey;
            var repository = Context.Repository
                                    .Include(x => x.AssociatedRepositoryBranches)
                                    .ThenInclude(AssociatedRepositoryBranches => AssociatedRepositoryBranches.Branch)
                                    .ThenInclude(Branch => Branch.AssociatedBranchCommits)
                                    .ThenInclude(AssociatedBranchCommits => AssociatedBranchCommits.Commit)
                                    .FirstOrDefault(x =>
                                    x.AssociatedRepositoryBranches
                                    .Any(z =>
                                    z.Branch.AssociatedBranchCommits
                                    .Any(y =>
                                    y.Commit.CommitKey == commitKey)));
            var fileName = file.FilePath.Split('/').LastOrDefault();
            var fileLanauge = GitRepositoryManager.GetFileLanaguage(fileName);
            var fileData = ExecuteSingle($"{Program.Configuration.ShellScripts.FirstOrDefault(x => x.Name == "GetCommitFile.sh").Path}", $"{repository.FolderPath} {commitKey}:{file.FilePath}");
            return new OutgoingSourceFile { Data = fileData, LanguageType = fileLanauge };
        }

        internal List<OutgoingCommitData> GetCommitData(int projectId, int branch)
        {
            return Context.Commits
                        .Include(x => x.AssociatedBranchCommits)
                        .ThenInclude(AssociatedBranchCommits => AssociatedBranchCommits.Branch)
                        .ThenInclude(Branch => Branch.AssociatedRepositoryBranches)
                        .ThenInclude(AssociatedRepositoryBranches => AssociatedRepositoryBranches.Repository)
                        .ThenInclude(Repository => Repository.Projects)
                        .Where(x =>
                        x.AssociatedBranchCommits
                        .Any(y =>
                        y.BranchId == branch
                        && y.Branch.AssociatedRepositoryBranches
                        .Any(z =>
                            z.Repository.Projects
                            .Any(d =>
                                d.Id == projectId)))).Select(commit => new OutgoingCommitData
                                {
                                    Author = commit.CommitedBy,
                                    CommitKey = commit.CommitKey,
                                    Date = commit.DateOfCommit.Value,
                                    PullRequest = "",
                                    Message = commit.CommitData,

                                }).ToList();

        }

        internal List<OutgoingCommitData> GetCommitDataMaster(int projectId)
        {
            return Context.Commits
                        .Include(x => x.AssociatedBranchCommits)
                        .ThenInclude(AssociatedBranchCommits => AssociatedBranchCommits.Branch)
                        .ThenInclude(Branch => Branch.AssociatedRepositoryBranches)
                        .ThenInclude(AssociatedRepositoryBranches => AssociatedRepositoryBranches.Repository)
                        .ThenInclude(Repository => Repository.Projects)
                        .Where(x =>
                        x.AssociatedBranchCommits
                        .Any(y =>
                        y.Branch.BranchName == "Master"
                        && y.Branch.AssociatedRepositoryBranches
                        .Any(z =>
                            z.Repository.Projects
                            .Any(d =>
                                d.Id == projectId)))).Select(commit => new OutgoingCommitData
                                {
                                    Author = commit.CommitedBy,
                                    CommitKey = commit.CommitKey,
                                    Date = commit.DateOfCommit.Value,
                                    PullRequest = "",
                                    Message = commit.CommitData,

                                }).ToList();

        }

        internal Branches CreateBrach(string b, int repoId, int projectId)
        {
            var branch = Context.Branches.Add(new Branches
            {
                BranchName = b,
                ProjectId = projectId,
            });
            Context.SaveChanges();
            Context.AssociatedRepositoryBranches.Add(new AssociatedRepositoryBranches
            {
                RepositoryId = repoId,
                BranchId = branch.Entity.Id
            });
            Context.SaveChanges();
            return branch.Entity;

        }

        internal List<string> GetBranches(int repoId)
        {
            return Context.AssociatedRepositoryBranches
                          .Include(x => x.Branch)
                          .Where(x => x.RepositoryId == repoId)
                          .Select(x => x.Branch.BranchName)
                          .ToList();
        }

        internal bool CheckIfBranchCommitExists(string commitId, int branchId)
        {
            return Context.Commits
                          .Include(x => x.AssociatedBranchCommits)
                          .Where(x => x.AssociatedBranchCommits.Any(y => y.BranchId == branchId))
                          .Any(x => x.CommitKey == commitId);
        }

        internal List<Repository> GetRepositories()
        {
            return Context.Repository.ToList();
        }

        internal List<OutgoingUserAccounts> GetUserAccounts()
        {
            return Context.UserAccounts.Select(x => new OutgoingUserAccounts
            {
                Name = $"{x.FirstName.FirstOrDefault()} {x.LastName}",
                AccountId = x.Id
            }).ToList();
        }

        internal void AssociatedCommitsWithBranch(List<Files> commitFiles, int id, string commitKey, string commitedBy)
        {
            var commit = default(Commits);
            var commiteExists = Context.Commits.Any(x => x.CommitKey == commitKey);
            if (!commiteExists)
            {
                commit = Context.Commits.Add(new Commits
                {
                    CommitedBy = commitedBy,
                    CommitKey = commitKey,
                    DateOfCommit = DateTime.Now,
                    CommitData = ""
                }).Entity;
                Context.SaveChanges();
            }
            else
                commit = Context.Commits.FirstOrDefault(x => x.CommitKey == commitKey);

            var branchCommitAssociation = default(AssociatedBranchCommits);
            var commitAssociationExist = Context.AssociatedBranchCommits.Include(x => x.Commit).FirstOrDefault(x => x.Commit.CommitKey == commitKey);
            if (commitAssociationExist != null)
                branchCommitAssociation = commitAssociationExist;
            else
            {
                branchCommitAssociation = Context.AssociatedBranchCommits.Add(new AssociatedBranchCommits
                {
                    BranchId = id,
                    CommitId = commit.Id,
                }).Entity;
                Context.SaveChanges();
            }
            commitFiles.ForEach(x =>
            {
                var file = x;
                if (!Context.AssociatedCommitFiles.Include(y => y.File)
                                                .Include(y => y.Commit)
                                                .Any(y => y.Commit.CommitKey == commitKey && file.FilePath == y.File.FilePath))
                {
                    var fileExist = Context.Files.FirstOrDefault(y => y.DateOfFile == file.DateOfFile
                                                && y.CurrentName == file.CurrentName
                                                && y.FilePath == file.FilePath);

                    var cFile = default(Files);
                    if (fileExist != null)
                        cFile = fileExist;
                    else
                        cFile = Context.Files.Add(new Files
                        {
                            CurrentName = file.CurrentName,
                            FilePath = file.FilePath,
                            FileData = "",
                            DateOfFile = file.DateOfFile,

                        }).Entity;
                    Context.AssociatedCommitFiles.Add(new AssociatedCommitFiles
                    {
                        CommitId = commit.Id,
                        FileId = cFile.Id,
                        DateOfCommit = DateTime.Now
                    });
                    Context.SaveChanges();
                }
            });

        }
        internal void AssociatedCommitFilesWithExistingBranch(List<Files> commitFiles, int id, string commitKey, string commitedBy)
        {

            var newCommit = default(bool);
            var commit = default(Commits);
            if (!Context.Commits.Any(x => x.CommitKey == commitKey))
            {
                newCommit = true;
                commit = Context.Commits.Add(new Commits
                {
                    CommitKey = commitKey,
                    CommitedBy = commitedBy,
                    CommitData = "",
                    DateOfCommit = DateTime.Now
                }).Entity;
                Context.SaveChanges();
            }
            commitFiles.ForEach(x =>
            {
                if (commit != null)
                {
                    var file = Context.Files.Add(x);
                    Context.SaveChanges();

                    var associatedCommitFile = Context.AssociatedCommitFiles.Add(new AssociatedCommitFiles
                    {
                        CommitId = commit.Id,
                        FileId = file.Entity.Id,
                        DateOfCommit = DateTime.Now
                    });
                    Context.SaveChanges();
                }
            });

            if (newCommit)
            {
                Context.AssociatedBranchCommits.Add(new AssociatedBranchCommits
                {
                    CommitId = commit.Id,
                    BranchId = id,
                });
                Context.SaveChanges();
            }
        }

        internal Branches GetBranch(string b, int id)
        {
            return Context.Branches.FirstOrDefault(x => x.BranchName == b && x.ProjectId == id);
        }

        internal List<OutgoingAccountManagment> GetOutgoingManagmentAccounts(int projectId)
        {
            var accounts = Context.UserAccounts.Include(x => x.AssociatedProjectMembers)
                                        .ThenInclude(AssociatedProjectsMembers => AssociatedProjectsMembers.Project)
                                        .Where(x=>x.AssociatedProjectMembers.Any(y=>y.ProjectId == projectId))
                                        .ToList();
            return accounts.Select(x => new OutgoingAccountManagment
            {
                AccountId = x.Id,
                Name = $"{x.FirstName} {x.LastName}",
                Type = x.ProjectRights == 1 ? "Regular" : "Administrator",
                Email = x.Email,
                CreationDate = x.CreationDate,
                Projects = GetJsonData(x.AssociatedProjectMembers.Select(y => y.Project).ToList())
            }).ToList();
        }


        internal List<AssociatedBoardWorkItems> GetProjectWorkItems(int id, int parentType)
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
                                       .Where(x => x.ProjectId == id)
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
                                                       .Where(x => x.ProjectId == id && x.WorkItem.WorkItemTypeId == parentType)
                                                       .ToList();
            }
             return items;
        }

        internal object GetProjects()
        {
            return Context.Projects.Select(x => new
            {
                Name = x.ProjectName,
                Id = x.Id
            }).ToList();
        }

        internal OutgoingProjectRules GetProjectRules(int id, int userId)
        {
            var projectDetails = Context.AssociatedProjectMembers.FirstOrDefault(x => x.ProjectId == id && x.UserAccountId == userId);
            return new OutgoingProjectRules
            {
                //CanClone = projectDetails.CanClone == 1 ? true : false,
                //CanView  =projectDetails.CanViewWork == 1 ? true : false,
                //CanCommit =projectDetails.CanCommit == 1 ? true : false,
                //CanCreateWork = projectDetails.CanCreateWork == 1 ? true : false,
                //CanDeleteWork =projectDetails.CanDeleteWork == 1 ? true : false,
            };
        }

        internal List<CommitChartBindingData> GetCommitsChartForProject(int id)
        {
            return Context.AssociatedBranchCommits.Where(x => x.Branch.ProjectId == id).Select(x => new CommitChartBindingData
            {
                DateOfCommit = x.Commit.DateOfCommit.Value,
                DayCount = Context.Commits.Where(y => y.DateOfCommit == x.Commit.DateOfCommit).Count(),
            }).ToList();
        }

        internal Projects GetProjectData(int id)
        {

            return Context.Projects.Include(x => x.Branches)
                                   .ThenInclude(Brances => Brances.AssociatedBranchCommits)
                                   .ThenInclude(AssociatedBranchCommits => AssociatedBranchCommits.Commit)
                                   .FirstOrDefault(x => x.Id == id);

        }

        internal void UpdateUserAccount(IncomingUserAccountUpdate userData)
        {
            var userAccount = Context.UserAccounts.FirstOrDefault(x => x.Id == userData.Id);
            Context.Attach(userAccount);
            userAccount.Email = userData.Email;
            if (!string.IsNullOrEmpty(userData.Password))
            {

                 // generate a 128-bit salt using a secure PRNG
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

                // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: userData.Password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                userAccount.Salt = Convert.ToBase64String(salt);
                userAccount.Password = hashed;
                Context.Entry(userAccount).Property("Password").IsModified = true;
                Context.Entry(userAccount).Property("Salt").IsModified = true;


            }
            userAccount.FirstName = userData.FirstName;
            userAccount.LastName = userData.LastName;
            Context.Update(userAccount);
            Context.SaveChanges();
            var rights = default(UserRights);
            var getCommonRight = Context.UserRights.FirstOrDefault(x=> x.ManageIterations == userData.Rights.IterationOptions &&
                                                                    x.ManageUserdays == userData.Rights.ScheduleManagement &&
                                                                    x.UpdateUserRights == userData.Rights.EditUserRights &&
                                                                    x.ViewOtherPeoplesWork == userData.Rights.ViewWorkItems &&
                                                                    x.WorkItemRule == userData.Rights.WorkItemOption &&
                                                                    x.ChatChannelsRule == userData.Rights.ChatChannels);
            if(getCommonRight != null)
            {
                rights = getCommonRight;
            }
            else
            {
                var newRights = Context.UserRights.Add(new UserRights{
                    ManageIterations= Convert.ToInt16(userData.Rights.IterationOptions),
                    ManageUserdays = Convert.ToInt16(userData.Rights.ScheduleManagement),
                    ChatChannelsRule = Convert.ToInt16(userData.Rights.ChatChannels),
                    UpdateUserRights = Convert.ToInt16(userData.Rights.EditUserRights),
                    ViewOtherPeoplesWork = Convert.ToInt16(userData.Rights.ViewWorkItems),
                    WorkItemRule = Convert.ToInt16(userData.Rights.WorkItemOption)  
                });
                rights = newRights.Entity;
                Context.SaveChanges();
            }

            Context.AssociatedProjectMemberRights.Add(new AssociatedProjectMemberRights{
                ProjectId  = userData.ProjectId,
                RightsId = rights.Id,
                UserAccountId = userAccount.Id
            });
            Context.SaveChanges();
        }

        
        internal void RemoveUserFromProject(IncomingRemoveUserFromProject userProject)
        {
            var userAccount = Context.AssociatedProjectMembers.FirstOrDefault(x => x.ProjectId == userProject.ProjectId && x.UserAccountId == userProject.UserId);
            Context.Attach(userAccount);
            Context.Remove(userAccount);
            Context.SaveChanges();
        }
         
        internal int AddUserAccount(IncomingNewUserAccount user)
        {
            var account = Context.UserAccounts.Add(new UserAccounts
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProjectRights = user.ProjectRights ? 1 : 0,
                CreationDate = DateTime.Now
            });
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            account.Entity.Salt = Convert.ToBase64String(salt);
            account.Entity.Password = hashed;
            Context.SaveChanges();
            return account.Entity.Id;
        }

        internal OutgoingAccountManagment GetSpecificUserEdit(int id)
        {
            var account = Context.UserAccounts.Include(x => x.AssociatedProjectMembers)
                                        .ThenInclude(AssociatedProjectsMembers => AssociatedProjectsMembers.Project)
                                       .FirstOrDefault(x => x.Id == id);

            return new OutgoingAccountManagment
            {
                AccountId = account.Id,
                Name = $"{account.FirstName} {account.LastName}",
                Type = account.ProjectRights == 1 ? "Regular" : "Administrator",
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                CreationDate = account.CreationDate,
                ProjectRights = account.ProjectRights == 1 ? true : false,
                Projects = $@"{GetJsonData(account.AssociatedProjectMembers.Select(y => y.Project).ToList())}"
            };

        }

        internal void AddProjectToUser(IncomingProjectUser incomingRequest)
        {
            var project = Context.Projects.FirstOrDefault(x => x.Id == incomingRequest.Id);
            Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers
            {
                ProjectId = incomingRequest.Id,
                UserAccountId = incomingRequest.UserId,
                RepositoryId = project.RepositoryId
            });
            Context.SaveChanges();
        }

        private string GetJsonData(Object currnet)
        {
            return JsonConvert.SerializeObject(currnet, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        internal List<BindingUserAccount> GetProjectMembers(int projectId)
        {
            return Context.UserAccounts
            .Include(x => x.AssociatedProjectMembers)
            .Where(x => x.AssociatedProjectMembers.Any(y => y.ProjectId == projectId)).Select(x => new BindingUserAccount
            {
                Email = x.Email,
                AliasName = $"{x.FirstName.FirstOrDefault()}.{x.LastName.FirstOrDefault()}",
                Id = x.Id
            })
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

        internal bool UpdateWorkItem(IncomingWorkItem currentItem) => WorkItemHadler.UpdateWorkItem(currentItem, Context);

        internal bool AddNewProject(IncomingProject currentProject, int v)
        {
            if (Context.UserAccounts.FirstOrDefault(x => x.Id == v).ProjectRights == 1)
            {

                var userAccounts = new List<UserAccounts>();

                var repoStatus = true; //RepositoryManager.AddNewProject($"/home/GitRepositories/",currentProject.ProjectName, userAccounts);
                if (repoStatus)
                {
                    var repository = Context.Repository.Add(new Repository
                    {
                        FolderPath = $"/home/GitRepositories/{currentProject.ProjectName}"
                    });
                    Context.SaveChanges();

                    var boardBacklog = Context.Boards.Add(new Boards
                    {
                        RepositoryId = repository.Entity.Id,
                        BoardType = 1,
                        BoardName = "Open"
                    });
                    var boardActive = Context.Boards.Add(new Boards
                    {
                        RepositoryId = repository.Entity.Id,
                        BoardType = 2,
                        BoardName = "InProgress"
                    });
                    var boardTesting = Context.Boards.Add(new Boards
                    {
                        RepositoryId = repository.Entity.Id,
                        BoardType = 3,
                        BoardName = "Testing"
                    });
                    var boardDone = Context.Boards.Add(new Boards
                    {
                        RepositoryId = repository.Entity.Id,
                        BoardType = 4,
                        BoardName = "Done"
                    });


                    Context.SaveChanges();
                    var project = Context.Projects.Add(new Projects
                    {
                        ProjectDescription = currentProject.ProjectDescription,
                        ProjectName = currentProject.ProjectName,
                        ProjectTitle = "",
                        RepositoryId = repository.Entity.Id,
                        BoardId = boardBacklog.Entity.Id
                    });
                    Context.SaveChanges();

                    currentProject.Iterations.ForEach(x =>
                    {
                        var iteration = x;
                        var currentIteration = Context.WorkItemIterations.Add(iteration);
                        Context.SaveChanges();
                        Context.AssociatedProjectIterations.Add(new AssociatedProjectIterations
                        {
                            ProjectId = project.Entity.Id,
                            IterationId = currentIteration.Entity.Id
                        });
                        Context.SaveChanges();
                    });
                    currentProject.Users.ForEach(x =>
                    {
                        userAccounts.Add(Context.UserAccounts.FirstOrDefault(y => y.Id == x.AccountId));
                        Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers
                        {
                            ProjectId = project.Entity.Id,
                            UserAccountId = x.AccountId,
                            RepositoryId = repository.Entity.Id
                        });
                        Context.SaveChanges();
                        var rights = Context.UserRights.FirstOrDefault(y => y.ManageIterations == x.IterationOptions &&
                                                                        y.ManageUserdays == x.ScheduleManagement &&
                                                                        y.UpdateUserRights == x.EditUserRights &&
                                                                        y.ViewOtherPeoplesWork == x.ViewWorkItems &&
                                                                        y.ChatChannelsRule == x.ChatChannels);
                        if (rights == null)
                        {
                            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<UserRights> entityEntry = Context.UserRights.Add(new UserRights
                            {
                                WorkItemRule = Convert.ToInt16(x.WorkItemOption),
                                ManageIterations = Convert.ToInt16(x.IterationOptions),
                                ManageUserdays = Convert.ToInt16(x.ScheduleManagement),
                                UpdateUserRights = Convert.ToInt16(x.EditUserRights),
                                ViewOtherPeoplesWork = Convert.ToInt16(x.ViewWorkItems),
                                ChatChannelsRule = Convert.ToInt16(x.ChatChannels)
                            });
                            Context.SaveChanges();
                            rights = entityEntry.Entity;
                        }

                        Context.AssociatedProjectMemberRights.Add(new AssociatedProjectMemberRights
                        {
                            ProjectId = project.Entity.Id,
                            UserAccountId = x.AccountId,
                            RightsId = rights.Id

                        });
                        Context.SaveChanges();
                    });
                    Context.AssociatedProjectBoards.Add(new AssociatedProjectBoards
                    {
                        ProjectId = project.Entity.Id,
                        BoardId = boardBacklog.Entity.Id,
                        Position = 1
                    });
                    Context.AssociatedProjectBoards.Add(new AssociatedProjectBoards
                    {
                        ProjectId = project.Entity.Id,
                        BoardId = boardActive.Entity.Id,
                        Position = 2
                    });
                    Context.AssociatedProjectBoards.Add(new AssociatedProjectBoards
                    {
                        ProjectId = project.Entity.Id,
                        BoardId = boardTesting.Entity.Id,
                        Position = 3
                    });
                    Context.AssociatedProjectBoards.Add(new AssociatedProjectBoards
                    {
                        ProjectId = project.Entity.Id,
                        BoardId = boardDone.Entity.Id,
                        Position = 4
                    });
                    Context.SaveChanges();
                    currentProject.Users.ForEach(x =>
                    {
                        Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers
                        {
                            ProjectId = project.Entity.Id,
                            RepositoryId = repository.Entity.Id,
                            UserAccountId = x.AccountId,

                        });
                        Context.SaveChanges();
                    });


                }

            }
            return true;
        }
        internal bool AddNewWorkItem(IncomingWorkItem currentItem)
        {
            return WorkItemHadler.AddNewWorkItem(currentItem, Context);

        }
        internal List<WorkItemRelations> GetProjectRelationships()
        {
            return Context.WorkItemRelations.ToList();
        }

        internal List<WorkItemActivity> GetProjectActivities(int projectId)
        {
            return Context.WorkItemActivity
            .ToList();
        }

        internal List<WorkItemSeverities> GetProjectSeverities(int projectId)
        {
            return Context.WorkItemSeverities
            .ToList();
        }

        internal List<WorkItemIterations> GetProjectIterations(int projectId)
        {
            return Context.AssociatedProjectIterations.Include(x => x.Iteration).Where(x => x.ProjectId == projectId)
            .Select(x => x.Iteration).ToList();
        }

        internal bool ValidateWorkItemConnection(IncomingWorkItemRelation incomingRequest)
        {
            var result = Context.WorkItem.FirstOrDefault(x => x.PriorityId == incomingRequest.ProjectId && x.Id == incomingRequest.CurrWorkItemId);
            return result == null ? true : false;
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

        internal List<WorkItemPriorities> GetProjectPriorities(int projectId)
        {
            return Context.WorkItemPriorities
            .ToList();
        }

        internal List<Builds> GetProjectBuilds(int projectId)
        {
            return Context.AssociatedProjectBuilds
            .Where(x => x.ProjectId == projectId)
            .Select(x => x.Build)
            .ToList();
        }

        internal string GetUsername(int currentId)
        {
            var account = Context.UserAccounts.FirstOrDefault(x => x.Id == currentId);
            return $"{account.FirstName} {account.LastName}";
        }

        internal List<Projects> GetUserProjects(int id)
        {
            return Context.Projects.Include(x => x.AssociatedProjectMembers)
                                    .ThenInclude(AssociatedProjectMembers => AssociatedProjectMembers.UserAccount)
                                    .Where(x => x.AssociatedProjectMembers.Any(y => y.UserAccountId == id)).ToList();
        }
        public List<string> Execute(string shPath, string repoPath)
        {
            System.Console.WriteLine(shPath);
            System.Console.WriteLine(repoPath);
            var current = OS.GetCurrent();
            System.Console.WriteLine(current);
            if (current == "gnu")
            {
                try
                {
                    var cmdResult = RepositoryManager.ExecuteCmd("/bin/bash", $"{shPath} {repoPath}");
                    var data = cmdResult.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    System.Console.WriteLine(cmdResult);
                    return data;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    //  return false;
                }
            }
            return null;
        }
        public string ExecuteSingle(string shPath, string repoPath)
        {
            System.Console.WriteLine(shPath);
            System.Console.WriteLine(repoPath);
            var current = OS.GetCurrent();
            System.Console.WriteLine(current);
            if (current == "gnu")
            {
                try
                {
                    return RepositoryManager.ExecuteCmd("/bin/bash", $"{shPath} {repoPath}");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    //  return false;
                }
            }
            return string.Empty;
        }

        internal Repository GetRepositoryByName(string rName)
        {
            var projectExist = Context.Projects.Include(x => x.Repository).FirstOrDefault(x => x.ProjectName == rName);
            if (projectExist != null)
                return projectExist.Repository;

            return null;
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }


        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DatabaseController()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion

    }
}