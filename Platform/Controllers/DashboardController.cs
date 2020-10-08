namespace Rokono_Control.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Models;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    using RokonoControl.Models;

    public class DashboardController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        private readonly AutherizationManager AutherizationManager;
        private int UserId;

        public DashboardController(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            var user = this.User;
            UserId = AutherizationManager.GetCurrentUser(UserId, httpContextAccessor.HttpContext.Request);
        }

        #region PageRenders
        public IActionResult Index()
        {

            using (var context = new WorkItemsContext(Context, Configuration))
                ViewData["CurrentIteration"] = context.GetProjectDefautIteration(5012);
            using (var context = new UsersContext(Context, Configuration))
            {

                ViewData["Projects"] = context.GetUserProjects(UserId);
                ViewData["Name"] = context.GetUsername(UserId);


            }
            return View();
        }
        public IActionResult AddNewProject(string user)
        {


            using (var context = new UsersContext(Context, Configuration))
            {
                ViewData["User"] = context.GetUsername(UserId);
            }
            return View();
        }
        public IActionResult AddNewWorkItem(int projectId, int workItemType, int parentId, string returnUrl, string title)
        {


            using (var context = new WorkItemsContext(Context, Configuration))
                ViewData["Iterations"] = context.GetProjectIterations(projectId);

            using (var context = new WorkItemsContext(Context, Configuration))
            {
                var workItemBytitle = context.GetWorkItemByTitle(title);
                if (workItemBytitle != null && parentId == 0)
                    parentId = workItemBytitle.Id;
                ViewData["Priorities"] = context.GetProjectPriorities(projectId);
                ViewData["Areas"] = context.GetProjectAreas(projectId);
                ViewData["Severities"] = context.GetProjectSeverities(projectId);
                ViewData["Activities"] = context.GetProjectActivities(projectId);
                ViewData["Reasons"] = context.GetProjectReasons(projectId);
                ViewData["Builds"] = context.GetProjectBuilds(projectId);
                ViewData["ValueAreas"] = context.GetProjectValueAreas(projectId);
                ViewData["Risks"] = context.GetProjectRisks(projectId);
                ViewData["WorkItemType"] = workItemType;
                ViewData["WorkItemTypeName"] = context.GetAllWorkItemTypes()
                                                      .FirstOrDefault(x => x.Id == workItemType)
                                                      .TypeName;
                ViewData["ProjectId"] = projectId;
                ViewData["ParentId"] = parentId;
                ViewData["ActiveIteration"] = context.GetProjectDefautIteration(projectId);
                ViewData["ReturnPath"] = returnUrl;
                ViewData["SystemInfoData"] = Request.Headers["User-Agent"].ToString();

            }
            using (var context = new UsersContext(Context, Configuration))
                ViewData["Projects"] = context.GetUserProjects(UserId);

            return View();
        }

        public IActionResult EditWorkItem(int projectId, int workItem, string returnUrl)
        {
            var defaultUserAccount = default(UserAccounts);
            using (var context = new UsersContext(Context, Configuration))
                defaultUserAccount = context.GetDefaultAccount();

            using (var context = new WorkItemsContext(Context, Configuration))
            {

                var workItemData = context.GetWorkItem(workItem, projectId);
                workItemData.DueDate = workItemData.DueDate.HasValue ? workItemData.DueDate.Value : new System.DateTime();
                workItemData.StartDate = workItemData.StartDate.HasValue ? workItemData.StartDate.Value : new System.DateTime();
                workItemData.EndDate = workItemData.EndDate.HasValue ? workItemData.EndDate.Value : new System.DateTime();
                workItemData.AssignedAccountNavigation = workItemData.AssignedAccountNavigation == null ? defaultUserAccount : workItemData.AssignedAccountNavigation;
                var currentWorkItem = workItemData;
                ViewData["Priorities"] = context.GetProjectPriorities(projectId);
                ViewData["Areas"] = context.GetProjectAreas(projectId);
                ViewData["Severities"] = context.GetProjectSeverities(projectId);
                ViewData["Activities"] = context.GetProjectActivities(projectId);
                ViewData["Reasons"] = context.GetProjectReasons(projectId);
                ViewData["Builds"] = context.GetProjectBuilds(projectId);
                ViewData["ValueAreas"] = context.GetProjectValueAreas(projectId);
                ViewData["Risks"] = context.GetProjectRisks(projectId);
                ViewData["Iterations"] = context.GetProjectIterations(projectId);
                ViewData["WorkItemFiles"] = context.GetWorkItemFiles(workItem);
                ViewData["WorkItemType"] = currentWorkItem.WorkItemTypeId;
                ViewData["WorkItemTypeName"] = context.GetAllWorkItemTypes()
                                        .FirstOrDefault(x => x.Id == currentWorkItem.WorkItemTypeId)
                                        .TypeName;
                ViewData["WorkItemData"] = currentWorkItem;
                ViewData["ProjectId"] = projectId;
                ViewData["ReturnPath"] = returnUrl;


            }
            return View();
        }

        public IActionResult ManageAccounts(int id)
        {
            using (var context = new UsersContext(Context, Configuration))
            {

                ViewData["UserAccounts"] = context.GetUserAccounts();
                ViewData["ProjectId"] = id;

            }
            return View();
        }

        public IActionResult ProjectDetails(int projectId, int dashboardId)
        {
            ViewData["ProjectId"] = projectId;
            ViewData["DashboardId"] = dashboardId;
            using (var context = new UsersContext(Context, Configuration))
            {
                ViewData["SavedWidgets"] = context.GetUserWidgets(UserId, projectId, dashboardId);
                ViewData["PremadeWidgets"] = context.GetUserPremadeWidgets(dashboardId);
            }
            return View();
        }

        public IActionResult EditAccount(int id, int projectId)
        {

            using (var context = new DatabaseController(Context, Configuration))
            {
                ViewData["UserAccount"] = context.GetSpecificUserEdit(id);
                ViewData["UserId"] = id;
                ViewData["ProjectId"] = projectId;
            }
            using (var context = new UsersContext(Context, Configuration))
            {
                ViewData["Projects"] = context.GetUserProjects(UserId);
                ViewData["UserRight"] = context.GetUserRights(id, projectId);

            }
            return View();
        }
        public IActionResult AddNewAccount()
        {
            using (var context = new WorkItemsContext(Context, Configuration))
                ViewData["Relationships"] = context.GetProjectRelationships();
            using (var context = new UsersContext(Context, Configuration))
                ViewData["Projects"] = context.GetUserProjects(UserId);

            return View();
        }
        public IActionResult AssignAccountProjects(int id)
        {
            using (var context = new DatabaseController(Context, Configuration))
            {
                ViewData["UserAccount"] = context.GetSpecificUserEdit(id);
                ViewData["UserId"] = id;
            }
            using (var context = new UsersContext(Context, Configuration))
                ViewData["Projects"] = context.GetUserProjects(UserId);

            return View();
        }


        public async Task<IActionResult> ProjectDashboardAsync(int id)
        {
            var project = default(Projects);
            using (var context = new DatabaseController(Context, Configuration))
                project = context.GetProjectData(id);

            using (var context = new UsersContext(Context, Configuration))
            {
                ViewData["ProjectMembers"] = context.GetProjectMembers(id);
                await RemovePastProjectClaimsAsync();
                var userRight = AutherizationManager.ValidateUserRights(id, UserId, context);
                await UpdateUserRightClaiimsAsync(userRight, project.ProjectTitle);
            }

            using (var context = new WorkItemsContext(Context, Configuration))
            {
                var initials = project.ProjectName.ToUpper().Substring(0, 2);
                ViewData["Project"] = project;
                ViewData["ProjectId"] = id;
                ViewData["Initials"] = initials;
                ViewData["WorkItemsCreated"] = context.GetCreatedWorkItemCount(id);
                ViewData["WorkItemsNew"] = context.GetWorkItemCountByType(id, 1);
                ViewData["WorkItemsActive"] = context.GetWorkItemCountByType(id, 2);
                ViewData["WorkItemsTesting"] = context.GetWorkItemCountByType(id, 3);
                ViewData["WorkItemsCompleated"] = context.GetWorkItemCountByType(id, 4);

            }

            return View();
        }

        private async Task UpdateUserRightClaiimsAsync(UserRights userRight, string projectName)
        {
            var principal = HttpContext.User;
            var list = new List<Claim>();
            if (userRight.ChatChannelsRule == 1)
                list.Add(new Claim(ClaimTypes.Role, "ChatAdministrator"));
            if (userRight.ManageIterations == 1)
                list.Add(new Claim(ClaimTypes.Role, "IterationManager"));
            if (userRight.ManageUserdays == 1)
                list.Add(new Claim(ClaimTypes.Role, "UserDays"));
            if (userRight.UpdateUserRights == 1)
                list.Add(new Claim(ClaimTypes.Role, "UpdateUserRights"));
            list.ForEach(x => {
                principal.Identities.FirstOrDefault().AddClaim(x);
            });
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                IsPersistent = true,
            };

            await HttpContext.SignOutAsync();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }

        private async Task RemovePastProjectClaimsAsync()
        {
            var identity = this.User.Identity as ClaimsIdentity;
            var principal = this.User;
            if (identity.TryRemoveClaim(identity.Claims.FirstOrDefault(x => x.Value == "ChatAdministrator")))
                principal.Claims.ToList().Remove(identity.Claims.FirstOrDefault(x => x.Value == "ChatAdministrator"));
            if (identity.TryRemoveClaim(identity.Claims.FirstOrDefault(x => x.Value == "IterationManager")))
                principal.Claims.ToList().Remove(identity.Claims.FirstOrDefault(x => x.Value == "IterationManager"));
            if (identity.TryRemoveClaim(identity.Claims.FirstOrDefault(x => x.Value == "UserDays")))
                principal.Claims.ToList().Remove(identity.Claims.FirstOrDefault(x => x.Value == "UserDays"));
            if (identity.TryRemoveClaim(identity.Claims.FirstOrDefault(x => x.Value == "UpdateUserRights")))
                principal.Claims.ToList().Remove(identity.Claims.FirstOrDefault(x => x.Value == "UpdateUserRights"));

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                IsPersistent = true,
            };

            await HttpContext.SignOutAsync();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }

        public IActionResult WorkItems(int projectId, int boardId)
        {

            using (var context = new DatabaseController(Context, Configuration))
            {
                ViewData["ProjectId"] = projectId;
                ViewData["BoardId"] = boardId;
            }
            using (var context = new UsersContext(Context, Configuration))
                ViewData["Name"] = context.GetUsername(UserId);

            return View();
        }
        public IActionResult ChangelogGenerator(int projectId)
        {
            ViewData["ProjectId"] = projectId;
            ViewData["BoardId"] = 0;
            return View();
        }
        #endregion

        #region AjaxRequests

        [HttpGet]
        public List<WorkItemRelations> GetWorkItemRelations()
        {
            var relationships = new List<WorkItemRelations>();
            using (var context = new WorkItemsContext(Context, Configuration))
                relationships = context.GetProjectRelationships();
            return relationships;
        }
        [HttpGet]
        public OutgoingBoundRelations GetAllWorkItemRelations(int workItemId, int projectId)
        {
            var result = default(OutgoingBoundRelations);
            using (var context = new WorkItemsContext(Context, Configuration))
            {
                result = context.GetAllWorkItemRelations(workItemId, projectId);
            }
            return result;
        }
        [HttpGet]
        public List<OutgoingBindingWorkItem> GetAllWorkItems(int projectId)
        {
            var result = new List<OutgoingBindingWorkItem>();
            using (var context = new WorkItemsContext(Context, Configuration))
            {
                result = context.GetAllWorkItems(projectId);
            }
            return result;
        }

        [HttpPost]
        public List<CommitChartBindingData> GetCommitChartBindingData([FromBody] IncomingIdRequest request)
        {
            var result = new List<CommitChartBindingData>();
            using (var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetCommitsChartForProject(request.Id);
            }
            return result;
        }

        [HttpGet]
        public object GetProjects()
        {
            var result = default(object);
            using (var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetProjects();
            }
            return result;
        }

        [HttpPost]
        public List<AssociatedWorkItemMessages> GetWorkItemDiscussions([FromBody] IncomingWorkItem request)
        {
            var result = new List<AssociatedWorkItemMessages>();
            using (var context = new WorkItemsContext(Context, Configuration))
            {
                result = context.GetWorkItemDiscussions(request.ProjectId, request.WorkItemId);
            }
            return result;
        }
        [HttpPost]
        public AssociatedWorkItemMessages WorkItemAddMessage([FromBody] IncomingWorkItemMessage request)
        {
            var result = default(AssociatedWorkItemMessages);

            using (var context = new WorkItemsContext(Context, Configuration))
            {
                result = context.AddNewWorkItemMessage(request, UserId);
                result.Message.AssociatedWorkItemMessages = null;
            }
            using (var context = new UsersContext(Context, Configuration))
                result.Message.Sender = context.GetUserAccount(result.Message.SenderId);

            return result;
        }
        //GetWorkItemDiscussions

        [HttpPost]
        public OutgoingProjectRules GetProjectUserRules([FromBody] IncomingProjectRulesRequest project)
        {
            var getProjectRules = default(OutgoingProjectRules);
            using (var context = new DatabaseController(Context, Configuration))
            {
                getProjectRules = context.GetProjectRules(int.Parse(project.ProjectId), int.Parse(project.UserId));
            }
            return getProjectRules;
        }




        [HttpPost]
        public OutgoingJsonData StageWorkItem([FromBody] IncomingWorkItem currentItem)
        {
            var result = new OutgoingJsonData { Data = "" };


            using (var context = new WorkItemsContext(Context, Configuration))
            {
                result = context.AddNewWorkItem(currentItem, UserId);
            }
            return result;
        }
        [HttpPost]
        public bool UpdateWorkItem([FromBody] IncomingWorkItem currentItem)
        {
            var result = default(bool);
            using (var context = new WorkItemsContext(Context, Configuration))
            {
                result = context.UpdateWorkItem(currentItem);
            }
            return result;
        }
        [HttpPost]
        public List<BindingUserAccount> GetUserAccounts([FromBody] IncomingIdRequest IncomingIdRequest)
        {
            var result = new List<BindingUserAccount>();
            using (var context = new UsersContext(Context, Configuration))
            {
                result = context.GetProjectMembers(IncomingIdRequest.Id);
            }
            return result;

        }
        [HttpPost]
        public bool RemoveUserFromProject([FromBody] IncomingRemoveUserFromProject userProject)
        {
            using (var context = new DatabaseController(Context, Configuration))
            {
                context.RemoveUserFromProject(userProject);
            }
            return true;

        }


        [HttpPost]
        public OutgoingValidWorkItem ValidateSelectedItem([FromBody] IncomingWorkItemRelation incomingRequest)
        {

            var result = new OutgoingValidWorkItem();
            var currentItem = default(WorkItem);
            var defaultUserAccount = default(UserAccounts);
            using (var context = new UsersContext(Context, Configuration))
                defaultUserAccount = context.GetDefaultAccount();

            using (var context = new WorkItemsContext(Context, Configuration))
            {
                result.WorkItem = new List<WorkItem>();
                if (!incomingRequest.LinkedItems.Any(x => x.WorkItemId == incomingRequest.WorkItemId) && incomingRequest.RelationType != "1")
                {
                    if (incomingRequest.CurrWorkItemId != 1)
                    {
                        var workItemData = context.GetWorkItemChildrenClean(incomingRequest.CurrWorkItemId);
                        var currentWorkItem = context.GetWorkItemClean(incomingRequest.CurrWorkItemId, incomingRequest.ProjectId);
                        if (currentWorkItem != null)
                        {
                            if (currentWorkItem.ParentId != 0)
                                result.WorkItem.Add(context.GetWorkItemClean(currentWorkItem.ParentId.Value, incomingRequest.ProjectId));
                        }
                        result.WorkItem = workItemData;

                    }
                    incomingRequest.LinkedItems.ForEach(x =>
                    {
                        result.WorkItem.Add(context.GetWorkItemClean(x.WorkItemId, incomingRequest.ProjectId));
                    });
                    currentItem = context.GetWorkItemById(incomingRequest.WorkItemId);
                    result.Last = currentItem;
                    result.Valid = true;
                    result.WorkItem.Add(context.GetWorkItemClean(incomingRequest.WorkItemId, incomingRequest.ProjectId));
                    result.WorkItemTypeId = incomingRequest.RelationType != null ? int.Parse(incomingRequest.RelationType) : 0;
                    result.WorkItemId = incomingRequest.WorkItemId;
                    result.RelationshipId = incomingRequest.RelationType;
                }
                else
                {

                    currentItem = context.GetWorkItemById(incomingRequest.WorkItemId);
                    result.Last = currentItem;
                    result.WorkItem.Add(context.AddChildrenToParent(incomingRequest.WorkItemId, incomingRequest.CurrWorkItemId));
                    result.Valid = true;
                    result.WorkItemTypeId = int.Parse(incomingRequest.RelationType);
                    result.WorkItemId = incomingRequest.WorkItemId;
                    result.RelationshipId = incomingRequest.RelationType;
                }
            }
            return result;
        }

        [HttpPost]
        public bool AssociatedWorkItemRelation([FromBody] IncomingWorkItemRelation incomingRelation)
        {
            using (var context = new WorkItemsContext(Context, Configuration))
            {
                context.AssociatedRelation(incomingRelation);
            }
            return true;
        }

        [HttpPost]
        public string AddProjectToUserAccount([FromBody] IncomingProjectUser incomingRequest)
        {
            using (var context = new DatabaseController(Context, Configuration))
            {
                context.AddProjectToUser(incomingRequest);
            }
            return "true";
        }

        [HttpPost]
        public bool AddNewProject([FromBody] IncomingProject currentProject)
        {
            var result = default(bool);
            using (var context = new UsersContext(Context, Configuration))
                currentProject.Users.Add(context.GetOutgoingUserAccount(UserId));
            using (var context = new DatabaseController(Context, Configuration))
                result = context.AddNewProject(currentProject, UserId);

            return result;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        //        [ValidateAntiForgeryToken]
        public IActionResult GetWorkItemFileUpload()
        {
            return ViewComponent("WorkItemFileUploader");
        }

        [HttpGet]
        public List<WorkItemAreas> GetWorkItemValueAreas()
        {
            var result = new List<WorkItemAreas>();
            using (var context = new WorkItemsContext(Context, Configuration))
                result = context.GetProjectAreas(0);
            return result;
        }
        [HttpGet]
        public List<WorkItemSeverities> GetWorkItemSeverities()
        {
            var result = new List<WorkItemSeverities>();
            using (var context = new WorkItemsContext(Context, Configuration))
                result = context.GetProjectSeverities(0);
            return result;
        }

        [HttpGet]
        public List<WorkItemReasons> GetWorkItemReasons()
        {
            var result = new List<WorkItemReasons>();
            using (var context = new WorkItemsContext(Context, Configuration))
                result = context.GetProjectReasons(0);
            return result;
        }
        #endregion
    }
}