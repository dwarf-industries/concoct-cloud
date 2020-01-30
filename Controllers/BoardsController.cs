using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rokono_Control;
using Rokono_Control.DatabaseHandlers;
using RokonoControl.Models;

namespace RokonoControl.Controllers
{
    public class BoardsController : Controller
    {
        
        public IActionResult Index(int projectId)
        {
            var currentUser = this.User;
            var rights = currentUser.Claims.LastOrDefault().Value;
            ViewData["IsAdmin"] = int.Parse(rights) == 1 ? true : false;
            using(var context = new DatabaseController())
            {
                ViewData["Relationships"] = context.GetProjectRelationships();
                ViewData["ProjectId"] = projectId;
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
                ViewData["ProjectName"] = context.GetProjectName(projectId);

             }
            return View();
        }

        [HttpGet]
        public List<BindingCards> GetWorkItems(int projectId, int workItemType)
        {
            var result = new List<BindingCards>();
            using(var context = new DatabaseController())
            {
               result =context.GetProjectCards(projectId, workItemType);
            }
            return result;
        }

        [HttpPost]
        public bool ChangeWorkItemBoard([FromBody] IncomingCardRequest card)
        {
            using(var context = new DatabaseController())
            {
                context.ChangeWorkItemBoard(card);
            }
            return true;
        }

        [HttpGet]
        public bool LogRepository(string repoName)
        {
            Program.InitCron(repoName);
            return true;
        }
    }
}