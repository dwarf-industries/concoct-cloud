using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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
                result = GetNavigation(request, context);
            }
            return result;
        }

        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public List<OutgoingChatItem> AddNewCategory([FromBody] IncomingIdRequest request)
        {
          
            var result = new  List<OutgoingChatItem>();
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.AddNewDocumentationCategory(request);
                result = GetNavigation(request, context);
            }
            return result;
        }


        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public List<OutgoingChatItem> AddNewCategoryField([FromBody] IncomingIdRequest request)
        {
          
            var result = new  List<OutgoingChatItem>();
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.AddNewDocumentationCategoryField(request);
                result = GetNavigation(request, context);
            }
            return result;
        }

        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public List<OutgoingChatItem> AddNewPage([FromBody] AssociatedDocumentationCategoryPage request)
        {
          
            var result = new  List<OutgoingChatItem>();
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.AddNewDocumentationpage(request);
            }
            return result;
        }
        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public List<OutgoingChatItem> EditPage([FromBody] AssociatedDocumentationCategoryPage request)
        {
          
            var result = new  List<OutgoingChatItem>();
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.UpdateDocumentationPage(request);
            }
            return result;
        }

        private static List<OutgoingChatItem> GetNavigation(IncomingIdRequest request, DatabaseController context)
        {
            return context.GetDocumentationNavigation(request.ProjectId);
        }
        
        [HttpGet]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public IActionResult DocumentationPage(int id, int projectId) 
        {
            return ViewComponent("DocumentationPage", new IncomingIdRequest{
                Id = id,
                ProjectId = projectId
            });
         }
        [HttpGet]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public IActionResult GetPageModal(int id, int category) 
        {
            return ViewComponent("DocumentationPageHandler", new IncomingIdRequest{
                 Id = id,
                 UserId = category
            });
         }

    }
}