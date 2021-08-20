namespace Platform.DatabaseHandlers.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Platform.Models;
    using Rokono_Control.Models;
    public class ApiKeysContext : IDisposable
    {
        RokonocontrolContext Context;
        IConfiguration Configuration;
        private bool disposedValue;
        private bool disposedValue1;

        public ApiKeysContext(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }
        internal List<ApiKeys> GetProjectApiKeys(int projectId)
        {
            return Context.AssociatedProjectApiKeys.Include(x => x.Key)
                                                   .Where(x => x.ProjectId == projectId)
                                                   .Select(x => x.Key)
                                                   .ToList();
        }

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


        internal List<PublicMessages> GetAllFeedback(int id)
        {
            var feedback = Context.AssociatedProjectFeedback.Include(x => x.Message)
                                                            .Where(x => x.ProjectId == id)
                                                            .Select(x => x.Message)
                                                            .ToList();
            return feedback;
        }

        internal int CheckApiCallCredentials(IncomingApiAuthenicationRequest request)
        {
            var checkEnabledFeature = Context.AssociatedProjectApiKeys.Include(x => x.Key)
                                                                      .FirstOrDefault(x=>x.ApiSecret == request.PrivateSecret
                                                                                    && x.Key.FeatureName == request.FeatureRequest);
            if(checkEnabledFeature != null)
                return checkEnabledFeature.ProjectId.Value;
            return 0;
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
        // ~ApiKeysContext()
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