namespace Rokono_Control.DatabaseHandlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Platform.DataHandlers;
    using Platform.Models;
    using Rokono_Control.DataHandlers;
    using Rokono_Control.Models;
    using RokonoControl.DatabaseHandlers.WorkItemHandlers;
    using RokonoControl.Models;

    public class DatabaseController : IDisposable
    {
        RokonoControlContext Context;

        internal AssociatedProjectApiKeys GetProjectApiKey(int projectId, string keyName)
        {
            var projectKey = Context.AssociatedProjectApiKeys.Include(x => x.Key)
                                    .FirstOrDefault(x => x.ProjectId == projectId);
            if(projectKey == null)
                projectKey = GenerateProjectKey(keyName, projectId);
            
            return projectKey;
        }
 

        private AssociatedProjectApiKeys GenerateProjectKey(string keyName, int projectId)
        {
            var getKey = Context.ApiKeys.FirstOrDefault(x=> x.FeatureName == keyName);
            if(getKey == null)
                return null;

            var projectKey = Context.AssociatedProjectApiKeys.Add(new AssociatedProjectApiKeys{
                KeyId = getKey.Id,
                ProjectId = projectId,
                ApiSecret = $"{Guid.NewGuid()}{DateTime.Now.Ticks}"
            });
            Context.SaveChanges();
            projectKey.Entity.Key = getKey;
            return projectKey.Entity;
        }

        internal OutgoingProjectRightsDTO GetProjectOutboundFeatures(int projectId)
        {
            var project = Context.Projects.FirstOrDefault(x=>x.Id == projectId);
            if(project != null)
            return new OutgoingProjectRightsDTO{
                BugReportEnabled = project.AllowPublicBugs == 1 ? true :false,
                FeedbackEnabled = project.AllowPublicFeedback == 1 ? true : false,
                FeaturesEnabled = project.AllowPublicFeatures == 1 ? true : false,
                ChatEnabled = project.AllowPublicMessages == 1 ? true : false
            };
            
            return null;
        }

        internal void EnableProjectFeature(IncomignFeatureRequest request)
        {
            var project = Context.Projects.FirstOrDefault(x=>x.Id == request.ProjectId);
            switch(request.RuleName)
            {
                case "BugReport":
                    project.AllowPublicBugs = request.Value;
                break;
                case "FeatureRequest":
                    project.AllowPublicFeatures = request.Value;
                break;
                case "PublicMessage":
                    project.AllowPublicMessages = request.Value;
                break;
                case "FeedbackPage":
                    project.AllowPublicFeedback = request.Value;
                break;
            }
            Context.Attach(project);
            Context.Update(project);
            Context.SaveChanges();
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

        internal List<ApiKeys> GetProjectApiKeys(int projectId)
        {
            return Context.AssociatedProjectApiKeys.Include(x => x.Key)
                                                   .Where(x => x.ProjectId == projectId)
                                                   .Select(x => x.Key)
                                                   .ToList();
        }

        internal List<PublicMessages> GetAllPublicMessagesForProject(int id)
        {
            var result = Context.AssociatedProjectPublicDiscussions.Include(x => x.PublicMessage)
                                                             .Where(x => x.ProjectId == id)
                                                             .Select(x => x.PublicMessage)
                                                             .ToList();
            return result;
        }

        internal bool CheckProjectAuthorizedFeature(int autherizeReqiest, string featureRequest)
        {
            var result = default(bool);
            var getProject = Context.Projects.FirstOrDefault(x=>x.Id == autherizeReqiest);
            if(getProject == null)
                return result;
            switch(featureRequest)
            {
                case "BugReport":
                    result = getProject.AllowPublicBugs == 1 ? true: false;
                break;
                case "Discussions":
                    result = getProject.AllowPublicMessages == 1 ? true: false;
                break;
                case "Feedback":
                    result = getProject.AllowPublicFeedback == 1 ? true: false;
                break;
            }
            return result;
        }
        
        internal int GetProjectActiveRule(int projectId, string ruleName)
        {
            var result = default(int);
            var getProject = Context.Projects.FirstOrDefault(x=>x.Id == projectId);
            if(getProject == null)
                return result;
            switch(ruleName)
            {
                case "BugReport":
                    result = getProject.AllowPublicBugs == null ? 0 : getProject.AllowPublicBugs.Value;
                break;
                case "PublicMessage":
                    result = getProject.AllowPublicMessages == null ? 0 : getProject.AllowPublicMessages.Value;
                break;
                case "FeedbackPage":
                    result = getProject.AllowPublicFeedback == null ? 0 : getProject.AllowPublicFeedback.Value;
                break;
            }
            return result;
        }

        internal int CheckApiCallCredentials(IncomingApiAuthenicationRequest request)
        {
            var checkEnabledFeature = Context.AssociatedProjectApiKeys.Include(x => x.Key)
                                                                      .FirstOrDefault(x=>x.Key.SecretKey == request.PrivateSecret
                                                                                    && x.Key.FeatureName == request.FeatureRequest);
            if(checkEnabledFeature != null)
                return checkEnabledFeature.ProjectId.Value;
            return 0;
        }

        internal PublicMessages AddNewPublicMessage(IncomingPublicMessage newMessage)
        {
            var message = Context.PublicMessages.Add(new PublicMessages{
                DateOfMessage = DateTime.Now,
                SenderName = newMessage.SenderName,
                MessageContent = newMessage.MessageContent
            });
            Context.SaveChanges();
            var association = Context.AssociatedProjectPublicDiscussions.Add(new AssociatedProjectPublicDiscussions{
                ProjectId = newMessage.ProjectId,
                PublicMessageId = message.Entity.Id
            });
            Context.SaveChanges();
            return message.Entity;
        }

        internal PublicMessages AddNewFeedback(IncomingPublicMessage newMessage)
        {
            var message = Context.PublicMessages.Add(new PublicMessages{
                DateOfMessage = DateTime.Now,
                SenderName = newMessage.SenderName,
                MessageContent = newMessage.MessageContent
            });
            Context.SaveChanges();
            var association = Context.AssociatedProjectFeedback.Add(new AssociatedProjectFeedback{
                ProjectId = newMessage.ProjectId,
                MessageId = message.Entity.Id,
                Rating = 6
            });
            Context.SaveChanges();
            return message.Entity;
        }

        internal void AddNewBugReport(IncomingNewBugReport report, string title)
        {
            var currentItem = new WorkItem();
                currentItem.FoundInBuild = 0;
                // currentItem.ResolvedInBuild = 0;
            
            var databaseItem = new WorkItem();
            databaseItem.Title = title;
            databaseItem.WorkItemTypeId = 1;
            databaseItem.StateId = 1;
            databaseItem.Reason = 1;
            var projectActiveIteration = Context.AssociatedProjectIterations.Include(x=>x.Iteration).FirstOrDefault(x=>x.ProjectId == report.ProjectId);
            databaseItem.Iteration =  projectActiveIteration.Iteration.Id;
            databaseItem.AreaId = 1;
            databaseItem.PriorityId = 1;
            databaseItem.Activity = 1;
            databaseItem.RepoSteps = report.BugDescription;
            databaseItem.SystemInfo = "-";
            databaseItem.FoundInBuild = 0;
            databaseItem.IntegratedInBuild = 0;
            databaseItem.Compleated = "0";
            databaseItem.Remaining = "0";
            databaseItem.OriginEstitame = "0";
            databaseItem.Severity=1;
            databaseItem.IsPublic = 1;
//                databaseItem.ParentId = relationshipId;
                
            

            databaseItem.StartDate = DateTime.Now;
            databaseItem.EndDate = DateTime.Now.AddMonths(1);
            databaseItem.DueDate = DateTime.Now.AddMonths(1);
            var item = Context.WorkItem.Add(databaseItem);
            Context.SaveChanges();
            
           
            var pBoard = Context.AssociatedProjectBoards.Include(x=>x.Board).FirstOrDefault(x=> x.ProjectId == report.ProjectId && x.Board.BoardName == "Open");
            if(pBoard != null)
            {
                Context.AssociatedBoardWorkItems.Add(new AssociatedBoardWorkItems{
                    BoardId = pBoard.BoardId.Value,
                    WorkItemId = item.Entity.Id,
                    ProjectId = report.ProjectId
                });
                Context.SaveChanges();
            }
           
    
            var currentUser = "Unassigned";
            
            var notification = Context.Notifications.Add(new Notifications{
                DateOfMessage = DateTime.Now,
                Content = $"Work Item  Title:{item.Entity.Title} ID:{item.Entity.Id}- Has been created and assigned to {currentUser}, the item has been automatically generated by the bug reporting tool",
                NotificationType = 3,
            });
            Context.SaveChanges();
            var projectUsers = Context.AssociatedProjectMembers.Where(x=>x.ProjectId == report.ProjectId).ToList();

            //Todo: Thows duplicate error, database mapping should be reworked to allow cross project notification based on user view
            projectUsers.ForEach(x=>{
                Context.AssociatedProjectNotifications.Add(new AssociatedProjectNotifications{
                    NotificationId = notification.Entity.Id,
                    ProjectId = report.ProjectId,
                    NewNotification = 1,
                    UserAccountId = x.UserAccountId
                });
                Context.SaveChanges();
            });
            if(string.IsNullOrEmpty(report.ImagePath))
                return;
            var file = Context.SystemFiles.Add(new SystemFiles{
                FileType = 1,
                FileLocation = report.ImagePath,
                SenderName = report.SenderName,
                DateOfMessage = DateTime.Now
            });
            Context.SaveChanges();
            Context.AssociatedWorkItemFiles.Add(new AssociatedWorkItemFiles{
                FileId = file.Entity.Id,
                WorkItemId = databaseItem.Id
            });
            Context.SaveChanges();
        }

        IConfiguration Configuration;
        public DatabaseController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        internal List<UserNotes> GetUserNotes(int id, int projectId)
        {
            if(projectId == 0)
                return Context.AssociatedAccountNotes.Include(x => x.Note)
                                                     .Where(x => x.UserAccountId == id)
                                                     .Select(x => x.Note)
                                                     .ToList();
            else
                return Context.AssociatedAccountNotes.Include(x => x.Note)
                                                     .Where(x => x.UserAccountId == id && x.ProjectId == projectId)
                                                     .Select(x => x.Note)
                                                     .ToList();

        }

        internal List<Notifications> GetAllUserNotifications(int accountId, int projectId)
        {
            var projectNotifications = Context.AssociatedProjectNotifications.Include(x=>x.Notification).ThenInclude(Notification => Notification.NotificationTypeNavigation).Where(x=>x.UserAccountId == accountId && x.ProjectId == projectId && x.NewNotification == 1).ToList();
            var userNotifications = Context.AssociatedUserNotifications.Include(x=>x.Notification).ThenInclude(Notification => Notification.NotificationTypeNavigation).Where(x=>x.UserId == accountId && x.NewNotification == 1).ToList();
            var notifications = new List<Notifications>();
            notifications.AddRange(projectNotifications.Select(x=>x.Notification).ToList());
            notifications.AddRange(userNotifications.Select(x=>x.Notification).ToList());
            return notifications;
        }

        internal void NotificationRead(int id, int id1)
        {
            var notification = Context.AssociatedProjectNotifications.FirstOrDefault(x=>x.NotificationId == id && x.UserAccountId == id1);
            notification.NewNotification = 0;
            Context.Attach(notification);
            Context.Update(notification);
            Context.SaveChanges();
        }

        internal void AddNewUserNote(IncomingNoteRequest note, int id)
        {
            var currentNote = Context.UserNotes.Add(new UserNotes{
                Content = note.Content,
                DateOfMessage = DateTime.Now,
                NoteBackground = note.Background,
                NoteForeground = note.FontColor
            });
            Context.SaveChanges();
            Context.AssociatedAccountNotes.Add(new AssociatedAccountNotes{
                NoteId = currentNote.Entity.Id,
                ProjectId = note.ProjectId,
                UserAccountId = id
            });
            Context.SaveChanges();
        }

        internal void ChangeNotePosition(IncomingNoteRequest note)
        {
            var currentNote = Context.UserNotes.FirstOrDefault(x=>x.Id == note.NoteId);
            currentNote.TopPos = note.Top;
            currentNote.LeftPos = note.Left;
            Context.Attach(currentNote);
            Context.Update(currentNote);
            Context.SaveChanges();
        }

        internal void DeleteNote(IncomingNoteRequest note)
        {
            var currentNote = Context.UserNotes.FirstOrDefault(x=>x.Id == note.NoteId);
            Context.UserNotes.Remove(currentNote);
            var associatedNotes = Context.AssociatedAccountNotes.Where(x=>x.NoteId == note.NoteId).ToList();
            Context.AssociatedAccountNotes.RemoveRange(associatedNotes);
            Context.SaveChanges();
        }

        internal void EditNote(IncomingNoteRequest note)
        {
            var currentNote = Context.UserNotes.FirstOrDefault(x=>x.Id == note.NoteId);
            currentNote.NoteBackground = note.Background;
            currentNote.NoteForeground = note.FontColor;
            currentNote.Content = note.Content;
            Context.Attach(currentNote);
            Context.Update(currentNote);
            Context.SaveChanges();
        }

        public DatabaseController(int i, int internalId) 
        {
            this.I = i;
                this.InternalId = internalId;
               
        }
                private int I { get; set; }

        internal UserAccounts GetUserAccount(int id)
        {
            return Context.UserAccounts.FirstOrDefault(x=>x.Id == id);
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

        internal WorkItem GetWorkItemById(int parse)
        {
            return Context.WorkItem.FirstOrDefault(x=>x.Id == parse);
        }

        internal UserAccounts GetUserAccountByName(string name)
        {
            return Context.UserAccounts.FirstOrDefault(x=>x.Email == name);
        }

        internal void AddNewUserNotification(int v, WorkItem getWorKItemByName, int userId)
        {
            var notification = Context.Notifications.Add(new Notifications{
                NotificationType = v,
                Content = $"Work item -> {getWorKItemByName.Title} has been assigned to you",
                DateOfMessage = DateTime.Now,
                WorkItemRelationid = getWorKItemByName.Id
            });
            Context.SaveChanges();
            Context.AssociatedUserNotifications.Add(new AssociatedUserNotifications{
                NotificationId = notification.Entity.Id,
                UserId =  userId,
                NewNotification = 2
            });
            Context.SaveChanges();
        }

        internal WorkItem GetWorkItemByTitle(string title)
        {
            return Context.WorkItem.FirstOrDefault(x=>x.Title == title && x.WorkItemTypeId == 7);
        }

        internal void EditChangelog(ChangelogEditRequest changelog)
        {
            var current = Context.Changelogs.FirstOrDefault(x=>x.Id == changelog.Id);
            current.LogDetails = changelog.Content;
            Context.Attach(current);
            Context.Update(current);
            Context.SaveChanges();
        }

        internal Changelogs GetSpecificChangelog(int changelog)
        {
            return Context.Changelogs.FirstOrDefault(x=>x.Id == changelog);
        }

        internal List<Changelogs> GetProjectChangelogs(int projectId)
        {
            return Context.AssociatedProjectChangelogs.Include(x => x.Log)
                                                      .Where(x => x.ProjectId == projectId)
                                                      .Select(x => x.Log)
                                                      .ToList();
        }

        internal bool GetPublicBoardRights(int projectId)
        {
            var project = Context.Projects.FirstOrDefault(x=>x.Id == projectId);
            if(project != null)
            {
                return project.PublicBoard == 1 ? true :false;
            }
            else
                return false;
        }

        internal void AssociatedChangelogItems(IncomingGenerateChangelog changelog)
        {
            var currentChangelog = Context.Changelogs.Add(new Changelogs{  
                DayOfLog =  DateTime.Now.Day,
                LogDetails = changelog.Chagelog
            });
            Context.SaveChanges();
            Context.AssociatedProjectChangelogs.Add(new AssociatedProjectChangelogs{
                CurrentMonth = DateTime.Now.Month,
                ProjectId = changelog.ProjectId,
                LogYear = DateTime.Now.Year,
                LogId = currentChangelog.Entity.Id
            });
            Context.SaveChanges();
            changelog.Items.ForEach(x=>{
                Context.AssociatedWorkItemChangelogs.Add(new AssociatedWorkItemChangelogs{
                    LogId = currentChangelog.Entity.Id,
                    WorkitemId = x.Id,
                    ProjectId = changelog.ProjectId
                });
                Context.SaveChanges();
            });
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


        internal List<WorkItem> GetEmptyChangelogWorktItems(int projectId)
        {

            return Context.WorkItem.Include(x => x.AssociatedWorkItemChangelogs)
                                   .Include(x=>x.WorkItemType)
                                   .Where(x => !x.AssociatedWorkItemChangelogs.Any(y =>  y.ProjectId == projectId) && x.AssociatedBoardWorkItems.Any(x=>x.Board.BoardType == 4))
                                   .ToList();
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
                    //Context.Dispose();
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