namespace Rokono_Control.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    public class BacklogsController : Controller
    {
        
        public IActionResult Index(int projectId, int boardId)
        {
            var currentUser = this.User;
            var rights = currentUser.Claims.LastOrDefault().Value;
            ViewData["IsAdmin"] = int.Parse(rights) == 1 ? true : false;
            var id = currentUser.Claims.ElementAt(1);         
            using(var context = new DatabaseController())
            {
                var currentId = int.Parse(id.Value);
                ViewData["ProjectId"] = projectId;
                ViewData["Relationships"] = context.GetProjectRelationships();
                ViewData["Name"] = context.GetUsername(currentId);
                ViewData["BoardId"] = boardId;
            }
            return View();
        }

      //  [Authorize(Roles = "User")]
         [HttpPost]
        public List<OutgoingWorkItem> GetWorkItems([FromBody] IncomingIdRequest IncomingIdRequest)
        {
            
            var result = new List<OutgoingWorkItem>(); 
            using(var context = new DatabaseController())
            {
                var  data = context.GetProjectWorkItems(IncomingIdRequest.Id, IncomingIdRequest.WorkItemType);
                var bData =  data.Select(x=>x.WorkItem).ToList();
                bData.ForEach(x=>{
                    result.Add(new OutgoingWorkItem{
                        Id = x.Id,
                        WorkItemIcon = x.WorkItemType.Icon,
                        Title =x.Title,
                        Description = x.Description,
                        AssignedTo = x.AssignedAccountNavigation == null ? "": x.AssignedAccountNavigation.Email,
                    //    subtasks = new List<OutgoingWorkItem>()
                    });     
                });
               // result = GetChildren(data,result);
            }

            return result;
        }
        [HttpPost]
        public List<OutgoingWorkItem> GetBacklogWorkItems([FromBody] IncomingIdRequest IncomingIdRequest)
        {
            
            var result = new List<OutgoingWorkItem>(); 
            using(var context = new DatabaseController())
            {
                var  data = context.GetProjectWorkItems(IncomingIdRequest.Id, IncomingIdRequest.WorkItemType);
                var bData =  data.Select(x=>x.WorkItem).ToList();
                bData.ForEach(x=>{
                    result.Add(new OutgoingWorkItem{
                        Id = x.Id,
                   //     WorkItemIcon = x.WorkItemType.Icon,
                        Title =x.Title,
                        TypeId = x.WorkItemTypeId.Value,
                        Description = x.Description,
                        AssignedTo = x.AssignedAccountNavigation == null ? "": x.AssignedAccountNavigation.Email,
                        subtasks = x.AssociatedWrorkItemChildrenWorkItem != null ? context.GetWorkItemChildrenClean(x.Id).Select(y=>new OutgoingWorkItem {
                                Id = y.Id,
                              //  WorkItemIcon = y.WorkItemType.Icon,
                                Title =y.Title,
                                TypeId = y.WorkItemTypeId.Value,
                                Description = y.Description,
                                AssignedTo = y.AssignedAccountNavigation == null ? "": y.AssignedAccountNavigation.Email,

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