namespace Rokono_Control.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.Models;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    using RokonoControl.Models;

    public class DashboardController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        public DashboardController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }
        #region PageRenders
        public IActionResult Index()
        {
            var currentUser = this.User;
            var id = currentUser.Claims.ElementAt(1);
            using (var context = new DatabaseController(Context,Configuration))
            {
                var currentId = int.Parse(id.Value);
                ViewData["Projects"] = context.GetUserProjects(currentId);
                ViewData["Name"] = context.GetUsername(currentId);


            }
            return View();
        }
        public IActionResult AddNewProject(string user)
        {
            var currentUser = this.User;
            var id = currentUser.Claims.ElementAt(1);

            using (var context = new DatabaseController(Context,Configuration))
            {
                var currentId = int.Parse(id.Value);
                ViewData["User"] = context.GetUsername(currentId);
            }
            return View();
        }
        public IActionResult AddNewWorkItem(int projectId, int workItemType, int parentId, string returnUrl, string title)
        {
            var currentUser = this.User;
            var currentUserId = currentUser.Claims.ElementAt(1);


            using (var context = new DatabaseController(Context,Configuration))
            {
                var workItemBytitle = context.GetWorkItemByTitle(title);
                if(workItemBytitle != null && parentId == 0)
                    parentId = workItemBytitle.Id;
                ViewData["Priorities"] = context.GetProjectPriorities(projectId);
                ViewData["Areas"] = context.GetProjectAreas(projectId);
                ViewData["Iterations"] = context.GetProjectIterations(projectId);
                ViewData["Severities"] = context.GetProjectSeverities(projectId);
                ViewData["Activities"] = context.GetProjectActivities(projectId);
                ViewData["Reasons"] = context.GetProjectReasons(projectId);
                ViewData["Builds"] = context.GetProjectBuilds(projectId);
                ViewData["ValueAreas"] = context.GetProjectValueAreas(projectId);
                ViewData["Risks"] = context.GetProjectRisks(projectId);
                ViewData["WorkItemType"] = workItemType;
                ViewData["ProjectId"] = projectId;
                ViewData["ParentId"] = parentId;
                ViewData["Projects"] = context.GetUserProjects(int.Parse(currentUserId.Value));
                ViewData["ReturnPath"] = returnUrl;

            }
            return View();
        }

        public IActionResult EditWorkItem(int projectId, int workItem, string returnUrl)
        {
            var currentUser = this.User;
            var currentUserId = currentUser.Claims.ElementAt(1);

            using (var context = new DatabaseController(Context,Configuration))
            {
                var defaultUserAccount = context.GetDefaultAccount();

                var workItemData = context.GetWorkItem(workItem, projectId);
                workItemData.DueDate = workItemData.DueDate.HasValue ? workItemData.DueDate.Value : new System.DateTime();
                workItemData.StartDate = workItemData.StartDate.HasValue ? workItemData.StartDate.Value : new System.DateTime();
                workItemData.EndDate = workItemData.EndDate.HasValue ? workItemData.EndDate.Value : new System.DateTime();
                workItemData.AssignedAccountNavigation = workItemData.AssignedAccountNavigation == null ? defaultUserAccount : workItemData.AssignedAccountNavigation;
                var currentWorkItem = workItemData;
                ViewData["Priorities"] = context.GetProjectPriorities(projectId);
                ViewData["Areas"] = context.GetProjectAreas(projectId);
                ViewData["Iterations"] = context.GetProjectIterations(projectId);
                ViewData["Severities"] = context.GetProjectSeverities(projectId);
                ViewData["Activities"] = context.GetProjectActivities(projectId);
                ViewData["Reasons"] = context.GetProjectReasons(projectId);
                ViewData["Builds"] = context.GetProjectBuilds(projectId);
                ViewData["ValueAreas"] = context.GetProjectValueAreas(projectId);
                ViewData["Risks"] = context.GetProjectRisks(projectId);
                ViewData["WorkItemType"] = currentWorkItem.WorkItemTypeId;
                ViewData["WorkItemData"] = currentWorkItem;
                ViewData["ProjectId"] = projectId;
                ViewData["ReturnPath"] = returnUrl;


            }
            return View();
        }

        public IActionResult ManageAccounts(int id)
        {
            var currentUser = this.User;
            var currentUserId = currentUser.Claims.ElementAt(1);

            using (var context = new DatabaseController(Context,Configuration))
            {

                ViewData["UserAccounts"] = context.GetUserAccounts();
                ViewData["ProjectId"] = id;

            }
            return View();
        }

        public IActionResult ProjectDetails()
        {
            
            return View();
        }

        public IActionResult EditAccount(int id, int projectId)
        {
            var currentUser = this.User;
            var currentUserId = currentUser.Claims.ElementAt(1);

            using (var context = new DatabaseController(Context,Configuration))
            {
                ViewData["Projects"] = context.GetUserProjects(int.Parse(currentUserId.Value));


                ViewData["UserAccount"] = context.GetSpecificUserEdit(id);
                ViewData["UserId"] = id;
                ViewData["ProjectId"] = projectId;
                ViewData["UserRight"] = context.GetUserRights(id, projectId);
            }
            return View();
        }
        public IActionResult AddNewAccount()
        {
            var currentUser = this.User;
            var currentUserId = currentUser.Claims.ElementAt(1);

            using (var context = new DatabaseController(Context,Configuration))
            {
                ViewData["Projects"] = context.GetUserProjects(int.Parse(currentUserId.Value));


                ViewData["Relationships"] = context.GetProjectRelationships();


            }
            return View();
        }
        public IActionResult AssignAccountProjects(int id)
        {
            var currentUser = this.User;
            var currentUserId = currentUser.Claims.ElementAt(1);
            using (var context = new DatabaseController(Context,Configuration))
            {

                ViewData["UserAccount"] = context.GetSpecificUserEdit(id);
                ViewData["UserId"] = id;
                ViewData["Projects"] = context.GetUserProjects(int.Parse(currentUserId.Value));

            }
            return View();
        }


        public IActionResult ProjectDashboard(int id)
        {
            var currentUser = this.User;
            var currentUserId = currentUser.Claims.ElementAt(1);
            using (var context = new DatabaseController(Context,Configuration))
            {
                var project = context.GetProjectData(id);
                var initials = project.ProjectName.ToUpper().Substring(0, 2);
                var currentId = int.Parse(currentUserId.Value);

                ViewData["Project"] = project;
                ViewData["ProjectMembers"] = context.GetProjectMembers(id);
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

        public IActionResult WorkItems(int projectId, int boardId)
        {
            var currentUser = this.User;
            var id = currentUser.Claims.ElementAt(1);
            using (var context = new DatabaseController(Context,Configuration))
            {

    
                var currentId = int.Parse(id.Value);
                ViewData["ProjectId"] = projectId;
                ViewData["Name"] = context.GetUsername(currentId);
                ViewData["BoardId"] = boardId;
            }
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
        public List<OutgoingWorkItemSimple> UnassociatedChangelogItems(int projectId)
        {
            var result = new List<OutgoingWorkItemSimple>();
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetEmptyChangelogWorktItems(projectId).Select(y=> new OutgoingWorkItemSimple{
                    Id = y.Id,
                    Name = string.IsNullOrEmpty(y.Title) ? "" : y.Title ,
                    WorkItemTypeName = y.WorkItemType == null ? "" : y.WorkItemType.TypeName
                }).ToList();
            }
            return result;
        }
        [HttpGet]
        public List<WorkItemRelations> GetWorkItemRelations()
        {
            var relationships = new List<WorkItemRelations>();
            using (var context = new DatabaseController(Context,Configuration))
                relationships = context.GetProjectRelationships();
            return relationships;
        }
        [HttpGet]
        public OutgoingBoundRelations GetAllWorkItemRelations(int workItemId, int projectId)
        {
            var result = default(OutgoingBoundRelations);
            using (var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetAllWorkItemRelations(workItemId, projectId);
            }
            return result;
        }
        [HttpGet]
        public List<OutgoingBindingWorkItem> GetAllWorkItems(int projectId)
        {
            var result = new List<OutgoingBindingWorkItem>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetAllWorkItems(projectId);
            }
            return result;
        }

        [HttpPost]
        public List<CommitChartBindingData> GetCommitChartBindingData([FromBody] IncomingIdRequest request)
        {
            var result = new List<CommitChartBindingData>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetCommitsChartForProject(request.Id);
            }
            return result;
        }

        [HttpGet]
        public object GetProjects()
        {
            var result = default(object);
            using (var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetProjects();
            }
            return result;
        }

        [HttpPost]
        public List<AssociatedWorkItemMessages> GetWorkItemDiscussions([FromBody] IncomingWorkItem request)
        {
            var result = new List<AssociatedWorkItemMessages>();
            using(var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetWorkItemDiscussions(request.ProjectId, request.WorkItemId);
            }
            return result;
        }
        [HttpPost]
        public AssociatedWorkItemMessages WorkItemAddMessage([FromBody] IncomingWorkItemMessage request)
        {
            var result = default(AssociatedWorkItemMessages);
            var currentUser = this.User;
            var id = int.Parse(currentUser.Claims.ElementAt(1).Value);
            using(var context =new DatabaseController(Context,Configuration))
            {
                result = context.AddNewWorkItemMessage(request, id);
                result.Message.AssociatedWorkItemMessages = null;
                result.Message.Sender = context.GetUserAccount(result.Message.SenderId);
            }
            return result;
        }
        //GetWorkItemDiscussions

        [HttpPost]
        public OutgoingProjectRules GetProjectUserRules([FromBody] IncomingProjectRulesRequest project)
        {
            var getProjectRules = default(OutgoingProjectRules);
            using (var context = new DatabaseController(Context,Configuration))
            {
                getProjectRules = context.GetProjectRules(int.Parse(project.ProjectId), int.Parse(project.UserId));
            }
            return getProjectRules;
        }
 
 

     
        [HttpPost]
        public OutgoingJsonData StageWorkItem([FromBody] IncomingWorkItem currentItem)
        {
            var result = new OutgoingJsonData { Data = ""};

            var currentUser = this.User;
            var id = int.Parse(currentUser.Claims.ElementAt(1).Value);
            using (var context = new DatabaseController(Context,Configuration))
            {
                result = context.AddNewWorkItem(currentItem,id);
            }
            return result;
        }
        [HttpPost]
        public bool UpdateWorkItem([FromBody] IncomingWorkItem currentItem)
        {
            var result = default(bool);
            using (var context = new DatabaseController(Context,Configuration))
            {
                result = context.UpdateWorkItem(currentItem);
            }
            return result;
        }
        [HttpPost]
        public List<BindingUserAccount> GetUserAccounts([FromBody] IncomingIdRequest IncomingIdRequest)
        {
            var result = new List<BindingUserAccount>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetProjectMembers(IncomingIdRequest.Id);
            }
            return result;

        }
        [HttpPost]
        public bool RemoveUserFromProject([FromBody] IncomingRemoveUserFromProject userProject)
        {
            using (var context = new DatabaseController(Context,Configuration))
            {
                context.RemoveUserFromProject(userProject);
            }
            return true;

        }


        [HttpPost]
        public OutgoingValidWorkItem ValidateSelectedItem([FromBody] IncomingWorkItemRelation incomingRequest)
        {

            var result = new OutgoingValidWorkItem();

            using (var context = new DatabaseController(Context,Configuration))
            {
                var defaultUserAccount = context.GetDefaultAccount();
                result.WorkItem = new List<WorkItem>();
                if (!incomingRequest.LinkedItems.Any(x => x.WorkItemId == incomingRequest.WorkItemId))
                {
                    if (incomingRequest.CurrWorkItemId != 1)
                    {
                        var workItemData = context.GetWorkItemChildrenClean(incomingRequest.CurrWorkItemId);
                        var currentWorkItem = context.GetWorkItemClean(incomingRequest.CurrWorkItemId, incomingRequest.ProjectId);
                        if (currentWorkItem.ParentId != null)
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
                    result.Valid = true;
                    result.WorkItem.Add(context.GetWorkItemClean(incomingRequest.WorkItemId, incomingRequest.ProjectId));
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
            using (var context = new DatabaseController(Context,Configuration))
            {
                context.AssociatedRelation(incomingRelation);
            }
            return true;
        }

        [HttpPost]
        public string AddProjectToUserAccount([FromBody] IncomingProjectUser incomingRequest)
        {
            using (var context = new DatabaseController(Context,Configuration))
            {
                context.AddProjectToUser(incomingRequest);
            }
            return "true";
        }

        [HttpPost]
        public bool AddNewProject([FromBody] IncomingProject currentProject)
        {
            var result = default(bool);
            using (var context = new DatabaseController(Context,Configuration))
            {
                var currentUser = this.User;
                var userDto = currentUser.Claims.ElementAt(1);
                currentProject.Users.Add(context.GetOutgoingUserAccount(int.Parse(userDto.Value)));
                result = context.AddNewProject(currentProject, int.Parse(userDto.Value));
            }
            return result;
        }

        #endregion
    }
}