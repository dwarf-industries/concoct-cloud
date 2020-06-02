using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Platform.Models;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.Controllers
{
    public class DocumentationController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;

        public DocumentationController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }


        public IActionResult Index(int Id)
        {
            ViewData["ProjectId"] = Id;
            return View();
        }

        [HttpPost]
        public List<OutgoingChatItem> GetNavigation([FromBody] IncomingIdRequest request)
        {
            var result = new List<OutgoingChatItem>();
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetDocumentationNavigation(request.ProjectId);
            }
            return result;
        }
    }
}