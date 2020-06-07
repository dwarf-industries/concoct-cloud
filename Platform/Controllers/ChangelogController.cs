using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Platform.DataHandlers;
using Platform.DataHandlers.Interfaces;
using Platform.Models;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.Controllers
{
    public class ChangelogController : Controller
    {
        RokonoControlContext Context {get; set;}
        IConfiguration Config { get; set;}
        AutherizationManager AutherizationManager {get; set;}
        private int UserId {get; set;}
        public ChangelogController(RokonoControlContext context,IConfiguration currentConfig, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Config = currentConfig;
            AutherizationManager = (AutherizationManager) autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);

            
        }
        public IActionResult ViewChangelogs(int projectId)
        {
            var currentUser = this.User;
    
            using(var context = new DatabaseController(Context,Config))
            {            

                ViewData["ProjectId"] = projectId;
                ViewData["Name"] = context.GetUsername(UserId);
                ViewData["Changelogs"] = context.GetProjectChangelogs(projectId);
            }
            return View();
        }

        [HttpPost]
        [EnableCors("ApiPolicy")]
        public List<Changelogs> GetPublicChangelogs([FromBody] IncomingApiAuthenicationRequest request)
        {
            var result = new List<Changelogs>();
            using(var context = new DatabaseController(Context,Config))
            {
                var autherizeReqiest = context.CheckApiCallCredentials(request);
                if(autherizeReqiest != 0)
                    result = context.GetProjectChangelogs(autherizeReqiest);
            }
            return result;
        }

        public IActionResult EditChangelog(int projectId,int changelog)
        {
 
            using(var context = new DatabaseController(Context,Config))
            {            

                ViewData["ProjectId"] = projectId;
                ViewData["Name"] = context.GetUsername(UserId);
                ViewData["Changelog"] = context.GetSpecificChangelog(changelog);
            }
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

            using(var context = new DatabaseController(Context,Config))
            {
                context.AssociatedChangelogItems(changelog);
            }
            return Json(new object());
        }

        [HttpPost]
        public JsonResult EditChangelog([FromBody] ChangelogEditRequest changelog)
        {

            using(var context = new DatabaseController(Context,Config))
            {
                context.EditChangelog(changelog);
            }
            return Json(new object());
        }
        [HttpGet]
        public IActionResult GetChangelogById(int changelogId) 
        {
            return ViewComponent("ViewChangelog", changelogId);
        }
    }
}