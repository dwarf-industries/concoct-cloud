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
        RokonocontrolContext Context;
        RepositoryManager RepositoryManager;
        IConfiguration Configuration;
        string OS;
        
        private bool disposedValue2;

        public RepositoriesContext(RokonocontrolContext context, IConfiguration config, RepositoryManager repositoryManager)
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
            var getCommits = RepositoryManager.CommandOutput(OS, "git branch -r", Path.Combine(Program.Configuration.LocalRepo, Program.ServerOS == "win" ?
                                            $"{project.Repository.FolderPath}\\{GetRepositoryName(project)}" : $"{project.Repository.LinuxFolderPath}/{GetRepositoryName(project)}"));
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
        public string GetCommitDetailsRaw(IncomingIdRequest request)
        {
            var project = Context.Projects.Include(x => x.Repository).FirstOrDefault(x => x.Id == request.ProjectId);
            var masterVersion = string.Empty;
            var revVersion = string.Empty;
            var lookupPath = Path.Combine(Program.Configuration.LocalRepo, Program.ServerOS == "win" ?
                                            $"{project.Repository.FolderPath}\\{GetRepositoryName(project)}" : $"{project.Repository.LinuxFolderPath}/{GetRepositoryName(project)}");
            var result = RepositoryManager.CommandOutput(OS, $"git show \"{request.Phase}\" ", lookupPath);

            return result;
        }
        internal List<(string,string, string)> GetCommitDeatails(IncomingIdRequest request)
        {
            var result = new List<(string, string, string)>();
            var project = Context.Projects.Include(x => x.Repository).FirstOrDefault(x => x.Id == request.ProjectId);
            var first = true;
            var masterVersion = string.Empty;
            var revVersion = string.Empty;
            var lookupPath = Path.Combine(Program.Configuration.LocalRepo, Program.ServerOS == "win" ?
                                            $"{project.Repository.FolderPath}\\{GetRepositoryName(project)}" : $"{project.Repository.LinuxFolderPath}/{GetRepositoryName(project)}");
            var getCommitDetails = RepositoryManager.CommandOutput(OS, $"git show  --name-only  --oneline {request.Phase}", lookupPath);
            foreach (var line in getCommitDetails.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if(first)
                {
                    Program.Logger.Info($"Reading commit deatils: {line}");
                    first = false;
                }
                else
                {
                    var file = Path.Combine(lookupPath, line);

                    if (Path.HasExtension(file))
                    {
                        //Get version of master master:file
                        masterVersion = RepositoryManager.CommandOutput(OS, $"git show \"master:{line}\"", lookupPath);
                        var res = RepositoryManager.CommandOutput(OS, $"git show \"{request.Phase}\" ", lookupPath);

                        //Does a reverse loookup in git to get the file after it was renamed.
                        if (masterVersion.Contains("does not exist in 'master'"))
                        {
                            masterVersion = GetCommitDeatailSubHistory(line, lookupPath, line);
                        }
                        //Get rev version of the file REVGUID:FILEPATH
                        revVersion = RepositoryManager.CommandOutput(OS, $"git show \"{request.Phase}:{line}\" ", lookupPath);

                        result.Add(new(masterVersion, revVersion, line));
                    }

                }
            }


            return result;
        }
 
        internal string GetCommitDeatailSubHistory(string fileName, string folderPath, string lookupFile)
        {
            var getCommitDetails = RepositoryManager.CommandOutput(OS, $" git log  --name-only --oneline \"*{fileName.Split("/").LastOrDefault()}\"", folderPath);
            var commitDetails = getCommitDetails.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ElementAt(1);
            var result = RepositoryManager.CommandOutput(OS, $"git show \"master:{commitDetails}\"", folderPath);

            return result;
        }

        public List<OutgoingCommitTemp> GetCommitsForProject(int projectId, string branch)
        {
            var result = new List<OutgoingCommitTemp>();
            branch = $"origin/{branch}";
            if (!Program.ProjectBranches.Any(x => x.ProjectId == projectId))
                RepositoryManager.GetAllCommitsForProject(projectId, OS, Context.Projects.FirstOrDefault(x => x.Id == projectId));

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
        private static string GetRepositoryName(Projects x)
        {
            return x.Repository.RepositoryLocation.Split("/").ToList().LastOrDefault();
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
