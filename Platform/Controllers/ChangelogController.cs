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
    }
}