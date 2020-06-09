namespace Platform.DatabaseHandlers.Contexts
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Platform.Models;
    using Rokono_Control.Models;
    public class OutboundDetailsContext : IDisposable
    {
        
        RokonoControlContext Context;
        IConfiguration Configuration;
        private bool disposedValue;

        public OutboundDetailsContext(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
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


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~OutboundDetailsContext()
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