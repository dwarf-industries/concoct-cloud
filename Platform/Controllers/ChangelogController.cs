using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Platform.DataHandlers;
using Platform.Models;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.Controllers
{
    public class ChangelogController : Controller
    {
        RokonoControlContext Context; 
        public ChangelogController(RokonoControlContext context)
        {
            Context = context;
        }
        public IActionResult ViewChangelogs(int projectId)
        {
            var currentUser = this.User;
            var rights = currentUser.Claims.LastOrDefault().Value;
            ViewData["IsAdmin"] = rights;
            var id = currentUser.Claims.ElementAt(1);
            var currentId = int.Parse(id.Value);
            using(var context = new DatabaseController(Context))
            {            

                ViewData["Projects"] = context.GetUserProjects(int.Parse(id.Value));
                ViewData["ProjectId"] = projectId;
                ViewData["Relationships"] = context.GetProjectRelationships();
                ViewData["Name"] = context.GetUsername(currentId);
                ViewData["DefaultIteration"] = context.GetProjectDefautIteration(projectId);
                ViewData["Changelogs"] = context.GetProjectChangelogs(projectId);
            }
            return View();
        }
        public IActionResult EditChangelog(int projectId,int changelog)
        {
            var currentUser = this.User;
            var rights = currentUser.Claims.LastOrDefault().Value;
            ViewData["IsAdmin"] = rights;
            var id = currentUser.Claims.ElementAt(1);
            var currentId = int.Parse(id.Value);
            using(var context = new DatabaseController(Context))
            {            

                ViewData["Projects"] = context.GetUserProjects(int.Parse(id.Value));
                ViewData["ProjectId"] = projectId;
                ViewData["Relationships"] = context.GetProjectRelationships();
                ViewData["Name"] = context.GetUsername(currentId);
                ViewData["DefaultIteration"] = context.GetProjectDefautIteration(projectId);
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

            using(var context = new DatabaseController(Context))
            {
                context.AssociatedChangelogItems(changelog);
            }
            return Json(new object());
        }

        [HttpPost]
        public JsonResult EditChangelog([FromBody] ChangelogEditRequest changelog)
        {

            using(var context = new DatabaseController(Context))
            {
                context.EditChangelog(changelog);
            }
            return Json(new object());
        }
        
    }
}