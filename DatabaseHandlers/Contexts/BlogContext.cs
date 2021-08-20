using Microsoft.Extensions.Configuration;
using Rokono_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rokono_Control.DatabaseHandlers.Contexts
{
    public class BlogContext : IDisposable
    {
        RokonocontrolContext Context;
        IConfiguration Configuration;
        private bool disposedValue;
 
        public BlogContext(RokonocontrolContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
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
        // ~BlogContext()
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

        internal List<BlogPostCategories> GetBlogCategories(int projectId)
        {
            return Context.BlogPostCategories.Where(x => x.ProjectId == projectId).ToList();
        }
    }
}
