
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Rokono_Control.Models;

namespace Rokono_Control
{
    public class RepositoryManager
    {
        private static List<Projects> Projects { get; set; }
        private static string Os { get; set; }
        private static Timer _aTimer  {get; set;}

        public static bool AddNewProject(string repoName,string projectName, List<UserAccounts> accounts)
        {
            var current = OS.GetCurrent();
            System.Console.WriteLine(current);
            if(current == "gnu")
            { 
                try{
                    System.Console.WriteLine(ExecuteCmd("/bin/bash", $"{Program.Configuration.ShellScripts.FirstOrDefault(x=>x.Name == "CreateProj.sh").Path} {repoName} {projectName}"));  
    
                    accounts.ForEach(x=>{
                        System.Console.WriteLine(ExecuteCmd("/bin/bash", $"  {Program.Configuration.ShellScripts.FirstOrDefault(y=>y.Name == "AssignGroup.sh").Path} {projectName}Contribute {x.GitUsername}"));     
                    });
                }
                catch(Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return false;
                }
          
            }
            return true;
        }

        public static void GetAllCommitsForProject(int projectId, string os,Projects project)
        {
            var format = "commit:%H/r/nsubject:%s/r/nbody:%b/r/nsignature:%GK/r/nauthor:%aN/r/nemail:%ae/r/ndate:%aD/r/nbranch:%d";
            Program.Logger.Info($"Creating Folder for Project {project.ProjectName}");
            var getCommits = CommandOutput(os, "git log --branches=*  --pretty=format:" + format, 
                                            Path.Combine(Program.Configuration.LocalRepo, os == "win" ?
                                            $"{project.Repository.FolderPath}\\{GetRepositoryName(project)}" : $"{project.Repository.LinuxFolderPath}/{GetRepositoryName(project)}"));

            foreach (var line in getCommits.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {

                var items = line.Split("/r/n");
                var branchName = GetBranchName(items[7]);

                if (Program.ProjectBranches.FirstOrDefault(x => x.ProjectId == projectId && x.BranchName == branchName) != null)
                {
                    if (Program.ProjectBranches.FirstOrDefault(x => x.ProjectId == projectId && x.BranchName == branchName).Commits.FirstOrDefault(x => x.Id == items[0].Substring(7)) == null)
                    {
                        Program.ProjectBranches.FirstOrDefault(x => x.ProjectId == projectId && x.BranchName == branchName).Commits.Add(new OutgoingCommitTemp
                        {
                            Id = items[0].Substring(7),
                            Subject = items[1].Substring(8),
                            Body = items[2].Substring(5),
                            Signature = items[3].Substring(10),
                            Name = items[4].Substring(7),
                            Email = items[5].Substring(6),
                            Date = items[6].Substring(5),
                        });
                    }
                }
                else
                {
                    Program.ProjectBranches.Add(new ProjectBranches
                    {
                        BranchName = branchName,
                        ProjectName = project.ProjectName,
                        ProjectId = projectId,
                        Commits = new List<OutgoingCommitTemp>
                       {
                           new OutgoingCommitTemp
                           {
                                Id = items[0].Substring(7),
                                Subject = items[1].Substring(8),
                                Body = items[2].Substring(5),
                                Signature = items[3].Substring(10),
                                Name = items[4].Substring(7),
                                Email = items[5].Substring(6),
                                Date = items[6].Substring(5),

                           }
                       }
                    });
                }
            }
        }

        internal static void InitRepositories(List<Projects> projects, string serverOS)
        {
            Projects = projects;
            Os = serverOS;
            FetchDataSources(serverOS);
            InitTimer();
        }

        private static void InitTimer()
        {
            if(_aTimer != null)
            {
                _aTimer.Stop();
                _aTimer.Dispose();
            }
            _aTimer = new System.Timers.Timer(60 * 60 * 1000); //one hour in milliseconds
            _aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _aTimer.Start();
        }

        private static void FetchDataSources(string os)
        {
            Projects.ForEach(x =>
            {
                try
                {
                    var projectPath = Path.Combine(Program.Configuration.LocalRepo, os == "win" ? x.Repository.FolderPath : x.Repository.LinuxFolderPath);
                    if (!Directory.Exists(projectPath))
                    {
                        Program.Logger.Warning($"Directory \"{projectPath}\" doesn't exist");
                        Program.Logger.Info($"Creating Folder for Project {x.ProjectName}");
                        CommandOutput(Os, $"mkdir {projectPath}", Program.Configuration.LocalRepo);
                        Program.Logger.Info($"Cloning Repository {x.Repository.RepositoryLocation}");
                        CommandOutput(Os, $"git clone {x.Repository.RepositoryLocation}", projectPath);
                        Program.Logger.Message("Repository cloned");
                    }

                    Program.Logger.Info($"Fetchign all changes for repository {x.Repository.FolderPath}");
                    CommandOutput(Os, "git fetch --all", Path.Combine(Program.Configuration.LocalRepo, os == "win" ? $"{x.Repository.FolderPath}\\{GetRepositoryName(x)}" : $"{x.Repository.LinuxFolderPath}/{GetRepositoryName(x)}"));
                    CommandOutput(Os, "git pull --all", Path.Combine(Program.Configuration.LocalRepo, os == "win" ? $"{x.Repository.FolderPath}\\{GetRepositoryName(x)}" : $"{x.Repository.LinuxFolderPath}/{GetRepositoryName(x)}"));
                    GetAllCommitsForProject(x.Id, Os, x);
                }
                catch (Exception ex)
                {
                    Program.Logger.Warning(ex.ToString());
                }
               
            });
        }

        private static string GetRepositoryName(Projects x)
        {
            return x.Repository.RepositoryLocation.Split("/").ToList().LastOrDefault();
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            FetchDataSources(Program.ServerOS);
        }
        public static string CommandOutput(string os, string command,  string workingDirectory = null)
        {
            var result = string.Empty;
            switch(os)
            {
                case "win":
                    result = ReadCommandOutputWin(command, workingDirectory);
                    break;
                case "gnu":
                    result = ExecuteCmd(command, workingDirectory);
                    break;
            }
            return result;
        }

        private static string GetBranchName(string serilzied)
        {
            //"branch: (HEAD -> master, origin/master, origin/HEAD)"
            var split = serilzied.Split(":");
            var result = string.Empty;
            if (split.Length > 1 && serilzied != "branch:")
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


        private static string ReadCommandOutputWin(string command, string workingDirectory = null)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);

                procStartInfo.RedirectStandardError = procStartInfo.RedirectStandardInput = procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                if (null != workingDirectory)
                {
                    procStartInfo.WorkingDirectory = workingDirectory;
                }

                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                StringBuilder sb = new StringBuilder();
                proc.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
                {
                    sb.AppendLine(e.Data);
                };
                proc.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e)
                {
                    sb.AppendLine(e.Data);
                };

                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                proc.WaitForExit();
                return sb.ToString();
            }
            catch (Exception objException)
            {
                return $"Error in command: {command}, {objException.Message}";
            }
        }

        public static string ExecuteCmd(string arguments, string workingDiectory = null)
        {
            var result = string.Empty; 
            if(workingDiectory != null && Directory.Exists(workingDiectory))
            {
                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{arguments}\"",
                        // FileName = "ping",
                        //  Arguments = $"localhost",
                        WorkingDirectory = workingDiectory,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                process.Start();
                result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            return result;
        }
    }
}