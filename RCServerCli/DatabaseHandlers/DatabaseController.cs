using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RCServerCli.DataHandlers;
using RCServerCli.Models;

namespace RCServerCli.DatabaseHandlers
{
    public class DatabaseController : IDisposable
    {
        RokonoControlContext Context {get; set;}
        public DatabaseController()
        {
            Context = new RokonoControlContext();
        }

        internal void CreateUser(string user, string password, bool isAdmin)
        {
            var account = Context.UserAccounts.Add(new UserAccounts
            {
                Email = user,               
                FirstName = "user.FirstName",
                LastName = "user.LastName",
                ProjectRights = isAdmin ? 1 : 0,
                CreationDate = DateTime.Now
            });
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            account.Entity.Salt = Convert.ToBase64String(salt);
            account.Entity.Password = hashed;
            Context.SaveChanges();
        }
        internal void ListAllProjects()
        {
            var projects = Context.Projects.ToList();
            Console.WriteLine(projects.ToStringTable(new[] { "ID", "Project Name", "Members Count" },
                                                     a => a.Id,
                                                     a => a.ProjectTitle,
                                                     a => a.AssociatedProjectMembers.Count));
        }

        internal void GetProjectsForUser(int id)
        {
            System.Console.WriteLine($"Selected user: {Context.UserAccounts.FirstOrDefault(x=>x.Id == id).Email}");
            var userProjects = Context.AssociatedProjectMembers.Include(x => x.Project)
                                                               .Where(x => x.UserAccountId == id)
                                                               .Select(x => x.Project)
                                                               .ToList();
            System.Console.WriteLine("Projects:");
            Console.WriteLine(userProjects.ToStringTable(new[] { "ID", "Project Name", "Members Count" },
                                                      a => a.Id,
                                                      a => a.ProjectTitle,
                                                      a => a.AssociatedProjectMembers.Count));
        }

        internal void GetUserRightsForProject(int userId, int projectId)
        {
            var userRights = Context.AssociatedProjectMemberRights.Where(x=>x.ProjectId == projectId && x.UserAccountId == userId).LastOrDefault().Rights;
            System.Console.WriteLine($"Can create chat channels: {userRights.ChatChannelsRule}");
            System.Console.WriteLine($"View other peoples work: {userRights.ViewOtherPeoplesWork}");
            System.Console.WriteLine($"CRUD Work Items: {userRights.WorkItemRule}");
            System.Console.WriteLine($"Manage user schedules: {userRights.ManageUserdays}");
            System.Console.WriteLine($"Edit member rights: {userRights.UpdateUserRights}");
            System.Console.WriteLine($"CRUD Iterations: {userRights.ManageIterations}");

        }

        internal void ListAllUsers()
        {   
            var userAccounts = Context.UserAccounts.ToList();
            System.Console.WriteLine("Accounts:");
            Console.WriteLine(userAccounts.ToStringTable(new[] { "ID", "Email"},
                                                      a => a.Id,
                                                      a => a.Email));
        }

        internal void ChangeUserPassword(int userId, string passowrd)
        {
            var account = Context.UserAccounts.FirstOrDefault(x=>x.Id == userId);
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passowrd,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            account.Salt = Convert.ToBase64String(salt);
            account.Password = hashed;
            Context.Attach(account);
            Context.Update(account);
            Context.SaveChanges();
        }
        
        internal void GenerateMailPasswordRequest(int userId)
        {
            //TODO later
        }

        internal OutboundBackupModel BackUpSpecificProject(int projectId)
        {
            var project = Context.Projects.FirstOrDefault(x=>x.Id == projectId);
            var Iterations = new List<WorkItemIterations>();
            var members = new List<UserAccounts>();
            var boards = new List<Boards>();
            var workItems = new List<WorkItem>();
            var getProjectIterations = Context.AssociatedProjectIterations.Where(x=>x.ProjectId == projectId).ToList();
            getProjectIterations.ForEach(x=>{
                Iterations.Add(Context.WorkItemIterations.FirstOrDefault(y=>y.Id == x.IterationId));
            });
            var projectMembers = Context.AssociatedProjectMembers.Where(x=>x.ProjectId == projectId).ToList();
            projectMembers.ForEach(x=>{
                members.Add(Context.UserAccounts.FirstOrDefault(y=>y.Id == x.UserAccountId));
            });
            var memberRights = Context.AssociatedProjectMemberRights.Include(x => x.UserAccount)
                                                                    .Include(x => x.Rights)
                                                                    .Where(x => x.ProjectId == projectId)
                                                                    .ToList();
            var associatedProjectBoards = Context.AssociatedProjectBoards.Where(x=>x.ProjectId == projectId).ToList();
            associatedProjectBoards.ForEach(x=>{
                boards.Add(Context.Boards.FirstOrDefault(y=>y.Id == x.BoardId));
            });
            var assocaitedProjectWorkItems = Context.AssociatedBoardWorkItems.Where(x=>x.ProjectId == projectId).ToList();
            assocaitedProjectWorkItems.ForEach(x=>{
                workItems.Add(Context.WorkItem.FirstOrDefault(y=>y.Id == x.WorkItemId));
            });
            var assocaitedWorkItemChildren = Context.AssociatedWrorkItemChildren.Include(x => x.WorkItem)
                .ThenInclude(WorkItem => WorkItem.AssociatedBoardWorkItems)
                .Where(x=>x.WorkItem.AssociatedBoardWorkItems.Any(y=>y.ProjectId == projectId)).ToList();
            var result = new OutboundBackupModel{
                Iterations = Iterations,
                Boards  = boards,
                CurrentProject = project,
                MemberRights = memberRights,
                UserAccounts = members,
                WorkItems = workItems,
                WrorkItemChildrens = assocaitedWorkItemChildren
            };
            return result;
        }

        internal void ServerBackup()
        {
            var projects = Context.Projects.ToList();
            var projectResult = new List<OutboundBackupModel>();
            projects.ForEach(x=>{
                var projectData = BackUpSpecificProject(x.Id);
                projectResult.Add(projectData);
            });
            Backupwriter.CreateBackup("ServerBackup.json", JsonConvert.SerializeObject(projectResult));
            System.Console.WriteLine($"Project backup with the name ServerBackup,json has been created");

        }

        internal void RemoveUserFromProjectClean(int userId, int projectId)
        {
            throw new NotImplementedException();
        }

        internal void RemoveUserFromProjectAssing(int userId, int projectId, int secondUser)
        {
            throw new NotImplementedException();
        }



        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                    // TODO: dispose managed state (managed objects).
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