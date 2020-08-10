namespace RokonoControl.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
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

    public class BoardsController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;

        AutherizationManager AutherizationManager {get; set;}
        private int UserId {get; set;}


        public BoardsController(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager,IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager) autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IActionResult Index(int projectId)
        {
            using (var context = new DatabaseController(Context,Configuration))
            {
                ViewData["ProjectId"] = projectId;
                ViewData["ProjectName"] = context.GetProjectName(projectId);
            }
            using(var context = new WorkItemsContext(Context,Configuration))
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();

            return View();
        }

        public IActionResult ProjectBacklog(int projectId, int workItemType,int iteration)
        {
            ViewData["CurrentIteration"] =  iteration;
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                ViewData["GetSelectedWorkItem"] = context.GetWorkItemName(workItemType);
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
            }
            using (var context = new DatabaseController(Context,Configuration))
            {
                ViewData["ProjectId"] = projectId;
                ViewData["ProjectName"] = context.GetProjectName(projectId);
                ViewData["WorkItemType"] = workItemType;
             
            }
            using(var context = new UsersContext(Context,Configuration))
                ViewData["Projects"] = context.GetUserProjects(UserId);
            return View();
        }
        public IActionResult SprintBacklogs(int projectId, int boardId)
        {
 
            using (var context = new DatabaseController(Context,Configuration))
            {
                ViewData["ProjectId"] = projectId;
                ViewData["ProjectName"] = context.GetProjectName(projectId);
            }
            using(var context = new WorkItemsContext(Context,Configuration))
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
            using(var context = new UsersContext(Context,Configuration))
                ViewData["Projects"] = context.GetUserProjects(UserId);
            return View();
        }

        public IActionResult Sprints(int projectId, int iteration, int person)
        {
 
            using (var context = new DatabaseController(Context,Configuration))
            {
                ViewData["ProjectId"] = projectId;
                ViewData["ProjectName"] = context.GetProjectName(projectId);
                ViewData["Iteration"] = iteration;
                ViewData["Person"] = person;

            }
            using(var context = new UsersContext(Context,Configuration))
                ViewData["GetUserViewRights"] = context.CheckUserViewWorkitemRights(UserId, projectId);

            using(var context = new WorkItemsContext(Context,Configuration))
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
            return View();
        }

        public IActionResult PublicBoard(int projectId, int iteration, int person)
        {
            var viewRights = default(bool);
            using(var context = new OutboundDetailsContext(Context,Configuration))
                viewRights = context.GetPublicBoardRights(projectId);
            using (var context = new DatabaseController(Context,Configuration))
            {
                if(viewRights)
                {
                    ViewData["ProjectId"] = projectId;
                    ViewData["ProjectName"] = context.GetProjectName(projectId);
                    ViewData["Iteration"] = iteration;
                    ViewData["Person"] = person;
                    ViewData["GetUserViewRights"] = 1;
                }

            }
            using(var context = new WorkItemsContext(Context,Configuration))
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
            var view =  viewRights ? View() : View("~/Views/Home/Error.cshtml");
            return view;
        }

        [HttpGet]
        [Authorize (Roles = "ChatAdministrator")]
         public IActionResult GetIterationChanger(int projectId) 
        {
            return ViewComponent("IterationManager", new IncomingIdRequest{
                ProjectId = projectId
            });
         }

        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
        public void CloseIteration([FromBody] IncomingIdRequest request)
        {
            var result = new List<BindingCards>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                context.CloseIteration(request.ProjectId,request.WorkItemType, request.Id);
            }
        }

        [HttpGet]
        public List<BindingCards> GetWorkItems(int projectId, int workItemType)
        {
            var result = new List<BindingCards>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                result = context.GetProjectCards(projectId, workItemType);
            }
            return result;
        }
        [HttpGet]
        public List<WorkItemTypes> GetWorkItemTypes()
        {
            var result = new List<WorkItemTypes>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                result = context.GetAllWorkItemTypes();
            }
            return result;
        }
        [HttpGet]
        public List<WorkItemStates> GetWorkItemStates()
        {
            var result = new List<WorkItemStates>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                result = context.GetWorkItemStates();
            }
            return result;
        }
        [HttpGet]
        public List<WorkItemIterations> GetProjectIterations(int projectId)
        {
            var result = new List<WorkItemIterations>();
            using (var context = new WorkItemsContext(Context,Configuration))
                result = context.GetProjectIterations(projectId);
                 
            return result;
        }
        [HttpPost]
        public List<OutgoingIterationModel> GetIterations([FromBody] IncomingIterationRequest request)
        {
            var result = new List<OutgoingIterationModel>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                var dataResult = context.GetProjectIterations(request.ProjectId);
                dataResult.ForEach(x =>
                {

                    var boardName = string.Empty;
                    if(request.Calling == "Sprints")
                        boardName = request.IsPublic ? "Boards/PublicBoard" : "Boards/Sprints";
                    else
                        boardName = request.Calling;
                    result.Add(new OutgoingIterationModel
                    {
                        Text = x.IterationName,
                        IconCss = "e-ddb-icons e-settings",
                        Url = $"/{boardName}?projectId={request.ProjectId}&&workItemType=7&&iteration={x.Id}&&person=0"
                    });
                });
            }
            return result;
        }
        [HttpPost]
        public List<OutgoingIterationModel> GetPersons([FromBody] IncomingIterationRequest request)
        {
            var result = new List<OutgoingIterationModel>();
            using (var context = new UsersContext(Context,Configuration))
            {
                var dataResult = new List<UserAccounts>();
  
                var userRights = AutherizationManager.ValidateUserRights(request.ProjectId, UserId,context);
                if (userRights != null)
                {
                    result.Add(new OutgoingIterationModel
                    {
                        Text = "All",
                        IconCss = "e-ddb-icons e-settings",
                        Url = $"/Boards/Sprints?projectId={request.ProjectId}&&workItemType=7&&iteration={request.Iteration}&&person=0"
                    });
                    result.Add(new OutgoingIterationModel
                    {
                        Text = "@Mine",
                        IconCss = "e-ddb-icons e-settings",
                        Url = $"/Boards/Sprints?projectId={request.ProjectId}&&workItemType=7&&iteration={request.Iteration}&&person={userRights.Id}"
                    });
                    dataResult = context.GetProjectPersons(request.ProjectId);
                }
                dataResult.ForEach(x =>
                {
                    result.Add(new OutgoingIterationModel
                    {
                        Text = x.GitUsername,
                        IconCss = "e-ddb-icons e-settings",
                        Url = $"/Boards/Sprints?projectId={request.ProjectId}&&workItemType=7&&iteration={request.Iteration}&&person={x.Id}"
                    });
                });
            }
            return result;
        }

        [HttpPost]
        public List<BindingCards> GetSprints([FromBody] IncomingSprintRequest dataRequest)
        {   
            var result = new List<BindingCards>();
            var userRights = default(UserAccounts);
            using(var context = new UsersContext(Context, Configuration))
                userRights = context.GetUserAccounts(UserId);
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                if (userRights != null)
                    result = context.GetProjectSprints(dataRequest, userRights.AssociatedProjectMemberRights.FirstOrDefault().Rights.ViewOtherPeoplesWork == 1 ? true : false, userRights.Id);
            }
            return result;
        }

        [HttpPost]
        public List<BindingCards> GetSprintsPublic([FromBody] IncomingSprintRequest dataRequest)
        {
            var result = new List<BindingCards>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
             
                result = context.GetProjectSprints(dataRequest, true, 0);
            }
            return result;
        }
        [HttpPost]
        public bool ChangeWorkItemBoard([FromBody] IncomingCardRequest card)
        {
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                context.ChangeWorkItemBoard(card);
            }
            return true;
        }

        [HttpPost]
        public bool ChangeCardOwner([FromBody] IncomingCardOwnerRequest card)
        {
            var getUserByName =  default(UserAccounts);
            var workItem = default(WorkItem);
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                context.ChangeCardOwner(card);
                var getId = card.CardId.Split(" ");
                var parse = int.Parse(getId[1]);
                workItem = context.GetWorkItemById(parse);
            }
            using(var context = new UsersContext(Context,Configuration))
                getUserByName = context.GetUserAccountByName(card.Name);
            using (var context = new NotificationContext(Context,Configuration))
                context.AddNewUserNotification(1,workItem,getUserByName.Id);
            
           
            return true;
        }

        [HttpPost]
        public OutgoingJsonData MakeBoardPublic([FromBody] IncomingPublicBoardRequest request)
        {
            var result = string.Empty;
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                var domain = Request.Host.Host;

                result = context.ChangeProjectBoardStatus(request, domain);
            }
            return new OutgoingJsonData{ Data = result};
        }

        [HttpPost]
        public OutgoingJsonData ExportWorkItems([FromBody] IncomingPublicBoardRequest request)
        {
            var result = default(OutboundBackupModel);
            var text  = string.Empty;
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                result = context.BackUpSpecificProject(request.ProjectId);
             
                text =Backupwriter.CreateBackup($"{result.CurrentProject.ProjectName}.json",result );

            }
            return new OutgoingJsonData{ Data = text};
        }
        [HttpGet]
        public bool LogRepository(string repoName)
        {
            // Program.InitCron(repoName);
            return true;
        }
        [HttpPost]
        public OutgoingJsonData ImportWorkItems([FromBody] OutgoingJsonData data)
        {
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                context.ImportExistingProject(data.Data);
            }
            return new OutgoingJsonData { Data = ""} ;
        }
    }
}