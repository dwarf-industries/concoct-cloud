using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Platform.Models;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.Controllers
{
    public class ApiKeySettingsController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;

        public ApiKeySettingsController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IActionResult ProjectApiKeys(int projectId)
        {
            ViewData["ProjectId"] = projectId;
            return View();
        }

        [HttpGet]
        public List<ApiKeys> GetProjectFeatures(int projectId)
        {
            var result = new List<ApiKeys>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                 result = context.GetProjectApiKeys(projectId);
            }
            return result;
        }

        [HttpPost]
        public OutgoingJsonData EnableProjectFeature([FromBody] IncomignFeatureRequest request)
        {
            var currentUser = this.User;
            var id = int.Parse(currentUser.Claims.ElementAt(1).Value);
            using(var context = new DatabaseController(Context,Configuration))
            {
                // if()
                //TODO Implement user right check

                context.EnableProjectFeature(request);
            }
            return new OutgoingJsonData{};
        }
    }
}