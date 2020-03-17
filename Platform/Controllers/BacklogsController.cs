namespace Rokono_Control.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.Models;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    public class BacklogsController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;

        public BacklogsController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IActionResult Index(int projectId, int boardId, string phase)
        {           
            ViewData["ProjectId"] = projectId;
            ViewData["BoardId"] = boardId;
            if(string.IsNullOrEmpty(phase))
                ViewData["Phase"] = "!";
            else
                ViewData["Phase"] = phase;
            return View();
        }
      
        //  [Authorize(Roles = "User")]
        [HttpPost]
        public List<OutgoingWorkItem> GetWorkItems([FromBody] IncomingIdRequest IncomingIdRequest)
        {

            var result = new List<OutgoingWorkItem>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                var data = context.GetProjectWorkItems(IncomingIdRequest.Id, IncomingIdRequest.WorkItemType);
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
        public List<OutgoingWorkItem> GetEmptyStories([FromBody] IncomingIdRequest IncomingIdRequest)
        {

            var result = new List<OutgoingWorkItem>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                var data = context.GetProjectWorkItems(IncomingIdRequest.Id, IncomingIdRequest.WorkItemType);
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
            using(var context = new DatabaseController(Context,Configuration))
            {
                context.RemoveWorkItems(Items.Items);
            }
            return new OutgoingJsonData{ Data = ""};
        }
        [HttpPost]
        public List<OutgoingWorkItem> GetBacklogWorkItems([FromBody] IncomingIdRequest IncomingIdRequest)
        {

            var result = new List<OutgoingWorkItem>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                var data = context.GetProjectWorkItems(IncomingIdRequest.Id, IncomingIdRequest.WorkItemType);
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



//Select(z=> new OutgoingWorkItem{
//                   Id = z.Id,
//                   Title =z.WorkItem.Title,
//                   Description = z.WorkItem.Description,
//                   AssignedTo = z.WorkItem.AssignedAccountNavigation == null? "" : z.WorkItem.AssignedAccountNavigation.Email,
//               })