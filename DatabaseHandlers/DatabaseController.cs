namespace Rokono_Control.DatabaseHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Platform.Models;
    using Rokono_Control.DataHandlers;
    using Rokono_Control.Models;
    using RokonoControl.Models;

    public class DatabaseController : IDisposable
    {
        RokonocontrolContext Context;
        IConfiguration Configuration;
        public DatabaseController(int i, int internalId) 
        {
            this.I = i;
                this.InternalId = internalId;
               
        }
        private int I { get; set; }
        private int InternalId { get; set; }
        public DatabaseController(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        internal List<Repository> GetAllRepositories()
        {
            return Context.Repository.Include(x => x.Projects).ToList();
        }

        internal List<string> GetTables()
        {
            var tableNames = new List<string>();

            using (var connection = Context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Distinct TABLE_NAME FROM information_schema.TABLES";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableNames.Add(reader.GetString(0));
                            }
                        }
                        reader.Close();
                    }
                }
                connection.Close();
            }
            return tableNames;
        }

        internal List<WorkItem> GetAllWorkItemsForProject(int id)
        {
            return Context.AssociatedBoardWorkItems.Include(x => x.WorkItem)
                                                   .Where(x => x.ProjectId == id)
                                                   .Select(x => x.WorkItem)
                                                   .ToList(); 
        }
 
        internal List<Projects> AuthenicatedUser(string key, string username, string password)
        {
            var user = default(UserAccounts);
            if(!string.IsNullOrEmpty(key))
                user = Context.UserAccounts.FirstOrDefault(x => x.AccessToken == key);
            else
            {
                user = Context.UserAccounts
                                .FirstOrDefault(x => x.Email == username);

                if (user == null)
                    return null;

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                  password: password,
                  salt: Convert.FromBase64String(user.Salt),
                  prf: KeyDerivationPrf.HMACSHA1,
                  iterationCount: 10000,
                  numBytesRequested: 256 / 8));



                if (hashed != user.Password)
                    user = null;
            }

            if (user == null)
                return null;

            var userProjects = Context.AssociatedProjectMembers.Include(x => x.Project)
                                                               .Include(x => x.UserAccount)
                                                               .Where(x => x.UserAccountId == user.Id)
                                                               .Select(x => x.Project)
                                                               .ToList();
            return userProjects;
        }

        internal int GetProjectByOrganization(string data)
        {
            var project = Context.Projects.FirstOrDefault(x => x.OrganizationName.ToLower() == data.ToLower());
            if (project == null)
                return 0;

            return project.Id;
        }

        internal Projects GetOrganizationName(int projectId)
        {
            return Context.Projects.FirstOrDefault(x => x.Id == projectId);
        }

        internal string GetPremadeName(int id) => Context.PremadeWidgets.FirstOrDefault(x => x.Id == id).ViewComponentName;

        internal List<BindingQueryProperty> GetTableProperties(string phase)
        {
            var found = default(bool);
            var result = new List<BindingQueryProperty>();
            foreach (var entityType in Context.Model.GetEntityTypes())
            {
                if(found)
                    return result;
                if(entityType.Name.Equals($"Rokono_Control.Models.{phase}"))
                {
                    found = true;
                    foreach (var propertyType in entityType.GetProperties())
                    {
                        var fieldName = propertyType.GetColumnName();
                        var fieldType =propertyType.GetColumnType();
                    
                        result.Add(new BindingQueryProperty{
                            Label = fieldName,
                            Field = fieldName,
                            Type =  GetPropertyBindingType(fieldType),
                            Format = GetPropertyFormat(fieldType),
                            Values = GetPropertyDefaultvalue(fieldType)
                        });
                    }
                }
            }
            return result;
        }

        internal List<PremadeWidgets> GetPremadeWidgets()
        {
            return Context.PremadeWidgets.ToList();
        }

        internal void UpdateWidgetResized(IncomingIdRequest request)
        {
            var associationVal = int.Parse(request.__RequestVerificationToken.Replace("layout_",""));
            var values = request.Phase.Split(",");
            var association = Context.AssociatedUserDashboardPremade.FirstOrDefault(x=>x.Id == associationVal);
             association.DataCol = int.Parse(values[0] == "NaN" ? "0" : values[0]);
            association.DataRow = int.Parse(values[1] == "NaN" ? "0" : values[1]);
            association.DataSizeX = int.Parse(values[2] == "NaN" ? "0" : values[2]);
            association.DataSizeY = int.Parse(values[3] == "NaN" ? "0" : values[3]);
            Context.Attach(association);
            Context.Update(association);
            Context.SaveChanges();
        }

        internal (int, string) CheckProjectAccess(int projectId, string domain)
        {
            var getProject = Context.Projects.Include(x=>x.AssociatedProjectIterations).FirstOrDefault(x => x.Id == projectId);
            if (getProject == null)
                return (0, "");

            return getProject.PublicBoard == 0 ? (0, "") : (1, $"https://{domain}/Boards/PublicBoard?projectId={getProject.Id}&iteration={getProject.AssociatedProjectIterations.FirstOrDefault(x=>x.ActiveIteration == 1).IterationId}&person=0");
        }

        internal PremadeWidgets SaveWidgetToBoard(IncomingIdRequest request)
        {
            Context.AssociatedUserDashboardPremade.Add(new AssociatedUserDashboardPremade{
                UserDashboard = request.Id,
                PremadeWidgetId  = request.WorkItemType,

            });
            Context.SaveChanges();
            return Context.PremadeWidgets.FirstOrDefault(x=>x.Id == request.WorkItemType);
        }

        internal int AddUserQuery(IncomingWidgetCreatorRequest request, int userId)
        {
            var query =string.Empty;
            query = $"select * from {request.TableName} where ";
            request.rule.Rules.ForEach(x=>{
                query +=  $"{x.field} {GetOperator(x.Operator.ToLower())} "; 
                var data = IsList(x.value);
                if(data.Count > 0)
                {
                    var i = 0;
                    data.ForEach(y=>{
                        if(i == 0)
                            query += $"{y}";
                        else
                            query += $" and {y}";
                        i++;
                    });
                }
                else
                    query += $"({x.value})";
            });
            var queryData = Context.UserQueries.Add(new UserQueries{
                QueryData = query,
                QueryName = request.ControlName,
                UserId = userId,
                DateOfQuery = DateTime.Now
            });
            Context.SaveChanges();
            return queryData.Entity.Id;
        }

        internal PremadeWidgets SaveBurndownChart(IncomingIdRequest request, int userId)
        {
             Context.AssociatedUserDashboardPremade.Add(new AssociatedUserDashboardPremade{
                UserDashboard = request.Id,
                PremadeWidgetId  = request.WorkItemType,
                CustomSettings  = request.Phase
            });
            Context.SaveChanges();
            return Context.PremadeWidgets.FirstOrDefault(x=>x.Id == request.WorkItemType);
 
        }

        public List<string> IsList(object o)
        {
            var result  = new List<string>();
            if(o.ToString().Contains('[') || o.ToString().Contains(']'))
            {
                var local = o.ToString().ToList();
                local.ForEach(x=>{
                    if(Char.IsDigit(x))
                        result.Add(x.ToString());
                });
            }
            return result;
        }
        private string GetOperator(string current)
        {
            var result = string.Empty;
            switch(current)
            {
                case "equal":
                    result = "=";
                break;
                case "notequal":
                    result = "!=";
                break;
                case "greaterthanorequal":
                    result = ">=";
                break;
                case "lessthanorequal":
                    result = "<=";
                break;
                case "greaterthan":
                    result = ">";
                break;
                case "lessthan":
                    result = "<";
                break;
                case "notbetween":
                    result = "Not Between";
                break;
                case "between":
                    result = "Between";
                break;
                case "in":
                    result = "IN";
                break;
                case "notin":
                    result = "NOT IN";
                break;
                case "isnull":
                    result = "Is Null";
                break;
                case "isnotnull":
                    result = "Is Not Null";
                break;
                case "isempty":
                    result = "Is Null";
                break;
                case "isnotempty":
                    result = "Is Not Null";
                break;
                case "startswith":
                    result = "LIKE";
                break;
                case "endswith":
                    result = "LIKE";
                break;
                case "contains":
                    result = "CONTAINS";
                break;
            }
            return result;
        }

        private string[] GetPropertyDefaultvalue(string fieldType)
        {
            if(fieldType == "boolean")
                return new string[2]{"True", "False"};

            return null;
                
        }

        private string GetPropertyFormat(string fieldType)
        {
            if(fieldType == "DateTime")
                return "dd/MM/yyyy";

            return string.Empty;
        }

        private string GetPropertyBindingType(string getColumnType)
        {
            var result = string.Empty;
            switch(getColumnType)
            {
                case "int":
                    result = "number";
                break;
                case "string":
                    result = "string";
                break;
                case "datetime":
                    result = "date";
                break;
                case "boolean":
                    result = "boolean";
                break;
                default:
                    result = "string";
                break;
            }   
            return result;
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
        internal DataTable GetUserQueryData(int userId, int queryId)
        {
            var table = new DataTable();
            var queryData = Context.UserQueries.FirstOrDefault(x=>x.Id == queryId && x.UserId == userId);
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = queryData.QueryData;
                Context.Database.OpenConnection();
                 // Use the NewRow method to create a DataRow with
              
                using (var result = command.ExecuteReader())
                {
                    if(result.HasRows)
                    {
                        while(result.Read())
                        {
                            var row = table.NewRow();
                            for(var i = 0; i < result.FieldCount; i++)
                            {
                                var column = result.GetName(i);
                                var value = result.GetValue(i);
                                // Set values in the columns:
                                if(!table.Columns.Contains(column))
                                    table.Columns.Add(new DataColumn{
                                        ColumnName = column
                                    });
                                row[column] = value;
                            }
                            table.Rows.Add(row);
                        }
                    }
                }
            }
            return table;
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
                        FolderPath = $"{Program.Configuration.LocalRepo}/{currentProject.ProjectName}"
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
                        BoardId = boardBacklog.Entity.Id,
                        OrganizationName = currentProject.OrganizationName
                    });
                    Context.SaveChanges();
                    var first = default(int);
                    currentProject.Iterations.ForEach(x =>
                    {
                        var iteration = x;
                        iteration.IsActive = first == 0 ? 1 : 0; 
                        var currentIteration = Context.WorkItemIterations.Add(iteration);
                        if(iteration.IsActive == 1)
                        {
                            int unixTimestampStart = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                            var endTime = (int)(DateTime.UtcNow.AddMonths(1).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                            iteration.StartDate = unixTimestampStart;
                            iteration.EndDate = endTime;
                        }
                        Context.SaveChanges();
                        Context.AssociatedProjectIterations.Add(new AssociatedProjectIterations
                        {
                            ProjectId = project.Entity.Id,
                            IterationId = currentIteration.Entity.Id,
                            ActiveIteration = first == 0 ? 1 : 0
                        });
                        Context.SaveChanges();
                        first++;
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