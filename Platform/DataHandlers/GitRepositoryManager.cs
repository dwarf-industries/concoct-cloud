using System;
using System.Collections.Generic;
using System.Linq;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Rokono_Control.DataHandlers
{
    public class GitRepositoryManager : IDisposable
    {
        internal void CollectCommits(string rName)
        {
            //using (var context = new DatabaseController(Context))
            //{

            //    var x = context.GetRepositoryByName(rName);
            //    var dbBranches = context.GetBranches(x.Id);
            //    var repoBranches = Execute($"{Program.Configuration.ShellScripts.FirstOrDefault(y => y.Name == "GetBranches.sh").Path}", x.FolderPath);
            //    var repoNames = new List<string>();
            //    repoBranches.ForEach(z =>
            //    {
            //        var strings = z.Split(' ').ToList();
            //        strings.ForEach(y =>
            //        {
            //            if (y == "*") { }
            //            else if (string.IsNullOrEmpty(y)) { }
            //            else if (string.IsNullOrWhiteSpace(y)) { }
            //            else
            //                repoNames.Add(y);
            //        });
            //    });

            //    repoNames.ForEach(b =>
            //    {
            //        var branch = dbBranches.FirstOrDefault(dbBranch => dbBranch == b);
            //        var cBranch = default(Branches);
            //        if (branch != null)
            //            cBranch = context.GetBranch(b, x.Projects.FirstOrDefault().Id);
            //        else
            //            cBranch = context.CreateBrach(b, x.Id, x.Projects.FirstOrDefault().Id);
            //        var branchCommits = Execute($"{Program.Configuration.ShellScripts.FirstOrDefault(y => y.Name == "GetGitList.sh").Path}", $"{x.FolderPath} {b}");
            //        var z = branchCommits.FirstOrDefault();
            //        if (z != null)
            //        {
            //            if (!context.CheckIfBranchCommitExists(z, cBranch.Id))
            //            {

            //                var commitFiles = new List<Files>();
            //                var repositoryCommitData = Execute($"{Program.Configuration.ShellScripts.FirstOrDefault(y => y.Name == "GetCommitData.sh").Path}", $"{x.FolderPath} {z} {b}");
            //                repositoryCommitData.Skip(4).ToList().ForEach(f =>
            //                {
            //                    var fileName = f.Split('/').LastOrDefault();
            //                    var fileData = ExecuteSingle($"{Program.Configuration.ShellScripts.FirstOrDefault(y => y.Name == "GetCommitFile.sh").Path}", $"{x.FolderPath} {z}:{f}");
            //                    commitFiles.Add(new Files
            //                    {
            //                        DateOfFile = DateTime.Now,
            //                        FilePath = f,
            //                        CurrentName = fileName,
            //                        FileData = fileData
            //                    });
            //                });
            //                context.AssociatedCommitsWithBranch(commitFiles, cBranch.Id, z, repositoryCommitData.ElementAt(1));
            //            }
            //        }
            //    });

            //}
            ////     Program.InitCron();
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

        public static string GetFileLanaguage(string filename)
        {
            var result = string.Empty;
            var extension = filename.Split('.').LastOrDefault();
            switch (extension)
            {
                case "cs":
                    result = "csharp";
                    break;
                case "cpp":
                    result = "cpp";
                    break;
                case "css":
                    result = "css";
                    break;
                case "js":
                    result = "javascript";
                    break;
                case "btach":
                    result = "bat";
                    break;
                case "sh":
                    result = "bat";
                    break;
                case "html":
                    result = "html";
                    break;
                case "cshtml":
                    result = "razor";
                    break;
            }
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
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GitRepositoryManager()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }


        #endregion

    }
}