namespace Platform.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Models;
    using Rokono_Control.Models;

    public class ChangelogController : Controller
    {
        RokonocontrolContext Context {get; set;}
        IConfiguration Config { get; set;}
        AutherizationManager AutherizationManager {get; set;}
        private int UserId {get; set;}
        public ChangelogController(RokonocontrolContext context,IConfiguration currentConfig, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Config = currentConfig;
            AutherizationManager = (AutherizationManager) autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);

            
        }
        public IActionResult ViewChangelogs(int projectId)
        {
            var currentUser = this.User;
    
            using(var context = new ChangelogContext(Context,Config))
            {            
                ViewData["ProjectId"] = projectId;
                ViewData["Changelogs"] = context.GetProjectChangelogs(projectId);
            }
            using(var context = new UsersContext(Context,Config))
                ViewData["Name"] = context.GetUsername(UserId);
            return View();
        }

        [HttpPost]
        [EnableCors("ApiPolicy")]
        public List<Changelogs> GetPublicChangelogs([FromBody] IncomingApiAuthenicationRequest request)
        {
            var result = new List<Changelogs>();
            var autherizeReqiest  = default(int);
            using(var context = new ApiKeysContext(Context,Config))
            {
                autherizeReqiest = context.CheckApiCallCredentials(request);
            }
            if(autherizeReqiest == 0)
                return result;

            using(var context = new ChangelogContext(Context,Config))
                return context.GetProjectChangelogs(autherizeReqiest);
        }

        public IActionResult EditChangelog(int projectId,int changelog)
        {
 
            using(var context = new ChangelogContext(Context,Config))
            {            
                ViewData["ProjectId"] = projectId;
                ViewData["Changelog"] = context.GetSpecificChangelog(changelog);
            }
            using(var context = new UsersContext(Context,Config))
                ViewData["Name"] = context.GetUsername(UserId);
            return View();
        }
        
        [HttpPost]
        public IncomingGenerateChangelog GenerateChangelog([FromBody] IncomingGenerateChangelog changelog)
        {
            var result = new IncomingGenerateChangelog();
            using(var generator = new ChangelogGenerator(Context))
            {
                result.Chagelog = generator.GenerateChangelog(changelog);
            }
            return result;
        }

        [HttpPost]
        public JsonResult ConfirmChangelog([FromBody] IncomingGenerateChangelog changelog)
        {

            using(var context = new ChangelogContext(Context,Config))
            {
                context.AssociatedChangelogItems(changelog);
            }
            return Json(new object());
        }

        [HttpPost]
        public JsonResult EditChangelog([FromBody] ChangelogEditRequest changelog)
        {

            using(var context = new ChangelogContext(Context,Config))
            {
                context.EditChangelog(changelog);
            }
            return Json(new object());
        }
        [HttpGet]
        public List<OutgoingWorkItemSimple> UnassociatedChangelogItems(int projectId)
        {
            var result = new List<OutgoingWorkItemSimple>();
            using(var context = new ChangelogContext(Context, Config))
            {
                result = context.GetEmptyChangelogWorktItems(projectId).Select(y=> new OutgoingWorkItemSimple{
                    Id = y.Id,
                    Name = string.IsNullOrEmpty(y.Title) ? "" : y.Title ,
                    WorkItemTypeName = y.WorkItemType == null ? "" : y.WorkItemType.TypeName
                }).ToList();
            }
            return result;
        }
        [HttpGet]
        public IActionResult GetChangelogById(int changelogId) 
        {
            return ViewComponent("ViewChangelog", changelogId);
        }
    }
}