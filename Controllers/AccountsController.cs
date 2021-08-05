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
    using Rokono_Control.DatabaseHandlers;
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

        public IActionResult ProjectSignup(int projectId)
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
        public  IActionResult GetWidgets(int projectId, int Dashboard)
        {
            
            return ViewComponent("WidgetBuilder", new IncomingIdRequest{
                ProjectId = projectId,
                Id = Dashboard
            });
        }
        [HttpGet]
        public  List<PremadeWidgets> GetWidgetComponents(int projectId)
        {
            var result = new List<PremadeWidgets>();
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetPremadeWidgets();
            }
            return  result;
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
        [HttpPost]
        public OutgoingJsonData MakeSignupPublic([FromBody] IncomingPublicBoardRequest request)
        {
            var result = string.Empty;
            using (var context = new UsersContext(Context, Configuration))
            {
                var domain = Request.Host.Host;

                result = context.SignUpPublic(request.ProjectId,request.IsChecked, domain);
            }
            return new OutgoingJsonData { Data = result };

        }
        [HttpPost]
        public dynamic CheckAccountExists([FromBody] IncomingIdRequest request)
        {
            dynamic cResult = new System.Dynamic.ExpandoObject();
            using (var context = new UsersContext(Context, Configuration))
            {
                var exist = context.CheckAccountExist(request.Phase);
                var checkAccoutnExistInProject = context.CheckProjectAssociation(request.Phase, request.ProjectId);
                cResult.existInAccounts = exist;
                cResult.existInProject = checkAccoutnExistInProject;
            }
            return cResult;

        }

        [HttpPost]
        public dynamic RegisterPublic([FromBody] IncomingProjectAccount request)
        {
            dynamic cResult = new System.Dynamic.ExpandoObject();

            using (var context = new UsersContext(Context, Configuration))
            {
                var result = context.CheckProjectSignUpPolicy(request.ProjectId,request.email);
                if (result.Item1 && !result.Item2 )
                {
                    request.accountRights = new OutgoingUserAccounts
                    {
                        ChatChannels = 1,
                        EditUserRights = 0,
                        IterationOptions = 0,
                        ScheduleManagement = 0,
                        ViewWorkItems = 1,
                        WorkItemOption = 1,
                        Documentation = 0
                    };
                    context.AddProjectInvitation(request);
                    cResult.Success = true;
                    return cResult;
                }
                else
                {
                    cResult.Error = result.Item1;
                    cResult.EmailError = result.Item2;
                    return cResult;
                }
            }
           
        }

        [HttpPost]
        public dynamic RequestAccess([FromBody] IncomingProjectAccount request)
        {
            dynamic cResult = new System.Dynamic.ExpandoObject();

            using (var context = new UsersContext(Context, Configuration))
            {
                var result = context.CheckProjectSignUpPolicy(request.ProjectId,string.Empty).Item1;
                if (result == true)
                {
                    var getUserAccountByEmail = context.GetUserAccountByName(request.email);
                    context.AssociatedProjectExistingMembers(new IncomingExistingProjectMembers { 
                    ProjectId = request.ProjectId,
                    Accounts = new List<OutgoingUserAccounts>
                    {
                        new OutgoingUserAccounts
                        {
                            AccountId = getUserAccountByEmail.Id,
                            ChatChannels = 1,
                            EditUserRights = 0,
                            IterationOptions = 0,
                            ScheduleManagement = 0,
                            ViewWorkItems = 1,
                            WorkItemOption = 1,
                            Documentation = 0
                        }
                    }
                    });
                    cResult.Success = true;
                    return cResult;
                }
                else
                {
                    cResult.Error = true;
                    return cResult;
                }
            }

        }
    }
}