namespace Platform.DatabaseHandlers.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Platform.Models;
    using Rokono_Control.Models;
    public class ChangelogContext : IDisposable
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        private bool disposedValue;

        public ChangelogContext(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }
        internal List<Changelogs> GetProjectChangelogs(int projectId)
        {
            return Context.AssociatedProjectChangelogs.Include(x => x.Log)
                                                      .Where(x => x.ProjectId == projectId)
                                                      .Select(x => x.Log)
                                                      .ToList();
        }

        internal List<WorkItem> GetEmptyChangelogWorktItems(int projectId)
        {

            return Context.WorkItem.Include(x => x.AssociatedWorkItemChangelogs)
                                   .Include(x=>x.WorkItemType)
                                   .Where(x => !x.AssociatedWorkItemChangelogs.Any(y =>  y.ProjectId == projectId) && x.AssociatedBoardWorkItems.Any(x=>x.Board.BoardType == 4))
                                   .ToList();
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
        // ~ChangelogContext()
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