using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rokono_Control.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rokono_Control.DatabaseHandlers.Contexts
{
    public class RepositoriesContext :IDisposable
    {
        RokonoControlContext Context;
        RepositoryManager RepositoryManager;
        IConfiguration Configuration;
        string OS;
        
        private bool disposedValue2;

        public RepositoriesContext(RokonoControlContext context, IConfiguration config, RepositoryManager repositoryManager)
        {
            Context = context;
            Configuration = config;
            RepositoryManager = repositoryManager;
            OS = Program.ServerOS;
        }

        public List<Branches> GetProjectBranches(int projectId)
        {
            return Context.Branches.Where(x => x.ProjectId == projectId).ToList();
        }

        public List<Branches> GetBranchesGit(int projectId)
        {
            var result = new List<Branches>();
            var project = Context.Projects.Include(x => x.Repository).FirstOrDefault(x => x.Id == projectId);
            var index = 0;
            var getCommits = RepositoryManager.CommandOutput(OS, "git branch -r", Path.Combine(Program.Configuration.LocalRepo, project.Repository.FolderPath));
            foreach (var line in getCommits.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                
                if(!line.Contains("HEAD"))
                {
                    result.Add(new Branches
                    {
                        BranchName = line.Substring(9),
                        Id = index++
                    });
                }
            }
            return result;
        }

        public List<OutgoingCommitTemp> GetCommitsForProject(int projectId, string branch)
        {
            var result = new List<OutgoingCommitTemp>();
            branch = $"origin/{branch}";
            result = Program.ProjectBranches.FirstOrDefault(x => x.ProjectId == projectId && x.BranchName == branch).Commits;
            return result;
        }

        private string GetBranchName(string serilzied)
        {
            //"branch: (HEAD -> master, origin/master, origin/HEAD)"
            var split = serilzied.Split(":");
            var result = string.Empty;
            if(split.Length > 1 && serilzied != "branch:")
            {
                var temp = split[1].Trim(new Char[] { ' ', '(', ')' });
                result = temp;
            }
            else
            {
                result = "origin/master";
            }

            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue2)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue2 = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RepositoriesContext()
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
