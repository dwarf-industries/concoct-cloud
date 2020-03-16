using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Platform.DataHandlers;
using Platform.Models;
using Rokono_Control;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;
using RokonoControl.Models;

namespace RokonoControl.Controllers
{
    public class BoardsController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;

        public BoardsController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IActionResult Index(int projectId)
        {
            var currentUser = this.User;
            var id = currentUser.Claims.ElementAt(1);

            using (var context = new DatabaseController(Context,Configuration))
            {
                ViewData["ProjectId"] = projectId;
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
                ViewData["ProjectName"] = context.GetProjectName(projectId);
            }
            return View();
        }

        public IActionResult ProjectBacklog(int projectId, int workItemType)
        {
            var currentUser = this.User;
            var id = currentUser.Claims.ElementAt(1);

            using (var context = new DatabaseController(Context,Configuration))
            {
                ViewData["Projects"] = context.GetUserProjects(int.Parse(id.Value));

                ViewData["ProjectId"] = projectId;
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
                ViewData["ProjectName"] = context.GetProjectName(projectId);
                ViewData["WorkItemType"] = workItemType;
                ViewData["GetSelectedWorkItem"] = context.GetWorkItemName(workItemType);


            }
            return View();
        }
        public IActionResult SprintBacklogs(int projectId, int boardId)
        {
            var currentUser = this.User;
            var id = currentUser.Claims.ElementAt(1);

            using (var context = new DatabaseController(Context,Configuration))
            {
                ViewData["Projects"] = context.GetUserProjects(int.Parse(id.Value));
                ViewData["ProjectId"] = projectId;
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
                ViewData["ProjectName"] = context.GetProjectName(projectId);

            }
            return View();
        }

        public IActionResult Sprints(int projectId, int iteration, int person)
        {
            var currentUser = this.User;
            var id = currentUser.Claims.ElementAt(1);

            using (var context = new DatabaseController(Context,Configuration))
            {
                ViewData["ProjectId"] = projectId;
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
                ViewData["ProjectName"] = context.GetProjectName(projectId);
                ViewData["Iteration"] = iteration;
                ViewData["Person"] = person;
                ViewData["GetUserViewRights"] = context.CheckUserViewWorkitemRights(int.Parse(id.Value), projectId);

            }
            return View();
        }

        public IActionResult PublicBoard(int projectId, int iteration, int person)
        {
           var viewRights = default(bool);
            using (var context = new DatabaseController(Context,Configuration))
            {
                viewRights = context.GetPublicBoardRights(projectId);
                if(viewRights)
                {
                    ViewData["ProjectId"] = projectId;
                    ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
                    ViewData["ProjectName"] = context.GetProjectName(projectId);
                    ViewData["Iteration"] = iteration;
                    ViewData["Person"] = person;
                    ViewData["GetUserViewRights"] = 1;
                }

            }
            var view =  viewRights ? View() : View("~/Views/Home/Error.cshtml");
            return view;
        }


        [HttpGet]
        public List<BindingCards> GetWorkItems(int projectId, int workItemType)
        {
            var result = new List<BindingCards>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetProjectCards(projectId, workItemType);
            }
            return result;
        }
        [HttpPost]
        public List<OutgoingIterationModel> GetIterations([FromBody] IncomingIterationRequest request)
        {
            var result = new List<OutgoingIterationModel>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                var dataResult = context.GetProjectIterations(request.ProjectId);
                dataResult.ForEach(x =>
                {
                    var boardName = request.IsPublic ? "PublicBoard" : "Sprints";
                    result.Add(new OutgoingIterationModel
                    {
                        Text = x.IterationName,
                        IconCss = "e-ddb-icons e-settings",
                        Url = $"/Boards/{boardName}?projectId={request.ProjectId}&&workItemType=7&&iteration={x.Id}&&person=0"
                    });
                });
            }
            return result;
        }
        [HttpPost]
        public List<OutgoingIterationModel> GetPersons([FromBody] IncomingIterationRequest request)
        {
            var result = new List<OutgoingIterationModel>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                var dataResult = new List<UserAccounts>();
                var currentUser = this.User;
                var email = currentUser.Claims.LastOrDefault().Value;
                var userRights = context.GetUserAccounts(int.Parse(email));
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
                    dataResult = context.GetProjectPerons(request.ProjectId);
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
            using (var context = new DatabaseController(Context,Configuration))
            {
                var currentUser = this.User;
                var email = currentUser.Claims.LastOrDefault().Value;
                var userRights = context.GetUserAccounts(int.Parse(email));
                if (userRights != null)
                    result = context.GetProjectSprints(dataRequest, userRights.ProjectRights == 1 ? true : false, userRights.Id);
            }
            return result;
        }

        [HttpPost]
        public List<BindingCards> GetSprintsPublic([FromBody] IncomingSprintRequest dataRequest)
        {
            var result = new List<BindingCards>();
            using (var context = new DatabaseController(Context,Configuration))
            {
             
                result = context.GetProjectSprints(dataRequest, true, 0);
            }
            return result;
        }
        [HttpPost]
        public bool ChangeWorkItemBoard([FromBody] IncomingCardRequest card)
        {
            using (var context = new DatabaseController(Context,Configuration))
            {
                context.ChangeWorkItemBoard(card);
            }
            return true;
        }

        [HttpPost]
        public bool ChangeCardOwner([FromBody] IncomingCardOwnerRequest card)
        {
            using (var context = new DatabaseController(Context,Configuration))
            {
                context.ChangeCardOwner(card);
            }
            return true;
        }

        [HttpPost]
        public OutgoingJsonData MakeBoardPublic([FromBody] IncomingPublicBoardRequest request)
        {
            var result = string.Empty;
            using(var context = new DatabaseController(Context,Configuration))
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
            using(var context = new DatabaseController(Context,Configuration))
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
            using(var context = new DatabaseController(Context,Configuration))
            {
                context.ImportExistingProject(data.Data);
            }
            return new OutgoingJsonData { Data = ""} ;
        }
    }
}