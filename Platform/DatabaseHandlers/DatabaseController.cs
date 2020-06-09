namespace Rokono_Control.DatabaseHandlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Platform.DataHandlers;
    using Platform.Hubs;
    using Platform.Models;
    using Rokono_Control.DataHandlers;
    using Rokono_Control.Models;
    using RokonoControl.DatabaseHandlers.WorkItemHandlers;
    using RokonoControl.Models;

    public class DatabaseController : IDisposable
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        private int I { get; set; }
        private int InternalId { get; set; }
        public DatabaseController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }
        public DatabaseController(int i, int internalId) 
        {
            this.I = i;
            this.InternalId = internalId;
        }


        internal List<Repository> GetAllRepositories()
        {
            return Context.Repository.Include(x => x.Projects).ToList();
        }

      
        internal object GetProjectName(int projectId)
        {
            return Context.Projects.FirstOrDefault(x => x.Id == projectId).ProjectName;
        }
        internal List<Branches> GetProjectBranches(int projectId)
        {
            var branches = Context.Projects.Include(x => x.Repository)
                                   .ThenInclude(Repository => Repository.AssociatedRepositoryBranches)
                                   .ThenInclude(AssociatedRepositoryBranches => AssociatedRepositoryBranches.Branch)
                                   .FirstOrDefault(x => x.Id == projectId);
            if (branches != null)
                return branches.Repository.AssociatedRepositoryBranches.Select(y => y.Branch).ToList();
            else
                return null;

        }

        internal OutgoingSourceFile GetSelectedFileByName(string fileName, int projectId, int branchId)
        {
            var project = Context.Projects.Include(x => x.Repository).FirstOrDefault(x => x.Id == projectId);
            var branch = Context.Branches.FirstOrDefault(x => x.Id == branchId);
            var fileLanauge = GitRepositoryManager.GetFileLanaguage(fileName);
            var branchCommits = Execute($"{Program.Configuration.ShellScripts.FirstOrDefault(x => x.Name == "GetGitList.sh").Path}", $"{project.Repository.FolderPath} {branch.BranchName}");
            var fileData = ExecuteSingle($"{Program.Configuration.ShellScripts.FirstOrDefault(x => x.Name == "GetCommitFile.sh").Path}", $"{project.Repository.FolderPath} {branchCommits.FirstOrDefault()}:{fileName}");
            return new OutgoingSourceFile { Data = fileData, LanguageType = fileLanauge };
        }
        internal List<string> GetBranchFiles(int projectId, int branchId)
        {
            var result = new List<string>();
            var project = Context.Projects.FirstOrDefault(x => x.Id == projectId);
            if (project == null)
                return null;
            var getBranch = Context.AssociatedRepositoryBranches.Include(x => x.Branch)
                                                                .Include(x => x.Repository)
                                                                .FirstOrDefault(x => x.RepositoryId == project.RepositoryId && x.Branch.Id == branchId);
            if (getBranch != null)
            {
                var name = getBranch.Repository.FolderPath;
                // var fileName = file.FilePath.Split('/').LastOrDefault();
                //var fileLanauge = GitRepositoryManager.GetFileLanaguage(fileName);
                result = Execute($"{Program.Configuration.ShellScripts.FirstOrDefault(x => x.Name == "LsFiles.sh").Path}", $"{name}");

            }
            return result;
        }

        internal List<CommitFileHirarhicalData> GetFileHirarchy(List<string> files)
        {
            var folders = new List<CommitFileHirarhicalData>();
            var data = new List<CommitFileHirarhicalData>();
            var count = 1;
            InternalId = 1;
            files.ForEach(x =>
            {

                // folders.Add(GenerateDirectory(x,folders.Count));
                var item = GenerateDirectory(x, $"{count++}");
                if (item != null)
                    folders.Add(item);
                count++;
            });
            data.AddRange(folders);
            return data;
        }
        public CommitFileHirarhicalData GenerateDirectory(string path, string count)
        {
            if (Directory.Exists(path))
            {
                var dFiles = Directory.EnumerateFiles(path);
                var item = new CommitFileHirarhicalData
                {
                    Name = path.Split("/").LastOrDefault(),
                    FullPathName = path,
                    InternalId = InternalId,
                    Id = $"{count + 1}",
                    SubChild = new List<CommitFileHirarhicalData>()
                };
                item.SubChild = GenerateSubDirectory(item, dFiles.ToList(), $"{count}-{I++}", path);
                InternalId++;

                var directories = Directory.GetDirectories(path);
                directories.ToList().ForEach(e =>
                {
                    item.SubChild.Add(GenerateDirectory(e, $"{count}-{item.SubChild.Count + 1}"));
                });

                return item;
            }
            else
            {
                var item = new CommitFileHirarhicalData
                {
                    Name = path,
                    InternalId = InternalId,

                    FullPathName = path,
                    Id = $"{count + 1}"
                };
                InternalId++;

                return item;
            }

        }

        public List<CommitFileHirarhicalData> GenerateSubDirectory(CommitFileHirarhicalData item, List<string> files, string parent, string directory)
        {
            InternalId++;

            files.ToList().ForEach(z =>
            {
                item.SubChild.Add(new CommitFileHirarhicalData
                {
                    Name = z.Split("/").LastOrDefault(),
                    FullPathName = z,
                    Id = parent,
                    InternalId = InternalId,

                    SubChild = new List<CommitFileHirarhicalData>()
                });
            });

            return item.SubChild;
        }

        internal List<Branches> GetBranchesForProject(int projectId)
        {
            return Context.AssociatedRepositoryBranches
                        .Include(x => x.Branch)
                        .Include(x => x.Repository)
                        .ThenInclude(Repository => Repository.Projects)
                        .Where(x => x.Repository.Projects.Any(y => y.Id == projectId))
                        .Select(x => new Branches
                        {
                            BranchName = x.Branch.BranchName,
                            Id = x.Branch.Id,
                        }).ToList();

        }

        internal List<CommitFileHirarhicalData> GetCommitFilesHirarchy(string commitId)
        {
            var folders = new List<CommitFileHirarhicalData>();
            var data = new List<CommitFileHirarhicalData>();
            var files = Context.AssociatedCommitFiles
                    .Include(x => x.File)
                    .Include(x => x.Commit)
                    .Where(x => x.Commit.CommitKey == commitId)
                    .ToList();

            files.ForEach(x =>
            {
                var ifSingle = x.File.FilePath.Split('/').ToList();
                if (ifSingle.Count() <= 1)
                    data.Add(new CommitFileHirarhicalData
                    {
                        Id = $"{data.Count + 1}",
                        Name = x.File.FilePath,
                        ItemId = x.File.Id

                    });

            });
            var count = data.Count + 1;
            files.ForEach(x =>
            {
                var ifSingle = x.File.FilePath.Split('/').ToList();
                var file = ifSingle.LastOrDefault();

                if (ifSingle.Count() > 1)
                {
                    ifSingle.Remove(file);
                    var folder = string.Empty;
                    ifSingle.ForEach(cFolder =>
                    {
                        folder += $"{cFolder}/";
                    });
                    if (folders.FirstOrDefault(cFolders => cFolders.Name == folder) == null)
                        folders.Add(new CommitFileHirarhicalData
                        {
                            Id = $"{count++}",
                            Name = folder,
                            SubChild = new List<CommitFileHirarhicalData>(),
                            ItemId = x.File.Id

                        });
                    else
                    {
                        if (folders.FirstOrDefault(z => z.Name == folder).SubChild == null)
                            folders.FirstOrDefault(z => z.Name == folder).SubChild = new List<CommitFileHirarhicalData>();
                        folders.FirstOrDefault(z => z.Name == folder).SubChild.Add(new CommitFileHirarhicalData
                        {
                            Id = $"{count}-{folders.FirstOrDefault(z => z.Name == folder).SubChild.Count + 1}",
                            Name = file,
                            ItemId = x.File.Id
                        });
                    }
                }
            });
            data.AddRange(folders);
            return data;
        }

        internal OutgoingSourceFile GetSelectedFileById(int fileId, int branch)
        {
            var file = Context.Files
                            .Include(x => x.AssociatedCommitFiles)
                            .ThenInclude(AssociatedCommitFiles => AssociatedCommitFiles.Commit)
                            .FirstOrDefault(x => x.Id == fileId);
            var commitKey = file.AssociatedCommitFiles.FirstOrDefault().Commit.CommitKey;
            var repository = Context.Repository
                                    .Include(x => x.AssociatedRepositoryBranches)
                                    .ThenInclude(AssociatedRepositoryBranches => AssociatedRepositoryBranches.Branch)
                                    .ThenInclude(Branch => Branch.AssociatedBranchCommits)
                                    .ThenInclude(AssociatedBranchCommits => AssociatedBranchCommits.Commit)
                                    .FirstOrDefault(x =>
                                    x.AssociatedRepositoryBranches
                                    .Any(z =>
                                    z.Branch.AssociatedBranchCommits
                                    .Any(y =>
                                    y.Commit.CommitKey == commitKey)));
            var fileName = file.FilePath.Split('/').LastOrDefault();
            var fileLanauge = GitRepositoryManager.GetFileLanaguage(fileName);
            var fileData = ExecuteSingle($"{Program.Configuration.ShellScripts.FirstOrDefault(x => x.Name == "GetCommitFile.sh").Path}", $"{repository.FolderPath} {commitKey}:{file.FilePath}");
            return new OutgoingSourceFile { Data = fileData, LanguageType = fileLanauge };
        }

        internal List<OutgoingCommitData> GetCommitData(int projectId, int branch)
        {
            return Context.Commits
                        .Include(x => x.AssociatedBranchCommits)
                        .ThenInclude(AssociatedBranchCommits => AssociatedBranchCommits.Branch)
                        .ThenInclude(Branch => Branch.AssociatedRepositoryBranches)
                        .ThenInclude(AssociatedRepositoryBranches => AssociatedRepositoryBranches.Repository)
                        .ThenInclude(Repository => Repository.Projects)
                        .Where(x =>
                        x.AssociatedBranchCommits
                        .Any(y =>
                        y.BranchId == branch
                        && y.Branch.AssociatedRepositoryBranches
                        .Any(z =>
                            z.Repository.Projects
                            .Any(d =>
                                d.Id == projectId)))).Select(commit => new OutgoingCommitData
                                {
                                    Author = commit.CommitedBy,
                                    CommitKey = commit.CommitKey,
                                    Date = commit.DateOfCommit.Value,
                                    PullRequest = "",
                                    Message = commit.CommitData,

                                }).ToList();

        }

        internal List<OutgoingCommitData> GetCommitDataMaster(int projectId)
        {
            return Context.Commits
                        .Include(x => x.AssociatedBranchCommits)
                        .ThenInclude(AssociatedBranchCommits => AssociatedBranchCommits.Branch)
                        .ThenInclude(Branch => Branch.AssociatedRepositoryBranches)
                        .ThenInclude(AssociatedRepositoryBranches => AssociatedRepositoryBranches.Repository)
                        .ThenInclude(Repository => Repository.Projects)
                        .Where(x =>
                        x.AssociatedBranchCommits
                        .Any(y =>
                        y.Branch.BranchName == "Master"
                        && y.Branch.AssociatedRepositoryBranches
                        .Any(z =>
                            z.Repository.Projects
                            .Any(d =>
                                d.Id == projectId)))).Select(commit => new OutgoingCommitData
                                {
                                    Author = commit.CommitedBy,
                                    CommitKey = commit.CommitKey,
                                    Date = commit.DateOfCommit.Value,
                                    PullRequest = "",
                                    Message = commit.CommitData,

                                }).ToList();

        }

        internal Branches CreateBrach(string b, int repoId, int projectId)
        {
            var branch = Context.Branches.Add(new Branches
            {
                BranchName = b,
                ProjectId = projectId,
            });
            Context.SaveChanges();
            Context.AssociatedRepositoryBranches.Add(new AssociatedRepositoryBranches
            {
                RepositoryId = repoId,
                BranchId = branch.Entity.Id
            });
            Context.SaveChanges();
            return branch.Entity;

        }

        internal List<string> GetBranches(int repoId)
        {
            return Context.AssociatedRepositoryBranches
                          .Include(x => x.Branch)
                          .Where(x => x.RepositoryId == repoId)
                          .Select(x => x.Branch.BranchName)
                          .ToList();
        }

        internal bool CheckIfBranchCommitExists(string commitId, int branchId)
        {
            return Context.Commits
                          .Include(x => x.AssociatedBranchCommits)
                          .Where(x => x.AssociatedBranchCommits.Any(y => y.BranchId == branchId))
                          .Any(x => x.CommitKey == commitId);
        }

        internal List<Repository> GetRepositories()
        {
            return Context.Repository.ToList();
        }

       
        internal void AssociatedCommitsWithBranch(List<Files> commitFiles, int id, string commitKey, string commitedBy)
        {
            var commit = default(Commits);
            var commiteExists = Context.Commits.Any(x => x.CommitKey == commitKey);
            if (!commiteExists)
            {
                commit = Context.Commits.Add(new Commits
                {
                    CommitedBy = commitedBy,
                    CommitKey = commitKey,
                    DateOfCommit = DateTime.Now,
                    CommitData = ""
                }).Entity;
                Context.SaveChanges();
            }
            else
                commit = Context.Commits.FirstOrDefault(x => x.CommitKey == commitKey);

            var branchCommitAssociation = default(AssociatedBranchCommits);
            var commitAssociationExist = Context.AssociatedBranchCommits.Include(x => x.Commit).FirstOrDefault(x => x.Commit.CommitKey == commitKey);
            if (commitAssociationExist != null)
                branchCommitAssociation = commitAssociationExist;
            else
            {
                branchCommitAssociation = Context.AssociatedBranchCommits.Add(new AssociatedBranchCommits
                {
                    BranchId = id,
                    CommitId = commit.Id,
                }).Entity;
                Context.SaveChanges();
            }
            commitFiles.ForEach(x =>
            {
                var file = x;
                if (!Context.AssociatedCommitFiles.Include(y => y.File)
                                                .Include(y => y.Commit)
                                                .Any(y => y.Commit.CommitKey == commitKey && file.FilePath == y.File.FilePath))
                {
                    var fileExist = Context.Files.FirstOrDefault(y => y.DateOfFile == file.DateOfFile
                                                && y.CurrentName == file.CurrentName
                                                && y.FilePath == file.FilePath);

                    var cFile = default(Files);
                    if (fileExist != null)
                        cFile = fileExist;
                    else
                        cFile = Context.Files.Add(new Files
                        {
                            CurrentName = file.CurrentName,
                            FilePath = file.FilePath,
                            FileData = "",
                            DateOfFile = file.DateOfFile,

                        }).Entity;
                    Context.AssociatedCommitFiles.Add(new AssociatedCommitFiles
                    {
                        CommitId = commit.Id,
                        FileId = cFile.Id,
                        DateOfCommit = DateTime.Now
                    });
                    Context.SaveChanges();
                }
            });

        }
        internal void AssociatedCommitFilesWithExistingBranch(List<Files> commitFiles, int id, string commitKey, string commitedBy)
        {

            var newCommit = default(bool);
            var commit = default(Commits);
            if (!Context.Commits.Any(x => x.CommitKey == commitKey))
            {
                newCommit = true;
                commit = Context.Commits.Add(new Commits
                {
                    CommitKey = commitKey,
                    CommitedBy = commitedBy,
                    CommitData = "",
                    DateOfCommit = DateTime.Now
                }).Entity;
                Context.SaveChanges();
            }
            commitFiles.ForEach(x =>
            {
                if (commit != null)
                {
                    var file = Context.Files.Add(x);
                    Context.SaveChanges();

                    var associatedCommitFile = Context.AssociatedCommitFiles.Add(new AssociatedCommitFiles
                    {
                        CommitId = commit.Id,
                        FileId = file.Entity.Id,
                        DateOfCommit = DateTime.Now
                    });
                    Context.SaveChanges();
                }
            });

            if (newCommit)
            {
                Context.AssociatedBranchCommits.Add(new AssociatedBranchCommits
                {
                    CommitId = commit.Id,
                    BranchId = id,
                });
                Context.SaveChanges();
            }
        }

        internal Branches GetBranch(string b, int id)
        {
            return Context.Branches.FirstOrDefault(x => x.BranchName == b && x.ProjectId == id);
        }

        internal object GetProjects()
        {
            return Context.Projects.Select(x => new
            {
                Name = x.ProjectName,
                Id = x.Id
            }).ToList();
        }

        internal OutgoingProjectRules GetProjectRules(int id, int userId)
        {
            var projectDetails = Context.AssociatedProjectMembers.FirstOrDefault(x => x.ProjectId == id && x.UserAccountId == userId);
            return new OutgoingProjectRules
            {
                //CanClone = projectDetails.CanClone == 1 ? true : false,
                //CanView  =projectDetails.CanViewWork == 1 ? true : false,
                //CanCommit =projectDetails.CanCommit == 1 ? true : false,
                //CanCreateWork = projectDetails.CanCreateWork == 1 ? true : false,
                //CanDeleteWork =projectDetails.CanDeleteWork == 1 ? true : false,
            };
        }

        internal List<CommitChartBindingData> GetCommitsChartForProject(int id)
        {
            return Context.AssociatedBranchCommits.Where(x => x.Branch.ProjectId == id).Select(x => new CommitChartBindingData
            {
                DateOfCommit = x.Commit.DateOfCommit.Value,
                DayCount = Context.Commits.Where(y => y.DateOfCommit == x.Commit.DateOfCommit).Count(),
            }).ToList();
        }

        internal Projects GetProjectData(int id)
        {

            return Context.Projects.Include(x => x.Branches)
                                   .ThenInclude(Brances => Brances.AssociatedBranchCommits)
                                   .ThenInclude(AssociatedBranchCommits => AssociatedBranchCommits.Commit)
                                   .FirstOrDefault(x => x.Id == id);

        }

        internal void RemoveUserFromProject(IncomingRemoveUserFromProject userProject)
        {
            var userAccount = Context.AssociatedProjectMembers.FirstOrDefault(x => x.ProjectId == userProject.ProjectId && x.UserAccountId == userProject.UserId);
            Context.Attach(userAccount);
            Context.Remove(userAccount);
            Context.SaveChanges();
        }
         
        internal OutgoingAccountManagment GetSpecificUserEdit(int id)
        {
            var account = Context.UserAccounts.Include(x => x.AssociatedProjectMembers)
                                        .ThenInclude(AssociatedProjectsMembers => AssociatedProjectsMembers.Project)
                                       .FirstOrDefault(x => x.Id == id);

            return new OutgoingAccountManagment
            {
                AccountId = account.Id,
                Name = $"{account.FirstName} {account.LastName}",
                Type = account.ProjectRights == 1 ? "Regular" : "Administrator",
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                CreationDate = account.CreationDate,
                ProjectRights = account.ProjectRights == 1 ? true : false,
                Projects = $@"{GetJsonData(account.AssociatedProjectMembers.Select(y => y.Project).ToList())}"
            };

        }

        internal void AddProjectToUser(IncomingProjectUser incomingRequest)
        {
            var project = Context.Projects.FirstOrDefault(x => x.Id == incomingRequest.Id);
            Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers
            {
                ProjectId = incomingRequest.Id,
                UserAccountId = incomingRequest.UserId,
                RepositoryId = project.RepositoryId
            });
            Context.SaveChanges();
        }

        private string GetJsonData(Object currnet)
        {
            return JsonConvert.SerializeObject(currnet, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        internal bool AddNewProject(IncomingProject currentProject, int v)
        {
            if (Context.UserAccounts.FirstOrDefault(x => x.Id == v).ProjectRights == 1)
            {

                var userAccounts = new List<UserAccounts>();

                var repoStatus = true; //RepositoryManager.AddNewProject($"/home/GitRepositories/",currentProject.ProjectName, userAccounts);
                if (repoStatus)
                {
                    var repository = Context.Repository.Add(new Repository
                    {
                        FolderPath = $"/home/GitRepositories/{currentProject.ProjectName}"
                    });
                    Context.SaveChanges();

                    var boardBacklog = Context.Boards.Add(new Boards
                    {
                        RepositoryId = repository.Entity.Id,
                        BoardType = 1,
                        BoardName = "Open"
                    });
                    var boardActive = Context.Boards.Add(new Boards
                    {
                        RepositoryId = repository.Entity.Id,
                        BoardType = 2,
                        BoardName = "InProgress"
                    });
                    var boardTesting = Context.Boards.Add(new Boards
                    {
                        RepositoryId = repository.Entity.Id,
                        BoardType = 3,
                        BoardName = "Testing"
                    });
                    var boardDone = Context.Boards.Add(new Boards
                    {
                        RepositoryId = repository.Entity.Id,
                        BoardType = 4,
                        BoardName = "Done"
                    });


                    Context.SaveChanges();
                    var project = Context.Projects.Add(new Projects
                    {
                        ProjectDescription = currentProject.ProjectDescription,
                        ProjectName = currentProject.ProjectName,
                        ProjectTitle = "",
                        RepositoryId = repository.Entity.Id,
                        BoardId = boardBacklog.Entity.Id
                    });
                    Context.SaveChanges();

                    currentProject.Iterations.ForEach(x =>
                    {
                        var iteration = x;
                        var currentIteration = Context.WorkItemIterations.Add(iteration);
                        Context.SaveChanges();
                        Context.AssociatedProjectIterations.Add(new AssociatedProjectIterations
                        {
                            ProjectId = project.Entity.Id,
                            IterationId = currentIteration.Entity.Id
                        });
                        Context.SaveChanges();
                    });
                    currentProject.Users.ForEach(x =>
                    {
                        userAccounts.Add(Context.UserAccounts.FirstOrDefault(y => y.Id == x.AccountId));
                        Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers
                        {
                            ProjectId = project.Entity.Id,
                            UserAccountId = x.AccountId,
                            RepositoryId = repository.Entity.Id
                        });
                        Context.SaveChanges();
                        var rights = Context.UserRights.FirstOrDefault(y => y.ManageIterations == x.IterationOptions &&
                                                                        y.ManageUserdays == x.ScheduleManagement &&
                                                                        y.UpdateUserRights == x.EditUserRights &&
                                                                        y.ViewOtherPeoplesWork == x.ViewWorkItems &&
                                                                        y.ChatChannelsRule == x.ChatChannels);
                        if (rights == null)
                        {
                            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<UserRights> entityEntry = Context.UserRights.Add(new UserRights
                            {
                                WorkItemRule = Convert.ToInt16(x.WorkItemOption),
                                ManageIterations = Convert.ToInt16(x.IterationOptions),
                                ManageUserdays = Convert.ToInt16(x.ScheduleManagement),
                                UpdateUserRights = Convert.ToInt16(x.EditUserRights),
                                ViewOtherPeoplesWork = Convert.ToInt16(x.ViewWorkItems),
                                ChatChannelsRule = Convert.ToInt16(x.ChatChannels)
                            });
                            Context.SaveChanges();
                            rights = entityEntry.Entity;
                        }

                        Context.AssociatedProjectMemberRights.Add(new AssociatedProjectMemberRights
                        {
                            ProjectId = project.Entity.Id,
                            UserAccountId = x.AccountId,
                            RightsId = rights.Id

                        });
                        Context.SaveChanges();
                    });
                    Context.AssociatedProjectBoards.Add(new AssociatedProjectBoards
                    {
                        ProjectId = project.Entity.Id,
                        BoardId = boardBacklog.Entity.Id,
                        Position = 1
                    });
                    Context.AssociatedProjectBoards.Add(new AssociatedProjectBoards
                    {
                        ProjectId = project.Entity.Id,
                        BoardId = boardActive.Entity.Id,
                        Position = 2
                    });
                    Context.AssociatedProjectBoards.Add(new AssociatedProjectBoards
                    {
                        ProjectId = project.Entity.Id,
                        BoardId = boardTesting.Entity.Id,
                        Position = 3
                    });
                    Context.AssociatedProjectBoards.Add(new AssociatedProjectBoards
                    {
                        ProjectId = project.Entity.Id,
                        BoardId = boardDone.Entity.Id,
                        Position = 4
                    });
                    Context.SaveChanges();
                    currentProject.Users.ForEach(x =>
                    {
                        Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers
                        {
                            ProjectId = project.Entity.Id,
                            RepositoryId = repository.Entity.Id,
                            UserAccountId = x.AccountId,

                        });
                        Context.SaveChanges();
                    });


                }

            }
            return true;
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

        internal Repository GetRepositoryByName(string rName)
        {
            var projectExist = Context.Projects.Include(x => x.Repository).FirstOrDefault(x => x.ProjectName == rName);
            if (projectExist != null)
                return projectExist.Repository;

            return null;
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //Context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }


        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DatabaseController()
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