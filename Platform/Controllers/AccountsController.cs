using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;
using RokonoControl.Models;

namespace Rokono_Control.Controllers
{
    public class AccountsController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;

        public AccountsController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageProjectMemebers(int projectId)
        {
            var currentUser = this.User;
            var currentUserId = currentUser.Claims.ElementAt(1);
            var rights = currentUser.Claims.LastOrDefault().Value;
            ViewData["ProjectId"] = projectId;
            ViewData["IsAdmin"] = int.Parse(rights) == 1 ? true : false;

            using (var context = new DatabaseController(Context, Configuration))
            {
                ViewData["Relationships"] = context.GetProjectRelationships();
                ViewData["Branches"] = context.GetBranchesForProject(projectId);
                ViewData["Projects"] = context.GetUserProjects(int.Parse(currentUserId.Value));
                ViewData["DefaultIteration"] = context.GetProjectDefautIteration(projectId);
            }
            return View();
        }
        [HttpGet]
        public  List<OutgoingUserAccounts> GetProjectUsers(int projectId)
        {
            var outgoingUserList = new List<OutgoingUserAccounts>();
            using(var context = new DatabaseController(Context,Configuration))
            {
                outgoingUserList = context.GetProjectUsers(projectId);
            }
            return  outgoingUserList;
        }
        [HttpPost]
        public JsonResult AssociateNewUserAccount([FromBody] IncomingProjectAccount projectAccount)
        {
            using(var context = new DatabaseController(Context,Configuration))
            {
                context.AddProjectInvitation(projectAccount);
            }
             return  Json(new IncomingProjectAccount {

            });
        }

        [HttpPost] 
        public JsonResult AssociateMembers([FromBody] IncomingExistingProjectMembers accounts)
        {
            using(var context  = new DatabaseController(Context,Configuration))
            {
                context.AssociatedProjectExistingMembers(accounts);
            }
            return  Json(new IncomingExistingProjectMembers {

            });
        }


        [HttpPost] 
        public JsonResult DeleteProjectAccount([FromBody] IncomingProjectAccount accounts)
        {
            using(var context  = new DatabaseController(Context,Configuration))
            {
                context.DeleteAccountFromProject(accounts);
            }
            return  Json(new IncomingExistingProjectMembers {

            });
        }
    }
}