namespace Platform.DatabaseHandlers.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.Models;
    public class NotificationContext : IDisposable
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        private bool disposedValue;

        public NotificationContext(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }
        internal List<Notifications> GetAllUserNotifications(int accountId, int projectId)
        {
            var projectNotifications = Context.AssociatedProjectNotifications.Include(x => x.Notification)
                                                                             .ThenInclude(Notification => Notification.NotificationTypeNavigation)
                                                                             .Where(x => x.UserAccountId == accountId && x.ProjectId == projectId && x.NewNotification == 1)
                                                                             .ToList();
            var userNotifications = Context.AssociatedUserNotifications.Include(x => x.Notification)
                                                                       .ThenInclude(Notification => Notification.NotificationTypeNavigation)
                                                                       .Where(x => x.UserId == accountId && x.NewNotification == 1)
                                                                       .ToList();
            var notifications = new List<Notifications>();
            notifications.AddRange(projectNotifications.Select(x=>x.Notification).ToList());
            notifications.AddRange(userNotifications.Select(x=>x.Notification).ToList());
            return notifications;
        }
        internal object GetNewNotifications(object value)
        {
            throw new NotImplementedException();
        }
        internal void NotificationRead(int id, int id1)
        {
            var notification = Context.AssociatedProjectNotifications.FirstOrDefault(x=>x.NotificationId == id && x.UserAccountId == id1);
            if(notification != null)
            {
                notification.NewNotification = 0;
                Context.Attach(notification);
                Context.Update(notification);
                Context.SaveChanges();
            }
            var personalMessage =  Context.AssociatedUserNotifications.FirstOrDefault(x => x.NotificationId == id && x.UserId == id1);
            if(personalMessage != null)
            {
                personalMessage.NewNotification = 0;
                Context.Attach(personalMessage);
                Context.Update(personalMessage);
                Context.SaveChanges();
            }
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
        // ~NotificationContext()
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