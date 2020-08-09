using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Rokono_Control.Models;

namespace Platform.DataHandlers
{
    public class SurveyContext : IDisposable
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        private bool disposedValue;
        private bool disposedValue1;

        public SurveyContext(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }


        internal List<SurveyComponent> GetSurveyComponents()
        {
            return Context.SurveyComponent.ToList();
        }

        internal async Task<List<Surveys>> GetProjectSurveys(int projectId)
        {
            return await Task.Run(() => Context.Surveys.Where(x=>x.ProjectId == projectId).ToList());
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
        // ~SurveyContext()
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