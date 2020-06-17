﻿namespace Rokono_Control.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Models;
    using Rokono_Control.Models;
    using RokonoControl.Models;

    public class AccountsController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        AutherizationManager AutherizationManager {get; set;}
        private int UserId {get; set;}

        public AccountsController(RokonoControlContext context, IConfiguration config,  IAutherizationManager autherizationManager,IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager) autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUserProfile(int projectId)
        {
            ViewData["ProjectId"] = projectId;
            using(var context = new UsersContext(Context, Configuration))
            {
                ViewData["UserData"] = context.GetUserAccount(UserId);
                ViewData["UserRights"] = AutherizationManager.ValidateUserRights(projectId,UserId,context);
            }
            return View();
        }
        public IActionResult ManageProjectMemebers(int projectId)
        {
            ViewData["ProjectId"] = projectId;
            return View();
        }
        [HttpGet]
        public  List<OutgoingUserAccounts> GetProjectUsers(int projectId)
        {
            var outgoingUserList = new List<OutgoingUserAccounts>();
            using(var context = new UsersContext(Context,Configuration))
            {
                outgoingUserList = context.GetProjectUsers(projectId);
            }
            return  outgoingUserList;
        }


        [HttpGet]
        public  IActionResult GetWidgets()
        {
            
            return ViewComponent("WidgetBuilder");
        }
        [HttpGet]
        public  List<OutgoingBindingControl> GetWidgetComponents(int projectId)
        {
            return  new List<OutgoingBindingControl>{
                new OutgoingBindingControl{
                    ControlName = "Map"
                },
                new OutgoingBindingControl{
                    ControlName = "Grid"
                },
                new OutgoingBindingControl{
                    ControlName = "Commit Board"
                },
                new OutgoingBindingControl{
                    ControlName = "Map Heat"
                },
                new OutgoingBindingControl{
                    ControlName = "Project Members"
                },
                new OutgoingBindingControl{
                    ControlName = "Big Data Map"
                },
                new OutgoingBindingControl{
                    ControlName = "Bubble Chart"
                },
                new OutgoingBindingControl{
                    ControlName = "Intensity Chart"
                },
                new OutgoingBindingControl{
                    ControlName = "Basic Line Chart"
                },
                new OutgoingBindingControl{
                    ControlName = "Spline"
                },
                new OutgoingBindingControl{
                    ControlName = "100% Stacked Line"
                },
                new OutgoingBindingControl{
                    ControlName = "Area"
                },
                     new OutgoingBindingControl{
                    ControlName = "Spline Area"
                },
                     new OutgoingBindingControl{
                    ControlName = "Bar Column"
                },
                     new OutgoingBindingControl{
                    ControlName = "Rounded Column"
                },
                     new OutgoingBindingControl{
                    ControlName = "Pie with Legend"
                },
                     new OutgoingBindingControl{
                    ControlName = "Pyramid"
                },
                     new OutgoingBindingControl{
                    ControlName = "Category Axis"
                },
                     new OutgoingBindingControl{
                    ControlName = "Logarithmic Scale"
                },
                new OutgoingBindingControl{
                    ControlName = "Network Diagram"
                },
                new OutgoingBindingControl{
                    ControlName = "UML Diagram"
                },
            };
        }

        
        [HttpPost]
        public JsonResult AssociateNewUserAccount([FromBody] IncomingProjectAccount projectAccount)
        {
            using(var context = new UsersContext(Context,Configuration))
            {
                context.AddProjectInvitation(projectAccount);
            }
             return  Json(new IncomingProjectAccount {

            });
        }

        [HttpPost] 
        public JsonResult AssociateMembers([FromBody] IncomingExistingProjectMembers accounts)
        {
            using(var context  = new UsersContext(Context,Configuration))
            {
                context.AssociatedProjectExistingMembers(accounts);
            }
            return  Json(new IncomingExistingProjectMembers {

            });
        }

        [HttpPost] 
        public JsonResult DeleteProjectAccount([FromBody] IncomingProjectAccount accounts)
        {
            using(var context  = new UsersContext(Context,Configuration))
            {
                context.DeleteAccountFromProject(accounts);
            }
            return  Json(new IncomingExistingProjectMembers {

            });
        }

        [HttpPost] 
        public JsonResult ChangeNotificationRight([FromBody] IncomingIdRequest request)
        {
            using(var context  = new UsersContext(Context,Configuration))
            {
                context.UpdateUserNotificationRight(UserId, request.ProjectId, request.Id, request.Phase);   
            }
            return  Json(new IncomingExistingProjectMembers {

            });
        }
    }
}