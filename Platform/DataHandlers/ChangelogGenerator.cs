using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Platform.Models;
using Rokono_Control.Models;

namespace Platform.DataHandlers
{
    public class ChangelogGenerator :IDisposable
    {
        RokonoControlContext Context;
        public ChangelogGenerator(RokonoControlContext context)
        {
            Context = context;
        }

        internal string GenerateChangelog(IncomingGenerateChangelog changelogData)
        {
            var result = $"<h1>Changelog {DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Date}";
            var bugsData = "<h2>Resolved Bugs</h2><ul>";
            var featuresData = $"<h2>Added Features</h2><ul>";
            var improvements = $"<h2>Improvements</h2><ul>";
            var newTests = $"<h2>New unit tests</h2><ul>";
            var releasedModues = $"<h2>Released Modules<h2><ul>";
            var breakingChanges = $"<h2>Breaking Changes<h2><ul>";
            var reslvedStories = $"<h2>Resolved Stories<h2><ul>";
            changelogData.Items.ForEach(x=>{
                var getItem = Context.WorkItem.Include(y=>y.WorkItemType).FirstOrDefault(y=>y.Id == x.Id);
                switch(getItem.WorkItemTypeId)
                {
                    case 1:
                        bugsData += $"<l1>{x.Name}</li>";
                    break;
                    case 2:
                        featuresData += $"<li>{x.Name}</li>";
                    break;
                    case 3:
                        improvements += $"<li>{x.Name}</li>";
                    break;
                    case 4:
                        newTests += $"<li>{x.Name}</li>";
                    break;
                    case 5:
                        releasedModues = $"<li>{x.Name}</li>";
                    break;
                    case 6:
                        breakingChanges = $"<li>{x.Name}</li>";
                    break;  
                    case 7: 
                        reslvedStories = $"<li>{x.Name}</li>";
                    break;
                }
            });
            bugsData += "</ul>";
            featuresData += "</ul>";
            improvements += "</ul";
            newTests += "</ul>";
            releasedModues += "</ul>";
            breakingChanges += "</ul>";
            reslvedStories += "</ul>";
            result += releasedModues;
            result += featuresData;
            result += reslvedStories;
            result += improvements;
            result += newTests;
            result += breakingChanges;
            return result;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ChangelogGenerator()
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