using Microsoft.AspNetCore.Mvc;
using Platform.DataHandlers;
using Platform.Models;
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
        public string GenerateChangelog([FromBody] IncomingGenerateChangelog changelog)
        {
            var result = string.Empty;
            using(var generator = new ChangelogGenerator(Context))
            {
                result = generator.GenerateChangelog(changelog);
            }
            return result;
        }
    }
}