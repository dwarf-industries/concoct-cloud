namespace Rokono_Control.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Models;
    using Rokono_Control.Models;

    public class BacklogsController : Controller
    {
       RokonoControlContext Context;
        IConfiguration Configuration;

        AutherizationManager AutherizationManager {get; set;}
        private int UserId {get; set;}
        public BacklogsController(RokonoControlContext context, IConfiguration config,   IAutherizationManager autherizationManager,IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager) autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IActionResult Index(int projectId, int boardId, string phase, int iteration)
        {           
            ViewData["ProjectId"] = projectId;
            ViewData["BoardId"] = boardId;
            if(string.IsNullOrEmpty(phase))
                ViewData["Phase"] = "!";
            else
                ViewData["Phase"] = phase;

            ViewData["Iteration"] = iteration;
           
            return View();
        }
      
        //  [Authorize(Roles = "User")]
        [HttpPost]
        public List<OutgoingWorkItem> GetWorkItems([FromBody] IncomingIdRequest IncomingIdRequest)
        {

            var result = new List<OutgoingWorkItem>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                var data = context.GetProjectWorkItems(IncomingIdRequest.Id, IncomingIdRequest.WorkItemType, IncomingIdRequest.ProjectId);
                if(IncomingIdRequest.Phase != "!")
                    data = data.Where(x=>x.WorkItem.Title.Contains(IncomingIdRequest.Phase)).ToList();
                var bData = data.Select(x => x.WorkItem).ToList();
                bData.ForEach(x =>
                {
            
                        result.Add(new OutgoingWorkItem
                        {
                            Id = x.Id,
                            WorkItemIcon = x.WorkItemType.Icon,
                            Title = x.Title,
                            Description = x.Description,
                            AssignedTo = x.AssignedAccountNavigation == null ? "" : x.AssignedAccountNavigation.Email,
                            //    subtasks = new List<OutgoingWorkItem>()
                        });
                  
                });
                // result = GetChildren(data,result);
            }

            return result;
        }

        [HttpPost]
        public List<OutgoingWorkItem> GetUserWorkItems([FromBody] IncomingIdRequest IncomingIdRequest)
        {

            var result = new List<OutgoingWorkItem>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                var data = context.GetUserWorkItems(IncomingIdRequest.ProjectId, UserId);
                if(IncomingIdRequest.Phase != "!")
                    data = data.Where(x=>x.WorkItem.Title.Contains(IncomingIdRequest.Phase)).ToList();
                var bData = data.Select(x => x.WorkItem).ToList();
                bData.ForEach(x =>
                {
            
                        result.Add(new OutgoingWorkItem
                        {
                            Id = x.Id,
                            WorkItemIcon = x.WorkItemType.Icon,
                            Title = x.Title,
                            Description = x.Description,
                            AssignedTo = x.AssignedAccountNavigation == null ? "" : x.AssignedAccountNavigation.Email,
                            //    subtasks = new List<OutgoingWorkItem>()
                        });
                  
                });
                // result = GetChildren(data,result);
            }

            return result;
        }

        
        [HttpPost]
        public List<OutgoingWorkItem> GetPublicBugReports([FromBody] IncomingIdRequest IncomingIdRequest)
        {

            var result = new List<OutgoingWorkItem>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                var data = context.GetPublicBugReports(IncomingIdRequest.Id);
           
                var bData = data.Select(x => x.WorkItem).ToList();
                bData.ForEach(x =>
                {
            
                        result.Add(new OutgoingWorkItem
                        {
                            Id = x.Id,
                            WorkItemIcon = x.WorkItemType.Icon,
                            Title = x.Title,
                            Description = x.Description,
                            AssignedTo = x.AssignedAccountNavigation == null ? "" : x.AssignedAccountNavigation.Email,
                            //    subtasks = new List<OutgoingWorkItem>()
                        });
                  
                });
                // result = GetChildren(data,result);
            }

            return result;
        }



        [HttpPost]
        public WorkItem GetPublicBugReport([FromBody] IncomingIdRequest IncomingIdRequest)
        {

            var result = default(WorkItem);
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                result = context.GetPublicBugReport(IncomingIdRequest.Id);
            }

            return result;
        }
        
        [HttpPost]
        public List<OutgoingWorkItem> GetEmptyStories([FromBody] IncomingIdRequest IncomingIdRequest)
        {

            var result = new List<OutgoingWorkItem>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                var data = context.GetProjectWorkItems(IncomingIdRequest.Id, IncomingIdRequest.WorkItemType, IncomingIdRequest.ProjectId);
                var bData = data.Select(x => x.WorkItem).ToList();
                bData.ForEach(x =>
                {
                    if(x.AssociatedWrorkItemChildrenWorkItem.Count == 0 && !context.IsNotParent(x.Id))
                        result.Add(new OutgoingWorkItem
                        {
                            Id = x.Id,
                            WorkItemIcon = x.WorkItemType.Icon,
                            Title = x.Title,
                            Description = x.Description,
                            AssignedTo = x.AssignedAccountNavigation == null ? "" : x.AssignedAccountNavigation.Email,
                            //    subtasks = new List<OutgoingWorkItem>()
                        });
                  
                });
                // result = GetChildren(data,result);
            }

            return result;
        }

        [HttpPost]
        public OutgoingJsonData ItemsRemoved([FromBody] IncomingWorkItemRecycle Items)
        {
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                context.RemoveWorkItems(Items.Items);
            }
            return new OutgoingJsonData{ Data = ""};
        }

        [HttpPost]
        public OutgoingJsonData MakeWorkItemPrivate([FromBody] IncomingIdRequest request)
        {
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                context.MakeWorkItemPrivate(request.Id,0);
            }
            return new OutgoingJsonData{ Data = ""};
        }
        [HttpPost]
        public OutgoingJsonData MakeWorkItemPublic([FromBody] IncomingIdRequest request)
        {
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                context.MakeWorkItemPrivate(request.Id,1);
            }
            return new OutgoingJsonData{ Data = ""};
        }
        [HttpPost]
        public List<OutgoingWorkItem> GetBacklogWorkItems([FromBody] IncomingIdRequest IncomingIdRequest)
        {

            var result = new List<OutgoingWorkItem>();
            using (var context = new WorkItemsContext(Context,Configuration))
            {
                var data = context.GetProjectWorkItems(IncomingIdRequest.Id, IncomingIdRequest.WorkItemType, IncomingIdRequest.ProjectId);
                var bData = data.Select(x => x.WorkItem).ToList();
                bData.ForEach(x =>
                {
                    result.Add(new OutgoingWorkItem
                    {
                        Id = x.Id,
                        //     WorkItemIcon = x.WorkItemType.Icon,
                        Title = x.Title,
                        TypeId = x.WorkItemTypeId.Value,
                        Description = x.Description,
                        AssignedTo = x.AssignedAccountNavigation == null ? "" : x.AssignedAccountNavigation.Email,
                        subtasks = x.AssociatedWrorkItemChildrenWorkItem != null ? context.GetWorkItemChildrenClean(x.Id).Select(y => new OutgoingWorkItem
                        {
                            Id = y.Id,
                            //  WorkItemIcon = y.WorkItemType.Icon,
                            Title = y.Title,
                            TypeId = y.WorkItemTypeId.Value,
                            Description = y.Description,
                            AssignedTo = y.AssignedAccountNavigation == null ? "" : y.AssignedAccountNavigation.Email,

                        }).ToList() : null
                    });
                });
                // result = GetChildren(data,result);
            }

            return result;
        }

    }
}

 