namespace Platform.DatabaseHandlers.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.Models;
    using RokonoControl.Models;

    public class UsersContext  : IDisposable
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        private bool disposedValue;

        public UsersContext(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        internal UserAccounts GetDefaultAccount()
        {
            return Context.UserAccounts.FirstOrDefault(x => x.Id == 1);
        }
        internal UserAccounts GetUserAccount(int id)
        {
            return Context.UserAccounts.FirstOrDefault(x=>x.Id == id);
        }

        internal List<Teams> GetPojectTeams(int projectId)
        {
            return Context.Teams.Where(x => x.ProjectId == projectId)
                                .ToList();
        }

        internal OutgoingUserAccounts GetOutgoingUserAccount(int v)
        {
            var account = Context.UserAccounts.FirstOrDefault(x => x.Id == v);
            return new OutgoingUserAccounts
            {
                AccountId = account.Id,
                ChatChannels = 1,
                EditUserRights = 1,
                IterationOptions = 1,
                ScheduleManagement = 1,
                ViewWorkItems = 1,
                WorkItemOption = 1
            };
        }

        internal List<UserQueries> GetSharedQueries(int projectId)
        {
            return Context.UserQueries.Where(x=>x.IsShared == 1 && x.ProjectId == projectId).ToList();
        }

        internal NotificationRights GetProectNotificationSetting(string phase, int projectId, int userId)
        {
            var result = new NotificationRights();
            var right = Context.AssociatedAccountProjectNotificationRights.Include(x => x.Right)
                                                    .FirstOrDefault(x=>x.ProjectId == projectId && x.UserId == userId);
            if(right == null)
                result = AddNewUserNotificationSetting(projectId, userId);

            result = right.Right;
            return result;
        }

        internal int GetUserDefaultDashboard(int userId, int projectId)
        {
            var dashboard = Context.UserDashboards.FirstOrDefault(x=>x.UserId == userId);
            if(dashboard == null)
                return GenerateDefaultDashboard(userId, projectId);
            return dashboard.Id;
        }

        private int GenerateDefaultDashboard(int userId, int projectId)
        {
            var dashboard = Context.UserDashboards.Add(new UserDashboards{
                DashboardName = "Default Dashboard",
                DateOfDashboard = DateTime.Now,
                ProjectId = projectId,
                UserId = userId
            });
            Context.SaveChanges();
            return dashboard.Entity.Id;
        }

        internal NotificationRights AddNewUserNotificationSetting(int projectId, int userId)
        {
            NotificationRights right = Context.NotificationRights.Add(new NotificationRights
            {
                BugReportNenabled = 0,
                ChanegelogNenabled = 0,
                ChatChannelNenabled = 0,
                CreateWorkItemNenabled = 0,
                FeedbackNenabled = 0,
                PersonalMessageNenabled = 0,
                PublicDiscussionMnenabled = 0,
                UpdateWorkItemNenabled = 0

            }).Entity;
            Context.SaveChanges();
            Context.AssociatedAccountProjectNotificationRights.Add(new AssociatedAccountProjectNotificationRights
            {
                ProjectId = projectId,
                RightId = right.Id,
                UserId = userId
            });
            Context.SaveChanges();
            return right;
        }

        internal int CheckUserViewWorkitemRights(int userId, int projectId)
        {
            return Context.AssociatedProjectMemberRights.Include(x => x.Rights)
                                                        .FirstOrDefault(x => x.UserAccountId == userId
                                                        && x.ProjectId == projectId)
                                                        .Rights.ViewOtherPeoplesWork == 1 ? 1 : 0;
        }

        internal List<BindingUserAccount> GetProjectMembers(int projectId)
        {
            return Context.UserAccounts
            .Include(x => x.AssociatedProjectMembers)
            .Where(x => x.AssociatedProjectMembers.Any(y => y.ProjectId == projectId)).Select(x => new BindingUserAccount
            {
                Email = x.Email,
                AliasName = $"{x.FirstName.FirstOrDefault()}.{x.LastName.FirstOrDefault()}",
                Id = x.Id
            })
            .ToList();
        }

        internal void UpdateUserNotificationRight(int userId, int projectId, int id, string phase)
        {
            var right = Context.AssociatedAccountProjectNotificationRights.Include(x => x.Right)
                                                    .FirstOrDefault(x=>x.ProjectId == projectId && x.UserId == userId);
            if(right == null)
                AddNewUserNotificationSetting(projectId, userId);
            else
            {
               var current = UpdateNotificationRightValue(right.Right,phase, id);
               Context.Attach(current);
               Context.Update(current);
               Context.SaveChanges();
            }
            
        }

        private NotificationRights UpdateNotificationRightValue(NotificationRights right, string phase, int id)
        {
            switch(phase)
            {
                case "PersonalMessage":
                    right.PersonalMessageNenabled = id;
                break;
                case "NewWorkItem":
                    right.CreateWorkItemNenabled = id;
                break;
                case "UpdatedWorkItem": 
                    right.UpdateWorkItemNenabled = id;
                break;
                case "PublicFeedback":
                    right.FeedbackNenabled = id;
                break;
                case "PublicBugreport":
                    right.BugReportNenabled = id;
                break;
                case "PublicDiscussion":
                    right.PublicDiscussionMnenabled = id;
                break;
                case "ChatChannelMessage":
                    right.ChatChannelNenabled = id;
                break;
                case "ChangelogGenerated":
                    right.ChanegelogNenabled = id;
                break;
            }
            return right;
        }

        internal List<UserAccounts> GetProjectPersons(int projectId)
        {
            return Context.AssociatedProjectMembers.Include(x => x.UserAccount).Where(x => x.ProjectId
            == projectId).Select(x => x.UserAccount).ToList();
        }


        internal UserRights GetUserRights(int id, int projectId)
        {
            var rights = Context.AssociatedProjectMemberRights
                          .Include(x=>x.Rights)
                          .Where(x=>x.ProjectId == projectId && x.UserAccountId == id)
                          .ToList()
                          .LastOrDefault();
            if(rights != null)
                return rights.Rights;
            rights =  Context.AssociatedProjectMemberRights
                        .Include(x=>x.Rights)
                        .FirstOrDefault(x=>x.ProjectId == projectId && x.UserAccountId == id);
             if(rights != null)
                return rights.Rights;
            return null;
        }

        internal List<AssociatedUserDashboardPremade> GetUserPremadeWidgets(int dashboard)
        {
            return Context.AssociatedUserDashboardPremade.Include(x => x.PremadeWidget)
                                                         .Where(x => x.UserDashboard == dashboard)
                                                         .ToList();
        }

        internal UserDashboards GetUserWidgets(int userId, int projectId,int dashboard)
        {
            return Context.UserDashboards.Include(x => x.UserDashboardItem)
                                         .ThenInclude(UserDashboardItem => UserDashboardItem.AssociatedUserDashboardItemComponent)
                                         .ThenInclude(AssociatedUserDashboardItemComponent => AssociatedUserDashboardItemComponent.ItemComponentNavigation)
                                         .FirstOrDefault(x=>x.ProjectId == projectId && x.Id == dashboard && x.UserId == userId);
        }

        internal UserAccounts GetUserAccountByName(string name)
        {
            return Context.UserAccounts.FirstOrDefault(x=>x.Email == name);
        }
        internal List<OutgoingUserAccounts> GetProjectUsers(int projectId)
        {
            return Context.AssociatedProjectMembers.Include(x=>x.UserAccount).Where(x=>x.ProjectId == projectId).Select(x=> new OutgoingUserAccounts{
                Name = x.UserAccount.Email,
                AccountId = x.UserAccount.Id
            }).ToList();
        }

        internal UserAccounts GetUserAccounts(int userId)
        {
            var result = Context.AssociatedProjectMembers.Include(x => x.UserAccount)
                                                         .Include(x => x.UserAccount.AssociatedProjectMemberRights)
                                                         .ThenInclude(AssociatedProjectMemberRights => AssociatedProjectMemberRights.Rights)
                                                         .FirstOrDefault(x => x.UserAccount.Id == userId);
            return result != null ? result.UserAccount : null;
        }

        internal void AddProjectInvitation(IncomingProjectAccount projectAccount)
        {
            var accountId = AddUserAccount(new IncomingNewUserAccount{
                Email = projectAccount.email,
                Password = projectAccount.password,
                ProjectRights = false,
                FirstName = "Unassigned",
                LastName = "Unassigned"
            });
            var projectRepository = Context.Projects.FirstOrDefault(x=> x.Id == projectAccount.ProjectId).RepositoryId;
            Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers{
                ProjectId = projectAccount.ProjectId,
                RepositoryId = projectRepository,
                UserAccountId = accountId,
            });
            Context.SaveChanges();
            var commonRightsId = default(int);
            var getCommonRights  = Context.UserRights.FirstOrDefault(x=>x.ManageIterations == projectAccount.accountRights.IterationOptions && 
                                                                        x.ChatChannelsRule == projectAccount.accountRights.ChatChannels &&
                                                                        x.ManageUserdays == projectAccount.accountRights.ScheduleManagement &&
                                                                        x.UpdateUserRights == projectAccount.accountRights.EditUserRights &&
                                                                        x.ViewOtherPeoplesWork == projectAccount.accountRights.ViewWorkItems &&
                                                                        x.WorkItemRule == projectAccount.accountRights.WorkItemOption);
            if(getCommonRights != null)
                commonRightsId = getCommonRights.Id;
            else
            {

                commonRightsId = Context.UserRights.Add(new UserRights{
                     ChatChannelsRule = Convert.ToInt16(projectAccount.accountRights.ChatChannels),
                     ManageIterations = Convert.ToInt16(projectAccount.accountRights.IterationOptions),
                     ViewOtherPeoplesWork = Convert.ToInt16(projectAccount.accountRights.ViewWorkItems),
                     ManageUserdays = Convert.ToInt16(projectAccount.accountRights.ScheduleManagement),
                     UpdateUserRights = Convert.ToInt16(projectAccount.accountRights.EditUserRights),
                     WorkItemRule = Convert.ToInt16(projectAccount.accountRights.WorkItemOption)
                 }).Entity.Id;
                Context.SaveChanges();
            }
            Context.AssociatedProjectMemberRights.Add(new AssociatedProjectMemberRights{
                ProjectId = projectAccount.ProjectId,
                UserAccountId = accountId,
                RightsId = commonRightsId
            });
            Context.SaveChanges();
        }


        internal int AddUserAccount(IncomingNewUserAccount user)
        {
            var account = Context.UserAccounts.Add(new UserAccounts
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProjectRights = user.ProjectRights ? 1 : 0,
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
                password: user.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            account.Entity.Salt = Convert.ToBase64String(salt);
            account.Entity.Password = hashed;
            Context.SaveChanges();
            return account.Entity.Id;
        }
        internal void AssociatedProjectExistingMembers(IncomingExistingProjectMembers accounts)
        {
            var projectRepository = Context.Projects.FirstOrDefault(x=> x.Id == accounts.ProjectId).RepositoryId;
            accounts.Accounts.ForEach(y=>{

            
                Context.AssociatedProjectMembers.Add(new AssociatedProjectMembers{
                    ProjectId = accounts.ProjectId,
                    RepositoryId = projectRepository,
                    UserAccountId = y.AccountId,
                });
                Context.SaveChanges();
                var commonRightsId = default(int);
                var getCommonRights  = Context.UserRights.FirstOrDefault(x=>x.ManageIterations == y.IterationOptions && 
                                                                            x.ChatChannelsRule == y.ChatChannels &&
                                                                            x.ManageUserdays == y.ScheduleManagement &&
                                                                            x.UpdateUserRights == y.EditUserRights &&
                                                                            x.ViewOtherPeoplesWork == y.ViewWorkItems &&
                                                                            x.WorkItemRule == y.WorkItemOption);
                if(getCommonRights != null)
                    commonRightsId = getCommonRights.Id;
                else
                {

                    commonRightsId = Context.UserRights.Add(new UserRights{
                        ChatChannelsRule = Convert.ToInt16(y.ChatChannels),
                        ManageIterations = Convert.ToInt16(y.IterationOptions),
                        ViewOtherPeoplesWork = Convert.ToInt16(y.ViewWorkItems),
                        ManageUserdays = Convert.ToInt16(y.ScheduleManagement),
                        UpdateUserRights = Convert.ToInt16(y.EditUserRights),
                        WorkItemRule = Convert.ToInt16(y.WorkItemOption)
                    }).Entity.Id;
                    Context.SaveChanges();
                }
                Context.AssociatedProjectMemberRights.Add(new AssociatedProjectMemberRights{
                    ProjectId = accounts.ProjectId,
                    UserAccountId = y.AccountId,
                    RightsId = commonRightsId
                });
                Context.SaveChanges();
            });
        }
        internal void DeleteAccountFromProject(IncomingProjectAccount accounts)
        {
            var association = Context.AssociatedProjectMembers.FirstOrDefault(x=>x.ProjectId == accounts.ProjectId && x.UserAccountId == accounts.AccountId);
            Context.AssociatedProjectMembers.Remove(association);
            Context.SaveChanges();
            var memberRights = Context.AssociatedProjectMemberRights.Where(x=>x.ProjectId == accounts.ProjectId && x.UserAccountId == accounts.AccountId).ToList();
            Context.AssociatedProjectMemberRights.RemoveRange(memberRights);
            Context.SaveChanges();
            var WorkItemsAssigned = Context.WorkItem.Where(x=>x.AssignedAccount == accounts.AccountId).ToList();
            WorkItemsAssigned.ForEach(x=>{
                var workItem = x;
                workItem.AssignedAccount =null;
                Context.Update(workItem);
                Context.SaveChanges();
            });
        }


        internal void UpdateUserAccount(IncomingUserAccountUpdate userData)
        {
            var userAccount = Context.UserAccounts.FirstOrDefault(x => x.Id == userData.Id);
            Context.Attach(userAccount);
            userAccount.Email = userData.Email;
            if (!string.IsNullOrEmpty(userData.Password))
            {

                 // generate a 128-bit salt using a secure PRNG
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

                // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: userData.Password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                userAccount.Salt = Convert.ToBase64String(salt);
                userAccount.Password = hashed;
                Context.Entry(userAccount).Property("Password").IsModified = true;
                Context.Entry(userAccount).Property("Salt").IsModified = true;


            }
            userAccount.FirstName = userData.FirstName;
            userAccount.LastName = userData.LastName;
            Context.Update(userAccount);
            Context.SaveChanges();
            var rights = default(UserRights);
            var getCommonRight = Context.UserRights.FirstOrDefault(x=> x.ManageIterations == userData.Rights.IterationOptions &&
                                                                    x.ManageUserdays == userData.Rights.ScheduleManagement &&
                                                                    x.UpdateUserRights == userData.Rights.EditUserRights &&
                                                                    x.ViewOtherPeoplesWork == userData.Rights.ViewWorkItems &&
                                                                    x.WorkItemRule == userData.Rights.WorkItemOption &&
                                                                    x.ChatChannelsRule == userData.Rights.ChatChannels);
            if(getCommonRight != null)
            {
                rights = getCommonRight;
            }
            else
            {
                var newRights = Context.UserRights.Add(new UserRights{
                    ManageIterations= Convert.ToInt16(userData.Rights.IterationOptions),
                    ManageUserdays = Convert.ToInt16(userData.Rights.ScheduleManagement),
                    ChatChannelsRule = Convert.ToInt16(userData.Rights.ChatChannels),
                    UpdateUserRights = Convert.ToInt16(userData.Rights.EditUserRights),
                    ViewOtherPeoplesWork = Convert.ToInt16(userData.Rights.ViewWorkItems),
                    WorkItemRule = Convert.ToInt16(userData.Rights.WorkItemOption)  
                });
                rights = newRights.Entity;
                Context.SaveChanges();
            }

            Context.AssociatedProjectMemberRights.Add(new AssociatedProjectMemberRights{
                ProjectId  = userData.ProjectId,
                RightsId = rights.Id,
                UserAccountId = userAccount.Id
            });
            Context.SaveChanges();
        }

        
        internal UserAccounts LoginUser(IncomingLoginRequest request)
        {
            var account = Context.UserAccounts
                                 .FirstOrDefault(x => x.Email == request.Email);
            if (account == null)
                return null;
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: request.Password,
              salt: Convert.FromBase64String(account.Salt),
              prf: KeyDerivationPrf.HMACSHA1,
              iterationCount: 10000,
              numBytesRequested: 256 / 8));



            if (hashed == account.Password)
                return account;
            else
                return null;
        }
        internal List<OutgoingAccountManagment> GetOutgoingManagmentAccounts(int projectId)
        {
            var accounts = Context.UserAccounts.Include(x => x.AssociatedProjectMembers)
                                        .ThenInclude(AssociatedProjectsMembers => AssociatedProjectsMembers.Project)
                                        
                                        .Where(x=>x.AssociatedProjectMembers.Any(y=>y.ProjectId == projectId))
                                        .ToList();
            return accounts.Select(x => new OutgoingAccountManagment
            {
                AccountId = x.Id,
                Name = $"{x.FirstName} {x.LastName}",
                Type = x.ProjectRights == 1 ? "Regular" : "Administrator",
                Email = x.Email,
                CreationDate = x.CreationDate,
            }).ToList();
        }
        internal string GetUsername(int currentId)
        {
            var account = Context.UserAccounts.FirstOrDefault(x => x.Id == currentId);
            return $"{account.FirstName} {account.LastName}";
        }

        internal List<OutgoingUserAccounts> GetUserAccounts()
        {
            return Context.UserAccounts.Select(x => new OutgoingUserAccounts
            {
                Name = $"{x.FirstName.FirstOrDefault()} {x.LastName}",
                AccountId = x.Id
            }).ToList();
        }

        internal List<Projects> GetUserProjects(int id)
        {
            return Context.Projects.Include(x => x.AssociatedProjectMembers)
                                    .ThenInclude(AssociatedProjectMembers => AssociatedProjectMembers.UserAccount)
                                    .Where(x => x.AssociatedProjectMembers.Any(y => y.UserAccountId == id)).ToList();
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
        // ~UsersContext()
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