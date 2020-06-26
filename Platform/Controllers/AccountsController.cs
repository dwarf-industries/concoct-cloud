namespace Rokono_Control.Controllers
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
                    ControlName = "Assigned To Me",
                    Description = "Allows team members to quickly view and manage work assigned to them."
                },
                new OutgoingBindingControl{
                    ControlName = "Build History",
                    Description = "Allows team members to quickly view and manage work assigned to them."
                },
                new OutgoingBindingControl{
                    ControlName = "Burndown",
                    Description = "Displays burndown across multiple teams and multiple sprints. Create a release burndown or bug burndown."
                },
                new OutgoingBindingControl{
                    ControlName = "Burnup",
                    Description = "Displays burnup across multiple teams and multiple sprints. Create a release burnup or bug burnup."
                },
                new OutgoingBindingControl{
                    ControlName = "Chart for Test Plans",
                    Description = "Create charts for test case authoring and test case execution status for test plans and test suites"
                },
                new OutgoingBindingControl{
                    ControlName = "Chart For Work Items",
                    Description = "Visualize work items like bugs, user stories, and features using shared work item queries."
                },
                new OutgoingBindingControl{
                    ControlName = "Tile",
                    Description = "Displays the number of recent changes in a code repository."
                },
                new OutgoingBindingControl{
                    ControlName = "Cumulative Flow Diagram (CFD)",
                    Description = "Visualize the flow of work and identify bottlenecks in the software development process."
                },
                new OutgoingBindingControl{
                    ControlName = "Cycle Time",
                    Description = "Visualize and analyze your team's cycle time using a control chart."
                },
                new OutgoingBindingControl{
                    ControlName = "Deployment status",
                    Description = "Shows the deployment and test status of a branch across the stages in your release pipelines."
                },
                new OutgoingBindingControl{
                    ControlName = "Embedded Webpage",
                    Description = "Embed an external webpage on your dashboard within an iframe."
                },
                new OutgoingBindingControl{
                    ControlName = "Lead Time",
                    Description = "Visualize and analyze your team's lead time using a control chart."
                },
                     new OutgoingBindingControl{
                    ControlName = "Markdown",
                    Description = "Enables custom text, links, images, and more using Markdown syntax."
                },
                     new OutgoingBindingControl{
                    ControlName = "New Work Item",
                    Description = "Enables quick creation of new work items directly from the dashboard."
                },
                     new OutgoingBindingControl{
                    ControlName = "Other Links",
                    Description = "Adds a quick link to Feedback Client. Admins can configure iterations and work areas."
                },
                new OutgoingBindingControl{
                    ControlName = "Pull Requests",
                    Description = "Check on the status of your pull requests."
                },
                new OutgoingBindingControl{
                    ControlName = "Query Results",
                    Description = "Displays the result from a query"
                },
                new OutgoingBindingControl{
                    ControlName = "Query Tile",
                    Description = "Displays the total number of results for a query."
                },
                new OutgoingBindingControl{
                    ControlName = "Release Pipeline Overview",
                    Description = "Shows the status of stages in a release pipeline."
                },
                new OutgoingBindingControl{
                    ControlName = "Requirements quality",
                    Description = "Shows the quality of requirement(s) associated to automated tests."
                },
                new OutgoingBindingControl{
                    ControlName = "Sprint Burndown",
                    Description = "Displays a burndown chart for the work of a team in a single iteration."
                },

                new OutgoingBindingControl{
                    ControlName = "Sprint Capacity",
                    Description = "Displays a visual overview of the current sprint capacity and highlights if the team is under or over capacity."
                },
                new OutgoingBindingControl{
                    ControlName = "Team Members",
                    Description = "Displays the number of team members and enables quick add and remove of team members from the dashboard."
                },
                new OutgoingBindingControl{
                    ControlName = "Test Results Trend",
                    Description = "Displays the trend of test results for a build or release pipeline with metrics such as pass rate, test duration and count."
                },
                new OutgoingBindingControl{
                    ControlName = "Test Results Trend (Advanced)",
                    Description = "Displays the trend of test results for build or release pipelines with metrics such as pass rate, test duration and count."
                },
                new OutgoingBindingControl{
                    ControlName = "Velocity",
                    Description = "Displays your team velocity. Shows what your team delivered as compared to plan."
                },
                new OutgoingBindingControl{
                    ControlName = "Welcome",
                    Description = "Provides quick links to different parts of the product and reference documentation."
                },
                new OutgoingBindingControl{
                    ControlName = "Work Links",
                    Description = "Adds quick shortcuts to different parts of the product to manage your work."
                },
                new OutgoingBindingControl{
                    ControlName = "XAML Build History",
                    Description = "Shows the build history of a selected build pipeline."
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