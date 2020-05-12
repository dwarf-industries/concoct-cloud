using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Platform.DataHandlers;
using Platform.Models;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.Controllers
{
    public class ChangelogController : Controller
    {
        RokonoControlContext Context; 
        IConfiguration Configuration;
        public ChangelogController(RokonoControlContext context)
        {
            Context = context;
        }
        public IActionResult ViewChangelogs(int projectId)
        {
            var currentUser = this.User;

            var id = currentUser.Claims.ElementAt(1);
            var currentId = int.Parse(id.Value);
            using(var context = new DatabaseController(Context,Configuration))
            {            

                ViewData["ProjectId"] = projectId;
                ViewData["Name"] = context.GetUsername(currentId);
                ViewData["Changelogs"] = context.GetProjectChangelogs(projectId);
            }
            return View();
        }

        [HttpPost]
        public List<Changelogs> GetPublicChangelogs([FromBody] IncomingApiAuthenicationRequest request)
        {
            var result = new List<Changelogs>();
            using(var context = new DatabaseController(Context,Configuration))
            {
                var autherizeReqiest = context.CheckApiCallCredentials(request);
                if(context.CheckProjectAuthorizedFeature(autherizeReqiest, request.FeatureRequest))
                    result = context.GetProjectChangelogs(autherizeReqiest);
            }
            return result;
        }

        public IActionResult EditChangelog(int projectId,int changelog)
        {
            var currentUser = this.User;
            var id = currentUser.Claims.ElementAt(1);
            var currentId = int.Parse(id.Value);
            using(var context = new DatabaseController(Context,Configuration))
            {            

                ViewData["ProjectId"] = projectId;
                ViewData["Name"] = context.GetUsername(currentId);
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

            using(var context = new DatabaseController(Context,Configuration))
            {
                context.AssociatedChangelogItems(changelog);
            }
            return Json(new object());
        }

        [HttpPost]
        public JsonResult EditChangelog([FromBody] ChangelogEditRequest changelog)
        {

            using(var context = new DatabaseController(Context,Configuration))
            {
                context.EditChangelog(changelog);
            }
            return Json(new object());
        }
        
    }
}